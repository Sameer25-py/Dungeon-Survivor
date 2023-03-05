using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Scenes.Shaheer.Revamp
{
    public class UIManager : Core.Managers.Singleton<UIManager>
    {
        [SerializeField] private TMP_Text message;
        [SerializeField] private GameObject dimmer, loading;
        [SerializeField] private float maxRotation;
        
        private GameObject messageContainer;
        private Vector3 scaleOut;
        // private bool previousState, nextState;
        
        protected override void BootOrderAwake()
        {
            messageContainer = message.transform.parent.gameObject;
            scaleOut = 1.2f * Vector3.one;
            base.BootOrderAwake();
        }
        // private void Update()
        // {
        //     if (previousState == nextState) return;
        //     
        //     dimmer.SetActive(nextState);
        //     loading.SetActive(nextState);
        //     previousState = nextState;
        // }
        // public void SetWaitingState(bool newState)
        // {
        //     nextState = newState;
        // }
        public void ErrorMessage(string text)
        {
            LeanTween
                .rotateZ(messageContainer, Random.Range(-maxRotation, maxRotation), 0.6f)
                .setEasePunch()
                .setOnComplete(() => { LeanTween.rotateZ(messageContainer, 0f, 0.2f); });
            message.text = text;
        }
        public void SuccessMessage(string text)
        {
            LeanTween
                .scale(messageContainer, scaleOut, 0.6f)
                .setEasePunch()
                .setOnComplete(() => { LeanTween.scale(messageContainer, Vector3.one, 0.2f); });
            message.text = text;
        }
    }
}