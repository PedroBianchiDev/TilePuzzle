using TilePuzzle.UI;

namespace TilePuzzle.Gameplay
{
    public class GameplayPanel : PanelBase
    {
        private GameplayCanvasManager canvasManager;

        public override void Initialize(CanvasManager canvasManager)
        {
            this.canvasManager = canvasManager as GameplayCanvasManager;
        }
    }
}