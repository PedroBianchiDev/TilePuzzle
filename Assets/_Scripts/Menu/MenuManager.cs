using TilePuzzle.Audio;
using TilePuzzle.Save;
using UnityEngine;

namespace TilePuzzle.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private MenuCanvasManager menuCanvasManager;

        private void Start()
        {
            Core.Application.Instance.GetService<SaveService>().LoadPlayerData();
            Core.Application.Instance.GetService<AudioService>().PlaySong("MenuSong");
        }
    }
}