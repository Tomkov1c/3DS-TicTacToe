using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MultiplayerMenuNavActions : MonoBehaviour , INavigationInterface
{
    public Text P1Name;
    public Text P2Name;

    private bool forP1;
    private bool forP2;

    public void OnSelect()
    {
        switch (this.name)
        {
            case "Player 1":
                UnityEngine.N3DS.Keyboard.Show();
                forP1 = true;

                break;

            case "Player 2":
                UnityEngine.N3DS.Keyboard.Show();
                forP2 = true;

                break;

            case "Start":
                SceneManager.LoadScene(2);
                break;

            default:

                break;
        }
    }

    public void OnBack()
    {
        SceneManager.LoadScene(0);
    }

    public void OnStart()
    {

    }

    void Update()
    {
        if ((UnityEngine.N3DS.Keyboard.GetResult() == (int)N3dsKeyboardResult.Okay) && forP1)
        {
            P1Name.text = UnityEngine.N3DS.Keyboard.GetText();
            forP1 = false;
        }
        else if ((UnityEngine.N3DS.Keyboard.GetResult() == (int)N3dsKeyboardResult.Okay) && forP2)
        {
            P2Name.text = UnityEngine.N3DS.Keyboard.GetText();
            forP2 = false;
        }
        PlayerPrefs.SetString("Player1Name", P1Name.text);
        PlayerPrefs.SetString("Player2Name", P2Name.text);
    }
}
