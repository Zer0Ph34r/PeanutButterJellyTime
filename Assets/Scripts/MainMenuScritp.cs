using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScritp : MonoBehaviour {

	public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleButton(Button button)
    {
        if (button.IsActive())
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
