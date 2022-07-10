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

    public Transform LvlUPopUP;
    [Range(0f,10f)]public float LvlUpopUPTimeOnScreen = 3f;

    public int _currentLvl;
    public List<Lvl> Levels = new List<Lvl>();
    public float PrestigePeoplePercentege = 0.6f;
    public int _currentNPC = 0;
    public GameObject NPCpref;
    public Transform SpawnPoint;
    public Transform EndPoint;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _currentLvl = 0;
        StartCoroutine(SpawnNPC());
        Prestige = 0;
        Areas[0].gameObject.SetActive(true);
        Areas[0].Recalculate();
        SetMorb();
        /*
        for (int i = 1; i < Areas.Count; i++)
        {
            Areas[i].gameObject.SetActive(false);
        }*/

        LvlUPopUP.gameObject.SetActive(true);

    }

    public static void Recalculate()
    {
        Instance.RecalculatePrestige();
    }

    private void RecalculatePrestige()
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

        if (Prestige >= Levels[_currentLvl].PrestigeThreshold)
        {
            Levels[_currentLvl].OnEnd.Invoke();
            _currentLvl++;
            if (_currentLvl >= Levels.Count)
            {
                StartCoroutine(End());
            }
            else
            {
                StartCoroutine (LvlUp());
                SetMorb();
                
            }
        }
        else
        {
            Morb.currentValue = Prestige;
        }

    }

    IEnumerator LvlUp ()
    {
        LvlUPopUP.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        LvlUPopUP.gameObject.SetActive(false);
    }

    IEnumerator End()
    {
        FadeImage.DOFade(1, 3f);
        yield return new WaitForSeconds(3);

        FadeText.DOColor(Color.black, 3);
        yield return new WaitForSeconds(10);

        SceneManager.LoadSceneAsync(0);
    }

    private void SetMorb()
    {
        Morb.currentValue = Prestige;
        Morb.nextLevelValue = Levels[_currentLvl].PrestigeThreshold;
        if (_currentLvl == 0)
        {
            Morb.levelStartPoint = 0;
            return;
        }
        Morb.levelStartPoint = Levels[_currentLvl - 1].PrestigeThreshold;
    }

    IEnumerator SpawnNPC ()
    {
        float waitTime;
        while (true)
        {
            waitTime = Random.Range(0.6f, 1.5f);

            yield return new WaitForSeconds (waitTime);

            Instantiate(NPCpref, SpawnPoint.position, Quaternion.Euler (0,Random.Range(0,360),0));
            _currentNPC++;
            while (_currentNPC > Prestige * PrestigePeoplePercentege)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}


[System.Serializable]
public class Lvl
{
    public int PrestigeThreshold;

    public UnityEvent OnEnd;
}