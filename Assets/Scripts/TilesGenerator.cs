using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TilesGenerator : MonoBehaviour
{
    public int preparedTilesQuantity;
    public GameObject tilePrefab;
    public float distance;
    public float maxWidth;
    public void GenerateStartTiles(List<GameObject> tales)
    {
        tales.Add( Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity));
        
        for (int i = 1; i < preparedTilesQuantity; i++)
        {
            tales.Add( Instantiate(tilePrefab, new Vector3(Random.Range(-maxWidth, maxWidth), 0, distance * i), Quaternion.identity));
        }
    }
}
