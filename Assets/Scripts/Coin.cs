using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinObject;
    
    public void ActiveCoin()
    {
        coinObject.SetActive(true);
    }
}
