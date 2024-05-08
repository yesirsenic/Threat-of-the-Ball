using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainmenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Threat of the Ball");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
