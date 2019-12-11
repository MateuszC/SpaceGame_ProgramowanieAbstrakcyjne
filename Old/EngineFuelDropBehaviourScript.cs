using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineFuelDropBehaviourScript : MonoBehaviour {

	private float initialScaleX;
	private float initialScaleY;
	private float initialScaleZ;
	private float timeToDissapear;
	private float changeScalePerSecond;

	// Use this for initialization
	void Start () {
		initialScaleX = transform.localScale.x;
		initialScaleY = transform.localScale.y;
		initialScaleZ = transform.localScale.z;
		timeToDissapear = Random.Range (0.5f, 1.5f);
		changeScalePerSecond = 60f;

		InvokeRepeating ("changeScale",0.5f,1f/changeScalePerSecond);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void changeScale(){
		transform.localScale -= new Vector3(initialScaleX/(changeScalePerSecond*timeToDissapear), initialScaleY/(changeScalePerSecond*timeToDissapear), initialScaleZ/(changeScalePerSecond*timeToDissapear));
		if(transform.localScale.x<0){
			Destroy (gameObject);
		}
	}
}
