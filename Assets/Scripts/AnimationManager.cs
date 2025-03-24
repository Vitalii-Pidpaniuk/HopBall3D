using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    [CanBeNull] public CanvasGroup canvasGroup, perfectShotCanvasGroup, tutorialCanvasGroup;
    public float fadeTime, perfectShotFadeTime = 0.2f;
    public Button pauseButton;
    public GameObject tutorialCanvas;
    
    public void PanelJumpFadeIn(GameObject panel)
    {
        RectTransform rectTransform;
        canvasGroup.alpha = 0f;
        rectTransform = panel.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.Log("rect transform empty");
        }
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.interactable = true;
        Debug.Log("Win panel animation");
        pauseButton.interactable = false;
    }
    
    public void PanelJumpFadeOut(GameObject panel)
    {
        RectTransform rectTransform;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = false;
        rectTransform = panel.GetComponent<RectTransform>();
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(0, fadeTime);
        pauseButton.interactable = false;
        StartCoroutine(ButtonCoolDown());
    }

    private IEnumerator ButtonCoolDown()
    {
        yield return new WaitForSeconds(fadeTime);
        pauseButton.interactable = true;
    }

    public void PanelFadeIn(CanvasGroup passedCanvasGroup)
    {
        passedCanvasGroup.alpha = 0f;
        passedCanvasGroup.DOFade(1, perfectShotFadeTime);
    }
    
    public void PanelFadeOut(CanvasGroup passedCanvasGroup)
    {
        passedCanvasGroup.alpha = 1f;
        passedCanvasGroup.DOFade(0, perfectShotFadeTime);
    }
}
