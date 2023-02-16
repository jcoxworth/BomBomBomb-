using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public float speed = 10f;
    public float offset_Y = -5f;
    public float offset_X = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (!cameraTarget)
            Debug.Log("Camera has no target");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = cameraTarget.position - transform.position;
        dir.y += offset_Y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
    }
}
