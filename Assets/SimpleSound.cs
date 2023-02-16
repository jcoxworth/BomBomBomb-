using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSound : MonoBehaviour
{
    public float volume = 0.5f;
    public AudioClip clip;
    private void OnEnable()
    {
        float distance = Mathf.Abs(GamePlayer._playerPlane.transform.position.y - transform.position.y);
        GameSound._gameSound.PlaySimpleSound(distance, volume, clip);
    }
}
