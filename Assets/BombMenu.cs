using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombMenu : Bomb
{
    public override void DropBomb()
    {
        base.DropBomb();
        //rb.AddForce(Vector3.down * 1000f, ForceMode.Impulse);
        rb.velocity = Vector3.down * 40f;

    }
    
}
