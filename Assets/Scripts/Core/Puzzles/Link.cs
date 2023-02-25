using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class Link : MonoBehaviour
    {   
        #region Variables

        [SerializeField] private Gate _gate;
        
        #endregion
    
        #region Private/Helper Functions
        #endregion
        
        #region EventListeners
        
        private void OnGateOpenCalled(int id)
        {
            if (!_gate) { print("Gate reference not set"); return; }
            if (id != _gate.ID) return;
            if (_gate.IsOpened) return;
            print($"Link for Gate#{_gate.ID} colored");
        }
        
        private void OnGateCloseCalled(int id)
        {
            if (!_gate) { print("Gate reference not set"); return; }
            if (id != _gate.ID) return;
            if (!_gate.IsOpened) return;
            print($"Link for Gate#{_gate.ID} uncolored");
        }
        
        #endregion
    
        #region UnityLifeCycle
        
        private void OnEnable()
        {
            GateOpen.AddListener(OnGateOpenCalled);
            GateClose.AddListener(OnGateCloseCalled);
        }
        
        private void OnDisable()
        {
            GateOpen.RemoveListener(OnGateOpenCalled);
            GateClose.RemoveListener(OnGateCloseCalled);
        }

        #endregion
    }
}
