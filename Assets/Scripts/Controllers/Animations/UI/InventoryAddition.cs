using System;
using UnityEngine;

namespace DungeonSurvivor.Controllers.Animations.UI
{
    public class InventoryAddition : MonoBehaviour
    {
        public void Animate()
        {
            LeanTween.color(gameObject.GetComponent<RectTransform>(), Color.white, 0.5f)
                .setEaseInOutQuint();
        }
    }
}