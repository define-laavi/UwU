using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public AudioSource source;
    public AudioSource theSoundOfTheVoid;
    public CanvasGroup FadeScreen;
    public CanvasGroup EnterText;
    void Start()
    {
        theSoundOfTheVoid.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Return))
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
        //TODO: Switch scene
        SceneManager.LoadScene("Level");
    }
}
