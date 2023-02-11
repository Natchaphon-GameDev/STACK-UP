using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharGenerate : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _pointText;
    [SerializeField] private TMP_Text _playerPointText;
    [SerializeField] private int _point = 0;
    [SerializeField] private ProgressBar progressBar;
    [Header("Typing")] 
    [SerializeField] private GameObject _leaderboardUI;
    [SerializeField] private GameObject _presentUI;
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _enterKey;
    [SerializeField] private LeaderboardController _leaderboardController;
    [SerializeField] private TMP_InputField _inputName;
    [SerializeField] private KeyboardController _keyboard;

    private List<string> words = new List<string>()
            {
                "Dog",
                "Cat",
                "Need",
                "Fish",
                "Pig",
                "Boy",
                "Shop",
                "Town",
                "Car",
                "Pen",
                "Movie",
                "Wood",
                "City",
                "Guitar",
                "Girl",
                "Meal",
                "Cookie",
                "King",
                "Potato",
                "Ear",
                "Home",
                "Size",
                "Key",
                "Answer",
                "Picture",
                "Image",
                "Man",
                "Sun",
                "Bus",
                "Item",
                "Room",
                "Bottom",
                "Ice",
                "Exit",
                "Button",
                "Rain",
                "Text",
                "Bowl",
                "Campaign",
                "Park",
                "Fruit",
                "Red",
                "Vegetable",
                "Kitchen",
                "Sea",
                "Blue",
                "Dream",
                "Doctor",
                "Bat",
                "Telephone"
            };
    
    private void OnEnable()
    {
        _point = 0;
        _keyboard._inputField = _inputField;
        _inputField.text = "";
        _enterKey.SetActive(false);
        StartCoroutine(CountToBegin());
        
    }

    private IEnumerator CountToBegin()
    {
        _text.text = "3";
        yield return new WaitForSeconds(1);
        _text.text = "2";
        yield return new WaitForSeconds(1);
        _text.text = "1";
        yield return new WaitForSeconds(1);
        GenerateWord();
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        progressBar.currentPercent = 100; // set current percent
        progressBar.speed = 2; // set speed
        progressBar.invert = true; // 100 to 0
        progressBar.restart = false; // restart when it's 100
        progressBar.isOn = true; // enable or disable counting
    }
    
    public void OnGameEnd(float value)
    {
        if (value <= 0)
        {
            ShowLBTyping(_point);
            _playerPointText.text = $"~{_point}~";
            _keyboard._inputField = _inputName;
            _gameplayUI.SetActive(false);
            _enterKey.SetActive(true);
            _leaderboardUI.SetActive(true);
            _presentUI.SetActive(true);
        }
    }
    
    private void SetPlayerScore(int score)
    {
        _leaderboardController._playerScore = score;
    }
    
    private void ShowLBTyping(int score)
    {
        SetPlayerScore(score);
        _leaderboardController.GetLeaderBoard("Typing");
    }

    private void GenerateWord()
    {
        var rand = Random.Range(0, words.Count);
        _text.text = words[rand];
    }
    
    public void OnTyping(string value)
    {
        if (value == _text.text)
        {
            GenerateWord();
            _inputField.text = "";
            _point += 10;
            _pointText.text = $"Point : {_point}";
        }
    }
}
