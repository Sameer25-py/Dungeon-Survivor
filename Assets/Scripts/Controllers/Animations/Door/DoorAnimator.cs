using UnityEngine;

namespace DungeonSurvivor.Controllers.Animations.Door
{

    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject rotatatable;
        private                  LTDescr    _ltDescr = null;
      
        public void OpenDoor()
        {
            if (_ltDescr is not null)
            {
                LeanTween.cancel(_ltDescr.id);
            }
           
            _ltDescr = LeanTween.rotateLocal(rotatatable, Vector3.down * 90f, 0.8f)
                .setEaseOutExpo();
        }

        public void CloseDoor()
        {
            if (_ltDescr is not null)
            {
                LeanTween.cancel(_ltDescr.id);
            }
          

            _ltDescr = LeanTween.rotateLocal(rotatatable, Vector3.zero, 0.5f)
                .setEaseOutExpo();
        }
    }
}