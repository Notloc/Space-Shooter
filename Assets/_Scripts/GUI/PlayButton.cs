namespace SpaceAdventure.GUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayButton : MonoBehaviour
    {
        [SerializeField]
        string sceneName;

        public void StartGame()
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}