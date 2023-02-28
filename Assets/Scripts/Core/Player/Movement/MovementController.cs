using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player.Movement
{
    public class MovementController : MonoBehaviour
    {
        #region Variables

        public NavMeshAgent Agent { get; private set; }
         public Inventory inventory;
         public Image uibox;
        [SerializeField] private NavMeshAgent agent;

        #endregion

        #region EventListeners
        private  void OnCollisionEnter(Collision other)
        {
            IInventoryItem item=other.gameObject.GetComponent<IInventoryItem>();
            if(item !=null)
            {
                print(" item collider");
                inventory.AddItem(item);
            }
            if(other.gameObject.tag=="gem"){
                print("got you");
               // uibox.GetComponent<enabled>=true;
               uibox.sprite=other.gameObject.GetComponent<Image>().sprite;
               other.gameObject.SetActive(false);

            }

        }
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