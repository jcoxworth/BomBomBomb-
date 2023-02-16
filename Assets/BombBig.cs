using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBig : Bomb
{
    public override void ExplodeBomb()
    {
        explodedYet = true;
        GameObject g = GameResources.GetBigExplosion();
        g.transform.position = transform.position;
        g.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
