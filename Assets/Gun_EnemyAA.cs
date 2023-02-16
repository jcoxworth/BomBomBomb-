using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_EnemyAA : Gun
{
    public override void InitializeGun()
    {
        base.InitializeGun();
        gunTarget = GamePlayer._playerPlane.transform.position;
    }
    //enemy guns only activate if they somewhat in range of being visible to the player
    public override void ActivateGun()
    {
        base.ActivateGun(); //in the base we might set it active true if it's not destroyed (set active false by building script)

        //but then this part might set it active based on position
        if (transform.position.z < 220f && transform.position.z > -140f && GamePlayer._playerPlane.transform.position.x < 200f)
        {
            if (aimPosition == Vector3.zero)
                aimPosition = GetAimPosition();
        }
        else
        {
            Active = false;
        }
    }
    public override float GetRelativeRange()
    {
        return GamePlayer._playerPlane.transform.position.y;
    }
    public override Vector3 GetAimPosition()
    {
        Vector3 nextAimPosition = GamePlayer._playerPlane.transform.position;

        float accuracy = (200f - gunAccuracy * 200f);
        nextAimPosition += new Vector3(
            Random.Range(-accuracy, accuracy), //x
            Random.Range(-accuracy, 0), //y
            Random.Range(0, accuracy)); //z always shoot infront of the plane
        nextAimPosition.y = Mathf.Clamp(nextAimPosition.y, lowerRange, upperRange);
        return nextAimPosition;
    }
}
