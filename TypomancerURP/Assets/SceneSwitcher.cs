using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private float switchDelay = 2f; // Time in seconds before switching scenes
    [SerializeField] private Animator animator;
    [SerializeField] private bool autoswitch;


    [SerializeField] private int sceneToLoad;
    [SerializeField] private bool specialLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)|| 
            Input.GetKeyDown(KeyCode.Return) || 
            Input.GetKeyDown(KeyCode.KeypadEnter) ||
            Input.GetKeyDown(KeyCode.JoystickButton5) ||
            Input.GetKeyDown(KeyCode.JoystickButton4)
            )
        {
            animator.Play("FadeOut");
            StartCoroutine(SwitchSceneWithDelay());
        }

        if (autoswitch)
        { 
            StartCoroutine(AutoSwitchWithFadeOut());
            autoswitch = false;
        }
    }

    private IEnumerator SwitchSceneWithDelay()
    {
        yield return new WaitForSeconds(switchDelay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(specialLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No more levels!");
            }
        }



    }

    private IEnumerator AutoSwitchWithFadeOut()
    {
        yield return new WaitForSeconds(switchDelay - 1f); // Trigger fade-out 1 second before switching
        if (animator != null)
        {
            animator.Play("FadeOut");
        }


        yield return new WaitForSeconds(1f); // Wait the last second for fade-out animation
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels!");
        }
    }
}
