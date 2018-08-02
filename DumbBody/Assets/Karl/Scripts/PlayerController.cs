using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {

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

        if(Input.GetMouseButton(0))
            ButtonHandler();

        camParent.position = head.position;

        Vector3 turn = cam.eulerAngles;
        turn.x = 0f;
        turn.z = 0f;
        transform.eulerAngles = turn;

        // For debugging
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.Log("hit: " + hit.collider.gameObject.name);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicked" + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }

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
    /*
    public void OnSetButton(ulong bits)
    {

        for (int i = 0; i < 64; i++)
        {
            //Debug.Log("Setting buttons: " + i / 8 + " " + i % 8);
            Debug.Log((onOff >> i & 1) == 1);
            if ((onOff >> i & 1) == 1)
                CreateSequencerButtons.instance.buttons[i / 8, i % 8].GetComponent<Image>().color = Color.blue;
            else
                CreateSequencerButtons.instance.buttons[i / 8, i % 8].GetComponent<Image>().color = Color.white;
        }

        CmdPressed(bits);
    }

    [Command]
    public void CmdPressed(ulong bits)
    {
        onOff = bits;
    }
    */
}
