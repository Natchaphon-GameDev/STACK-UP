using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text minText;
    [SerializeField] private TMP_Text secText;
    [SerializeField] private TMP_Text milliSecText;
    [SerializeField] private Material buttonColor;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _stackCups;
    [SerializeField] private TMP_Text _playerScore;
    public static event Action<int> onGameEnded;

    private bool _isLeftHandOn = false;
    private bool _isRightHandOn = false;
    private int _timerStatus = 0; 
    
    // 0 = noting, 1 = TimerBegin, 2 = TimerStop
    public int MIN { get; private set; }
    public int Sec { get; private set; }
    public float MilliSec { get; private set; }

    private void OnEnable()
    {
        buttonColor.color = Color.red;   
        _stackCups.gameObject.SetActive(false);
        ResetTimer();
        _timerStatus = 0;
        
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

    public void SetLeftHandStatus(bool isOn)
    {
        _isLeftHandOn = isOn;
    }
    
    public void SetRightHandStatus(bool isOn)
    {
        _isRightHandOn = isOn;
    }
    
    private bool IsHandOnButton()
    {
        return _isLeftHandOn && _isRightHandOn;
    }

    private void Update()
    {
        if (IsHandOnButton() && _timerStatus == 0 && MilliSec == 0)
        {
            _timerStatus = 1;
        }
        else if (_timerStatus == 1 && !IsHandOnButton())
        { 
            buttonColor.color = Color.green;
            _stackCups.SetActive(true);
            Timer();
        }
        else if (_timerStatus == 1 && IsHandOnButton() && MilliSec > 0 && _gameManager.isFinish)
        {
            buttonColor.color = Color.red;
            _timerStatus = 2;
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

        }
        else if (_timerStatus == 2 && IsHandOnButton())
        {
            _timerStatus = 0;
        }
    }

    private void ResetTimer()
    {
        MIN = 0;
        Sec = 0;
        MilliSec = 0;
    }
}