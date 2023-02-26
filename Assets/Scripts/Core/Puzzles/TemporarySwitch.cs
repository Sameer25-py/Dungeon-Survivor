using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class TemporarySwitch : PermanentSwitch
    {
        #region UnityLifeCycle

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (_gate && isSwitchPressed)
            {
                isSwitchPressed = false;
                switchAnimator.ReleaseButton();
                GateClose?.Invoke(_gate.ID);
            }
            else print("Gate reference not set");
        }

        #endregion
    }
}