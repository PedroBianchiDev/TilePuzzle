using TilePuzzle.Scenes;
using TilePuzzle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Gameplay
{
    public class LosePanel : PanelBase
    {
        private GameplayCanvasManager canvasManager;

        [SerializeField]
        private Button loseButton;

        public override void Initialize(CanvasManager canvasManager)
        {
            this.canvasManager = canvasManager as GameplayCanvasManager;

            loseButton.onClick.AddListener(() =>
            {
                SceneService sceneService = Core.Application.Instance.GetService<SceneService>();
                sceneService.LoadScene(0);
            });
        }
    }
}