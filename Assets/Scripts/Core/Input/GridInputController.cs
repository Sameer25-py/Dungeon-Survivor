using UnityEngine;
using DungeonSurvivor.Core.GridFunctionality;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Input
{
    public class GridInputController : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("up"))
            {
                MoveInDirection?.Invoke(Direction.Down);   
            }
            else if (UnityEngine.Input.GetKeyDown("down"))
            {
                MoveInDirection?.Invoke(Direction.Up);  
            }
            else if (UnityEngine.Input.GetKeyDown("left"))
            {
                MoveInDirection?.Invoke(Direction.Left);  
            }
            else if (UnityEngine.Input.GetKeyDown("right"))
            {
                MoveInDirection?.Invoke(Direction.Right);
            }
        }
    }
}