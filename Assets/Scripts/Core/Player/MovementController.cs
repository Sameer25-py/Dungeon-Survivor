using UnityEngine;
using UnityEngine.AI;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player
{
    public class MovementController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private NavMeshAgent agent;

        #endregion

        #region EventListeners

        private void OnMoveToPositionCalled(Vector3 targetPos)
        {
            if (agent is not null)
            {
                agent.SetDestination(targetPos);
            }
        }

        #endregion

        #region UnityLifeCycle

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