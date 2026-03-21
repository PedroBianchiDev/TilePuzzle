using System.Collections.Generic;
using TilePuzzle.Gameplay;
using TilePuzzle.Save;
using TilePuzzle.Scenes;
using TilePuzzle.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Menu
{
    public class LevelSelection : PanelBase
    {
        private MenuCanvasManager menuCanvasManager;

        [Header("Levels")]
        [SerializeField]
        private List<Level> Levels = new();

        [SerializeField]
        private GameObject levelPrefab;

        [SerializeField]
        private Transform levelButtonContainer;

        private List<LevelUI> levelUis = new();

        [Header("Navigation")]
        [SerializeField]
        private Button backButton;

        [Header("Information")]
        [SerializeField]
        private TextMeshProUGUI goldText;

        [Header("Information")]
        [SerializeField]
        private GameObject notGoldPanel;

        public override void Initialize(CanvasManager canvasManager)
        {
            menuCanvasManager = canvasManager as MenuCanvasManager;
            backButton.onClick.AddListener(() => menuCanvasManager.ChangeActivePanel("ButtonSelection"));
        }

        private void InstantiateLevels()
        {
            List<string> playerLevels = Core.Application.Instance.GetService<SaveService>().GetPlayerData().boughtLevels;

            foreach (Level level in Levels)
            {
                LevelUI levelUi = Instantiate(levelPrefab, levelButtonContainer).GetComponent<LevelUI>();
                bool isBought = false;

                if (playerLevels != null)
                {
                    isBought = playerLevels.Contains(level.levelId);
                }

                levelUi.Initialize(level, isBought);
                SetLevelClickAction(level, levelUi, isBought);
                levelUis.Add(levelUi);
            }
        }

        public void SetLevelClickAction(Level level, LevelUI levelUi, bool isBought)
        {
            levelUi.SetClickAction(() =>
            {
                bool isUnlocked = isBought || !level.blocked;

                if (isUnlocked)
                {
                    SceneService sceneService = Core.Application.Instance.GetService<SceneService>();
                    sceneService.selectedLevel = level;
                    sceneService.LoadScene(1);
                }
                else
                {
                    SaveService saveService = Core.Application.Instance.GetService<SaveService>();
                    int currentGold = saveService.GetPlayerData().gold;

                    if (currentGold >= level.levelCost)
                    {
                        saveService.ModifyGold(-level.levelCost);
                        saveService.AddLevel(level.levelId);
                        saveService.SavePlayerData();
                        SceneService sceneService = Core.Application.Instance.GetService<SceneService>();
                        sceneService.selectedLevel = level;
                        sceneService.LoadScene(1);
                    }
                    else
                    {
                        notGoldPanel.SetActive(true);
                    }
                }
            });
        }

        private void ClearLevels()
        {
            foreach (LevelUI levelUi in levelUis)
            {
                Destroy(levelUi.gameObject);
            }
            levelUis.Clear();
        }

        public override void OnHide()
        {
            this.gameObject.SetActive(false);
            ClearLevels();
        }

        public override void OnShow()
        {
            this.gameObject.SetActive(true);
            goldText.text = Core.Application.Instance.GetService<SaveService>().GetPlayerData().gold.ToString();
            InstantiateLevels();
        }
    }
}