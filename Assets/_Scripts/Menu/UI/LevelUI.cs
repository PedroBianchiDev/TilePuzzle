using System;
using TilePuzzle.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Menu
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField]
        private Level level;

        [SerializeField]
        private Image levelImage;

        [SerializeField]
        private TextMeshProUGUI levelTitle;

        [SerializeField]
        private GameObject goldContainer;

        [SerializeField]
        private GameObject blockImage;

        [SerializeField]
        private TextMeshProUGUI goldText;

        [SerializeField]
        private Button playButton;

        public void Initialize(Level level, bool isBought)
        {
            this.level = level;
            levelImage.sprite = level.artDisplayer;
            levelTitle.text = level.levelName;

            bool shouldBlock = level.blocked && !isBought;

            goldContainer.SetActive(shouldBlock);
            blockImage.SetActive(shouldBlock);
            goldText.text = level.levelCost.ToString();
        }

        public void SetClickAction(Action clickEvent = null)
        {
            if (clickEvent != null)
            {
                playButton.onClick.AddListener(() =>
                {
                    playButton.interactable = false;
                    clickEvent();
                    playButton.interactable = true;
                });
            }
        }
    }
}