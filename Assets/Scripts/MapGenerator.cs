using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int preparedTilesQuantity;
    //public GameObject mapPrefab;
    public float distance;
    public void GenerateStartTiles(List<GameObject> tales, GameObject talePrefab)
    {
        for (int i = 0; i < preparedTilesQuantity; i++)
        {
            tales.Add( Instantiate(talePrefab, new Vector3(0, 0, distance * i), Quaternion.identity));
        }
    }
}
