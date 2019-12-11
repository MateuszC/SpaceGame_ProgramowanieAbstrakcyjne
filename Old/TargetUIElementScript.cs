using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIElementScript : MonoBehaviour {

	private GameObject spaceShip;
	private GameObject target;
	private UnityEngine.UI.Button button;

	// Use this for initialization
	void Start () {
		transform.Find("TargetName").GetComponent<UnityEngine.UI.Text>().text = target.name;
		button = GetComponent<UnityEngine.UI.Button> ();
		button.onClick.AddListener (onClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick(){
		spaceShip.GetComponent<SpaceShipBehaviourScript> ().setTarget (target);
	}


	public void setSpaceShip(GameObject s){
		spaceShip = s;
	}

	public void setTarget(GameObject t){
		target = t;
	}

	public GameObject getTarget(){
		return target;
	}
}
