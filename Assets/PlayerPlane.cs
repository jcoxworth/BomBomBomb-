using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlane : MonoBehaviour
{
    public static PlayerPlane _playerPlane;
    public GameObject _playerPlaneMesh;
    private Vector3 planePosition;
    public int maxHealth = 1000;
    public static int _maxHealth = 1000;
    public int currentHealth;
    public static int _currentHealth;

    public int maxBombs = 10;
    public static int _maxBombs = 10;

    public int currentBombs = 10;
    public static int _currentBombs = 10;

    public int currentBigBombs = 0;
    public int currentRocketBombs = 0;

    public float reloadTime = 3f;
    public float currentReloadTime = 0f;
    public bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerPlane();
    }
    public void InitializePlayerPlane()
    {
        _playerPlane = this;
        currentHealth = maxHealth;
        currentBombs = maxBombs;
        reloading = false;
    }
    // Update is called once per frame
    void Update()
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
        _currentBombs = currentBombs;

        planePosition = new Vector3(GamePlayer._currentFlightPath, GamePlayer._currentAltitude, 0f);
        transform.position = planePosition;

        if (reloading)
        {
            currentReloadTime += Time.deltaTime;
            if (currentReloadTime > reloadTime)
            {
                currentBombs = maxBombs;
                reloading = false;
            }
        }
    }
    public void Damage(int amount)
    {
        if (currentHealth < 1)
        {
            GamePlayer._gamePlayer.ShotDown();
        }
        else
        {
            GameUI._gameUI.FlashDamage();
            currentHealth -= amount;
        }
    }

    public void DropBomb()
    {
        //Debug.Log("dropping");
        currentBombs -= 1;

        if (currentBombs < 1)
        {
            if (!reloading)
            {
                currentReloadTime = 0f;
                reloading = true;
            }
        }
    }
    public void Change_BigBomb(int amount)
    {
        currentBigBombs+= amount;
    }
    public void Change_RocketBomb(int amount)
    {
        currentRocketBombs += amount;
    }
}
