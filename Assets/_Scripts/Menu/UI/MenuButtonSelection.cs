using TilePuzzle.Audio;
using TilePuzzle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Menu
{
    public class MenuButtonSelection : PanelBase
    {
        private MenuCanvasManager menuCanvasManager;

        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button optionsButton;

        [SerializeField]
        private Button exitButton;

        public override void Initialize(CanvasManager canvasManager)
        {
            menuCanvasManager = canvasManager as MenuCanvasManager;

            playButton.onClick.AddListener(() => {
                Click();
                menuCanvasManager.ChangeActivePanel("LevelSelection");
            });

            optionsButton.onClick.AddListener(() => {
                Click();
                menuCanvasManager.ChangeActivePanel("Options");
            });

            exitButton.onClick.AddListener(() => {
                QuitGame();
            });
        }

        private void Click()
        {
            Core.Application.Instance.GetService<AudioService>().PlaySFX("Click");
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}