using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {

    public static bool clicked = false;

    [HideInInspector]
    public const float MAXSPEED = 1.5f;

    [HideInInspector]
    public float speed = 0.0f;

    [HideInInspector]
    public Transform head;

    int moveTimer = 0;
    private Transform cam;
    public Transform camParent;

	public override void OnStartLocalPlayer () {
        transform.Find("Torso").GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        head = transform.Find("Head");
        cam = Camera.main.transform;
        camParent = cam.parent;
        camParent.position = head.position;
	}
	

	void Update () {

        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButton(0))
            ButtonHandler();

        camParent.position = head.position;

        Vector3 turn = cam.eulerAngles;
        turn.x = 0f;
        turn.z = 0f;
        transform.eulerAngles = turn;

	}

    public void ButtonHandler()
    {
        if (Input.GetMouseButton(0) && moveTimer > 20) // if mouse button is being held down for certain amount of time
        {
            if (Input.GetMouseButtonDown(0)) { moveTimer = 0; speed = 0.0f; return; } // if button is released, reset variables and exit function
            if (speed < MAXSPEED) // accellerate
            {
                camParent.position = head.position;
                Vector3 moveDirection = cam.forward.normalized; // direction to move player towards
                moveDirection.y = 0f; // do not change y-position
                transform.Translate(transform.InverseTransformDirection(moveDirection) * speed);
                speed += 0.005f; //increase speed

            }
            else
            { //if MAXSPEED reached, continue at that rate
                camParent.position = head.position;
                Vector3 moveDirection = cam.forward.normalized;
                moveDirection.y = 0f;
                transform.Translate(transform.InverseTransformDirection(moveDirection) * MAXSPEED);

            }
        }
        else
        {
            //if button not held down long enough, shoot projectile
            if (Input.GetMouseButtonDown(0))
            {
                moveTimer = 0; // reset time
            }
            else
                moveTimer++; //otherwise add time if button is being held down
        }
    }

    //To perform action of changing button colors on client side
    [ClientRpc]
    public void RpcChangeBits(ulong bits)
    {
        if (!isLocalPlayer)
            return;

        for (int i = 0; i < 64; i++)
        {
            Debug.Log((bits >> i & 1) == 1);
            if ((bits >> i & 1) == 1)
            {
                Debug.Log("Setting buttons: " + i / 8 + " " + i % 8);
                CreateSequencerButtons.buttons[i / 8, i % 8].GetComponent<Image>().color = Color.blue;
            }
            else
                CreateSequencerButtons.buttons[i / 8, i % 8].GetComponent<Image>().color = Color.white;
        }
    }
}
