using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
namespace DungeonSurvivor
{

    public class Canvas : MonoBehaviour
    {

        #region UIbutton
        public GameObject OnclickPop;
        public TextMeshProUGUI playerNames;
        public TextMeshProUGUI playerScores;
        public TextMeshProUGUI CurrentplayerScores;
        public TextMeshProUGUI CurrentLevel;



        #endregion

        #region Private/Helper Functions
        #endregion

        #region Public Functions
        #endregion

        #region EventListeners
        #endregion

        #region UnityLifeCycle
        public void OnClick()
        {
            int leaderboardKey = 12117;
            int count = 10;
            int Level = 1;
            int Your_Score = 1000;
            int pid = PlayerPrefs.GetInt("pid");
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                   
                    return;
                }
                else
                {
                    print(pid);

                    LootLockerSDKManager.SubmitScore(pid.ToString(), Your_Score, leaderboardKey, (response) =>
                    {
                        if (response.statusCode == 200)
                        {
                            Debug.Log("Successful");
                        }
                        else
                        {
                            Debug.Log("failed: " + response.Error);
                        }
                    });


                    LootLockerSDKManager.GetScoreList(leaderboardKey, count, 0, (response) =>
                    {
                        if (response.statusCode == 200)
                        {
                            Debug.Log("Successful");
                            Debug.Log(response.ToString());
                            string tempPlayerNames = " Names\n";
                            string tempPlayerScores = "Scores\n";
                            LootLockerLeaderboardMember[] members = response.items;
                            for(int i = 0; i < members.Length; i++)
                            {
                                tempPlayerNames += "#"+members[i].rank + " ";
                                if(members[i].player.name != "")
                                {
                                    tempPlayerNames += members[i].player.name;

                                }
                                else
                                {
                                    tempPlayerNames += members[i].player.id;
                                }
                                tempPlayerScores += members[i].score + "\n";
                                tempPlayerNames += "\n";
                            }
                            playerNames.text = tempPlayerNames;
                            playerScores.text = tempPlayerScores;
                            CurrentLevel.text = Level.ToString();
                            CurrentplayerScores.text = Your_Score.ToString();
                        }
                        else
                        {
                            Debug.Log("failed: " + response.Error);
                        }
                    });
                    OnclickPop.SetActive(true);
                    Debug.Log("session started successfully");
                  
                }
            });
            
        }
    #endregion
    }
}
