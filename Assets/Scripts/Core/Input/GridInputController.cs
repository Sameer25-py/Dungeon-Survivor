using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Input
{
    public class GridInputController : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKey("up"))
            {
                MoveInDirection?.Invoke(0, Vector2Int.up);   
            }
            else if (UnityEngine.Input.GetKey("down"))
            {
                MoveInDirection?.Invoke(0, Vector2Int.down);  
            }
            else if (UnityEngine.Input.GetKey("left"))
            {
                MoveInDirection?.Invoke(0, Vector2Int.left);  
            }
            else if (UnityEngine.Input.GetKey("right"))
            {
                MoveInDirection?.Invoke(0, Vector2Int.right);
            }
        }
    }
}