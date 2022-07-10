using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager Instance;

    public int Prestige;
    public List<ExhibitionArea> Areas = new List<ExhibitionArea>();

    public MorbContextBinder Morb;

    public Image FadeImage;
    public TextMeshProUGUI FadeText;

    public int _currentLvl;
    public List<Lvl> Levels = new List<Lvl> ();

    void Awake ()
    {
        Instance = this;
    }

    void Start()
    {
        _currentLvl = 0;
        SetMorb();
        Prestige = 0;
        Areas[0].gameObject.SetActive(true);/*
        for (int i = 1; i < Areas.Count; i++)
        {
            Areas[i].gameObject.SetActive(false);
        }*/
    }

    public static void Recalculate ()
    {
        Instance.RecalculatePrestige();
    }

    private void RecalculatePrestige ()
    {
        Prestige = 0;
        for (int i = 0; i < Areas.Count; i++)
        {
            if (!Areas[i].IsAvailable)
            {
                break;
            }
            Prestige += Areas[i].Prestige;

        }

        if (Prestige > Levels[_currentLvl].PrestigeThreshold)
        {
            _currentLvl++;
            if (SetMorb())
            {
                StartCoroutine(End());
            }
        } else
        {
            Morb.currentValue = Prestige;
        }

    }

    IEnumerator End ()
    {
        FadeImage.DOFade(1, 3f);
        yield return new WaitForSeconds(3);

        FadeText.DOColor(Color.black, 3);
        yield return new WaitForSeconds(10);

        SceneManager.LoadSceneAsync(0);
    }

    private bool SetMorb ()
    {
        int l = _currentLvl;
        Morb.currentValue = Prestige;
        Morb.levelStartPoint = Levels[_currentLvl].PrestigeThreshold;
        if (_currentLvl + 1 < Levels.Count)
        {
            Morb.nextLevelValue = Levels[_currentLvl+1].PrestigeThreshold;

            return false;
        }
        Morb.nextLevelValue = _currentLvl;
        return true;
    }
}
[System.Serializable]
public class Lvl
{
    public int PrestigeThreshold;

    public UnityEvent OnEnd;
}