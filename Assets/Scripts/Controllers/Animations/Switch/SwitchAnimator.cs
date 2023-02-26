using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DungeonSurvivor.Controllers.Animations.Switch
{
    public class SwitchAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject pressable;
        [SerializeField] private Color      defaultColor;
        [SerializeField] private Color      pressedColor;

        [SerializeField] private Vector3 pressedPosition   = Vector3.zero;
        [SerializeField] private Vector3 unPressedPosition = new Vector3(0f, 0.08f, 0f);

        private                 Renderer _pressableRenderer;
        private static readonly int      s_baseColor = Shader.PropertyToID("_BaseColor");

        public void PressButton()
        {
            LeanTween.moveLocal(pressable, pressedPosition, 0.5f)
                .setEaseOutExpo();
            LeanTween.value(gameObject, UpdateColor, defaultColor, pressedColor, 0.2f)
                .setEaseInCirc();
        }

        public void ReleaseButton()
        {
            LeanTween.moveLocal(pressable, unPressedPosition, 0.2f)
                .setEaseInExpo();
            LeanTween.value(gameObject, UpdateColor, pressedColor, defaultColor, 0.2f)
                .setEaseOutCirc();
        }

        private void UpdateColor(Color color)
        {
            MaterialPropertyBlock mpb = new();
            _pressableRenderer.GetPropertyBlock(mpb);
            mpb.SetColor(s_baseColor, color);
            _pressableRenderer.SetPropertyBlock(mpb);
        }

        private void OnEnable()
        {
            _pressableRenderer = pressable.GetComponent<Renderer>();
            UpdateColor(defaultColor);
        }
    }
}