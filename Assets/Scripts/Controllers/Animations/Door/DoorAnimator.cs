using UnityEngine;

namespace DungeonSurvivor.Controllers.Animations.Door
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject rotatatable;

        private LTDescr _descr;
        public void OpenDoor()
        {
            LeanTween.rotateLocal(rotatatable, Vector3.down * 90f, 1f).setEaseInExpo();
        }

        public void CloseDoor()
        {
            LeanTween.rotateLocal(rotatatable, Vector3.zero, 0.5f).setEaseOutExpo();
        }
    }
}