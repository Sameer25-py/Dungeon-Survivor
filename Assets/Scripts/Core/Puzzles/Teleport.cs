using UnityEngine;
using DungeonSurvivor.Core.Player;
using static DungeonSurvivor.Core.Events.GameplayEvents.Puzzles;

namespace DungeonSurvivor.Core.Puzzles
{
    public class Teleport : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Color _color;
        [SerializeField] private Vector3 _offset;

        public Color Color { get; private set; }
        public GameObject Object { get; private set; }
        public float DirectionEntered { get; private set; }

        #endregion
        
        #region EventListeners

        private void TeleportToThis(Teleport teleport)
        {
            if (teleport == this || Color != teleport.Color) return;

            GetComponent<Collider>().enabled = false;

            if (teleport.Object.TryGetComponent<MovementController>(out var player))
            {
                var target = transform.position + _offset;
                teleport.Object.transform.position = target;
                player.Agent.SetDestination(target);
            }
            // else if (teleport.Object.TryGetComponent<Box>(out var box))
            // else if (teleport.Object.TryGetComponent<Box>(out var boulder))
        }

        #endregion

        #region UnityLifeCycle

        private void Awake()
        {
            Color = _color;
        }

        private void OnEnable()
        {
            TeleportPosition.AddListener(TeleportToThis);
        }

        private void OnDisable()
        {
            TeleportPosition.RemoveListener(TeleportToThis);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.CompareTag("Player") || other.CompareTag("Obstacle"))) return;
            Object = other.gameObject;
            DirectionEntered = Vector3.Angle(transform.position, other.transform.position);
            TeleportPosition?.Invoke(this);
        }
        
        #endregion
    }
}
