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
        SystemManager.instance.BackToMenu();
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
