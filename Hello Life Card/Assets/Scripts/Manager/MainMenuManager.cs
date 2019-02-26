using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void InstructionButton()
    {
        instructionPanel.SetActive(true);
    }

    public void returnButton()
    {
        instructionPanel.SetActive(false);
    }

}
