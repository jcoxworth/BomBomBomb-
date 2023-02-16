using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivate : MonoBehaviour
{
    public float timeLimit = 10f;
    private void OnEnable()
    {
        StartCoroutine(Deactivate());
    }
    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(timeLimit);
        gameObject.SetActive(false);
    }
}
