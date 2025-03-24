using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundGenerator : MonoBehaviour
{
    private int configNum;
    public List<GameObject> mapTales;
    public List<GameObject> TalesList;
    public List<Material> skyMaterials;
    public List<GameObject> lightPrefabs;
    public GameObject staticBackgroundPrefab;
    private MapGenerator mapGenerator;
    [SerializeField] private MapMover mapMover;
    void Start()
    {
        configNum = Random.Range(0, skyMaterials.Count - 1);
        mapGenerator = GetComponent<MapGenerator>();
        mapGenerator.GenerateStartTiles(mapTales, TalesList[configNum]);
        RenderSettings.skybox = skyMaterials[configNum];
        Instantiate(lightPrefabs[configNum]);
        Instantiate(staticBackgroundPrefab);
    }
    void Update()
    {
        mapMover.MovePlatforms(mapTales);
    }
}
