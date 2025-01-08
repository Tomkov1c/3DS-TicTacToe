using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleplayerGameEventHandler : MonoBehaviour
{
    [Space]
    public bool XTurn = true;
    public bool OTurn = false;

    [Space]
    public GameObject Player1Name;
    public GameObject Player1Score;
    public GameObject PlayerWinStreak;

    [Space]
    public int turn = 0;
    public char[,] table = new char[3, 3];

    public bool gameEnded = false;

    [Header("Prompts")]
    public GameObject BackButton;
    public GameObject RestartButton;

    private int winStreak = 0;
    private bool aiPlaced = false;

    void Start()
    {
        LoadUpperLCD();
        BackButton.SetActive(false);
        RestartButton.SetActive(false);

        if(!PlayerPrefs.HasKey("PlayerStreak"))
        {
            PlayerPrefs.SetInt("PlayerStreak", 0);
        }
    }

    void LoadUpperLCD()
    {
        string user;
        bool profane;
        UnityEngine.N3DS.Config.GetUserName(out user, out profane);
        Player1Name.GetComponent<Text>().text = user + " score streak";
        Player1Score.GetComponent<Text>().text = PlayerPrefs.GetInt("Player1Score").ToString();
        winStreak = PlayerPrefs.GetInt("Player1Score");

        if (PlayerPrefs.GetInt("PlayerStreak") < winStreak)
        {
            PlayerPrefs.SetInt("PlayerStreak", winStreak);
            PlayerPrefs.Save();
        }

        PlayerWinStreak.GetComponent<Text>().text = "Win streak: " + PlayerPrefs.GetInt("PlayerStreak").ToString();
    }

    void Update()
    {
        CheckGameStatus();

        if (OTurn && !gameEnded && !aiPlaced)
        {
            AIPlayerMove();
        }
    }

    public void Switch()
    {
        this.XTurn = !XTurn;
        this.OTurn = !OTurn;
    }

    void AIPlayerMove()
    {
        aiPlaced = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (table[i, j] != 'X' && table[i, j] != 'O')
                {
                    table[i, j] = 'O';
                    if (CheckWinner('O'))
                    {
                        Switch();
                        aiPlaced = false;
                        turn++;
                        return;
                    }
                    table[i, j] = ' ';
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (table[i, j] != 'X' && table[i, j] != 'O')
                {
                    table[i, j] = 'X';
                    if (CheckWinner('X'))
                    {
                        table[i, j] = 'O';
                        Switch();
                        aiPlaced = false;
                        turn++;
                        return;
                    }
                    table[i, j] = ' ';
                }
            }
        }

        // If no winning or blocking moves, pick random
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (table[i, j] != 'X' && table[i, j] != 'O')
                {
                    table[i, j] = 'O';
                    Switch();
                    aiPlaced = false;
                    turn++;
                    return;
                }
            }
        }
    }

    public void CheckGameStatus()
    {

        if (!gameEnded)
        {
            if (CheckWinner('X'))
            {
                DisableAllTTTNavigation();
                gameEnded = true;
                UpdateScore("Player1Score");
                winStreak++;
                LoadUpperLCD();
                BackButton.SetActive(true);
                RestartButton.SetActive(true);
            }
            else if (CheckWinner('O'))
            {
                DisableAllTTTNavigation();
                gameEnded = true;
                UpdateScore("Player2Score");
                LoadUpperLCD();
                winStreak = 0;
                PlayerPrefs.SetInt("Player1Score", 0);
                BackButton.SetActive(true);
                RestartButton.SetActive(true);
            }
            else if (turn >= 9)
            {
                DisableAllTTTNavigation();
                gameEnded = true;
                BackButton.SetActive(true);
                RestartButton.SetActive(true);
            }
        }
    }

    private void UpdateScore(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 1);
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
