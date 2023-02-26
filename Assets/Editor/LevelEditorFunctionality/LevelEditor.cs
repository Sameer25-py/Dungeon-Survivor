using System.Collections.Generic;
using DungeonSurvivor.Core.Data;
using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Core.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
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

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/LevelEditor/LevelEditor.uxml");
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
            if (data.currentLevel >= data.levelSizes.Count) return;

            var levelSize = data.levelSizes[data.currentLevel];
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

            if (data.currentLevel >= data.levelSizes.Count) return;
            var levelSize = data.levelSizes[data.currentLevel];

            var newIndex = new Vector2Int(selectedRow, selectedCol) + direction;
            if (0 > newIndex.x || newIndex.x >= levelSize.x || 0 > newIndex.y || newIndex.y >= levelSize.y) return;

            var location = new Vector3(newIndex.x, 0, newIndex.y);

            if (ObjectPresent(location))
                Debug.LogWarning($"Object might already be present at {location}");

            var obj = (PrefabUtility.InstantiatePrefab(blockMap[blockType]) as GameObject)?.transform;
            obj.position = location;
            obj.parent = FindObjectOfType<GameManager>()
                .transform.GetChild(data.currentLevel);

            var blk = obj.GetComponent<Block>();
            blk.index = newIndex;
            blk.level = data.currentLevel;
            blk.OnDrawGizmos();

            FocusPosition(location);
            UpdateIndex(newIndex);
        }

        private bool ObjectPresent(Vector3 location)
        {
            var intersecting = Physics.OverlapSphere(location, 0.01f);
            return intersecting.Length != 0;
        }

        private void FocusPosition(Vector3 pos)
        {
            SceneView.lastActiveSceneView.Frame(new Bounds(pos, 8f * Vector3.one), false);
        }
    }
}