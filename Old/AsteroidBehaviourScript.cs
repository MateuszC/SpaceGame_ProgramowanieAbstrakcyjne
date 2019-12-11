using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviourScript : MonoBehaviour {

	public float rotationX = 0f;
	public float rotationY = 0f;
	public float rotationZ = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FoxedUpdate () {
		transform.Rotate (new Vector3 (rotationX * Time.fixedDeltaTime, rotationY * Time.fixedDeltaTime, rotationZ * Time.fixedDeltaTime));
	}
}
