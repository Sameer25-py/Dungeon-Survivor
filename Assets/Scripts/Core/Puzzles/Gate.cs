using UnityEngine;
using DungeonSurvivor.Core.ID;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class Gate : MonoBehaviour
    {
        #region Variables

        public int ID { get; private set; }
        public bool IsOpened { get; private set; }

        #endregion
        
        #region EventListeners
        
        private void OnGateOpenCalled(int id)
        {
            if (id != ID) return;
            if (IsOpened) return;
            print($"Gate#{ID} opened");
            IsOpened = true;
        }
        
        private void OnGateCloseCalled(int id)
        {
            if (id != ID) return;
            if (!IsOpened) return;
            print($"Gate#{ID} closed");
            IsOpened = false;
        }
        
        #endregion

        #region UnityLifeCycle

        private void Awake()
        {
            ID = IDManager.AssignGateID();
        }
        
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
