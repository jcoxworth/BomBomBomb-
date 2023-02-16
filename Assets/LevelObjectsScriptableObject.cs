using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level_Objects", order =1)]

public class LevelObjectsScriptableObject : ScriptableObject
{
    
    public int TargetsToDestroy;
    public int CloudHeight_Max;
    public int CloudHeight_Min;
    public GameObject[]
        BLD_32x32,
        BLD_16x16,
        BLD_8x8,
        BLD_4x4,
        BLD_Tgt_32x32,
        BLD_Tgt_16x16,
        BLD_Tgt_8x8,
        BLD_Tgt_4x4,
        BLD_Spl_16x16,
        BLD_Spl_8x8,
        BLD_Spl_4x4,
        AA_32x32,
        AA_16x16,
        AA_8x8,
        AA_4x4, 
        Cloud,
        Mountain;
    
}
