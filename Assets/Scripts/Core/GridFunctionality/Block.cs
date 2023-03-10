#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace DungeonSurvivor.Core.GridFunctionality
{
    public class Block : MonoBehaviour
    {
        public Vector2Int index;
        public BlockType type;

        #if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            GUI.color = Color.red;
            Handles.Label(transform.position + 2f * Vector3.up, $"{index.x},{index.y}");
        }
        #endif
    }
}