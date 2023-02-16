using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum CraterType { none, size8x8, size16x16}
    public CraterType craterType = CraterType.none;
    public SphereCollider sphere;
    public float sphereStartSize = 5f;
    public float sphereExpansionRate = 10f;
    public float damageTime = 1f;
    private float currentTime = 0f;
    public int startingDamage = 100;
    private int currentDamage = 100;
    private void OnEnable()
    {
        if (!sphere)
            sphere = GetComponent<SphereCollider>();
        sphere.isTrigger = true;
        sphere.enabled = true;
        sphere.radius = sphereStartSize;
        currentTime = 0f;
        currentDamage = startingDamage;


        switch (craterType)
        {
            case CraterType.none:
                break;
            case CraterType.size8x8:
                if (transform.position.y < 10f) // only make craters close to the ground
                {
                    GameObject crater = GameResources.GetCrater8x8();
                    Vector3 pos = transform.position;
                    pos.y = 1.2f;
                    crater.transform.position = pos;
                    crater.SetActive(true);
                }
                break;
            case CraterType.size16x16:
                if (transform.position.y < 10f) // only make craters close to the ground
                {
                    GameObject crater = GameResources.GetCrater16x16();
                    Vector3 pos = transform.position;
                    pos.y = 1.2f;

                    crater.transform.position = pos;
                    crater.SetActive(true);
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        if (currentDamage > 1)
        {
            currentDamage -= 1;
        }
        currentTime += Time.deltaTime;
        sphere.radius += (Time.deltaTime * sphereExpansionRate);
        if (currentTime > damageTime)
        {
            sphere.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        other.transform.BroadcastMessage("Damage", currentDamage, SendMessageOptions.DontRequireReceiver);
    }
}
