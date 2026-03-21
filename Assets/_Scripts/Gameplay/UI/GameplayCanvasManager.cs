using System.Collections.Generic;
using TilePuzzle.UI;
using UnityEngine;

namespace TilePuzzle.Gameplay
{
    public class GameplayCanvasManager : CanvasManager
    {
        [Header("Panel Bases")]
        [SerializeField]
        private List<PanelBase> panelBases = new();

        private PanelBase currentActivePanel;

        private void Start()
        {
            foreach (PanelBase panelBase in panelBases)
            {
                panelBase.Initialize(this);
            }

            ChangeActivePanel(panelBases[0].name);
        }

        public void ChangeActivePanel(string name)
        {
            if (currentActivePanel != null)
                currentActivePanel.OnHide();

            PanelBase panel = GetPanel(name);

            if (panel == null)
                return;

            currentActivePanel = panel;
            currentActivePanel.OnShow();
        }

        private PanelBase GetPanel(string name)
        {
            // return panelBases.FirstOrDefault(panel => panel.name == name);
            foreach (PanelBase panel in panelBases)
            {
                if (panel.panelName == name)
                    return panel;
            }

            return null;
        }
    }
}