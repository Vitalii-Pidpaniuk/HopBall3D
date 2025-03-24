using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    private int _coins;
    [SerializeField] private TextMeshProUGUI priceTag;

    public int GetCoins()
    {
        return _coins;
    }

    public void SetCoins(int coins)
    {
        _coins = coins;
        priceTag.text = coins.ToString();
    }
}
