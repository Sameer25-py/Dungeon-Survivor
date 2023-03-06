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

            if (Mathf.Abs(h_inp) >= Mathf.Abs(v_inp))
            {
                MoveInDirection?.Invoke(0, h_inp > 0 ? Vector2Int.right : Vector2Int.left);
            }
            else
            {
                MoveInDirection?.Invoke(0, v_inp > 0 ? Vector2Int.up : Vector2Int.down);
            }
        }
    }
}