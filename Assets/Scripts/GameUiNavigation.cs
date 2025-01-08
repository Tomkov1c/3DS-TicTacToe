using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUiNavigation : MonoBehaviour, INavigationInterface
{

	public GameObject EventHandler;

	public GameObject Sign;

    public Sprite x;
    public Sprite o;

    public bool IsEmpty = true;

    [Header("Grid Positions")]
    public int GridX;
    public int GridY;

    private GameEventHandler eventHandler;
	private Image sign;

    void Start() 
	{
		sign = Sign.GetComponent<Image>();
        sign.enabled = false;

		eventHandler = EventHandler.GetComponent<GameEventHandler>();

        string[] parts = this.name.Split(' ');

        if (parts.Length == 2)
        {
            GridX = int.Parse(parts[0].Substring(1));
            GridY = int.Parse(parts[1]);
        }
    }

	void Update()
	{
        
    }

    void GetFromTable()
    {
        char thisTTTs = eventHandler.table[GridX - 1, GridY - 1];
        if (thisTTTs == 'X')
        {
            sign.enabled = true;
            sign.sprite = x;
        }
        else if (thisTTTs == 'O')
        {
            sign.enabled = true;
            sign.sprite = o;
        }
    }

    public void OnSelect()
    {
        if (IsEmpty)
        {
            char xoro = ' ';
            if (eventHandler.XTurn)
            {
                sign.sprite = x;
                xoro = 'X';
            }
            else if (eventHandler.OTurn)
            {
                sign.sprite = o;
                xoro = 'O';
            }
            sign.enabled = true;
            eventHandler.Switch();
            eventHandler.turn++;
            eventHandler.table[GridX - 1, GridY - 1] = xoro;
            this.IsEmpty = false;
        }

    }

    public void OnBack()
    {
        if(eventHandler.gameEnded)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OnStart()
    {
        if (eventHandler.gameEnded)
        {
            SceneManager.LoadScene(2);
        }
    }
}
