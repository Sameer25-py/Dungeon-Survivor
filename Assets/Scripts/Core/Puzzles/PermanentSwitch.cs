using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class PermanentSwitch : MonoBehaviour
    {   
        #region Variables

        [SerializeField] protected Gate _gate;
        
        #endregion

        #region UnityLifeCycle
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (_gate) GateOpen?.Invoke(_gate.ID);
            else print("Gate reference not set");
        }
        
        #endregion
    }
}
