using System.Collections.Generic;
using System.Linq;
using DungeonSurvivor.Core.Data;
using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Core.Managers;
using DungeonSurvivor.Core.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Transform = log4net.Util.Transform;
using Vector3 = UnityEngine.Vector3;

namespace DungeonSurvivor.Editor.LevelEditorFunctionality
{
    public class LevelEditor : EditorWindow
    {
        public GameData data;

        private int       selectedRow, selectedCol;
        private TextField rowInput,    colInput;
        private Label     rowText,     colText;

        [MenuItem("Dungeon Survivor/LevelEditor")]
        public static void ShowExample()
        {
            var wnd = GetWindow<LevelEditor>();
            wnd.titleContent = new GUIContent("LevelEditor");
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/LevelEditorFunctionality/LevelEditor.uxml");
            root.Add(visualTree.Instantiate());

            rowInput = root.Q<TextField>("row-input");
            colInput = root.Q<TextField>("col-input");
            rowText  = root.Q<Label>("row");
            colText  = root.Q<Label>("col");

            root.Q<Button>("select")
                .clicked += Select;
            root.Q<Button>("u-stand")
                .clicked += () => CreateBlock(Vector2Int.up, BlockType.Standing);
            root.Q<Button>("u-water")
                .clicked += () => CreateBlock(Vector2Int.up, BlockType.Water);
            root.Q<Button>("u-wall")
                .clicked += () => CreateBlock(Vector2Int.up, BlockType.Wall);
            root.Q<Button>("d-stand")
                .clicked += () => CreateBlock(Vector2Int.down, BlockType.Standing);
            root.Q<Button>("d-water")
                .clicked += () => CreateBlock(Vector2Int.down, BlockType.Water);
            root.Q<Button>("d-wall")
                .clicked += () => CreateBlock(Vector2Int.down, BlockType.Wall);
            root.Q<Button>("l-stand")
                .clicked += () => CreateBlock(Vector2Int.left, BlockType.Standing);
            root.Q<Button>("l-water")
                .clicked += () => CreateBlock(Vector2Int.left, BlockType.Water);
            root.Q<Button>("l-wall")
                .clicked += () => CreateBlock(Vector2Int.left, BlockType.Wall);
            root.Q<Button>("r-stand")
                .clicked += () => CreateBlock(Vector2Int.right, BlockType.Standing);
            root.Q<Button>("r-water")
                .clicked += () => CreateBlock(Vector2Int.right, BlockType.Water);
            root.Q<Button>("r-wall")
                .clicked += () => CreateBlock(Vector2Int.right, BlockType.Wall);
        }

        private void UpdateIndex(Vector2Int index)
        {
            selectedRow  = index.x;
            selectedCol  = index.y;
            rowText.text = selectedRow.ToString();
            colText.text = selectedCol.ToString();
        }

        private void Select()
        {
            if (data.currentLevel > data.levelSizes.Count) return;

            var levelSize = data.levelSizes[data.currentLevel - 1];
            var inputRow  = int.Parse(rowInput.value);
            var inputCol  = int.Parse(colInput.value);
            if (0 > inputRow || inputRow >= levelSize.x || 0 > inputCol || inputCol >= levelSize.y) return;

            UpdateIndex(new Vector2Int(inputRow, inputCol));
            FocusPosition(new Vector3(inputRow, 0, inputCol));
        }

        private void CreateBlock(Vector2Int direction, BlockType blockType)
        {
            var blockMap = new Dictionary<BlockType, GameObject>
            {
                { BlockType.Standing, data.prefabStand },
                { BlockType.Water, data.prefabWater },
                { BlockType.Wall, data.prefabWall }
            };

            if (data.currentLevel > data.levelSizes.Count) return;
            var levelSize = data.levelSizes[data.currentLevel - 1];

            var newIndex = new Vector2Int(selectedRow, selectedCol) + direction;
            if (0 > newIndex.x || newIndex.x >= levelSize.x || 0 > newIndex.y || newIndex.y >= levelSize.y) return;

            var location = new Vector3(newIndex.x, 0, newIndex.y);

            var intersecting = Physics.OverlapSphere(location, 0.01f);
            if (intersecting.Length != 0)
            {
                foreach (var collider in intersecting)
                {
                    if (!collider.TryGetComponent<Block>(out var old)) return;
                    DestroyImmediate(old.gameObject);
                    var locationInt = Vector3Int.RoundToInt(location);
                    Debug.LogWarning($"Block replaced at index: {locationInt.x}, {locationInt.z}");
                }
            }

            var obj = (PrefabUtility.InstantiatePrefab(blockMap[blockType]) as GameObject)?.transform;
            obj.position = location;
            obj.parent = GameObject.FindWithTag("BlockParent").transform;
            
            var blk = obj.GetComponent<Block>();
            blk.index = newIndex;
            blk.OnDrawGizmos();

            FocusPosition(location);
            UpdateIndex(newIndex);
        }

        private void FocusPosition(Vector3 pos)
        {
            SceneView.lastActiveSceneView.Frame(new Bounds(pos, 4f * Vector3.one), false);
        }
    }
}