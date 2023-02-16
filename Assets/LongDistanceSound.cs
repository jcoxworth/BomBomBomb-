using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceSound : MonoBehaviour
{
    public bool shakeCamera = false;
    public float volume = 0.7f;
    public AudioClip[] clip;
    private void OnEnable()
    {
        float distance = Mathf.Abs(GamePlayer._playerPlane.transform.position.y - transform.position.y);
       // Debug.Log("distance" + distance);
        GameSound._gameSound.PlayDistantSound(distance, volume, clip[Random.Range(0,clip.Length)], shakeCamera);
    }
}
