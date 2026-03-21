using System.Collections.Generic;
using TilePuzzle.Audio;
using UnityEngine;

namespace TilePuzzle.Gameplay
{
    [CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
    public class Level : ScriptableObject
    {
        public string levelId;
        public string levelName;

        [Header("Menu Level Settings")]
        public Sprite artDisplayer;
   
        public int levelCost = 0;
        public bool blocked = false;

        [Header("Gameplay Level Settings")]
        public float timer = 60f;
        public int gridSize = 3;
        public float cellSize = 230f;
        public Vector2 cellSpacing = new(10f, 10f);
        public List<Sprite> sprites = new();
    }
}