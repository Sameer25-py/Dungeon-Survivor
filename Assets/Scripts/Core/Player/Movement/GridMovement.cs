using UnityEngine;
using DungeonSurvivor.Core.ID;
using DungeonSurvivor.Core.GridFunctionality;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player.Movement
{
    public abstract class GridMovement : MonoBehaviour
    {
        [SerializeField] protected int ID;
        [SerializeField] protected Vector2Int currentIndex;
        [SerializeField] protected float moveTime;
        [SerializeField] protected float height;
        [SerializeField] protected float exitTime;
        
        protected Vector3 targetPosition;
        protected float timeSinceInput;

        protected virtual void Update()
        {
            if (timeSinceInput < exitTime) timeSinceInput += Time.deltaTime;
        }
        
        private void OnMoveInDirectionCalled(int id, Vector2Int direction)
        {
            if (id != ID) return;
            var block = GridManager.Instance.GetBlock(currentIndex + direction);

            if (!block) return;
            if (block.type is BlockType.None) return;
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f) return;
            Move(block);
            timeSinceInput = 0;
        }

        private void Move(Block block)
        {
            targetPosition = block.transform.position;
            targetPosition.y = height;
            currentIndex = block.index;
            Animate();
        }

        protected virtual void Animate()
        {
            transform.LookAt(targetPosition);
            LeanTween.move(gameObject, targetPosition, moveTime).setEaseInSine();
        }

        private void OnEnable()
        {
            MoveInDirection.AddListener(OnMoveInDirectionCalled);
            targetPosition = transform.position;
        }

        private void OnDisable()
        {
            MoveInDirection.RemoveListener(OnMoveInDirectionCalled);
        }
    }
    
    public abstract class AIMovement : GridMovement
    {
        private void Start()
        {
            ID = IDManager.AssignEnemyID();
        }
    }
}