using Cinemachine;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DungeonSurvivor.Controllers.Camera
{
    public class LevelCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera     miniGameCamera;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        private void OnSwitchToMiniGameCameraCalled()
        {
            miniGameCamera.MoveToTopOfPrioritySubqueue();
        }

        private void OnSwitchToPlayerFollowCameraCalled()
        {
            stateDrivenCamera.MoveToTopOfPrioritySubqueue();
        }

        private void OnEnable()
        {   
            stateDrivenCamera.MoveToTopOfPrioritySubqueue();
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
            SwitchToPlayerFollowCamera.AddListener(OnSwitchToPlayerFollowCameraCalled);
        }

        private void OnDisable()
        {
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
            SwitchToPlayerFollowCamera.RemoveListener(OnSwitchToPlayerFollowCameraCalled);
        }
    }
}