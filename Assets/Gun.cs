using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    
    public bool Active = false;
    public GameObject BarrelTransform;
    [Range(0f, 1f)]     public float gunAccuracy = 0.5f;
    [Range(0.5f, 20f)]  public float timeToReady = 4f;
    [Range(10f, 400f)]  public float upperRange = 300f;
    [Range(10f, 400f)]  public float lowerRange = 100f;

    [HideInInspector] public Quaternion originalRotation;
    [HideInInspector] public Vector3 aimPosition;
    [HideInInspector] public Vector3 gunTarget;
    [HideInInspector] public float currentReadyTime = 0f;
    [HideInInspector] public float gunTargetDistance = 0f;
    [HideInInspector] public IWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGun();
    }
    public virtual void InitializeGun()
    {
        if (!BarrelTransform)
        {
            Debug.Log("No barrel Transform assigned, making one for ya");
            GameObject g = new GameObject();
            g.transform.SetParent(transform);
            BarrelTransform = g;
        }
        originalRotation = BarrelTransform.transform.localRotation;
        currentReadyTime = 0f;
        weapon = GetComponent<IWeapon>();
    }
    // Update is called once per frame
    void Update()
    {
        ActivateGun();
        RotateToAim();
        FireWhenReady();
    }
    public virtual void ActivateGun()
    {
        Active = BarrelTransform.activeSelf;
    }
    public virtual void RotateToAim()
    {
        if (Active)
        {
            Vector3 direction = aimPosition - transform.position;
            BarrelTransform.transform.localRotation = Quaternion.RotateTowards(BarrelTransform.transform.localRotation, Quaternion.LookRotation(direction), 1f);
        }
        else
        {
            BarrelTransform.transform.localRotation = Quaternion.Slerp(BarrelTransform.transform.localRotation, originalRotation, 5f * Time.deltaTime);
        }
    }
    public virtual void FireWhenReady()
    {
        if (Active)
        {
            currentReadyTime += Time.deltaTime;

            if (currentReadyTime > timeToReady)
            {
                if (
                (GetRelativeRange() <= upperRange)
                &&
                (GetRelativeRange() >= lowerRange))
                    if (weapon.weaponReady)
                    {
                        weapon.FireWeapon(aimPosition); //the gun should be ready to fire periodically, and then we can update our aim periodcially as well
                        aimPosition = GetAimPosition();  //fire the gun and then get a new position to aim
                    }
            }
        }
        else
        {
            currentReadyTime = 0f;
        }
    }
    public virtual float GetRelativeRange()
    {
        if (Active)
        {
            return Mathf.Abs(gunTarget.y - transform.position.y); 
            //target at 100, I'm at 50 = 50
            //target at 50, I'm at 100 = -50
        }
        else
        {
            return 0f;
        }
    }
    public virtual Vector3 GetAimPosition()
    {
        Vector3 nextAimPosition = gunTarget;

        float accuracy = (100f - gunAccuracy * 100f);
        nextAimPosition += new Vector3(
            Random.Range(-accuracy, accuracy), //x
            Random.Range(-accuracy, accuracy), //y
            Random.Range(-accuracy, accuracy)); //z always shoot infront of the target
        nextAimPosition.y = Mathf.Clamp(nextAimPosition.y, lowerRange, upperRange);
        return nextAimPosition;
    }
    
}
