using System.Collections.Generic;
using UnityEngine;

namespace DungeonSurvivor.Core.Managers
{
    public class BootOrder : Singleton<BootOrder>
    {
        [SerializeField] private List<SingletonBase> singletons;
        private void Awake()
        {
            Application.targetFrameRate = 120;
            singletons.ForEach(singleton => singleton.Initialize());
        }
    }
}