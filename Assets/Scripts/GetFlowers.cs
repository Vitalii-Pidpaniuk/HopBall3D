using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetFlowers : MonoBehaviour
{
    private int _stars;
    [SerializeField] private TextMeshProUGUI flowers;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            _stars += DataManager.LoadLevelProgress(i).stars;   
        }

        flowers.text = _stars.ToString();
    }
}
