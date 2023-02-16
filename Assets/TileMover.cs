using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePlayer._gamePlayer.currentGameState == GamePlayer.GameState.gameOver)
            return;

        transform.position = transform.position + Vector3.back * (GamePlayer._currentPlaneSpeed * Time.deltaTime);
        if (transform.position.z < -544f)
        {
            Vector3 restartPosition = new Vector3(transform.position.x, transform.position.y, 544f);
            transform.position = restartPosition;

            //we will record how many tiles we pass, then when it's a number over 16, we will know we've passed over once, and we can add defenses
           // if (reportToGamePlayer)
            //    GamePlayer._tilesPassed++;

        }
    }
}
