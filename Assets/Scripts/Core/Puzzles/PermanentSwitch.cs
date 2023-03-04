using System;
using System.Collections.Generic;
using DungeonSurvivor.Controllers.Animations.Switch;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class PermanentSwitch : MonoBehaviour
    {
        #region Variables

        [SerializeField] protected Gate _gate;

        protected SwitchAnimator switchAnimator;

        protected bool isSwitchPressed;

        protected readonly List<string> ValidTags = new() { "Player", "Pushable" };

        #endregion

        #region UnityLifeCycle

        private void OnEnable()
        {
            switchAnimator = GetComponent<SwitchAnimator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!ValidTags.Contains(other.tag)) return;
            if (_gate && !isSwitchPressed)
            {
                isSwitchPressed = true;
                switchAnimator.PressButton();
                GateOpen?.Invoke(_gate.ID);
            }
            else print("Gate reference not set");
        }

        #endregion
    }
}