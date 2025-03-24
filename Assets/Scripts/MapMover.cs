using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private float speed;
    
    public void MovePlatforms(List<GameObject> tales)
    {
        foreach (var platform in tales)
        {
            if (platform.transform.position.z <= -mapGenerator.distance)
            {
                platform.transform.position = new Vector3(0, 0, (mapGenerator.preparedTilesQuantity - 1) * mapGenerator.distance);
            }
            platform.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }
}
