using UnityEngine;
using DungeonSurvivor.Core.ID;
using DungeonSurvivor.Core.GridFunctionality;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player.Movement
{
    public abstract class GridMovement : MonoBehaviour
    {
        [SerializeField] protected int        ID;
        [SerializeField] protected Vector2Int currentIndex;
        [SerializeField] protected float      moveTime;
        [SerializeField] protected float      height;
        [SerializeField] protected float      exitTime;

        protected Vector3 targetPosition;
        protected float   timeSinceInput;

        protected virtual void Update()
        {
            if (timeSinceInput < exitTime) timeSinceInput += Time.deltaTime;
        }

        private void OnMoveInDirectionCalled(int id, Vector2Int direction)
        {
            if (id != ID) return;
            var block = GridManager.Instance.GetBlock(currentIndex + direction);
            if (!block || block.type is not BlockType.Standing) return;
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f) return;
            Move(block, direction);
            timeSinceInput = 0;
        }

        protected virtual void Move(Block block, Vector2Int direction, float time = 0f)
        {
            targetPosition   = block.transform.position;
            targetPosition.y = height;
            currentIndex     = block.index;
            Animate(time);
        }

        protected virtual void Animate(float time)
        {
            if (time == 0f)
            {
                time = moveTime;
            }

            transform.LookAt(targetPosition);
            LeanTween.move(gameObject, targetPosition, time)
                .setEaseInSine();
        }

        private void OnEnable()
        {
            MoveInDirection.AddListener(OnMoveInDirectionCalled);
            targetPosition = transform.position;
            currentIndex.x = (int)targetPosition.x;
            currentIndex.y = (int)targetPosition.z;
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