using UnityEngine;

namespace DungeonSurvivor.Core.Pushables
{
    public class PushableBoulder : PushableBase
    {
        public AudioSource asource;
        protected override void OnPushApplied(Vector3 targetPosition, Vector2Int direction)
        {
            base.OnPushApplied(targetPosition, direction);
            Vector3 rotationDirectionVector = Vector3.zero;
            if (direction.x != 0)
            {
                rotationDirectionVector.z = -direction.x;
            }

            if (direction.y != 0)
            {
                rotationDirectionVector.x = direction.y;
            }
            asource.Play();
            LeanTween.rotateAroundLocal(gameObject, rotationDirectionVector, 360f, MoveSpeed)
                .setEaseInCirc().setOnComplete(() => {

                    asource.Pause();
                
                });
                
            
        }
        private void Update()
        {
            
        }
    }
}