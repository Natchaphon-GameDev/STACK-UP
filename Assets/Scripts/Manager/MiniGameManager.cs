using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject Stack;
    [SerializeField] private GameObject Typing;
    [SerializeField] private GameObject Wire;
    [SerializeField] private GameObject Menu;
    [SerializeField] private LeaderboardController _leaderboardController;
    [SerializeField] private List<GameObject> keyboards;
    private void Start()
    {
        Stack.SetActive(false);
        Typing.SetActive(false);
        Wire.SetActive(false);
        Menu.SetActive(true);
    }

    private void SetScene(int mode)
    {
        Stack.SetActive(false);
        Typing.SetActive(false);
        Wire.SetActive(false);
        Menu.SetActive(false);

        if (mode == 0)
        {
            Stack.SetActive(true);
        }
        else if (mode == 1)
        {
            Typing.SetActive(true);
        }
        else
        {
            Wire.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (MenuSystem.mode == 0)
        {
            SetScene(0);
        }
        else if (MenuSystem.mode == 1)
        {
            SetScene(1);
        }
        else
        {
            SetScene(2);
        }
    }
    
    public void BackToMenu()
    {
        StartCoroutine(WaitForBackToMenu());
    }

    private IEnumerator WaitForBackToMenu()
    {
        yield return new WaitForSeconds(3);
        _leaderboardController.ResetPlayerName();
        foreach (var keyboard in keyboards)
        {
            keyboard.gameObject.SetActive(false);
        }
        Stack.SetActive(false);
        Typing.SetActive(false);
        Wire.SetActive(false);
        Menu.SetActive(true);
    }

    private GameObject GetActiveScene()
    {
        if (Stack.gameObject.activeSelf)
        {
            return Stack;
        }

        if(Typing.gameObject.activeSelf)
        {
            return Typing;
        }

        return Menu;
    }

    // public void GetScene(GameObject scene)
    // { 
    //     StartCoroutine(ChangeScene(GetActiveScene(),scene));
    // }
    //
    // private IEnumerator ChangeScene(GameObject oldScene, GameObject newScene)
    // {
    //     newScene.gameObject.SetActive(true);
    //
    //     yield return new WaitForSeconds(0.5f);
    //     
    //     oldScene.gameObject.SetActive(false);
    //
    // }
}
