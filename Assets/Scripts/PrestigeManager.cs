using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager Instance;

    public int Prestige;
    public List<ExhibitionArea> Areas = new List<ExhibitionArea>();

    void Awake ()
    {
        Instance = this;
    }

    void Start()
    {
        Prestige = 0;
        Areas[0].gameObject.SetActive(true);
        for (int i = 1; i < Areas.Count; i++)
        {
            Areas[i].gameObject.SetActive(false);
        }
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
    }
}
