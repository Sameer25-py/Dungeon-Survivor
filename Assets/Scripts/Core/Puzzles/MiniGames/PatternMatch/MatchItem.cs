using System;
using UnityEngine;
using UnityEngine.UI;
using static DungeonSurvivor.Core.Events.GameplayEvents.MiniGames.PatternMatch;

namespace DungeonSurvivor.Core.Puzzles.MiniGames.PatternMatch
{
    public class MatchItem : MonoBehaviour
    {
        public  int  ID;
        private Button _button;

        private void OnClick()
        {
            MatchItemClicked?.Invoke(ID);
        }

        private void OnEnable()
        {   
            if(TryGetComponent(out Button _button))
            {
                _button.onClick.AddListener(OnClick);
            }
        }

        private void OnDisable()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
            }
            
        }
    }
}