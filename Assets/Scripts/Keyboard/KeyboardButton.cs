using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using OculusSampleFramework;
using TMPro;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
   private KeyboardController _keyboardController;
   private TMP_Text _buttonText;

   private void Start()
   {
      _keyboardController = GetComponentInParent<KeyboardController>();
      _buttonText = GetComponent<TMP_Text>();
      
      _keyboardController.OnCaps += ToUpperText;
      
      if (_buttonText.text.Length == 1)
      {
         NameToButtonText();
         GetComponentInParent<InteractableUnityEventWrapper>().WhenSelect.AddListener(delegate { _keyboardController.InsertChar(_buttonText.text);});
      }
   }

   private void NameToButtonText()
   {
      _buttonText.text = transform.parent.parent.parent.name;
   }

   private void ToUpperText(bool isCaps)
   {
      if (isCaps)
      {
         if (_buttonText.text.Length == 1)
         {
            _buttonText.text = _buttonText.text.ToUpper();
         }
      }
      else
      {
         if (_buttonText.text.Length == 1)
         {
            _buttonText.text = _buttonText.text.ToLower();
         }
      }
   }
}
