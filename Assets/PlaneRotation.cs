using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotation : MonoBehaviour
{
    public Transform planeTransform;
    public float rotationAmount = 100;
    private float x_current;
    private float x_old;
    private float xDeltaOld;
    private float xDelta;
    [Range(-60f, 60f)]
    public float xDeltaSmooth;
    
    private float y_current;
    private float y_old;
    private float yDeltaOld;
    private float yDelta;
    [Range(-45f, 45f)]
    public float yDeltaSmooth;
    
    private Vector3 targetRot;
    private Vector3 currentRot;

    // Start is called before the first frame update
    void Start()
    {
        x_current = transform.position.x;
        x_old = transform.position.x;
        xDeltaOld = 0;
        xDelta = 0;
        y_current = transform.position.y;
        y_old = transform.position.y;
        yDeltaOld = 0;
        yDelta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        x_old = x_current;
        x_current = transform.position.x;

        xDeltaOld = xDelta;
        xDelta =  (x_current - x_old) * -rotationAmount;
        xDeltaSmooth = Mathf.Min(xDeltaOld, xDelta);
        xDeltaSmooth = Mathf.Clamp(xDeltaSmooth, -60f, 60f);





        y_old = y_current;
        y_current = transform.position.y;

        yDeltaOld = yDelta;
        yDelta = (y_current - y_old) * -rotationAmount;
        yDeltaSmooth = Mathf.Min(yDeltaOld, yDelta);
        yDeltaSmooth = Mathf.Clamp(yDeltaSmooth, -45f, 45f);




        targetRot = new Vector3(yDeltaSmooth, 0f, xDeltaSmooth);
        currentRot = Vector3.Lerp(currentRot, targetRot, Time.deltaTime);
        planeTransform.localRotation = Quaternion.Euler(currentRot);
    }
}
