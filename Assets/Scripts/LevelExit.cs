using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    float nextLevelLoadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("ExitLevel");
        }
    }

    IEnumerator ExitLevel()
    {
        yield return new WaitForSecondsRealtime(nextLevelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersists>().ResetScenePersist();
        SceneManager.LoadScene (nextSceneIndex);
    }
}
