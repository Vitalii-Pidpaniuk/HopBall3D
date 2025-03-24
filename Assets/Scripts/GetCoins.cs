using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCoins : MonoBehaviour
{
    private int _coins;
    [SerializeField] private TextMeshProUGUI coinsText;
    void Start()
    {
        _coins += DataManager.LoadCoins();
        coinsText.text = _coins.ToString();
    }
}
