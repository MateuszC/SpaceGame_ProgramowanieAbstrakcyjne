using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	private GMS gms = GMS.instance();

    public InterfaceScript gameInterface;

	private GameObject owner = null;
	private SpaceShipScript ownerShipScript = null;

	void Start () {
		owner = GameObject.Find("SpaceShip");
		ownerShipScript = owner.GetComponent<SpaceShipScript> ();
	}

	void FixedUpdate () {
        /*
		if (Input.GetKey (KeyCode.V)) ownerShipScript.increaseEngineIntensity (1f);
		if (Input.GetKey (KeyCode.C)) ownerShipScript.increaseEngineIntensity (1f/3f * Time.fixedDeltaTime);
		if (Input.GetKey (KeyCode.X)) ownerShipScript.increaseEngineIntensity (-1f/3f * Time.fixedDeltaTime);
		if (Input.GetKey (KeyCode.Z)) ownerShipScript.increaseEngineIntensity (-1f);

		if (Input.GetKey (KeyCode.W)) ownerShipScript.turnUp ();
		if (Input.GetKey (KeyCode.S)) ownerShipScript.turnDown ();
		if (Input.GetKey (KeyCode.A)) ownerShipScript.turnLeft ();
		if (Input.GetKey (KeyCode.D)) ownerShipScript.turnRight ();
		if (Input.GetKey (KeyCode.Q)) ownerShipScript.turnRollLeft ();
		if (Input.GetKey (KeyCode.E)) ownerShipScript.turnRollRight ();

        if (Input.GetKey(KeyCode.BackQuote)) gameInterface.clearSelection();
        if (Input.GetKey(KeyCode.Alpha1)) gameInterface.equipmentSetState(Equipment.State.TurnedOff);
        if (Input.GetKey(KeyCode.Alpha2)) gameInterface.equipmentSetState(Equipment.State.TurnedOn);
        if (Input.GetKey(KeyCode.Alpha3)) gameInterface.equipmentSetState(Equipment.State.Active);
        if (Input.GetKey(KeyCode.Alpha4)) gameInterface.equipmentSetState(Equipment.State.Overdrive);
        */
    }


}
