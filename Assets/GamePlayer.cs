using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePlayer : MonoBehaviour
{
    public static   GamePlayer _gamePlayer;
    //--------------------------------------
    public          GameObject playerPlane;
    public static   GameObject _playerPlane;
    public PlaneRotation playerPlaneRotation;
    private GameTiles gameTiles;
    //--------------------------------------
    public enum GameState { bombing, shop, shotDown, gameOver}
    public GameState currentGameState = GameState.bombing;
    //--------------------------------------
    public float basePlaneSpeed = 10f;
    public float inertiaSpeed = 0f;
    public float currentPlaneSpeed = 10f;
    public static float _currentPlaneSpeed;
    //--------------------------------------
    private float maxAltitude = 300f;
    private float minAltitude = 50f;
    [Range(10f, 300f)] public float targetAltitude = 0;
    public static float _targetAltitude = 0;
    public float currentAltitude = 200f;
    public static float _currentAltitude;
    public float altitudeChangeSpeed = 0.25f;
    //--------------------------------------
    [Range(-64f, 64f)] public float targetFlightPath = 0;
    public static float _targetFlightPath;
    public float currentFlightPath;
    public static float _currentFlightPath;
    public float flightPathChangeSpeed = 0.25f;
    //--------------------------------------
    
    public static int _tilesPassed = 0;
    private bool madePlaneCrash = false;
    public int targetsDestroyed = 0;
    public int targetsNeededToDestroy = 0;
    public bool levelClear = false;
    /*
    privat 
    public float speedChance;
    private float maxSpeed = 50f;
    private float minSpeed = 5f;
    [Range(-5f, 50f)] public float targetSpeed = 0;
    */
    // Start is called before the first frame update
    void Start()
    {
        InitializeGamePlayer();
    }
    void InitializeGamePlayer()
    {
        _gamePlayer = this;
        _tilesPassed = 0;
        if (!playerPlane)
            Debug.Log("PlayerPlane not set for GamePlayer.cs");
        _playerPlane = playerPlane;

        if (!playerPlaneRotation)
            playerPlane.transform.GetComponent<PlaneRotation>();
        targetAltitude = 300f;
        targetFlightPath = 0f;
        madePlaneCrash = false;
        gameTiles = GetComponent<GameTiles>();

        StartCoroutine(CheckTargetStatus());
        levelClear = false;
    }
    public void StartNewLevel()
    {
        gameTiles.NextLevel();
        targetAltitude = 300f;
        _targetAltitude = targetAltitude;
        targetFlightPath = 0f;
        _targetFlightPath = targetFlightPath;
        madePlaneCrash = false;
        currentGameState = GameState.bombing;
        targetsDestroyed = 0;
        targetsNeededToDestroy = gameTiles.LevelObjectSets[gameTiles.currentLevel].TargetsToDestroy;

        StartCoroutine(CheckTargetStatus());
        levelClear = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        switch (currentGameState)
        {
            case GameState.bombing:
                NormalFlight();
                GameCamera._gameCamera.SetCamBombing();
                break;
            case GameState.shop:
                NormalFlight();
                GameCamera._gameCamera.SetCamShop();
                break;
            case GameState.shotDown:
                ShotDownFlight();
                if (currentAltitude < 5f)
                {
                    if (!madePlaneCrash)
                    {
                        GameObject crashEffect = GameResources.GetPlayerPlaneCrash();
                        crashEffect.transform.position = PlayerPlane._playerPlane.transform.position;
                        crashEffect.SetActive(true);
                    }
                    currentGameState = GameState.gameOver;
                }
                else if (currentAltitude < 80f)
                {
                    GameCamera._gameCamera.SetCamGameOver();
                }
                else
                {
                    GameCamera._gameCamera.SetCamBombing();
                }
                break;
            case GameState.gameOver:
                {
                    GameCamera._gameCamera.SetCamGameOver();
                    PlayerPlane._playerPlane._playerPlaneMesh.transform.gameObject.SetActive(false);
                }
                break;

        }
    }
    private IEnumerator CheckTargetStatus()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1f);
            int destroyedTargets = 0;

            foreach (GameObject b in GameTiles.targetList)
            {

                if (b.transform.GetComponent<Building>().Destroyed)
                    destroyedTargets++;
            }
            targetsDestroyed = destroyedTargets;
            targetsNeededToDestroy = gameTiles.LevelObjectSets[gameTiles.currentLevel].TargetsToDestroy;

            levelClear = destroyedTargets >= gameTiles.LevelObjectSets[gameTiles.currentLevel].TargetsToDestroy;
        }
    }
    public void NormalFlight()
    {
        currentAltitude = Mathf.MoveTowards(currentAltitude, targetAltitude, 10*Time.deltaTime);
        currentFlightPath = Mathf.MoveTowards(currentFlightPath, targetFlightPath, 10* Time.deltaTime);

       //currentAltitude = Mathf.SmoothStep(currentAltitude, targetAltitude, altitudeChangeSpeed * Time.deltaTime);
     //   currentFlightPath = Mathf.SmoothStep(currentFlightPath, targetFlightPath, flightPathChangeSpeed * Time.deltaTime);
        // currentPlaneSpeed = Mathf.SmoothDamp(currentPlaneSpeed, targetSpeed, ref r3, 1f);

        //we won't make plane speed adjustable for now
        


        inertiaSpeed = playerPlaneRotation.yDeltaSmooth * Time.deltaTime * (basePlaneSpeed * 0.5f);
        inertiaSpeed = Mathf.Clamp(inertiaSpeed, basePlaneSpeed * -0.8f, basePlaneSpeed* 0.8f);

        inertiaSpeed = Mathf.MoveTowards(inertiaSpeed, 0f, Time.deltaTime);

        currentPlaneSpeed = basePlaneSpeed + inertiaSpeed;




        _currentAltitude = currentAltitude;
        _currentPlaneSpeed = currentPlaneSpeed;
        _currentFlightPath = currentFlightPath;

        _targetAltitude = Mathf.Round(targetAltitude);
        _targetFlightPath = Mathf.Round(targetFlightPath);

        //when we escape we go to another flight path, and we can enter the shop if we're in the right position
        if (_currentAltitude > 200 && _currentFlightPath > 200 && levelClear) //adding the level clear boolean will prevent mistriggering when the player first touches the menu
        {
            currentGameState = GameState.shop;
        }
    }


    public void ShotDownFlight()
    {
        //this should make the plane pitch down
       // PlayerPlane._playerPlane._playerPlaneMesh.transform.localRotation = Quaternion.Euler(30f, 0, 0f );

        currentAltitude = Mathf.MoveTowards(currentAltitude, 0, 1f);
        currentFlightPath = Mathf.MoveTowards(currentFlightPath, targetFlightPath, flightPathChangeSpeed);
        // currentPlaneSpeed = Mathf.SmoothDamp(currentPlaneSpeed, targetSpeed, ref r3, 1f);

        //we won't make plane speed adjustable for now
        currentPlaneSpeed = 15f;

        _currentAltitude = currentAltitude;
        _currentPlaneSpeed = currentPlaneSpeed;
        _currentFlightPath = currentFlightPath;
    }
    public void ChangeFlightPath(float amount)
    {
        targetFlightPath += amount;
        targetFlightPath = Mathf.Clamp(targetFlightPath, -64f, 64f);
    }
    public void FinishFlightPath()
    {
        targetFlightPath = 256f;
        targetAltitude = 280f;
        StopCoroutine(CheckTargetStatus());
    }
    
    public void ChangeAltitude(float amount)
    {

        targetAltitude += amount;
        targetAltitude = Mathf.Clamp(targetAltitude, 10f, 300f);
    }
    public void ShotDown()
    {
        currentGameState = GameState.shotDown;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);

    }
    /* public void ChangeSpeed(float amount)
     {
         targetSpeed += amount;
         targetSpeed = Mathf.Clamp(targetSpeed, 5f, 50f);
     }*/
}
