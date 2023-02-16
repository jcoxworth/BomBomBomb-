using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAir : MonoBehaviour
{
    private PlaneHealth planeHealth;

    public static float maxAccuracy = 0.5f;
    [Range(0f, 1f)]
    public static float artilleryMaxAccuracy = 0.5f;
    private static float artCurrAcc;
    [Range(0f, 1f)]
    public static float gunMaxAccuracy = 0.1f;
    private static float gunCurrAcc;
    [Range(0, 1f)]
    public static float accuracy = 0f;


    float altitudeRatio;

    private void Update()
    {
        UpdateAccuracy();

        if (Input.GetKey(KeyCode.A))
            ArtilleryIncoming();
        if (Input.GetKey(KeyCode.G))
            GunIncoming();
    }
    private void UpdateAccuracy()
    {
        altitudeRatio = (GamePlayer._currentAltitude + 1f) / 310f;
        altitudeRatio = 1f - altitudeRatio;

        maxAccuracy = Mathf.Clamp(altitudeRatio, 0.1f, 1f);
        //if plane changing altitude, reduce accuracy
        if (GamePlayer._currentAltitude != GamePlayer._targetAltitude)
        {
            accuracy -= (altitudeRatio * Time.deltaTime);
            accuracy = Mathf.Clamp(accuracy, 0.05f, 1f);
        }
        //if plane is turning, reduce accuracy again
        if (GamePlayer._currentFlightPath != GamePlayer._targetFlightPath)
        {
            accuracy -= (altitudeRatio * Time.deltaTime);
            accuracy = Mathf.Clamp(accuracy, 0.05f, 1f);
        }
        //finally
        //if Settled, build accuracy
        if (GamePlayer._currentFlightPath == GamePlayer._targetFlightPath &&
            GamePlayer._currentAltitude == GamePlayer._targetAltitude)
        {
            accuracy = Mathf.MoveTowards(accuracy, maxAccuracy, altitudeRatio * Time.deltaTime);
        }
        //Debug.Log(altitudeRatio);
    }
    public static void ArtilleryIncoming()
    {
        artCurrAcc = accuracy * artilleryMaxAccuracy;
        float r = Random.value;

        if (r < artCurrAcc)
            Debug.Log("art hit");
        else Debug.Log("art miss");
    }
    public static void GunIncoming()
    {
        gunCurrAcc = accuracy * gunMaxAccuracy;
        float r = Random.value;

        if (r < gunCurrAcc)
            Debug.Log("Gun hit");
        else Debug.Log("gun miss");
    }
    /*
    public bool Active = false;
    public Transform BarrelTransform;
    private Quaternion originalRotation;
    private ShootArtillery shootArtillery;
    [Range(10f, 400f)] public float upperRange = 300f;
    [Range(10f, 400f)] public float lowerRange = 100f;
    Vector3 aimPosition;

    public float timeToReady = 4f;
    private float currentReadyTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        shootArtillery = GetComponent<ShootArtillery>();
        if (!BarrelTransform)
            BarrelTransform = GetComponentInChildren<Transform>();
        originalRotation = BarrelTransform.localRotation;
        //get some aim position to start with
        currentReadyTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ActivateByPosition();
        RotateToAim();
        FireWhenReady();
    }
    private void ActivateByPosition()
    {
        if (transform.position.z < 220f && transform.position.z > -140f && GamePlayer._currentFlightPath < 200)
        {
            if (aimPosition == Vector3.zero)
                aimPosition = GetAimPosition();
            Active = true;
        }
        else
        {
            Active = false;
        }
    }

    private void RotateToAim()
    {
        if (Active)
        {
            Vector3 direction = aimPosition - transform.position;
            BarrelTransform.transform.localRotation = Quaternion.RotateTowards(BarrelTransform.localRotation, Quaternion.LookRotation(direction), 1f);
        }
        else
        {
            BarrelTransform.transform.localRotation = Quaternion.Slerp(BarrelTransform.localRotation, originalRotation, 5f * Time.deltaTime);
        }
    }
    private void FireWhenReady()
    {
        if (Active)
        {
            currentReadyTime += Time.deltaTime;

            if (currentReadyTime > timeToReady)
            {
                if (
                (GamePlayer._playerPlane.transform.position.y <= upperRange)
                &&
                (GamePlayer._playerPlane.transform.position.y >= lowerRange))
                    if (shootArtillery.gunReady)
                    {
                        shootArtillery.Fire(aimPosition); //the gun should be ready to fire periodically, and then we can update our aim periodcially as well
                        aimPosition = GetAimPosition();  //fire the gun and then get a new position to aim
                    }
            }
        }
        else
        {
            currentReadyTime = 0f;
        }
    }

    private Vector3 GetAimPosition()
    {
        Vector3 nextAimPosition = GamePlayer._playerPlane.transform.position;

        float accuracy = (100f * 0.1f);
        nextAimPosition += new Vector3(
            Random.Range(-accuracy, accuracy), //x
            Random.Range(-accuracy, accuracy), //y
            Random.Range(0, accuracy)); //z always shoot infront of the plane
        nextAimPosition.y = Mathf.Clamp(nextAimPosition.y, lowerRange, upperRange);
        return nextAimPosition;
    }*/
}
