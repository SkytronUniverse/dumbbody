﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class CreateSequencerButtons : NetworkBehaviour {

    #region Variables

    Color originalColor = new Color(238,6,155,255);

    [SerializeField]
    private Transform sequencerGrid;

    [SerializeField]
    private GameObject buttonPrefab;

    public static int sizeI = 8;

    public static int sizeJ = 8;

    public  GameObject[,] buttons;

    public int buttonNumber;

    public string buttonName;

    #endregion Variables

    #region Methods

    
    void Start()
    {

        //Creates buttons for sequencer
        buttons = new GameObject[sizeI, sizeJ];
        int buttonCount = 0;
        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                GameObject button = (GameObject)Instantiate(buttonPrefab);
                button.transform.SetParent(sequencerGrid, false);
                button.name = "" + buttonCount;
                button.GetComponent<Button>().onClick.AddListener(GetButtonName);
                button.SetActive(true);
                buttons[i, j] = button;
                buttonCount++;
            }
        }
        
    }

    // Used to get button name that is then passed to the Command in DetectButtons.cs
    public void GetButtonName()
    {
        //Debug.Log("The button have been clicked in GetButtonName: " + EventSystem.current.currentSelectedGameObject.name);
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        buttonNumber = int.Parse(buttonName);
        DetectButtons.clicked = true;
    }


    #endregion Methods
}
