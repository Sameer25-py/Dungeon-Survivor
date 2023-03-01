using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Controllers.Animations.Objects
{
    public class FloatingAnimation : MonoBehaviour
    {
        public float DrownTime      = 0.1f;
        public float ShakeSpeed     = 0.1f;
        public float ShakeAmount    = 0.2f;
        public float ShakeDuration  = 0.5f;
        public float DrownPositionY = 0f;

        private Vector3 _drownedPosition;

        //courtesy ChatGPT
        private void ShakeObjectTween(float t)
        {
            float   shake        = Mathf.Sin(t * ShakeSpeed) * ShakeAmount * (1 - t);
            transform.position = _drownedPosition + new Vector3(shake, shake, 0f);
        }

        private void Start()
        {
            Vector3 finalPosition = new Vector3(transform.position.x, DrownPositionY, transform.position.z);
            LeanTween.moveLocal(gameObject, finalPosition, DrownTime)
                .setOnComplete(() =>
                {
                    _drownedPosition = transform.position;
                    LeanTween.value(gameObject, ShakeObjectTween, 0f, 1f, ShakeDuration)
                        .setEaseShake()
                        .setLoopPingPong();
                });
        }
    }
}