using DungeonSurvivor.Core.GridFunctionality;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player
{
    public class GridMovement : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("up"))
            {
             MoveInDirection?.Invoke(Direction.Up);   
            }
            else if (UnityEngine.Input.GetKeyDown("down")) { }
            else if (UnityEngine.Input.GetKeyDown("left")) { }
            else if (UnityEngine.Input.GetKeyDown("right")) { }
        }
    }
}