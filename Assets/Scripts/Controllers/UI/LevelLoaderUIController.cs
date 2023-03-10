using System;
using UnityEngine;
using UnityEngine.UI;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Controllers.UI
{
    public class LevelLoaderUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup loaderCanvas;
        [SerializeField] private Image       loaderFill;
        [SerializeField] private Animator    spriteAnimator;

        private void OnShowSceneLoaderCalled()
        {
            spriteAnimator.enabled = true;
            LeanTween.value(gameObject, 0f, 1f, 0.25f)
                .setEaseLinear()
                .setOnUpdate(value => loaderCanvas.alpha = value);
        }

        private void OnHideSceneLoaderCalled()
        {
            LeanTween.value(gameObject, loaderCanvas.alpha, 0f, 0.5f)
                .setEaseLinear()
                .setOnUpdate(value => loaderCanvas.alpha = value)
                .setOnComplete(() => { spriteAnimator.enabled = false; });
        }

        private void OnSceneLoaderProgressed(float progress)
        {
            LeanTween.value(gameObject, loaderFill.fillAmount, progress, 0.1f)
                .setEaseLinear()
                .setOnUpdate(value => loaderFill.fillAmount = value);
        }
        

        private void OnEnable()
        {
            ShowSceneLoader.AddListener(OnShowSceneLoaderCalled);
            HideSceneLoader.AddListener(OnHideSceneLoaderCalled);
            SceneLoaderProgress.AddListener(OnSceneLoaderProgressed);
        }

        private void OnDisable()
        {
            ShowSceneLoader.RemoveListener(OnShowSceneLoaderCalled);
            HideSceneLoader.RemoveListener(OnHideSceneLoaderCalled);
            SceneLoaderProgress.RemoveListener(OnSceneLoaderProgressed);
        }
    }
}