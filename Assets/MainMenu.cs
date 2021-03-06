using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public AudioSource source;
    public CanvasGroup FadeScreen;
    public CanvasGroup EnterText;
    private bool _lock;
    void Start()
    {
        
    }

    void Update()
    {
        if (!_lock && Input.GetKeyDown (KeyCode.Return))
        {
            source.Play();
            StartCoroutine(Fade());

        }
    }

    IEnumerator Fade ()
    {
        EnterText.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.3f);
        FadeScreen.DOFade(1, 2f);
        yield return new WaitForSeconds (2f);
        SceneManager.LoadScene("Level");
    }
}
