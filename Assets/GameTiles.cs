using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTiles : MonoBehaviour
{
    public GameObject tile;
    public List<GameObject> tileList = new List<GameObject>();

    public static List<GameObject> targetList = new List<GameObject>();
    public float startZ = 1024f;
    public int numberOfTiles = 32;
    public int currentLevel = 0;
    public LevelObjectsScriptableObject[] LevelObjectSets;
    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
    }
    private void GenerateTiles()
    {
        foreach (GameObject oldTile in tileList)
        {
            Destroy(oldTile);
        }
        tileList.Clear();
        foreach (GameObject oldTile in targetList)
        {
            Destroy(oldTile);
        }
        targetList.Clear();

        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject t = Instantiate(
                tile, 
                new Vector3(-80f, 0f, startZ - (i * 64f)), 
                Quaternion.identity
                );

            ModifyTilePopulator(t, i);
            tileList.Add(t);
        }
    }
    public void NextLevel()
    {
        if (currentLevel < LevelObjectSets.Length - 1)
        {
            currentLevel++;
        }
        else
            currentLevel = 0;
        GenerateTiles();
    }
    //We don't want every tile to have the same attributes
    //this will make it so that tile intensity ramps up near the ends, and dips in the middle

    private void ModifyTilePopulator(GameObject tile, int count)
    {
        int offsetCount = Mathf.Abs(count - 8);
        tile.transform.GetComponent<TilePopulator>().levelObjects = LevelObjectSets[currentLevel];
        tile.transform.GetComponent<TilePopulator>().Intensity = (offsetCount*0.1f);
        tile.transform.GetComponent<TilePopulator>().MaximumBuildings *= (offsetCount +1);
    }
}
