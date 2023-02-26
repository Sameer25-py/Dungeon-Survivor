using UnityEngine;

namespace DungeonSurvivor.Core.Managers
{
    public abstract class SingletonBase : MonoBehaviour
    {
        public abstract void Initialize();
        protected virtual void BootOrderAwake()
        {
#if UNITY_EDITOR
            print(GetType().Name + " initialized.");
#endif
        }
    }
    public class Singleton <T> : SingletonBase where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }
        
        public override void Initialize()
        {
            if (Instance == null)
                SetInstance(this as T);
            else Destroy(this);
            
            BootOrderAwake();
        }
        
        protected virtual void SetInstance(T instance) => Instance = instance;
    }
    public class SingletonDontDestroy <T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void SetInstance(T instance)
        {
            Instance = instance;
            DontDestroyOnLoad(instance);
        }
    }
}