using UnityEngine;
using UnityEngine.AI;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player.Movement
{
    public class MovementController : MonoBehaviour
    {
        #region Variables

        public NavMeshAgent Agent { get; private set; }

        #endregion

        #region EventListeners

        private void OnMoveToPositionCalled(Vector3 targetPos)
        {
            if (Agent is not null)
            {
                Agent.SetDestination(targetPos);
            }
        }

        #endregion

        #region UnityLifeCycle
        
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            MoveToPosition.AddListener(OnMoveToPositionCalled);
        }

        private void OnDisable()
        {
            MoveToPosition.AddListener(OnMoveToPositionCalled);
        }

        #endregion
    }
}