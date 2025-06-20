using UnityEngine;
using UnityEngine.SceneManagement;

namespace WFC.Scenes
{
    public class ScenesInitializer : MonoBehaviour
    {
        private const string UI_SCENE_NAME = "UIScene";
        private const string LEVEL_SCENE_NAME = "LevelScene";

        private void Awake()
        {
            if (!IsSceneLoaded(UI_SCENE_NAME))
            {
                SceneManager.LoadScene(UI_SCENE_NAME, LoadSceneMode.Additive);
            }

            if (!IsSceneLoaded(LEVEL_SCENE_NAME))
            {
                SceneManager.LoadScene(LEVEL_SCENE_NAME, LoadSceneMode.Additive);
            }

            SceneManager.UnloadSceneAsync(0);
        }

        private void Start()
        {
            Scene scene = SceneManager.GetSceneByName(UI_SCENE_NAME);
            SceneManager.SetActiveScene(scene);
        }

        private bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}