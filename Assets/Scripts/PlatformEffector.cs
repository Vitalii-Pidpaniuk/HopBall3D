using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();

    private void Start()
    {
        // Зберігаємо початкові позиції та обертання частин
        foreach (Rigidbody part in allParts)
        {
            initialPositions.Add(part.transform.localPosition);
            initialRotations.Add(part.transform.localRotation);
        }
    }

    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
            part.AddExplosionForce(500f, transform.position, 5f); // Ефект вибуху
        }
    }

    public void SwapParts(float duration = 1.5f)
    {
        StartCoroutine(ReassembleParts(duration));
    }

    private IEnumerator ReassembleParts(float duration)
    {
        float elapsedTime = 0f;

        // Вимикаємо фізику перед збиранням
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = true;
        }

        while (elapsedTime < duration)
        {
            for (int i = 0; i < allParts.Count; i++)
            {
                allParts[i].transform.localPosition = Vector3.Lerp(allParts[i].transform.localPosition, initialPositions[i], elapsedTime / duration);
                allParts[i].transform.localRotation = Quaternion.Lerp(allParts[i].transform.localRotation, initialRotations[i], elapsedTime / duration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Фінальне позиціонування
        for (int i = 0; i < allParts.Count; i++)
        {
            allParts[i].transform.localPosition = initialPositions[i];
            allParts[i].transform.localRotation = initialRotations[i];
        }
    }
}

