using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private float switchDelay = 2f; // Time in seconds before switching scenes

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(SwitchSceneWithDelay());
        }
    }

    private IEnumerator SwitchSceneWithDelay()
    {
        yield return new WaitForSeconds(switchDelay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Prevents loading out-of-range scene
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels!"); // You can handle game completion here
        }
    }
}
