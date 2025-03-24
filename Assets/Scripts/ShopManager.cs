using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

public class PurchaseData
{
    public Dictionary<int, bool> purchases = new Dictionary<int, bool>();
}


public class ShopManager : MonoBehaviour
{
    private string savePath;
    private PurchaseData purchaseData = new PurchaseData();

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "purchases.json");
        LoadPurchases();
    }

    // Додає покупку та зберігає
    public void BuyItem(int itemId)
    {
        if (!purchaseData.purchases.ContainsKey(itemId))
        {
            purchaseData.purchases[itemId] = true;
            SavePurchases();
        }
    }

    // Перевіряє, чи куплений товар
    public bool IsItemPurchased(int itemId)
    {
        return purchaseData.purchases.TryGetValue(itemId, out bool isPurchased) && isPurchased;
    }

    // Виводить всі покупки у консоль
    public void PrintPurchases()
    {
        foreach (var item in purchaseData.purchases)
        {
            Debug.Log($"Item: {item.Key}, Purchased: {item.Value}");
        }
    }
    
    public Dictionary<int, bool> GetPurchases()
    {
        return purchaseData.purchases;
    }

    // Збереження у JSON
    private void SavePurchases()
    {
        string json = JsonUtility.ToJson(purchaseData);
        File.WriteAllText(savePath, json);
    }

    // Завантаження з JSON
    private void LoadPurchases()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            purchaseData = JsonUtility.FromJson<PurchaseData>(json);
        }
    }
}
