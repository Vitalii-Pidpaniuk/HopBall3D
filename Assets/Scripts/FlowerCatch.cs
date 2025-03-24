using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCatch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("FlowerCached!");
            gameObject.SetActive(false);
            GameManager.Instance.FlowerCatched();
        }
    }
}
