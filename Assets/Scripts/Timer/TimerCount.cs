using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerCount : MonoBehaviour
{
    [SerializeField] private TMP_Text minText;
    [SerializeField] private TMP_Text secText;
    [SerializeField] private TMP_Text milliSecText;
    [SerializeField] private GameObject _wireStick;
    [SerializeField] private TMP_Text _playerScore;
    private bool isButtonPressed = false;
    private bool isCalculateScore = false;
    public static event Action<int> onGameEnded;

    // 0 = noting, 1 = TimerBegin, 2 = TimerStop
    public int MIN { get; private set; }
    public int Sec { get; private set; }
    public float MilliSec { get; private set; }

    private void OnEnable()
    {
        ResetTimer();
        _wireStick.gameObject.SetActive(false);
        isButtonPressed = false;
        isCalculateScore = false;
    }

    private void Timer()
    {
        //Timer system
        MilliSec += (Time.deltaTime * 10);
        milliSecText.text = $"{MilliSec:F0}";
        if (MilliSec >= 9)
        {
            MilliSec = 0;
            Sec++;
        }

        if (Sec < 10)
        {
            secText.text = $"0{Sec}:";
        }
        else if (Sec >= 10 && Sec <= 59)
        {
            secText.text = $"{Sec}:";
        }
        else if (Sec == 60)
        {
            Sec = 0;
            MIN++;
        }

        if (MIN < 10)
        {
            minText.text = $"0{MIN}:";
        }
        else if (MIN >= 10 && MIN <= 59)
        {
            minText.text = $"{MIN}:";
        }
    }
    
    private void Update()
    {
        if (CheckPointSystem.isFinlished && !isCalculateScore)
        {
            CalculateScore();
        }
        else if (!CheckPointSystem.isFinlished && isButtonPressed)
        {
            Timer();
        }
    }

    public void StartGame()
    {
        isButtonPressed = true;
    }

    private void CalculateScore()
    {
        var scoreTemp = 0;

        if (MilliSec >= 0.5f)
        {
            if (MIN == 1)
            {
                scoreTemp += 60;
            }
            else if (MIN == 2)
            {
                scoreTemp += 120;
            }
            else if (MIN >= 3)
            {
                scoreTemp += 180;
            }

            scoreTemp += Sec;
            onGameEnded?.Invoke(scoreTemp);
            _playerScore.text = $"Score: {scoreTemp}";
        }
        else
        {
            if (MIN == 1)
            {
                scoreTemp += 60;
            }
            else if (MIN == 2)
            {
                scoreTemp += 120;
            }
            else if (MIN >= 3)
            {
                scoreTemp += 180;
            }
            scoreTemp += Sec;
            onGameEnded?.Invoke(scoreTemp++);
            _playerScore.text = $"Score: {scoreTemp++}";
        }

        isCalculateScore = true;
    }
    
    private void ResetTimer()
    {
        MIN = 0;
        Sec = 0;
        MilliSec = 0;
    }
}