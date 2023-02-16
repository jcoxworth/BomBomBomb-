using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Building : MonoBehaviour
{
    public int HitPoints = 10;
    public int PointReward = 500;
    private int currentHitPoints;
    private RandomMesh randomMesh;
    public GameObject explosion;
    public UnityEvent onDestroy;
    public bool Destroyed = false;

    private void Start()
    {
        randomMesh = GetComponent<RandomMesh>();
        currentHitPoints = HitPoints;
        if (!explosion)
            Debug.Log("Building didn't get an explosion");
        explosion.SetActive(false);
        randomMesh.chosenMesh.transform.GetComponent<MeshRenderer>().enabled = true;

    }
    public void Update()
    {
        if (randomMesh.chosenMesh)
        {
            if (transform.position.z < 200f && transform.position.z > -160f)
            {
                randomMesh.chosenMesh.transform.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                randomMesh.chosenMesh.transform.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    public void Damage(int amount)
    {
        currentHitPoints -= amount;
        if (currentHitPoints < 1)
            DestroyBuilding();
    }
    public void DestroyBuilding()
    {
        Destroyed = true;
        explosion.SetActive(true);
        randomMesh.chosenMesh.SetActive(false);
        onDestroy.Invoke();
        GetComponent<Collider>().enabled = false;
        GameScore._gameScore.ChangeScore(PointReward);
    }
}
