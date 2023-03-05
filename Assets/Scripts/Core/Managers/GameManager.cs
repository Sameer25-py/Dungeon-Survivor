using System;
using DungeonSurvivor.Core.Events;
using DungeonSurvivor.Core.Puzzles.MiniGames.PatternMatch;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;
using static DungeonSurvivor.Core.Events.GameplayEvents.MiniGames;

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

            SwitchToPlayerFollowCamera?.Invoke();
        }

        private void OnSwitchToMiniGameCameraCalled()
        {
            Invoke(nameof(InstantiateMiniGame), 2f);
        }

        private void OnMiniGameCompleted()
        {
            Invoke(nameof(DestroyMiniGame), 1f);
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
    }
}