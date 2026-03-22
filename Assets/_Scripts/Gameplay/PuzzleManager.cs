using System.Collections;
using System.Collections.Generic;
using TilePuzzle.Audio;
using TilePuzzle.Save;
using TilePuzzle.Scenes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TilePuzzle.Gameplay
{
    public class PuzzleManager : MonoBehaviour
    {
        public bool isGameActive = false;

        [Header("Configurações do Tabuleiro")]
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject emptyTilePrefab;
        [SerializeField] private List<Tile> tiles;
        [SerializeField] private Transform emptySpace;
        [SerializeField] private int emptySpaceIndex;

        [SerializeField] public Transform gameplayObject;

        [Header("Configurações de Jogo")]
        [SerializeField] private GameTimer gameTimer;

        [SerializeField] UnityEvent OnWinGame;
        [SerializeField] UnityEvent OnLoseGame;

        [SerializeField]
        public Level level;

        IEnumerator Start()
        {
            level = Core.Application.Instance.GetService<SceneService>().selectedLevel ?? level;

            if (level.levelId == "Mickey")
            {
                Core.Application.Instance.GetService<AudioService>().PlaySong("MickeyTheme");
            }
            else if (level.levelId == "SpiderMan")
            {
                Core.Application.Instance.GetService<AudioService>().PlaySong("SpiderManTheme");
            }
            else
            {
                Core.Application.Instance.GetService<AudioService>().PlaySong("PikachuTheme");
            }

            if (level == null) yield return null;

            emptySpaceIndex = (level.gridSize * level.gridSize) - 1;

            if (gameTimer != null)
            {
                gameTimer.Initialize(this);
            }

            ConfigureGrid();
            InstantiateTiles();
            InstantiateEmptyTile();

            yield return new WaitForSeconds(1f);

            if (gridLayout != null) gridLayout.enabled = false;

            Shuffle();

            isGameActive = true;

            if (gameTimer != null)
            {
                gameTimer.StartTimer();
            }
        }

        private void ConfigureGrid()
        {
            gridLayout.cellSize = new Vector2(level.cellSize, level.cellSize);
            gridLayout.spacing = level.cellSpacing;
            gridLayout.constraintCount = level.gridSize;
        }

        private void InstantiateTiles()
        {
            foreach (Sprite tileSprite in level.sprites)
            {
                Tile tileSpawned = Instantiate(tilePrefab, gridLayout.transform).GetComponent<Tile>();
                tiles.Add(tileSpawned);
                tileSpawned.Init(tiles.IndexOf(tileSpawned), this, tileSprite);
                emptySpace = tileSpawned.transform;
            }
        }

        private void InstantiateEmptyTile()
        {
            GameObject tileSpawned = Instantiate(emptyTilePrefab, gridLayout.transform);
            emptySpace = tileSpawned.transform;
        }

        public bool TryMoveTile(Tile tileToMove)
        {
            if (!isGameActive) return false;

            int tileIndex = tileToMove.currentPositionIndex;

            if (IsAdjacent(tileIndex, emptySpaceIndex))
            {
                RectTransform emptyRect = emptySpace.GetComponent<RectTransform>();
                Vector2 emptyVisualPos = emptyRect.anchoredPosition;

                emptyRect.anchoredPosition = tileToMove.startPosition;
                tileToMove.MoveToPosition(emptyVisualPos);

                tileToMove.currentPositionIndex = emptySpaceIndex;
                emptySpaceIndex = tileIndex;

                if (CheckWin())
                {
                    Vitoria();
                }

                return true;
            }

            return false;
        }

        private bool IsAdjacent(int index1, int index2)
        {
            if (index1 / level.gridSize == index2 / level.gridSize && Mathf.Abs(index1 - index2) == 1) return true;
            if (index1 % level.gridSize == index2 % level.gridSize && Mathf.Abs(index1 - index2) == level.gridSize) return true;
            return false;
        }

        private void Shuffle()
        {
            int movimentosDeEmbaralhamento = 100;

            for (int i = 0; i < movimentosDeEmbaralhamento; i++)
            {
                List<Tile> vizinhosValidos = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (IsAdjacent(tile.currentPositionIndex, emptySpaceIndex))
                    {
                        vizinhosValidos.Add(tile);
                    }
                }

                Tile pecaEscolhida = vizinhosValidos[UnityEngine.Random.Range(0, vizinhosValidos.Count)];

                RectTransform emptyRect = emptySpace.GetComponent<RectTransform>();
                RectTransform tileRect = pecaEscolhida.GetComponent<RectTransform>();

                Vector2 tempPos = emptyRect.anchoredPosition;
                emptyRect.anchoredPosition = tileRect.anchoredPosition;
                tileRect.anchoredPosition = tempPos;

                int tempIndex = pecaEscolhida.currentPositionIndex;
                pecaEscolhida.currentPositionIndex = emptySpaceIndex;
                emptySpaceIndex = tempIndex;
            }
        }

        private bool CheckWin()
        {
            foreach (Tile tile in tiles)
            {
                if (tile.currentPositionIndex != tile.correctPositionIndex)
                {
                    return false;
                }
            }
            return true;
        }

        private void Vitoria()
        {
            isGameActive = false;

            Core.Application.Instance.GetService<AudioService>().PlaySong("Victory");

            if (gameTimer != null)
            {
                gameTimer.StopTimer();
                int goldGanhoNestaPartida = gameTimer.ObterTempoRestante();
                SaveService saveService = Core.Application.Instance.GetService<SaveService>();
                saveService.ModifyGold(goldGanhoNestaPartida);
                saveService.SavePlayerData();
            }

            OnWinGame?.Invoke();
        }

        public void Derrota()
        {
            Core.Application.Instance.GetService<AudioService>().PlaySong("Lose");

            isGameActive = false;
            OnLoseGame?.Invoke();
        }
    }
}