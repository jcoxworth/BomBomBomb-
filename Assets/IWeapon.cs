using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    bool weaponReady { get; set; }
    void FireWeapon(Vector3 aimPosition);
}
