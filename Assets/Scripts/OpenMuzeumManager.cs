using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMuzeumManager : MonoBehaviour
{

    [SerializeField] private float MuzeumOpenTime = 30;
    [SerializeField] private GameObject NPCpref;


    private float _timer;

    void Start()
    {

    }

    private void OnEnable()
    {
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > MuzeumOpenTime)
        {
            _timer = float.NegativeInfinity;
            GameManager.Instance.SwitchMode();
        }
    }

    IEnumerator SpawnNPC ()
    {
        yield return null;
    }

}
