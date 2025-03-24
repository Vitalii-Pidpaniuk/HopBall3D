using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private TilesGenerator tilesGenerator;
    public float speed;
    
    public void MovePlatforms(List<GameObject> tales)
    {
        foreach (var platform in tales)
        {
            if (platform.transform.position.z <= -tilesGenerator.distance)
            {
                platform.transform.position = new Vector3(Random.Range(-tilesGenerator.maxWidth, tilesGenerator.maxWidth), 0, (tilesGenerator.preparedTilesQuantity - 1) * tilesGenerator.distance);
                platform.GetComponent<PlatformEffector>().SwapParts();
                tales[0].GetComponentInChildren<BoxCollider>().enabled = true;
            }
            platform.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }
}
