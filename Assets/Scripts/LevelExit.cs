    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());            
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceeneIndex = SceneManager.GetActiveScene().buildIndex;
        if(SceneManager.sceneCountInBuildSettings == currentSceeneIndex +1)
        {
            currentSceeneIndex = -1;
        }
        FindObjectOfType<Persist>().ResetScenePersist();
        SceneManager.LoadScene(currentSceeneIndex + 1);
    }
}

