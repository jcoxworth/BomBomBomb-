using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [HideInInspector] public static GameScore _gameScore;

    [HideInInspector] public int currentScore;
    [HideInInspector] public static int _currentScore;
    [HideInInspector] public int targetScore;

    [HideInInspector] public int currentMoney;
    [HideInInspector] public static int _currentMoney;
    [HideInInspector] public int targetMoney;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameScore = this;

        currentScore = 0;
        targetScore = 0;
        currentMoney = 0;
        targetMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //score
        currentScore = (int)Mathf.Lerp(currentScore, targetScore, Time.deltaTime * 10f);
        //money
        currentMoney = (int)Mathf.Lerp(currentMoney, targetMoney, Time.deltaTime * 10f);

        _currentScore = currentScore;
        _currentMoney = currentMoney;

    }

    public void ChangeScore(int amount)
    {
        targetScore += amount;
        targetMoney += amount;
    }
    public void ChangeMoney(int amount)
    {
        targetMoney += amount;
    }
}
