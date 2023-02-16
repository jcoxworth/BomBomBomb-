using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public static GameCamera _gameCamera;
    public Camera mainCamera;

    public Transform camTransform;

    public Transform camBombing, camGameOver, camShop;
    // How long the object should shake for.
    public static float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    Vector3 currentPos;
    Quaternion currentRot;

    

    void Start()
    {
        InitializeCamera();
    }
    public void InitializeCamera()
    {
        _gameCamera = this;
        if (!mainCamera)
            mainCamera = FindObjectOfType<Camera>();

        SetCamBombing();
    }
    public void SetCamBombing()
    {
        //mainCamera.transform.SetParent(camBombing);
        //mainCamera.transform.localPosition = Vector3.zero;
        //mainCamera.transform.localRotation = Quaternion.identity;

        mainCamera.transform.SetParent(PlayerPlane._playerPlane.transform);
        currentPos = camBombing.localPosition;
        currentRot = camBombing.localRotation;
    }
    public void SetCamShop()
    {
        //mainCamera.transform.SetParent(camBombing);
        //mainCamera.transform.localPosition = Vector3.zero;
        //mainCamera.transform.localRotation = Quaternion.identity;

        mainCamera.transform.SetParent(PlayerPlane._playerPlane.transform);
        currentPos = camShop.localPosition;
        currentRot = camShop.localRotation;
    }
    public void SetCamGameOver()
    {
        //mainCamera.transform.SetParent(camGameOver);
        //mainCamera.transform.localPosition = Vector3.zero;
        // mainCamera.transform.localRotation = Quaternion.identity;

        mainCamera.transform.SetParent(PlayerPlane._playerPlane.transform);
        currentPos = camGameOver.localPosition;
        currentRot = camGameOver.localRotation;
    }
    void Update()
    {
        UpdateCamera();
    }
    private void UpdateCamera()
    {
        if (shakeDuration > 0)
        {
            mainCamera.transform.localPosition = currentPos + new Vector3(0f, Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            mainCamera.transform.localPosition = currentPos;
        }

        mainCamera.transform.localRotation = currentRot;
    }
    public static void SmallShake()
    {
        shakeDuration = 0.2f;
    }
    public static void MediumShake()
    {
        shakeDuration = 0.4f;
    }
    public static void BigShake()
    {
        shakeDuration = 1.25f;
    }

}
