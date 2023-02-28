using UnityEngine;

namespace DungeonSurvivor.Core.Pushables
{
    public class PushableBoulder : PushableBase
    {
        protected override void OnPushApplied(Vector2Int direction,Vector3 targetPosition)
        {
            base.OnPushApplied(direction,targetPosition);
            Vector3 rotationDirectionVector = Vector3.zero;
            if (direction.x != 0)
            {
                rotationDirectionVector.z = direction.x;
            }

            if (direction.y != 0)
            {
                rotationDirectionVector.x =direction.y;
            }
            LeanTween.rotateAroundLocal(gameObject, rotationDirectionVector, 360f, MoveSpeed)
                .setEaseInCirc();
        }
    }
}