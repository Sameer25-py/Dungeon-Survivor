using System;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Controllers.Animations.UI
{
    public class InventoryAddition : MonoBehaviour
    {
        public void AnimateAdd()
        {
            LeanTween.color(gameObject.GetComponent<RectTransform>(), Color.white, 0.5f)
                .setEaseInOutQuint();
        }

        public void AnimateFail()
        {
            LeanTween.moveLocal(gameObject, Vector3.one * Random.Range(-1f, 1f) * 10f, 0.25f)
                .setEaseShake().setLoopCount(2);
        }
        
    }
}