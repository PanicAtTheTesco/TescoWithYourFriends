using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public CanvasGroup Pause;

    private void Start()
    {
        HidePause();

        if (Pause != null)
        {
            Pause.alpha = 0.0f;
            Pause.interactable = false;
            Pause.blocksRaycasts = false;
        }

    }


    #region ShowMethods

    public void ShowPause()
    {
        StartCoroutine(CanvasUpdater(Pause, 1.0f));
        GameManager.Instance.pause = true;
    }

    #endregion 
    
    #region HideMethods

    public void HidePause()
    {
        StartCoroutine(CanvasUpdater(Pause, 0.0f));
        GameManager.Instance.pause = false;
    }

    #endregion

    private IEnumerator CanvasUpdater(CanvasGroup canvas, float target)
    {
        if(canvas!= null)
        {
            float startAlpha = canvas.alpha;
            float time = 0f;

            canvas.interactable = (target >= 1.0f);
            canvas.blocksRaycasts = (target >= 1.0f);

            while (time < 0.5f)
            {
                time = Mathf.Clamp(time + Time.deltaTime, 0f, 0.5f);
                canvas.alpha = Mathf.SmoothStep(startAlpha, target, time / 0.5f);
                yield return null;
            }

        }
    }

}
