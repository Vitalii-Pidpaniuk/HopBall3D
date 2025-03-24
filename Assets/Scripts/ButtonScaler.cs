using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScaler : MonoBehaviour
{
    private Vector3 _standartScale;

    private void Start()
    {
        _standartScale = transform.localScale;
    }

    public void Scale()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        StartCoroutine(ReturnScale());
        Debug.Log("scale changed");
    }

    private IEnumerator ReturnScale()
    {
        yield return new WaitForSeconds(0.2f);
        transform.localScale = _standartScale;
    }
}
