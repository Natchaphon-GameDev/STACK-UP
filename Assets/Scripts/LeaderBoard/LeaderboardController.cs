using System;
using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using TMPro;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    // public TMP_InputField _memberID;
    [SerializeField] private TMP_InputField stack_playerName;
    [SerializeField] private TMP_InputField typing_playerName;
    [SerializeField] private TMP_InputField wire_playerName;
    private TMP_InputField _playerName;
    public int _playerScore;
    [SerializeField] private int stack_LB_ID = 8501;
    [SerializeField] private int typing_LB_ID = 9090;
    [SerializeField] private int wire_LB_ID = 9248;

    [SerializeField] private int maxScoreList = 10;
    public TMP_Text[] infos_StackCup;
    public TMP_Text[] infos_Typing;
    public TMP_Text[] infos_Wire;
    public TMP_Text[] scores_StackCup;
    public TMP_Text[] scores_Typing;
    public TMP_Text[] scores_Wire;

    [Header("MainLeaderBoard")] 
    public TMP_Text[] infos_Leaderboard;
    public TMP_Text[] scores_Leaderboard;

    [Header("MiniGame")] 
    [SerializeField] private GameObject stack;
    [SerializeField] private GameObject typing;
    [SerializeField] private GameObject wire;

    private void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void ResetPlayerName()
    {
        stack_playerName.text = "";
        typing_playerName.text = "";
        wire_playerName.text = "";
    }
        
    public void SetPlayerName(string game)
    {
        if (game == "Stack")
        {
            _playerName = stack_playerName;
        }
        else if (game == "Typing")
        {
            _playerName = typing_playerName;
        }
        else if (game == "Wire")
        {
            _playerName = wire_playerName;
        }


        LootLockerSDKManager.SetPlayerName(_playerName.text, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully set player Name");
                }
                else
                {
                    Debug.Log("Failed to set player Name" + response.Error);
                }
            }
        );
    }

    private IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
    }

    public void GetLeaderBoard(string game)
    {
        StartCoroutine(FetchTopHighScoreRoutine(game));
    }

    private IEnumerator LoginRoutine()
    {
        var done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("successfully started LootLocker session");
                // PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("error starting LootLocker session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    private IEnumerator SubmitScoreRoutine(string game)
    {
        var done = false;
        var playerID = "Anonymous";
        var id = 0;

        if (game == "Stack")
        {
            id = stack_LB_ID;
            playerID = stack_playerName.text;
        }
        else if (game == "Typing")
        {
            id = typing_LB_ID;
            playerID = typing_playerName.text;
        }
        else if (game == "Wire")
        {
            id = wire_LB_ID;
            playerID = wire_playerName.text;
        }

        LootLockerSDKManager.SubmitScore(playerID, _playerScore, id, (response) =>
        {
            if (response.success)
            {
                Debug.Log($"Player ID {response.member_id}");
                Debug.Log("Success");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile((() => done == false));
    }

    [ContextMenu("Submit Score")]
    private void TestSubmit()
    {
        SubmitScore("Typing");
    }

    public void SubmitScore(string game)
    {
        StartCoroutine(SubmitScoreRoutine(game));
        GetLeaderBoard(game);
    }

    private IEnumerator FetchTopHighScoreRoutine(string game)
    {
        var done = false;
        var id = 0;
        var infos = new TMP_Text[] { };
        var scores = new TMP_Text[] { };

        if (game == "Stack")
        {
           
            id = stack_LB_ID;
            if (stack.activeSelf)
            {
                infos = infos_StackCup;
                scores = scores_StackCup;
            }
            else
            {
                infos = infos_Leaderboard;
                scores = scores_Leaderboard;
            }
        }
        else if (game == "Typing")
        {
            id = typing_LB_ID;
            if (typing.activeSelf)
            {
                infos = infos_Typing;
                scores = scores_Typing;
            }
            else
            {
                infos = infos_Leaderboard;
                scores = scores_Leaderboard;
            }
        }
        else if (game == "Wire")
        {
            id = wire_LB_ID;
            if (wire.activeSelf)
            {
                infos = infos_Wire;
                scores = scores_Wire;
            }
            else
            {
                infos = infos_Leaderboard;
                scores = scores_Leaderboard;
            }
        }

        yield return new WaitForSeconds(1);

        LootLockerSDKManager.GetScoreList(id, maxScoreList, 0, (response) =>
        {
            if (response.success)
            {
                var tempPlayerNames = "Names";
                var tempPlayerScores = "Scores";

                var members = response.items;

                for (var i = 0; i < members.Length; i++)
                {
                    if (members[i].member_id != "")
                    {
                        tempPlayerNames += members[i].member_id;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }

                    infos[i].text = ($"#{members[i].rank} : {members[i].member_id}");
                    scores[i].text = members[i].score.ToString();

                    tempPlayerScores += members[i].score + "";
                    tempPlayerNames += "";
                }

                if (members.Length < maxScoreList)
                {
                    for (var i = members.Length; i < maxScoreList; i++)
                    {
                        infos[i].text = $"#{i + 1} : ...none...";
                    }
                }

                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}