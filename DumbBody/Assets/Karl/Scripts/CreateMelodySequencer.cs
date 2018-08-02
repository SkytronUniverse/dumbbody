using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateMelodySequencer : MonoBehaviour {

    #region Variables
    [SerializeField]
    private Transform sequencerGrid;

    [SerializeField]
    private GameObject buttonPrefab;

    public static int sizeI = 8;

    public static int sizeJ = 8;

    public static GameObject[,] melodyButtons;

    public static bool[,] melodyBool;
    #endregion Variables

    #region Methods
    private void Awake()
    {

        melodyBool = new bool[sizeI, sizeJ];

        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                melodyBool[i, j] = false;
            }
        }

        melodyButtons = new GameObject[sizeI, sizeJ];

        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                GameObject button = Instantiate(buttonPrefab);
                button.name = "" + i + " " + j;
                button.transform.SetParent(sequencerGrid, false);
                button.GetComponent<Button>().onClick.AddListener(SetForPlay);
                melodyButtons[i, j] = button;
            }
        }

    }

    public void SetForPlay()
    {
        Debug.Log("Button: " + EventSystem.current.currentSelectedGameObject.name);
        string[] ij = EventSystem.current.currentSelectedGameObject.name.Split(' ');
        int i = int.Parse(ij[0]);
        int j = int.Parse(ij[1]);
        if (!melodyBool[i, j])
        {
            melodyBool[i, j] = true;
            melodyButtons[i, j].GetComponent<Image>().color = Color.blue;
        }
        else
        {
            melodyBool[i, j] = false;
            melodyButtons[i, j].GetComponent<Image>().color = Color.white;
        }
    }
    #endregion Methods
}
