using DungeonSurvivor.Controllers.Animations.Door;
using UnityEngine;
using DungeonSurvivor.Core.ID;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class Gate : MonoBehaviour
    {
        #region Variables

        public int  ID       { get; private set; }
        public bool IsOpened { get; private set; }

        private DoorAnimator _doorAnimator;

        #endregion

        #region EventListeners

        private void OnGateOpenCalled(int id)
        {
            if (id != ID) return;
            if (IsOpened) return;
            _doorAnimator.OpenDoor();
            IsOpened = true;
        }

        private void OnGateCloseCalled(int id)
        {
            if (id != ID) return;
            if (!IsOpened) return;
            _doorAnimator.CloseDoor();
            IsOpened = false;
        }

        #endregion

        #region UnityLifeCycle

        private void Start()
        {
            ID = IDManager.AssignGateID();
        }

        private void OnEnable()
        {
            GateOpen.AddListener(OnGateOpenCalled);
            GateClose.AddListener(OnGateCloseCalled);
            _doorAnimator = GetComponent<DoorAnimator>();
        }

        private void OnDisable()
        {
            GateOpen.RemoveListener(OnGateOpenCalled);
            GateClose.RemoveListener(OnGateCloseCalled);
        }

        #endregion
    }
}