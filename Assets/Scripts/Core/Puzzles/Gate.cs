using DungeonSurvivor.Controllers.Animations.Door;
using DungeonSurvivor.Core.GridFunctionality;
using UnityEngine;
using DungeonSurvivor.Core.ID;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Puzzles
{
    public class Gate : MonoBehaviour
    {
        #region Variables

        public int  ID       { get; private set; }
        public bool IsOpened { get; private set; }

        private DoorAnimator _doorAnimator;

        private Vector2Int gridIndex;

        #endregion

        #region EventListeners

        private void OnGateOpenCalled(int id)
        {
            if (id != ID) return;
            if (IsOpened) return;
            _doorAnimator.OpenDoor();
            IsOpened = true;
            ChangeBlockType?.Invoke(gridIndex, BlockType.Standing);
        }

        private void OnGateCloseCalled(int id)
        {
            if (id != ID) return;
            if (!IsOpened) return;
            _doorAnimator.CloseDoor();
            IsOpened = false;
            ChangeBlockType?.Invoke(gridIndex, BlockType.Wall);
        }

        #endregion

        #region UnityLifeCycle

        private void Start()
        {
            ID = IDManager.AssignGateID();
            Vector3 parentPosition = transform.parent.position;
            gridIndex = new Vector2Int((int)parentPosition.x, (int)parentPosition.z);
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