using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCatch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("CoinCached!");
            gameObject.SetActive(false);
            GameManager.Instance.CoinCatched();
        }
    }
}
