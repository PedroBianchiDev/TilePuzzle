using System.Collections.Generic;
using TilePuzzle.Audio;
using TilePuzzle.Save;
using TilePuzzle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TilePuzzle.Menu
{
    public class MenuOptionsManager : PanelBase
    {
        private MenuCanvasManager menuCanvasManager;

        [SerializeField]
        private Button musicButton;
        [SerializeField]
        private Button sfxButton;

        private Image musicButtonImage;
        private Image sfxButtonImage;

        [SerializeField]
        private List<Sprite> buttonSprites;

        [Header("Navigation")]
        [SerializeField]
        private Button backButton;

        public override void Initialize(CanvasManager canvasManager)
        {
            menuCanvasManager = canvasManager as MenuCanvasManager;
            musicButtonImage = musicButton.GetComponent<Image>();
            sfxButtonImage = sfxButton.GetComponent<Image>();

            backButton.onClick.AddListener(() => {
                Core.Application.Instance.GetService<AudioService>().PlaySFX("Click");
                menuCanvasManager.ChangeActivePanel("ButtonSelection");
            });

            ApplicationData applicationData = Core.Application.Instance.GetService<SaveService>().GetApplicationData();

            if (applicationData != null)
            {
                int musicSpriteIndex = applicationData.isMusicMuted ? 1 : 0;
                int sfxSpriteIndex = applicationData.isSfxMuted ? 1 : 0;

                musicButtonImage.sprite = buttonSprites[musicSpriteIndex];
                sfxButtonImage.sprite = buttonSprites[sfxSpriteIndex];
            }

            musicButton.onClick.AddListener(ToggleMusic);
            sfxButton.onClick.AddListener(ToggleSFX);
        }

        private void ToggleMusic()
        {
            SaveService saveService = Core.Application.Instance.GetService<SaveService>();
            AudioService audioService = Core.Application.Instance.GetService<AudioService>();
            ApplicationData applicationData = saveService.GetApplicationData();

            if (applicationData == null) return;

            applicationData.isMusicMuted = !applicationData.isMusicMuted;
            saveService.SaveApplicationData();
            int musicSpriteIndex = applicationData.isMusicMuted ? 1 : 0;

            audioService.SetMusicVolume(applicationData.isMusicMuted ? 0 : 1);
            musicButtonImage.sprite = buttonSprites[musicSpriteIndex];
        }

        private void ToggleSFX()
        {
            SaveService saveService = Core.Application.Instance.GetService<SaveService>();
            AudioService audioService = Core.Application.Instance.GetService<AudioService>();
            ApplicationData applicationData = saveService.GetApplicationData();

            if (applicationData == null) return;

            applicationData.isSfxMuted = !applicationData.isSfxMuted;
            saveService.SaveApplicationData();
            int sfxSpriteIndex = applicationData.isSfxMuted ? 1 : 0;
            audioService.SetSFXVolume(applicationData.isMusicMuted ? 0 : 1);
            sfxButtonImage.sprite = buttonSprites[sfxSpriteIndex];
        }
    }
}