using System.Collections;
using TilePuzzle.Core;
using TilePuzzle.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TilePuzzle.Scenes
{
    public class SceneService : MonoBehaviourService
    {
        public GameObject loadingScreen;
        public Image loadingBarFill;
        public Level selectedLevel;

        public override void Initialize()
        {

        }

        public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

                loadingBarFill.fillAmount = progressValue;

                yield return null;
            }

            loadingBarFill.fillAmount = 0;
            loadingScreen.SetActive(false);
        }
    }
}
