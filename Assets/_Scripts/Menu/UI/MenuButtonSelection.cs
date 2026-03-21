using TilePuzzle.Audio;
using TilePuzzle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Menu
{
    public class MenuButtonSelection : PanelBase
    {
        [SerializeField]
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
                menuCanvasManager.ChangeActivePanel("LevelSelection");
            });

            exitButton.onClick.AddListener(QuitGame);
        }

        public override void OnHide()
        {
            this.gameObject.SetActive(false);
        }

        public override void OnShow()
        {
            this.gameObject.SetActive(true);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}