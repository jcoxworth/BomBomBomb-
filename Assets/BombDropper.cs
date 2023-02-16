using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropper : MonoBehaviour
{
    public bool isHoldingBomb = false;
    public GameObject myBomb;

    private Vector3 touchedPos;
    private Camera cam;

    public enum BombType { regular, rocket, electro, butterfly, big}
    public BombType bombtype = BombType.regular;

    // Use this for initialization
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        screenBoarderUpper = Screen.height - 200f;
        screenBoarderRight = Screen.width - 100f;
        screenBoarderLower = 300f;
        screenBoarderLeft = 100f;
    }
    float screenBoarderUpper; //100
    float screenBoarderRight; //200
    float screenBoarderLower; //300
    float screenBoarderLeft; //200

    // Update is called once per frame
    void Update()
    {
        if (GamePlayer._gamePlayer.currentGameState == GamePlayer.GameState.shop)
            return;
      //  Debug.Log(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7));

            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > screenBoarderLeft && Input.mousePosition.x < screenBoarderRight)
                {
                    if (Input.mousePosition.y > screenBoarderLower && Input.mousePosition.y < screenBoarderUpper)
                    {
                        if (PlayerPlane._currentBombs > 0)
                        {
                            touchedPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7));
                            {
                                getBomb();
                                isHoldingBomb = true;
                                moveBomb();
                            }
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isHoldingBomb)
                {
                    dropBomb();
                    isHoldingBomb = false;
                    bombtype = BombType.regular;
                }
            }
    }
    public void SwitchBomb_Big()
    {
        bombtype = BombType.big;
    }
    public void SwitchBomb_Rocket()
    {
        bombtype = BombType.rocket;
    }
    void getBomb()
    {
        if (!isHoldingBomb)
        {
            switch(bombtype)
            {
                case BombType.regular:
                    myBomb = GameResources.GetBomb();
                    break;
                case BombType.rocket:
                    myBomb = GameResources.GetRocketBomb();
                    break;
                case BombType.big:
                    myBomb = GameResources.GetBigBomb();
                    break;
            }
            myBomb.transform.rotation = Quaternion.LookRotation(Vector3.down);
            myBomb.SetActive(true);
        }
        else
            return;
    }
    void moveBomb()
    {
        myBomb.GetComponent<Rigidbody>().Sleep();
        touchedPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7));
        myBomb.transform.rotation = Quaternion.LookRotation(Vector3.down);
        myBomb.transform.position = touchedPos;
    }


    void dropBomb()
    {
        switch (bombtype)
        {
            case BombType.regular:
                PlayerPlane._playerPlane.DropBomb();
                break;
            case BombType.rocket:
                PlayerPlane._playerPlane.currentRocketBombs--;
                break;
            case BombType.big:
                PlayerPlane._playerPlane.currentBigBombs --;
                break;
        }
        myBomb.GetComponent<Bomb>().DropBomb();
        
    }
}
