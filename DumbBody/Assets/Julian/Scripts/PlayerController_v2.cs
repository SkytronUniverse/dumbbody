using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_v2 : MonoBehaviour {
    [HideInInspector]
    public const float MAXSPEED = 1.5f;
    [HideInInspector]
    public float speed = 0.0f;

    int moveTimer = 0;
    public Transform cam;
    public Transform camParent;

    void Start () {
        cam = Camera.main.transform;
        camParent = cam.parent;
    }
    

    void Update () {

        if(Input.GetMouseButton(0))
            ButtonHandler();
       
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
                Vector3 moveDirection = cam.forward.normalized; // direction to move player towards
                moveDirection.y = 0f; // do not change y-position
                transform.Translate(transform.InverseTransformDirection(moveDirection) * speed);
                speed += 0.005f; //increase speed

            }
            else
            { //if MAXSPEED reached, continue at that rate
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

}
