using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OpenMuzeumManager : MonoBehaviour
{
    public static OpenMuzeumManager Instance;

    [SerializeField] private float MuzeumOpenTime = 30;
    [SerializeField] private GameObject NPCpref;
    [SerializeField] private Transform[] SpawnPoints;
    public Transform ExitPoint;
    public ExhibitionArea[] Areas;

    [SerializeField] private Transform EnterPoint;

    private float _timer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    private void OnEnable()
    {
        _timer = 0;

        StartCoroutine(SpawnNPC());
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

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Transform s = SpawnPoints[i];
            Handles.DrawWireDisc(s.position, Vector3.up, 0.4f);
            Gizmos.DrawLine(s.position, s.position + s.forward);
        }
    }
#endif

    IEnumerator SpawnNPC ()
    {
        int count = Random.Range (3, 10);

        for (int i = 0; i < count; i++) {
            float randomTime = Random.Range(0.2f, 1f);
            yield return new WaitForSeconds (randomTime);
            Transform spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            GameObject go = Instantiate (NPCpref, spawn.position, spawn.rotation);
            go.GetComponent<NPC>().SetDestination (EnterPoint.position);

        }
    }

}
