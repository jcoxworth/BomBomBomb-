using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRocket : Bomb
{
    public override void DropBomb()
    {
        base.DropBomb();
        rb.AddForce(Vector3.down * 90f, ForceMode.VelocityChange);

    }

}
