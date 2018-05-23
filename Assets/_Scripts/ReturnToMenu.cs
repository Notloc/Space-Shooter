using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{

    [SerializeField] string menuSceneName;
    [SerializeField] float delay;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ReturnToMainMenu(delay, menuSceneName));
	}

    IEnumerator ReturnToMainMenu(float delay, string sceneName)
    {

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(menuSceneName);
    }
}
