using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool bulletFlying = true;
    public float bulletSpeed = 100f;
    public Rigidbody rb;
    public GameObject bulletMesh;
    public GameObject hitEffect;
    public float bulletLifetime = 4f;
    private float currentLifetime = 0f;
    // Start is called before the first frame update
   
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        bulletMesh.SetActive(true);
        hitEffect.SetActive(false);
        bulletFlying = true;
        currentLifetime = 0f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (bulletFlying)
        {
            currentLifetime += Time.deltaTime;
            rb.velocity = transform.forward * (bulletSpeed* Time.deltaTime)
            + Vector3.back * (GamePlayer._currentPlaneSpeed * Time.deltaTime);

            if (currentLifetime > bulletLifetime)
            {
                bulletFlying = false;
                gameObject.SetActive(false);
            }
            if (transform.position.y > GamePlayer._currentAltitude)
            {
                AntiAir.GunIncoming();
                bulletFlying = false;
                bulletMesh.SetActive(false);
                hitEffect.SetActive(true);
                gameObject.SetActive(false);

            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        bulletFlying = false;
        bulletMesh.SetActive(false);
        hitEffect.SetActive(true);
        StartCoroutine(DeactivateBullet());
    }*/
    private IEnumerator DeactivateBullet()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
