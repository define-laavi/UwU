using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeymasterSpeedrun : MonoBehaviour
{
    public CanvasGroup fadeOut;
    private bool isFadingOut;

    public CanvasGroup hint;
    private bool isHintShown = true;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadMenu());
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isHintShown)
            {
                hint.DOFade(1, 0.5f);
                isHintShown = true;
            }
            else
            {
                HideHint();
            }
            
            
        }
    }

    public void HideHint()
    {
        hint.DOFade(0, 0.5f);
        isHintShown = false;    
    }
    
    IEnumerator LoadMenu()
    {
        if (!isFadingOut)
        {
            isFadingOut = true;
            fadeOut.DOFade(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
