using TilePuzzle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Menu
{
    public class LevelSelection : PanelBase
    {
        [SerializeField]
        private Button backButton;


        [SerializeField]
        private MenuCanvasManager menuCanvasManager;
        public override void Initialize(CanvasManager canvasManager)
        {
            menuCanvasManager = canvasManager as MenuCanvasManager;
            backButton.onClick.AddListener(() => menuCanvasManager.ChangeActivePanel("ButtonSelection"));
        }

        public override void OnHide()
        {
            this.gameObject.SetActive(false);
        }

        public override void OnShow()
        {
            this.gameObject.SetActive(true);
        }
    }
}