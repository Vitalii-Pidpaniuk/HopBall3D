using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingProcess : MonoBehaviour
{
    private float _newScale = 0.1f;
    public RectTransform fullLine;
    public TextMeshProUGUI percentes;
    private void Start()
    {
        if (fullLine == null)
        {
            fullLine = GetComponent<RectTransform>();
        }

        StartCoroutine(LoadingRoutine());
    }

    private IEnumerator LoadingRoutine()
    {
        while (_newScale < 1.0f)
        {
            yield return new WaitForSeconds(Random.Range(0.002f, 0.1f));
            
            _newScale = Mathf.Min(_newScale + 0.01f, 1f);
            percentes.text = ((int) (_newScale * 100)).ToString() + "%";
            fullLine.localScale = new Vector3(_newScale, 1.0f, 1);
        }

        Debug.Log("Завантаження завершено!");
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
