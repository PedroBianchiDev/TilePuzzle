using UnityEngine;
using TMPro;

namespace TilePuzzle.Gameplay
{
    public class GameTimer : MonoBehaviour
    {
        private float currentTime;
        private bool isRunning = false;

        [Header("Referências")]
        public TextMeshProUGUI textoTempo;
        private PuzzleManager puzzleManager;

        public void Initialize(PuzzleManager puzzleManager)
        {
            this.puzzleManager = puzzleManager;
            currentTime = puzzleManager.level.timer;
            textoTempo.text = Mathf.Ceil(currentTime).ToString() + "s";
        }

        void Update()
        {
            if (isRunning)
            {
                currentTime -= Time.deltaTime;

                if (textoTempo != null)
                {
                    textoTempo.text = Mathf.Ceil(currentTime).ToString() + "s";
                }

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    StopTimer();

                    if (puzzleManager != null)
                    {
                        puzzleManager.Derrota();
                    }
                }
            }
        }

        public int ObterTempoRestante()
        {
            return Mathf.CeilToInt(currentTime);
        }

        public void StartTimer()
        {
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
        }
    }
}