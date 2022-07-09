using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;

    private ExhibitionArea _currentRoom;

    private bool _isInRoom = false;
    private bool _isGoingToRoom = false;
    private bool _isGoingToExit = false;

    private List<RoomInfo> roomInfos = new List<RoomInfo>();
    private int _currentRoomIndex = -1;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        ExhibitionArea[] ars = OpenMuzeumManager.Instance.Areas;
        for (int i = 0; i < ars.Length; i++)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.Area = ars[i];
            roomInfo.Done = false;
            roomInfos.Add(roomInfo);
        }
    }

    void Update()
    {
        Vector3 delta = Agent.destination - transform.position;

        if (delta.sqrMagnitude < 0.08f)
        {
            if (_isGoingToExit)
            {
                Destroy(gameObject);
            }
            if (_isGoingToRoom && !_isInRoom)
            {
                _isGoingToRoom=false;
                _isInRoom = true;
            }
            if (!_isGoingToRoom && _isInRoom)
            {
                NextExhibit();
            }
            else
            {
                if (SetNextRoom())
                {
                    ExitMuzeum ();
                }
            }
        }
    }

    private void NextExhibit ()
    {
        //TODO go exhibits

        _isInRoom = false;

        Agent.SetDestination(_currentRoom.exit.position);
    }

    /// <returns>if <see langword="true"/> exit muzeum</returns>
    private bool SetNextRoom()
    {
        if (_currentRoomIndex >= 0 && _currentRoomIndex < roomInfos.Count)
        {
            roomInfos[_currentRoomIndex].Done = true;
        }

        _currentRoomIndex++;

        if (_currentRoomIndex >= roomInfos.Count)
        {
            return true;
        }

        _currentRoom = roomInfos[_currentRoomIndex].Area;
        Agent.SetDestination(_currentRoom.entrance.position);
        _isGoingToRoom = true;
        return false;
    }

    public void SetDestination(Vector3 pos)
    {
        Agent.SetDestination(pos);
    }

    private void ExitMuzeum()
    {
        _isGoingToExit = true;
        Agent.SetDestination(OpenMuzeumManager.Instance.ExitPoint.position);
    }
}

public class RoomInfo
{
    public ExhibitionArea Area;
    public bool Done;
}