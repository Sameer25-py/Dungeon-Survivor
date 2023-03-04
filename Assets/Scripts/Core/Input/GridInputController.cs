using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Input
{
    public class GridInputController : MonoBehaviour
    {
        private const string horizontal = "Horizontal";
        private const string vertical = "Vertical";

        private float h_inp, v_inp;
        
        private void Update()
        {
            h_inp = SimpleInput.GetAxis(horizontal);
            v_inp = SimpleInput.GetAxis(vertical);
            
            if (h_inp == 0f && v_inp == 0f) return;

            print($"getting input x:{h_inp}, y:{v_inp}");
            if (Mathf.Abs(h_inp) >= Mathf.Abs(v_inp))
            {
                MoveInDirection?.Invoke(0, h_inp > 0 ? Vector2Int.right : Vector2Int.left);
            }
            else
            {
                MoveInDirection?.Invoke(0, v_inp > 0 ? Vector2Int.up : Vector2Int.down);
            }
            
            // MoveInDirection?.Invoke(0, h_inp >= v_inp
            //         ? (h_inp > 0 ? Vector2Int.right : Vector2Int.left)
            //         : (v_inp > 0 ? Vector2Int.up : Vector2Int.down));
            
        //     if (UnityEngine.Input.GetKey("up"))
        //     {
        //         MoveInDirection?.Invoke(0, Vector2Int.up);   
        //     }
        //     else if (UnityEngine.Input.GetKey("down"))
        //     {
        //         MoveInDirection?.Invoke(0, Vector2Int.down);  
        //     }
        //     else if (UnityEngine.Input.GetKey("left"))
        //     {
        //         MoveInDirection?.Invoke(0, Vector2Int.left);  
        //     }
        //     else if (UnityEngine.Input.GetKey("right"))
        //     {
        //         MoveInDirection?.Invoke(0, Vector2Int.right);
        //     }
        }
    }
}