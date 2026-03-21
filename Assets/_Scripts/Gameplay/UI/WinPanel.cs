using TilePuzzle.Scenes;
using TilePuzzle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Gameplay
{
    public class WinPanel : PanelBase
    {
        private GameplayCanvasManager canvasManager;

        [SerializeField]
        private Button WinButton;

        public override void Initialize(CanvasManager canvasManager)
        {
            this.canvasManager = canvasManager as GameplayCanvasManager;

            WinButton.onClick.AddListener(() =>
            {
                SceneService sceneService = Core.Application.Instance.GetService<SceneService>();
                sceneService.LoadScene(0);
            });
        }
    }
}