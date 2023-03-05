using Cinemachine;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DungeonSurvivor.Controllers.Camera
{
    public class LevelCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera     miniGameCamera;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private CinemachineVirtualCamera     endLevelCamera;

        private void OnSwitchToMiniGameCameraCalled()
        {
            miniGameCamera.MoveToTopOfPrioritySubqueue();
        }

        private void OnSwitchToPlayerFollowCameraCalled()
        {
            stateDrivenCamera.MoveToTopOfPrioritySubqueue();
        }
        
        private void OnSwitchToEndLevelCameraCalled()
        {
            endLevelCamera.MoveToTopOfPrioritySubqueue();
        }

        private void OnEnable()
        {   
            stateDrivenCamera.MoveToTopOfPrioritySubqueue();
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
            SwitchToPlayerFollowCamera.AddListener(OnSwitchToPlayerFollowCameraCalled);
            SwitchToEndLevelCamera.AddListener(OnSwitchToEndLevelCameraCalled);
        }

        private void OnDisable()
        {
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
            SwitchToPlayerFollowCamera.RemoveListener(OnSwitchToPlayerFollowCameraCalled);
            SwitchToEndLevelCamera.RemoveListener(OnSwitchToEndLevelCameraCalled);
        }
        
    }
}