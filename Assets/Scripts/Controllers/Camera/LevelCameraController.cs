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

        private void ApplyNoiseShake(CinemachineVirtualCamera virtualCamera)
        {
            CinemachineBasicMultiChannelPerlin noiseComponent =
                virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noiseComponent.m_FrequencyGain = 1f;
            LeanTween.value(1f, 0f, 3f)
                .setDelay(1.5f)
                .setEaseOutExpo()
                .setOnUpdate((float value) => { noiseComponent.m_FrequencyGain = value; });
        }

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
            ApplyNoiseShake(endLevelCamera);
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