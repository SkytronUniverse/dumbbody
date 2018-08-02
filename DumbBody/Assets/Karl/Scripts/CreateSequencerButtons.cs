using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class CreateSequencerButtons : NetworkBehaviour {

    #region Variables

    public static CreateSequencerButtons instance;
    Color originalColor = new Color(238,6,155,255);

    [SerializeField]
    private Transform sequencerGrid;

    [SerializeField]
    private GameObject buttonPrefab;

    public static int sizeI = 8;

    public static int sizeJ = 8;

    public GameObject[,] buttons;

    public bool[,] beatBool;

    [SyncVar(hook = "OnSetButton")]
    public ulong onOff;

    #endregion Variables

    #region Methods

    private void Awake()
    {
        onOff = 0x00000000;
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }
    void Start()
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
        int buttonCount = 0;
        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                GameObject button = Instantiate(buttonPrefab);
                button.name = "" + buttonCount;
                button.transform.SetParent(sequencerGrid, false);
                button.GetComponent<Button>().onClick.AddListener(SetForPlay);
                buttons[i, j] = button;
                buttonCount++;
            }
        }
        
    }
    /*
    public override void OnStartServer()
    {
        onOff = 0x00000000;
    }
    */
    public void SetForPlay()
    {
        //Debug.Log("Button: " + EventSystem.current.currentSelectedGameObject.name);
        int turnOnOff = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        onOff ^= 1ul << turnOnOff;
    }

    public void SetButton(int i , int j)
    {

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
 
    public void OnSetButton(ulong bits)
    {

        for(int i = 0; i < 64; i++)
        {
            //Debug.Log("Setting buttons: " + i / 8 + " " + i % 8);
            Debug.Log((onOff>> i & 1) == 1);
            if ((onOff >> i & 1) == 1)
                buttons[i/8,i%8].GetComponent<Image>().color = Color.blue;
            else
                buttons[i/8, i%8].GetComponent<Image>().color = Color.white;
        }

    }

    #endregion Methods
}
