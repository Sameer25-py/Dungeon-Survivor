using DungeonSurvivor.Controllers.Animations.Door;
using DungeonSurvivor.Controllers.Animations.Switch;
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
        
        public Color  Color  { get; private set; }
        public bool IsOpened { get; private set; }

        [SerializeField] private Renderer gemRenderer;
        [SerializeField] private SwitchAnimator switchAnimator;

        private DoorAnimator _doorAnimator;

        private Vector2Int gridIndex;
        private static readonly int baseColor = Shader.PropertyToID("_BaseColor");
        #endregion

        #region EventListeners

        private void OnGateOpenCalled(Color color)
        {
            if (color != Color) return;
            if (IsOpened) return;
            _doorAnimator.OpenDoor();
            IsOpened = true;
            ChangeBlockType?.Invoke(gridIndex, BlockType.Standing);
        }

        private void OnGateCloseCalled(Color color)
        {
            if (color != Color) return;
            if (!IsOpened) return;
            _doorAnimator.CloseDoor();
            IsOpened = false;
            ChangeBlockType?.Invoke(gridIndex, BlockType.Wall);
        }

        #endregion

        #region UnityLifeCycle

        private void Start()
        {
            // ID = IDManager.AssignGateID();
            Vector3 parentPosition = transform.parent.position;
            gridIndex = new Vector2Int((int)parentPosition.x, (int)parentPosition.z);
        }

        private void OnEnable()
        {
            Color = switchAnimator.GetColor;
            var mpb = new MaterialPropertyBlock();
            gemRenderer.GetPropertyBlock(mpb);
            mpb.SetColor(baseColor, Color);
            gemRenderer.SetPropertyBlock(mpb);
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