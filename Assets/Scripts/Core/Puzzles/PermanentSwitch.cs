using System;
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
        
        #endregion

        #region UnityLifeCycle

        private void OnEnable()
        {
            switchAnimator = GetComponent<SwitchAnimator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
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
