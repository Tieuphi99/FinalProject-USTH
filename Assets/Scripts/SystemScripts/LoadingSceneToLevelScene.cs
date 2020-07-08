using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SystemScripts
{
    public class LoadingSceneToLevelScene : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(LevelScene());
        }

        private static IEnumerator LevelScene()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(GameStatusController.CurrentLevel);
            GameStatusController.CurrentLevel += 1;
        }
    }
}
