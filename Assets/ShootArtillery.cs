using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArtillery : MonoBehaviour
{
    private float currentCoolDown = 0f;
    public float coolDownTime = 1f;
    private float extraCoolDown;
    private Vector3 originalBarrelPosition;
    public Transform barrelTransform;
    public GameObject barrelEffect;
    public bool gunReady = false;
    // Start is called before the first frame update
    void Start()
    {
        originalBarrelPosition = barrelTransform.localPosition;
        barrelEffect.SetActive(false);
        coolDownTime *= Random.Range(0.75f, 1.5f);
        extraCoolDown = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        currentCoolDown += Time.deltaTime;
        if (currentCoolDown > (coolDownTime + extraCoolDown))
        {
            gunReady = true;
        }

    }
    //the aiming is handled in AntiAir.cs
    public void Fire(Vector3 aimPosition) //the aim position is what the gun is aiming at, which the player can see visually, the gun will not aim directly at the player
    {
        if (currentCoolDown > (coolDownTime + extraCoolDown))
        {
            currentCoolDown = 0;
            StartCoroutine(ShowBarrelEffect());
            StartCoroutine(DetonateNearPlayer(aimPosition));
            gunReady = false;
        }
    }

    private IEnumerator ShowBarrelEffect()
    {
        barrelEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        barrelEffect.SetActive(false);
    }


    private IEnumerator DetonateNearPlayer(Vector3 aimPosition)
    {
     //   extraCoolDown = (3f - (GamePlayer._compoundedChance * 3f));
        yield return new WaitForSeconds(GamePlayer._currentAltitude * 0.015f);
        GameObject newShell = GameResources.GetAAShell();
        newShell.transform.position = aimPosition;
        newShell.SetActive(true);
    }
}
