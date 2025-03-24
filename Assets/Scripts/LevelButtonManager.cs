using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    public int buttonID;
    public TextMeshProUGUI levelScore;
    public List<GameObject> flowers;
    public Sprite pickedFlower;
    
    void Start()
    {
        (int stars, int score) = DataManager.LoadLevelProgress(buttonID);
        levelScore.text += score;
        
        for (int i = 0; i < stars; i++)
        {
            flowers[i].GetComponent<Image>().sprite = pickedFlower;
        }
    }
}
