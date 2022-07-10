using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup FadeScreen;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        FadeScreen.DOFade(0, 0.5f);
    }
}
