using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMesh : MonoBehaviour
{
    public GameObject[] Meshes;
    [HideInInspector]public GameObject chosenMesh;
    private void OnEnable()
    {
        int r = Random.Range(0, Meshes.Length);
        for (int i = 0; i < Meshes.Length; i++)
        {
            if (i == r)
            {
                chosenMesh = Meshes[i];
                Meshes[i].SetActive(true);
            }
            else
                Meshes[i].SetActive(false);
        }
    }
}
