using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateSequencerButtons : MonoBehaviour {

    #region Variables
    Color originalColor = new Color(238,6,155,255);
    [SerializeField]
    private Transform sequencerGrid;

    [SerializeField]
    private GameObject buttonPrefab;

    public static int sizeI = 8;

    public static int sizeJ = 8;

    public static GameObject[,] buttons;

    public static bool[,] beatBool;
    #endregion Variables

    #region Methods
    private void Awake()
    {

        beatBool = new bool[sizeI, sizeJ];

        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                beatBool[i, j] = false;
            }
        }

        buttons = new GameObject[sizeI, sizeJ];

        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                GameObject button = Instantiate(buttonPrefab);
                button.name = "" + i + " " + j;
                button.transform.SetParent(sequencerGrid, false);
                button.GetComponent<Button>().onClick.AddListener(SetForPlay);
                buttons[i, j] = button;
            }
        }

    }

    public void SetForPlay()
    {
        Debug.Log("Button: " + EventSystem.current.currentSelectedGameObject.name);
        string[] ij = EventSystem.current.currentSelectedGameObject.name.Split(' ');
        int i = int.Parse(ij[0]);
        int j = int.Parse(ij[1]);
        if (!beatBool[i, j])
        {
            beatBool[i, j] = true;
            buttons[i, j].GetComponent<Image>().color = Color.blue;
        }
        else
        {
            beatBool[i, j] = false;
            buttons[i, j].GetComponent<Image>().color = originalColor;
        }
    }
    #endregion Methods
}
