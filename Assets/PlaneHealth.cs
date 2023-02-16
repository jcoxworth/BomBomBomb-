using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    public delegate void OnRightWingDamage();
    public OnRightWingDamage onRightWingDamage;

    public delegate void OnLeftWingDamage();
    public OnLeftWingDamage onLeftWingDamage;

    public delegate void OnFrontDamage();
    public OnFrontDamage onFrontDamage;

    public delegate void OnRearDamage();
    public OnRearDamage onRearDamage;

    //By Probability, split the damage off into a general area
    public void TakeDamage()
    {
        float r = Random.value;
        if (r >= 0.98f) // 2%
        {
            Debug.Log("MIRACLE - No Damage!");
        }
        else if (r >= 0.85f && r < 0.98f) // 8% 
        {
            onFrontDamage?.Invoke();
        }
        else if (r >= 0.6f && r < 0.85f) // 10% 
        {
            onRearDamage.Invoke();
        }
        else if (r >= 0.3 && r < 0.6f) // 40%
        {
            onRightWingDamage?.Invoke();
        }
        else
        {
            onLeftWingDamage?.Invoke();
        }
    }
    public void Die()
    {

    }
}
