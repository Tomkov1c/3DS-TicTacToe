using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.N3DS;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEventHandler : MonoBehaviour {

    [Space]
	public bool XTurn = true;
	public bool OTurn = false;

    [Space]
    public GameObject Player1Name;
    public GameObject Player2Name;
    public GameObject Player1Score;
    public GameObject Player2Score;

    [Space]
    public int turn = 0;

	public char[,] table = new char[3, 3];

    public bool gameEnded = false;

    [Header("Prompts")]
    public GameObject BackButton;
    public GameObject RestartButton;

    public void Switch()
	{
        this.XTurn = !XTurn;
        this.OTurn = !OTurn;
    }

    void Start()
    {
        LoadUpperLCD();
        BackButton.active = false;
        RestartButton.active = false;
    }

    void LoadUpperLCD()
    {
        Player1Name.GetComponent<Text>().text = PlayerPrefs.GetString("Player1Name");
        Player2Name.GetComponent<Text>().text = PlayerPrefs.GetString("Player2Name");

        Player1Score.GetComponent<Text>().text = PlayerPrefs.GetInt("Player1Score").ToString();
        Player2Score.GetComponent<Text>().text = PlayerPrefs.GetInt("Player2Score").ToString();
    }

    void Update()
    {
        CheckGameStatus();
    }

    public void CheckGameStatus()
    {

        if(!gameEnded)
        {
            if (CheckWinner('X'))
            {
                DisableAllTTTNavigation();
                gameEnded = true;

                string key = "Player1Score";

                if (PlayerPrefs.HasKey(key))
                {
                    PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
                }
                else
                {
                    PlayerPrefs.SetInt(key, 1);
                }
                LoadUpperLCD();
                BackButton.active = true;
                RestartButton.active = true;
            }
            else if (CheckWinner('O'))
            {
                DisableAllTTTNavigation();
                gameEnded = true;

                string key = "Player2Score";

                if (PlayerPrefs.HasKey(key))
                {
                    PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
                }
                else
                {
                    PlayerPrefs.SetInt(key, 1);
                }
                LoadUpperLCD();
                BackButton.active = true;
                RestartButton.active = true;
            }
            else if (turn >= 9)
            {
                DisableAllTTTNavigation();
                gameEnded = true;
                BackButton.active = true;
                RestartButton.active = true;
            }
        }
    }

    private void DisableAllTTTNavigation()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("TTT Button");
        foreach (GameObject button in buttons)
        {
            NavigationManager nav = button.GetComponent<NavigationManager>();
            if (nav != null)
            {
                nav.IsPressEnabled = false;
                nav.ImageComponent.sprite = nav.ThisNoHover;
                nav.BackIsEnabled = true;
                nav.IsHoverEnabled = false;
            }
        }
    }

    private bool CheckWinner(char player)
    {
        for (int i = 0; i < 3; i++)
        {
            // Check rows
            if (table[i, 0] == player && table[i, 1] == player && table[i, 2] == player)
                return true;

            // Check columns
            if (table[0, i] == player && table[1, i] == player && table[2, i] == player)
                return true;
        }

        // Check diagonals
        if (table[0, 0] == player && table[1, 1] == player && table[2, 2] == player)
            return true;
        if (table[0, 2] == player && table[1, 1] == player && table[2, 0] == player)
            return true;

        return false;
    }
}