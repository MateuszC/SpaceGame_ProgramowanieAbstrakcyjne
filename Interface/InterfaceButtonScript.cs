using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceButtonScript : MonoBehaviour {


    public ObjectInSpace playerShip;
    public InterfaceScript interfaceScript;
    public GameObject instructionsPanel;

    private GMS gms = GMS.instance();
    private ObjectInSpace owner;

    void Start () {
        owner = playerShip;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.V)) owner.increaseEngineIntensity(1f);
        if (Input.GetKey(KeyCode.C)) owner.increaseEngineIntensity(1f / 3f * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.X)) owner.increaseEngineIntensity(-1f / 3f * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.Z)) owner.increaseEngineIntensity(-1f);

        if (Input.GetKey(KeyCode.W)) owner.turnUp();
        if (Input.GetKey(KeyCode.S)) owner.turnDown();
        if (Input.GetKey(KeyCode.A)) owner.turnLeft();
        if (Input.GetKey(KeyCode.D)) owner.turnRight();
        if (Input.GetKey(KeyCode.Q)) owner.turnRollLeft();
        if (Input.GetKey(KeyCode.E)) owner.turnRollRight();

        if (Input.GetKey(KeyCode.BackQuote)) interfaceScript.clearSelection();
        if (Input.GetKey(KeyCode.Alpha1)) interfaceScript.equipmentSetState(Equipment.State.TurnedOff);
        if (Input.GetKey(KeyCode.Alpha2)) interfaceScript.equipmentSetState(Equipment.State.TurnedOn);
        if (Input.GetKey(KeyCode.Alpha3)) interfaceScript.equipmentSetState(Equipment.State.Active);
        if (Input.GetKey(KeyCode.Alpha4)) interfaceScript.equipmentSetState(Equipment.State.Overdrive);
        
        if (Input.GetKeyDown(KeyCode.N)) toggleInterfaceVisibility();

        if (Input.GetKeyDown(KeyCode.I)) toggleInstructionsPanel();
    }

    private bool isVisible = true;
    public void toggleInterfaceVisibility()
    {
        if (isVisible)
        {
            interfaceScript.gameObject.SetActive(false);
            isVisible = false;
        }
        else
        {
            interfaceScript.gameObject.SetActive(true);
            isVisible = true;
        }
    }

    private bool instructionsPanelVisible = true;
    public void toggleInstructionsPanel() {
        if (instructionsPanelVisible) {
            instructionsPanelVisible = false;
            instructionsPanel.SetActive(false);
        } else {
            instructionsPanelVisible = true;
            instructionsPanel.SetActive(true);
        }
    }

}
