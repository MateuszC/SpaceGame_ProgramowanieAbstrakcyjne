using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortLivedElement : MonoBehaviour {

    public enum TipoRotac { LookAtThePlayer, FirstPerson, FollowPlayer, Orbital, Stop, StraightStop, OrbitalThatFollows, ETS_StyleCamera }
    [Tooltip("Here you must select the type of rotation and movement that camera will possess.")]
    public TipoRotac rotationType = TipoRotac.LookAtThePlayer;

    private float invokeDestroyTime;
	private bool invokeDestroy = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(invokeDestroy){
			Invoke ("destroyThisElement", invokeDestroyTime);
			invokeDestroy = false;
		}
	}


	public void invokeDestroyIn(float s){
		invokeDestroyTime = s;
		invokeDestroy = true;
	}

	private void destroyThisElement(){
		Destroy (gameObject);
	}



}
