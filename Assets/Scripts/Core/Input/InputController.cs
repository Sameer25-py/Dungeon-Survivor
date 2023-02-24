using DungeonSurvivor.Core.Events;

namespace DungeonSurvivor.Core.Input
{
    using UnityEngine;

    public class InputController : MonoBehaviour
    {
        #region Variables

        private Camera _mainCamera;

        #endregion

        #region UnityLifeCycle

        private void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 inputPos = Input.mousePosition;
                Ray     ray      = _mainCamera.ScreenPointToRay(inputPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameplayEvents.Movement.MoveToPosition?.Invoke(hit.point);
                }
            }
        }

        #endregion
    }
}