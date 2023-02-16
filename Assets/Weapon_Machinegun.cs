using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Machinegun : MonoBehaviour, IWeapon
{
    //private Vector3 originalBarrelPosition; this could be for animating a kickback effect

    //CoolDown
    private float currentCoolDown = 0f;
    public float coolDownTime = 1f;
    private float extraCoolDown;
    //Machine gun special attributes
    [Range(0.1f, 1f)] public float shotCoolDown = 0.2f;
    private float currentShotCoolDown = 0f;
    //Ready or Not
    public bool _weaponReady; // this is the one we can set ourselves
    public bool weaponReady { get { return _weaponReady; } set { } } //this one is in the interface
    //How long the gun fires for 
    [Range(0.1f, 3f)] public float fireTime = 0.75f;
    public bool gunFiring = false;
    //Effects
    public Transform barrelTransform;
    public GameObject barrelEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        // originalBarrelPosition = barrelTransform.localPosition;
        barrelEffect.SetActive(false);
        coolDownTime *= Random.Range(0.75f, 1.5f);
        extraCoolDown = 3f;
        gunFiring = false;
        currentCoolDown = 0f;
        currentShotCoolDown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentCoolDown += Time.deltaTime;
        _weaponReady = (currentCoolDown > (coolDownTime + extraCoolDown));
        weaponReady = _weaponReady;

        if (gunFiring)
        {
            if (currentShotCoolDown > shotCoolDown)
                fireBullet();
            else
                currentShotCoolDown += Time.deltaTime;
        }
    }
    private void fireBullet()
    {
        StartCoroutine(ShowBarrelEffect());
        GameObject newBullet = GameResources.GetAABullet();
        newBullet.transform.position = barrelTransform.position;
        newBullet.transform.rotation = barrelTransform.rotation;
        newBullet.SetActive(true);
        currentShotCoolDown = 0f;
    }
    //the aiming is handled in AntiAir.cs
    public void FireWeapon(Vector3 aimPosition) //the aim position is what the gun is aiming at, which the player can see visually, the gun will not aim directly at the player
    {
        if (currentCoolDown > (coolDownTime + extraCoolDown))
        {
            currentCoolDown = 0;
           // StartCoroutine(ShowBarrelEffect());
            StartCoroutine(FireMachinegun(aimPosition));
            _weaponReady = false;
        }
    }

    private IEnumerator ShowBarrelEffect()
    {
        barrelEffect.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        barrelEffect.SetActive(false);
    }
    private IEnumerator FireMachinegun(Vector3 aimPosition)
    {
     //   extraCoolDown = (3f - (GamePlayer._compoundedChance * 3f));
        gunFiring = true;
        yield return new WaitForSeconds(fireTime);
        gunFiring = false;
    }
}
