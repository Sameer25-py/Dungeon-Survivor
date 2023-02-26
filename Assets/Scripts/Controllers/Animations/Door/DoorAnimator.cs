using UnityEngine;

namespace DungeonSurvivor.Controllers.Animations.Door
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject rotatatable;
        public void OpenDoor()
        {
            LeanTween.rotateAround(rotatatable, Vector3.down, 90f, 1f).setEaseInExpo();
        }

        public void CloseDoor()
        {
            LeanTween.rotateAround(rotatatable, Vector3.up, 90f, 1f).setEaseOutExpo();
        }
    }
}