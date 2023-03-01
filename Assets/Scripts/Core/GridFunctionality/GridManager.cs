using System;
using System.Collections.Generic;
using System.Linq;
using DungeonSurvivor.Core.Managers;
using DungeonSurvivor.Core.Player;
using DungeonSurvivor.Core.Pushables;
using UnityEngine;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.GridFunctionality
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private List<Block>        currentLevelBlocks;
        [SerializeField] private List<PushableBase> pushables;

        private void GetCurrentLevelBlocks()
        {
            currentLevelBlocks = GetComponentsInChildren<Block>()
                .ToList();
            GridDataCollectionCompleted?.Invoke();
        }

        private Block GetBlockByIndex(Vector2Int index)
        {
            return currentLevelBlocks.FirstOrDefault(blk => blk.index.x == index.x && blk.index.y == index.y);
        }

        public Tuple<bool, PushableBase> CheckForPushable(Vector2Int index)
        {
            Tuple<bool, PushableBase> detectedPushable = new Tuple<bool, PushableBase>(false, null);
            foreach (PushableBase pushable in pushables)
            {
                if (pushable.CurrentIndex.Equals(index))
                {
                    detectedPushable = new Tuple<bool, PushableBase>(true, pushable);
                    break;
                }
            }

            return detectedPushable;
        }

        public Block GetBlock(Vector2Int index)
        {
            return GetBlockByIndex(index);
        }
        
        private void OnPushableDestroyed(PushableBase arg0)
        {
            pushables.Remove(arg0);
        }
        
        private void OnChangeBlockTypeCalled(Vector2Int arg0, BlockType arg1)
        {
            GetBlockByIndex(arg0)
                .type = arg1;
        }
        
        private void Start()
        {
            GetCurrentLevelBlocks();
            pushables = FindObjectsByType<PushableBase>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ToList();
        }

        private void OnEnable()
        {
            PushableDestroyed.AddListener(OnPushableDestroyed);
            ChangeBlockType.AddListener(OnChangeBlockTypeCalled);
        }

        private void OnDisable()
        {
            PushableDestroyed.RemoveListener(OnPushableDestroyed);
            ChangeBlockType.RemoveListener(OnChangeBlockTypeCalled);
        }
    }
}