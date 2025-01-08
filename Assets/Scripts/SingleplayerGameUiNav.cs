using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleplayerGameUiNav : MonoBehaviour, INavigationInterface
{
    public GameObject EventHandler;
    public GameObject Sign;
    public Sprite x;
    public Sprite o;
    public bool IsEmpty = true;

    [Header("Grid Positions")]
    public int GridX;
    public int GridY;

    private SingleplayerGameEventHandler eventHandler;
    private Image sign;

    private bool SelectLifted;

    void Start()
    {
        sign = Sign.GetComponent<Image>();
        sign.enabled = false;
        eventHandler = EventHandler.GetComponent<SingleplayerGameEventHandler>();

        string[] parts = this.name.Split(' ');

        if (parts.Length == 2)
        {
            GridX = int.Parse(parts[0].Substring(1));
            GridY = int.Parse(parts[1]);
        }
    }

    void Update()
    {
        GetFromTable();
        if(GamePad.GetButtonTrigger(N3dsButton.A) && !GamePad.GetButtonTrigger(N3dsButton.Start))
        {
            SelectLifted = true;
        }else
        {
            SelectLifted = false;
        }
    }

    void GetFromTable()
    {
        char thisTTTs = eventHandler.table[GridX - 1, GridY - 1];
        if (thisTTTs == 'X')
        {
            sign.enabled = true;
            sign.sprite = x;
            IsEmpty = false;
        }
        else if (thisTTTs == 'O')
        {
            sign.enabled = true;
            sign.sprite = o;
            IsEmpty = false;
        }
    }

    public void OnSelect()
    {
        if (IsEmpty && !eventHandler.gameEnded)
        {
            this.IsEmpty = false;
            char xoro = ' ';
            if (eventHandler.XTurn)
            {
                sign.sprite = x;
                xoro = 'X';
            }
            sign.enabled = true;
            eventHandler.Switch();
            eventHandler.turn++;
            eventHandler.table[GridX - 1, GridY - 1] = xoro;
        }

    }

    public void OnBack()
    {
        if(eventHandler.gameEnded)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnStart()
    {
        if(eventHandler.gameEnded && SelectLifted)
        {
            SceneManager.LoadScene(3);
        }
    }
}
