using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreenNavActions : MonoBehaviour, INavigationInterface {


	public void OnSelect()
	{
        PlayerPrefs.DeleteKey("Player1Name");
        PlayerPrefs.DeleteKey("Player2Name");
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        PlayerPrefs.DeleteKey("Player1Score");
        PlayerPrefs.DeleteKey("Player2Score");

        switch (this.name)
        {
            case "Singleplayer":
                SceneManager.LoadScene(3);
                break;

            case "Multiplayer":
                SceneManager.LoadScene(1);
                break;

            default:
                break;
        }
    }

    public void OnBack()
    {

    }

    public void OnStart()
    {
        PlayerPrefs.DeleteKey("Player1Name");
        PlayerPrefs.DeleteKey("Player2Name");
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        PlayerPrefs.DeleteKey("Player1Score");
        PlayerPrefs.DeleteKey("Player2Score");
        switch (this.name)
        {
            case "Singleplayer":
                SceneManager.LoadScene(3);
                break;

            case "Multiplayer":
                SceneManager.LoadScene(1);
                break;

            default:
                break;
        }
    }

}
