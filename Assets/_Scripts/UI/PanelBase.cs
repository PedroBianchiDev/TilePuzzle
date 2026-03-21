using UnityEngine;

namespace TilePuzzle.UI
{
    public abstract class PanelBase : MonoBehaviour
    {
        public string panelName;

        public abstract void Initialize(CanvasManager canvasManager);

        public virtual void OnHide()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void OnShow()
        {
            this.gameObject.SetActive(true);
        }
    }
}