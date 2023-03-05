using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class TemporarySwitch : PermanentSwitch
    {
        #region UnityLifeCycle

        private void OnTriggerExit(Collider other)
        {
            if (!ValidTags.Contains(other.tag)) return;
            if (_gate && isSwitchPressed)
            {
                isSwitchPressed = false;
                switchAnimator.ReleaseButton();
                GateClose?.Invoke(switchAnimator.GetColor);
            }
            else print("Gate reference not set");
        }

        #endregion
    }
}