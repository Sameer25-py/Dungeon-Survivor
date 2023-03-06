using System.Collections.Generic;
using DungeonSurvivor.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Scenes.Shaheer.Revamp
{
    public class UIManager : Core.Managers.Singleton<UIManager>
    {
        [SerializeField] private TMP_Text         message;
        [SerializeField] private float            maxRotation;
        public                   List<CanvasGroup> CanvasGroups;
        
        private GameObject messageContainer;
        private Vector3 scaleOut;

        public Button level1, level2, level3, playButton;
        private int selectedLevel = 1;
        
        protected override void BootOrderAwake()
        {
            messageContainer = message.transform.parent.gameObject;
            scaleOut = 1.2f * Vector3.one;
            level1.onClick.AddListener(() => ClickLevel(1));
            level2.onClick.AddListener(() => ClickLevel(2));
            level3.onClick.AddListener(() => ClickLevel(3));
            playButton.onClick.AddListener(PlayScene);
            SuccessMessage($"Welcome {PlayerPrefs.GetString("Nick")}!");
            base.BootOrderAwake();
        }
        public void ClickLevel(int level)
        {
            selectedLevel = level;
        }
        public void PlayScene()
        {
            foreach (var canvas in CanvasGroups)
            {
                LeanTween.value(gameObject, 1f, 0f, 1f)
                    .setOnUpdate((float value) =>
                    {
                        canvas.alpha = value;
                    });
            }
            GameManager.Instance.LoadScene($"Level{selectedLevel}");
        }
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