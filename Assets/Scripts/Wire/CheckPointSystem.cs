using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pipe
{
    public List<GameObject> pipes;
}

[Serializable]
public class CheckPointSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _waypoints;
    [SerializeField] private List<Pipe> pipeGroup;
    [SerializeField] private int _waypointCount = 0;
    [SerializeField] private GameObject keyboard;
    [SerializeField] private LeaderboardController _leaderboardController;

    public static bool isFinlished = false;

    private void Awake()
    {
        TimerCount.onGameEnded += ShowLBStack;
    }

    private void ShowLBStack(int score)
{
    SetPlayerScore(score);
    _leaderboardController.GetLeaderBoard("Wire");
    keyboard.SetActive(true);
}

private void SetPlayerScore(int score)
{
    _leaderboardController._playerScore = score;
}

    private void OnEnable()
    {
        ResetWayPoint();
        isFinlished = false;
    }

    public void CheckWaypoint(GameObject other)
    {
        if (_waypointCount <= 6)
        {
            if (other.gameObject == _waypoints[_waypointCount])
            {
                for (var i = 0; i < pipeGroup.Count; i++)
                {
                    foreach (var pipe in pipeGroup[i].pipes)
                    {
                        pipe.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                
                    if (i == _waypointCount)
                    {
                        break;
                    }
                }
                _waypointCount++;
            }

            if (_waypointCount == 7)
            {
                //EndGame
                isFinlished = true;
            }
        }
    }

    public void ResetWayPoint()
    {
        _waypointCount = 0;
        foreach (var pipes in pipeGroup)
        {
            foreach (var pipe in pipes.pipes)
            {
                pipe.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }
}