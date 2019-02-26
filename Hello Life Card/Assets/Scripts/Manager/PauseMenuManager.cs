using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void returnButton()
    {
        pauseMenuPanel.SetActive(false);
    }
}
