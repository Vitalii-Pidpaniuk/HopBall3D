using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private GameObject flowerObject;
    
    public void ActiveFlower()
    {
        flowerObject.SetActive(true);
    }
}
