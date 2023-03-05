using Cinemachine;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DungeonSurvivor.Controllers.Camera
{
    public class LevelCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera miniGameCamera;

        private void OnSwitchToMiniGameCameraCalled()
        {
            miniGameCamera.MoveToTopOfPrioritySubqueue();
        }

        private void OnEnable()
        {
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
        }

        private void OnDisable()
        {
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
        }
    }
}