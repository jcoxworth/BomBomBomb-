using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool dropped = false;
    public bool explodedYet = false;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public MeshRenderer bombMesh;
    private Transform hitCollider;

    private void OnEnable()
    {
        InitializeBomb();
    }

    public virtual void InitializeBomb()
    {
        dropped = false;
        explodedYet = false;
        transform.SetParent(null);

        if (!rb)
            rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = true;
        if (!bombMesh)
            bombMesh = GetComponentInChildren<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {

        bombMesh.transform.Rotate(0, 0, Time.deltaTime * 100f);
        
        if (dropped)
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(Vector3.down), 10f*Time.deltaTime));
    }

    public virtual void DropBomb()
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.down *10f, ForceMode.VelocityChange);
        rb.useGravity = true;
        dropped = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.root.CompareTag(transform.tag))
        {
            hitCollider = collision.transform.root;
            ExplodeBomb();

            /*  if (collision.transform.GetComponent<building>())
            {
                print("DIRECT HIT!");
            }*/
        }
    }
    public virtual void ExplodeBomb()
    {
        explodedYet = true;
        GameObject g = GameResources.GetExplosion();
        g.transform.position = transform.position;
        g.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
