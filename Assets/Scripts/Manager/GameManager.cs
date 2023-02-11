using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public bool isFinish = false;
    [Header("Stack")]
    [SerializeField] private List<StackCupBluePrint> stackCups;
    [SerializeField] private GameObject keyboard;
    [SerializeField] private LeaderboardController _leaderboardController;

    private void Awake()
    {
        foreach (var cup in stackCups)
        {
            cup.stackCup.onCupInArea += CupCheckList;
        }
        
        TimerController.onGameEnded += ShowLBStack;
    }

    private void ShowLBStack(int score)
    {
        SetPlayerScore(score);
        _leaderboardController.GetLeaderBoard("Stack");
        keyboard.SetActive(true);
    }

    private void SetPlayerScore(int score)
    {
        _leaderboardController._playerScore = score;
    }
    private void GoalCheck()
    {
        var _cupAmountCheck = 0;
        foreach (var stackCup in stackCups)
        {
            if (!stackCup.isOn)
            {
                return;
            }
            else
            {
                _cupAmountCheck++;
            }
        }

        if (_cupAmountCheck == stackCups.Count)
        {
            isFinish = true;
        }
    }

    private void CupCheckList(StackArea cup, bool isInArea)
    {
        if (isInArea)
        {
            foreach (var stackCup in stackCups)
            {
                if (stackCup.stackCup == cup)
                {
                    stackCup.isOn = true;
                }
            }

            Debug.Log(cup.gameObject.name + "In the Area");
        }
        else
        {
            //_cupAmountCheck -= 1;
            foreach (var stackCup in stackCups)
            {
                if (stackCup.stackCup == cup)
                {
                    stackCup.isOn = false;
                }
            }

            Debug.Log(cup.gameObject.name + "Out of Area");
        }

        GoalCheck();
    }
}