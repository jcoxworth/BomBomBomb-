using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI _gameUI;
    public GamePlayer gamePlayer;
    public GameObject GamePlayUI;
    public GameObject GameOverUI;
    public GameObject GameShopUI;
 
    public TMP_Text CurrentMoneyTXT;
    public TMP_Text FinalScoreTXT;

    public TMP_Text TargetsTXT;
    public GameObject levelFinishedButton;

    public TMP_Text NormalBombCountTXT;
    public Slider NormalBombSlider;

    public TMP_Text bigBombsTXT;
    public TMP_Text rocketBombsTXT;
   // public TMP_Text AltitudeTargetTXT;
    public TMP_Text CurrentAltitudeTXT;

    public Image planeStatus;
    public Gradient planeHealthColors;

    public Image damageImage;
    public static Color damageColor;


    public TMP_Text CurrentShopMoneyTXT;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateUI());
        InitializeUI();
    }
    public void InitializeUI()
    {
        _gameUI = this;
        gamePlayer = GetComponent<GamePlayer>();
        NormalBombSlider.wholeNumbers = true;
        NormalBombSlider.maxValue = PlayerPlane._maxBombs;
        damageColor = damageImage.color;
    }
    private void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, 5f * Time.deltaTime);

    }
    private IEnumerator UpdateUI()
    {
        while(enabled)
        {
            yield return new WaitForSeconds(0.1f);
            UpdateBombsUI();
            UpdateAltitudeUI();
            UpdatePlaneStatus();
            UpdateMoney();
            UpdateGameStatus();
            UpdateLevelStatus();
        }
    }
    void UpdateGameStatus()
    {
        //if not gameover, show the game stuffs
        bool gameUIShow = GamePlayer._gamePlayer.currentGameState == GamePlayer.GameState.bombing;
        bool gameOverUIShow = GamePlayer._gamePlayer.currentGameState == GamePlayer.GameState.gameOver;

        GamePlayUI.SetActive(gameUIShow);
        GameOverUI.SetActive(gameOverUIShow);
    }
    void UpdateBombsUI()
    {
        NormalBombCountTXT.text = PlayerPlane._currentBombs.ToString();
        NormalBombSlider.value = PlayerPlane._currentBombs;

        bigBombsTXT.text = PlayerPlane._playerPlane.currentBigBombs.ToString();
        rocketBombsTXT.text = PlayerPlane._playerPlane.currentRocketBombs.ToString();
    }
    void UpdateAltitudeUI()
    {
       // AltitudeTargetTXT.text = Mathf.RoundToInt(GamePlayer._gamePlayer.targetAltitude).ToString() + " ft.";
        CurrentAltitudeTXT.text = Mathf.RoundToInt(GamePlayer._currentAltitude).ToString() + " ft.";
    }
    void UpdatePlaneStatus()
    {
        float ch = PlayerPlane._currentHealth;
        float mh = PlayerPlane._maxHealth;
        float newHealth = ch / mh;
       // Debug.Log("new health" + newHealth);
        planeStatus.color = planeHealthColors.Evaluate(newHealth);
    }
    void UpdateMoney()
    {
        CurrentMoneyTXT.text = GameScore._currentMoney.ToString();

        CurrentShopMoneyTXT.text = GameScore._currentMoney.ToString();



        if (GamePlayer._gamePlayer.currentGameState == GamePlayer.GameState.gameOver)
                FinalScoreTXT.text = GameScore._currentMoney.ToString();


    }
    void UpdateLevelStatus()
    {
        TargetsTXT.text = "Targets: " + gamePlayer.targetsDestroyed.ToString() + " / " + gamePlayer.targetsNeededToDestroy.ToString();
        levelFinishedButton.SetActive(gamePlayer.levelClear);
        GameShopUI.SetActive(gamePlayer.currentGameState == GamePlayer.GameState.shop);
    }
    public void FlashDamage()
    {
        damageImage.color = Color.red;
    }
    
}
