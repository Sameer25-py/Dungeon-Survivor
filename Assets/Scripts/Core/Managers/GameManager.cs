using System;
using System.Collections;
using DungeonSurvivor.Analytics.Player;
using DungeonSurvivor.Core.Events;
using DungeonSurvivor.Core.Puzzles.MiniGames.PatternMatch;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;
using static DungeonSurvivor.Core.Events.GameplayEvents.MiniGames;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Managers
{
    public class GameManager : SingletonDontDestroy<GameManager>
    {
        [SerializeField] private Gameplay miniGame;

        private Gameplay _instanitatedMiniGame = null;

        private void InstantiateMiniGame()
        {
            if (_instanitatedMiniGame != null)
            {
                DestroyMiniGame();
            }

            _instanitatedMiniGame = Instantiate(miniGame);
        }

        private void DestroyMiniGame()
        {
            if (_instanitatedMiniGame == null) return;
            Destroy(_instanitatedMiniGame.gameObject);
            _instanitatedMiniGame = null;

            SwitchToEndLevelCamera?.Invoke();
        }

        private void OnSwitchToMiniGameCameraCalled()
        {
            Invoke(nameof(InstantiateMiniGame), 2f);
        }

        private void OnMiniGameCompleted()
        {
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {   
                int leaderboardKey = 12117;
                int pid            = PlayerPrefs.GetInt("pid");
                if (!response.success)
                {
                    return;
                }
                else
                {
                    print(pid);

                    LootLockerSDKManager.SubmitScore(pid.ToString(), PlayerDataHandler.PlayerMoveCountPerStage, leaderboardKey, (response) =>
                    {
                        if (response.statusCode == 200)
                        {
                            Debug.Log("Successful");
                        }
                        else
                        {
                            Debug.Log("failed: " + response.Error);
                        }
                        
                        PlayerDataHandler.ResetPlayerMoveCountPerStage();
                    });
                }
            });
            
            Invoke(nameof(DestroyMiniGame), 1f);
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
            MiniGameCompleted.AddListener(OnMiniGameCompleted);
        }

        private void OnDisable()
        {
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
            MiniGameCompleted.RemoveListener(OnMiniGameCompleted);
        }

        private IEnumerator BroadcastSceneLoadProgress(AsyncOperation sceneLoadOp)
        {
            while (sceneLoadOp.progress < 0.9f)
            {
                yield return new WaitForEndOfFrame();
                SceneLoaderProgress?.Invoke(sceneLoadOp.progress);
            }

            yield return new WaitForSeconds(1f);

            sceneLoadOp.allowSceneActivation = true;
            yield return new WaitUntil(() => sceneLoadOp.isDone);

            SceneLoaderProgress?.Invoke(1f);
            yield return new WaitForSeconds(0.1f);
            HideSceneLoader?.Invoke();
        }

        public void LoadScene(string sceneName)
        {
            AsyncOperation sceneLoadOp = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadOp.allowSceneActivation = false;
            ShowSceneLoader?.Invoke();
            SceneLoaderProgress?.Invoke(0.5f);
            StartCoroutine(BroadcastSceneLoadProgress(sceneLoadOp));
        }
    }
}