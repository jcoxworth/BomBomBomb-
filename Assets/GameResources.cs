using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private int normalBombAmount = 25;
    public static List<GameObject> normalBombs = new List<GameObject>();
    public static List<GameObject> normalBombExplosions = new List<GameObject>();
    public static List<GameObject> rocketBombs = new List<GameObject>();
    public static List<GameObject> electroBombs = new List<GameObject>();
    public static List<GameObject> butterflyBombs = new List<GameObject>();
    public static List<GameObject> bigBombs = new List<GameObject>();
    public static List<GameObject> bigBombExplosions = new List<GameObject>();

    public static List<GameObject> AA_Shells = new List<GameObject>();
    public static List<GameObject> AA_Bullets = new List<GameObject>();
    public static List<GameObject> Craters_8x8 = new List<GameObject>();
    public static List<GameObject> Craters_16x16 = new List<GameObject>();
    public static GameObject playerPlaneCrash;
    // Start is called before the first frame update
    void Awake()
    {
        CreatePool((GameObject)Resources.Load("Bomb_Normal"), normalBombAmount, normalBombs);
        CreatePool((GameObject)Resources.Load("Explosion_Normal"), normalBombAmount, normalBombExplosions);

        CreatePool((GameObject)Resources.Load("Bomb_Rocket"), 10, rocketBombs);
       // CreatePool((GameObject)Resources.Load("Bomb_Electro"), 10, electroBombs);
       // CreatePool((GameObject)Resources.Load("Bomb_Butterfly"), 10, butterflyBombs);
        CreatePool((GameObject)Resources.Load("Bomb_Big"), 10, bigBombs);
        CreatePool((GameObject)Resources.Load("Explosion_Big"), 10, bigBombExplosions);



        CreatePool((GameObject)Resources.Load("Explosion_AA"), normalBombAmount, AA_Shells);
        CreatePool((GameObject)Resources.Load("Bullet_AA"), 100, AA_Bullets);
        CreatePool((GameObject)Resources.Load("Crater_8x8"), 75, Craters_8x8);
        craterCounter8 = 0;
        CreatePool((GameObject)Resources.Load("Crater_16x16"), 25, Craters_16x16);
        craterCounter16 = 0;


        playerPlaneCrash = Instantiate((GameObject)Resources.Load("Explosion_PlayerCrash"));
    }
    void CreatePool(GameObject g, int amount, List<GameObject> list)
    {
        list.Clear();
        for (int i = 0; i < amount; i++)
        {
            GameObject newGuy = Instantiate(g);
            list.Add(newGuy);
            newGuy.SetActive(false);
           // newGuy.hideFlags = HideFlags.HideInHierarchy;
        }
    }
    public static GameObject GetBomb()
    {
        foreach(GameObject b in normalBombs)
        {
            if (b.activeInHierarchy == false)
                return b;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetRocketBomb()
    {
        foreach (GameObject b in rocketBombs)
        {
            if (b.activeInHierarchy == false)
                return b;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetElectroBomb()
    {
        foreach (GameObject b in electroBombs)
        {
            if (b.activeInHierarchy == false)
                return b;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetButterflyBomb()
    {
        foreach (GameObject b in butterflyBombs)
        {
            if (b.activeInHierarchy == false)
                return b;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetBigBomb()
    {
        foreach (GameObject b in bigBombs)
        {
            if (b.activeInHierarchy == false)
                return b;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetExplosion()
    {
        foreach (GameObject e in normalBombExplosions)
        {
            if (e.activeInHierarchy == false)
                return e;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetBigExplosion()
    {
        foreach (GameObject e in bigBombExplosions)
        {
            if (e.activeInHierarchy == false)
                return e;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetAAShell()
    {
        foreach (GameObject e in AA_Shells)
        {
            if (e.activeInHierarchy == false)
                return e;
            else
                continue;
        }
        return null;
    }
    public static GameObject GetAABullet()
    {
        foreach (GameObject e in AA_Bullets)
        {
            if (e.activeInHierarchy == false)
                return e;
            else
                continue;
        }
        return null;
    }
    private static int craterCounter8 = 0;

    public static GameObject GetCrater8x8()
    {
        craterCounter8++;
       // Debug.Log("Crater 8 " + craterCounter8);

        if (craterCounter8 > Craters_8x8.Count - 1)
            craterCounter8 = 0;
        return Craters_8x8[craterCounter8];
    }
    private static int craterCounter16 = 0;
    public static GameObject GetCrater16x16()
    {
        craterCounter16++;
      //  Debug.Log("Crater 16 " + craterCounter16);
        if (craterCounter16> Craters_16x16.Count - 1)
            craterCounter16 = 0;
        return Craters_16x16[craterCounter16];

    }
    public static GameObject GetPlayerPlaneCrash()
    {
        return playerPlaneCrash;
    }
}
