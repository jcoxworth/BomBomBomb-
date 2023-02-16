using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePopulator : MonoBehaviour
{
    [Range(0, 100)] public int MaximumBuildings = 30;
    private int currentBuildings = 0;
    [Range(0, 10)] public int StartingDefenses = 2;
    private int currentDefenses = 0;
    [Range(0f, 0.2f)] public float MegaBuildingChance = 0.01f;
    [Range(0f, 0.2f)] public float LargeBuildingChance = 0.01f;
    [Range(0f, 0.2f)] public float MediumBuildingChance = 0.01f;
    [Range(0f, 0.2f)] public float SmallBuildingChance = 0.01f;
    [Range(0f, 0.2f)] public float AA_Chance = 0.5f;
    [Range(0f, 0.2f)] public float Cloud_Chance = 0.5f;
    public LevelObjectsScriptableObject levelObjects;
    public GameObject BLD_16x16, BLD_8x8, BLD_4x4, AA_1, AA_2, Cloud01, Cloud02;
    public List<GameObject> buildingList = new List<GameObject>();
    public List<GameObject> openPositions = new List<GameObject>();
    public List<GameObject> mountains = new List<GameObject>();
    public float Intensity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PopulateTile();
    }
    
    public void CheckForDestroyedBuildings()
    {
        foreach(GameObject b in buildingList)
        {
            if (b.transform.GetComponent<Building>().Destroyed)
            {
                //buildingList.Remove(b);
                openPositions.Add(b);
            }
        }
        if (openPositions.Count > 0)
        {
            AddDefenses();
        }
    }
    public void AddDefenses()
    {
        GameObject newDefensePosition = openPositions[Random.Range(0, openPositions.Count)];
        if (newDefensePosition == null)
            return;
        else
        {
            GameObject newAA;
            float rAA = Random.value;
            if (rAA < 0.5f)
            {
                newAA = Instantiate(AA_1, transform);
            }
            else
            {
                newAA = Instantiate(AA_2, transform);
            }
            newAA.transform.localPosition = newDefensePosition.transform.localPosition;
            openPositions.Remove(newDefensePosition);
        }
    }
    public void PopulateTile()
    {
        foreach (GameObject b in buildingList)
        {
            Destroy(b);
        }
        buildingList.Clear();
        openPositions.Clear();
        mountains.Clear();
        currentBuildings = 0;
        PopulateTile_32();
        PopulateSideMountains();
    }

    void PopulateSideMountains()
    {
        //mountains on the right
        for (int h = 0; h < 2; h++) //32x32 size tiles are two rows high
        {
            for (int i = 0; i < 5; i++) //32x32 size tiles are 5 columns wide
            {

                GameObject newMountain = Instantiate(levelObjects.Mountain[Random.Range(0, levelObjects.Mountain.Length)], transform);
                newMountain.transform.localPosition = new Vector3(
                    160 + 16 + (i * 32),
                    0f, 
                    16 + (h * 32));
                mountains.Add(newMountain);
            }
        }
    }
    void PopulateTile_32()
    {
        for (int h = 0; h < 2; h++) //32x32 size tiles are two rows high
        {
            for (int i = 0; i < 5; i++) //32x32 size tiles are 5 columns wide
            {
                float r = Random.value;
                float adjustedChance = MegaBuildingChance - (MegaBuildingChance * (currentBuildings / MaximumBuildings));
                if (r < adjustedChance && currentBuildings < MaximumBuildings)
                {
                    if (Intensity > 0.7f && (levelObjects.BLD_Tgt_32x32.Length > 0))
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_Tgt_32x32[Random.Range(0, levelObjects.BLD_Tgt_32x32.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(16 + (i * 32), 0.5f, 16 + (h *32));
                        buildingList.Add(newBuilding);
                        //add it to some global list for evaluating mission status as well
                        GameTiles.targetList.Add(newBuilding);
                        currentBuildings++;
                    }
                    else
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_32x32[Random.Range(0, levelObjects.BLD_32x32.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(16 + (i * 32), 0.5f, 16 + (h * 32));
                        buildingList.Add(newBuilding);
                        currentBuildings++;
                    }
                }
                else
                {
                    PopulateTile_16((i * 16), (h * 16));
                }

            }
        }
    }
    void PopulateTile_16(float x, float z)
    {
        //the tile has 2 rows represented by h
        //and 10 columns, representd by i
        for (int h = 0; h < 4; h++)
        {
            for (int i = 0; i < 10; i++)
            {
                float r = Random.value;
                float adjustedChance = LargeBuildingChance - (LargeBuildingChance * (currentBuildings / MaximumBuildings));
                //adjustedChance *= Intensity;
                if (r < adjustedChance && currentBuildings < MaximumBuildings)
                {
                    if (Intensity > 0.7f && (levelObjects.BLD_Tgt_16x16.Length > 0))
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_Tgt_16x16[Random.Range(0, levelObjects.BLD_Tgt_16x16.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(8 + x+ (i * 16), 0.5f, 8 + z + (h * 16));
                        buildingList.Add(newBuilding);
                        //add it to some global list for evaluating mission status as well
                        GameTiles.targetList.Add(newBuilding);
                        currentBuildings++;
                    }
                    else
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_16x16[Random.Range(0, levelObjects.BLD_16x16.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(8 + x + (i * 16), 0.5f, 8 + z + (h * 16));
                        buildingList.Add(newBuilding);
                        currentBuildings++;
                    }
                }
                else
                {
                    PopulateTile_8((i * 16), (h * 16));
                }
            }
        }
    }
    
    void PopulateTile_8(float x, float z)
    {
        for (int h = 0; h < 2; h++)
        {
            for (int i = 0; i < 2; i++)
            {
                float r = Random.value;
                float adjustedChance = MediumBuildingChance - (MediumBuildingChance * (currentBuildings / MaximumBuildings));
                //adjustedChance += Intensity;

                if (r < adjustedChance && currentBuildings < MaximumBuildings)
                {
                    if (Intensity >0.7f && (levelObjects.BLD_Tgt_8x8.Length > 0))
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_Tgt_8x8[Random.Range(0, levelObjects.BLD_Tgt_8x8.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(4 + x + (i * 8), 0.5f, 4 + z + (h * 8));
                        buildingList.Add(newBuilding);
                        //add it to some global list for evaluating mission status as well
                        GameTiles.targetList.Add(newBuilding);
                        currentBuildings++;
                    }
                    else
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_8x8[Random.Range(0, levelObjects.BLD_8x8.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(4 + x + (i * 8), 0.5f, 4 + z + (h * 8));
                        buildingList.Add(newBuilding);
                        currentBuildings++;
                    }
                }
                else
                {
                    PopulateTile_4( x + (i * 8),z + (h * 8));
                }
            }
        }
    }
    void PopulateTile_4(float x, float z)
    {
        for (int h = 0; h < 2; h++)
        {
            for (int i = 0; i < 2; i++)
            {
                float r = Random.value;
                float adjustedChance = SmallBuildingChance - (SmallBuildingChance * (currentBuildings / MaximumBuildings));
                //adjustedChance *= Intensity;

                if (r < adjustedChance && currentBuildings < MaximumBuildings)
                {
                    if (Intensity > 0.7f && (levelObjects.BLD_Tgt_8x8.Length > 0))
                    {
                        //4x4 targets should be really hard to hit
                        GameObject newBuilding = Instantiate(levelObjects.BLD_Tgt_4x4[Random.Range(0, levelObjects.BLD_Tgt_4x4.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(2 + x + (i * 4), 0.5f, 2 + z + (h * 4));
                        buildingList.Add(newBuilding);
                        //add it to some global list for evaluating mission status as well
                        GameTiles.targetList.Add(newBuilding);
                        currentBuildings++;
                    }
                    else
                    {
                        GameObject newBuilding = Instantiate(levelObjects.BLD_4x4[Random.Range(0, levelObjects.BLD_4x4.Length)], transform);
                        newBuilding.transform.localPosition = new Vector3(2 + x + (i * 4), 0.5f, 2 + z + (h * 4));
                        buildingList.Add(newBuilding);
                        currentBuildings++;
                    }
                }
                else
                {
                    float aa = Random.value;
                    float aa_adjustedChance = AA_Chance - (AA_Chance * (currentDefenses / StartingDefenses));
                    if (aa < aa_adjustedChance && currentDefenses < StartingDefenses)
                    {
                        GameObject newAA;
                        newAA = Instantiate(levelObjects.AA_4x4[Random.Range(0, levelObjects.AA_4x4.Length)], transform);
                        newAA.transform.localPosition = new Vector3(2 + x + (i * 4), 0.5f, 2 + z + (h * 4));
                        buildingList.Add(newAA);
                        currentDefenses++;
                    }
                    else
                    {
                        PopulateNature(2 + x + (i * 4), 2 + z + (h * 4));
                    }
                    continue;
                }
            }
        }
    }


    void PopulateNature(float x, float z)
    {

        PopulateClouds(x, z);

    }
    void PopulateClouds(float x, float z)
    {
        float r = Random.value;
        if (r < Cloud_Chance)
        {
            GameObject newCloud= Instantiate(levelObjects.Cloud[Random.Range(0, levelObjects.Cloud.Length)], transform);
            
            newCloud.transform.localPosition = new Vector3(x, Random.Range(levelObjects.CloudHeight_Min, levelObjects.CloudHeight_Max), z);
            //buildingList.Add(newAA);
           // currentDefenses++;
        }
        else
        {
           // GameObject nothing = new GameObject();
         //   nothing.transform.SetParent(transform);
          //  openPositions.Add(nothing);
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
