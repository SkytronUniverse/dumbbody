using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Spawn an Object to keep track of the state of buttons on or off
 * 
 * Attached to SequencerStateObjectSpawner
 */
public class SpawnSequencerStateObject : NetworkBehaviour {

    public GameObject sequencerStateObject;

    public override void OnStartServer()
    {
        var spawnPosition = transform.position;

        var sequencerStatus = (GameObject)Instantiate(sequencerStateObject, spawnPosition, Quaternion.identity);

        NetworkServer.Spawn(sequencerStatus);
    }
}
