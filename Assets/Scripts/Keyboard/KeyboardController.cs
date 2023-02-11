using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
  public TMP_InputField _inputField;
  [SerializeField] private bool _isCaps;
  public event Action<bool> OnCaps;

  private void OnEnable()
  {
    _isCaps = false;
  }

  public void InsertChar(string character)
  {
    if (_inputField.text.Length < 10)
    {
      _inputField.text += character;
    }
  }

  public void DeleteChar()
  {
    if (_inputField.text.Length > 0)
    {
      _inputField.text = _inputField.text.Substring(0, _inputField.text.Length - 1);
    }
  }

  public void InsertSpace()
  {
    if (_inputField.text.Length < 10)
    {
      _inputField.text += " ";
    }
  }

  public void CapslockPressed()
  {
    if (!_isCaps)
    {
      _isCaps = true;
      OnCaps?.Invoke(_isCaps);
    }
    else
    {
      _isCaps = false;
      OnCaps?.Invoke(_isCaps);

    }
  }
}
