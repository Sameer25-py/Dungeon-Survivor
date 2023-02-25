using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using DungeonSurvivor.Core.Player;

namespace DungeonSurvivor.UI.LevelEditor
{
    public class LevelEditor : EditorWindow
    {
        public GameData data;
        
        private int selectedRow, selectedCol;
        private TextField rowInput, colInput;
        private Label rowText, colText;
        
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
            rowText = root.Q<Label>("row");
            colText = root.Q<Label>("col");
            
            root.Q<Button>("select").clicked += Select;
            root.Q<Button>("u-stand").clicked += () => CreateBlock(Direction.Up, BlockType.Standing);
            root.Q<Button>("u-water").clicked += () => CreateBlock(Direction.Up, BlockType.Water);
            root.Q<Button>("u-wall").clicked += () => CreateBlock(Direction.Up, BlockType.Wall);
            root.Q<Button>("d-stand").clicked += () => CreateBlock(Direction.Down, BlockType.Standing);
            root.Q<Button>("d-water").clicked += () => CreateBlock(Direction.Down, BlockType.Water);
            root.Q<Button>("d-wall").clicked += () => CreateBlock(Direction.Down, BlockType.Wall);
            root.Q<Button>("l-stand").clicked += () => CreateBlock(Direction.Left, BlockType.Standing);
            root.Q<Button>("l-water").clicked += () => CreateBlock(Direction.Left, BlockType.Water);
            root.Q<Button>("l-wall").clicked += () => CreateBlock(Direction.Left, BlockType.Wall);
            root.Q<Button>("r-stand").clicked += () => CreateBlock(Direction.Right, BlockType.Standing);
            root.Q<Button>("r-water").clicked += () => CreateBlock(Direction.Right, BlockType.Water);
            root.Q<Button>("r-wall").clicked += () => CreateBlock(Direction.Right, BlockType.Wall);
        }

        private void UpdateIndex(Vector2Int index)
        {
            selectedRow = index.x;
            selectedCol = index.y;
            rowText.text = selectedRow.ToString();
            colText.text = selectedCol.ToString();
        }
        
        private void Select()
        {
            if (data.CurrentLevel >= data.levelSizes.Count) return;
            
            var levelSize = data.levelSizes[data.CurrentLevel];
            var inputRow = int.Parse(rowInput.value);
            var inputCol = int.Parse(colInput.value);
            if (0 > inputRow || inputRow >= levelSize.x || 0 > inputCol || inputCol >= levelSize.y) return;
            
            UpdateIndex(new Vector2Int(inputRow, inputCol));
        }

        private void CreateBlock(Direction direction, BlockType blockType)
        {
            var directionMap = new Dictionary<Direction, Vector2Int>
            {
                { Direction.Up, Vector2Int.up },
                { Direction.Down, Vector2Int.down },
                { Direction.Left, Vector2Int.left },
                { Direction.Right, Vector2Int.right }
            };
            var blockMap = new Dictionary<BlockType, GameObject>
            {
                { BlockType.Standing, data.prefabStand },
                { BlockType.Water, data.prefabWater },
                { BlockType.Wall, data.prefabWall }
            };

            if (data.CurrentLevel >= data.levelSizes.Count) return;
            var levelSize = data.levelSizes[data.CurrentLevel];

            var newIndex = new Vector2Int(selectedRow, selectedCol) + directionMap[direction];
            if (0 > newIndex.x || newIndex.x >= levelSize.x || 0 > newIndex.y || newIndex.y >= levelSize.y) return;

            Instantiate(blockMap[blockType], new Vector3(newIndex.x, 0, newIndex.y), Quaternion.identity);

            UpdateIndex(newIndex);
        }
    }
}