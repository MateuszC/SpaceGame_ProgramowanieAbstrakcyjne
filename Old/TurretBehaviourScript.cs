using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviourScript : MonoBehaviour {

	public GameObject spaceShip;
	public GameObject missilePrefab;

	private AudioSource audio;
	private Transform rocketLauncherHead;
	private Renderer[] lamps;
	private Color[] lampsColor;
	private Transform[] shootPoints;
	private GameObject[] missiles;
	private bool[] missilesReady;
	private float reloadCooldown = 3f;
	private float reloadLastTime = -100f;
	private float shootCooldown = 1f;
	private float shootLastTime = -100f;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		rocketLauncherHead = transform.Find ("RocketLauncherHead");

		lamps = new Renderer[3];
		lampsColor = new Color[3];
		lamps[0] = rocketLauncherHead.Find ("Lamp1").GetComponent<Renderer>();
		lamps[1] = rocketLauncherHead.Find ("Lamp2").GetComponent<Renderer>();
		lamps[2] = rocketLauncherHead.Find ("Lamp3").GetComponent<Renderer>();

		int numberOfShootPoints = 0;
		for (int i = 0; i < rocketLauncherHead.transform.childCount; i++) {
			if(rocketLauncherHead.transform.GetChild(i).tag == "MissileLauncherShootPoint"){
				numberOfShootPoints++;
			}
		}

		shootPoints = new Transform[numberOfShootPoints];
		int numberOfShootPointsIndex = 0;
		for (int i = 0; i < rocketLauncherHead.transform.childCount; i++) {
			if(rocketLauncherHead.transform.GetChild(i).tag == "MissileLauncherShootPoint"){
				shootPoints [numberOfShootPointsIndex] = rocketLauncherHead.transform.GetChild (i).transform;
				numberOfShootPointsIndex++;
			}
		}

		missiles = new GameObject[numberOfShootPoints];
		missilesReady = new bool[numberOfShootPoints];
		for (int i = 0; i < missiles.Length; i++) {
			missiles [i] = null;
			missilesReady [i] = false;
		}

		setLamps ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void reload(){
		if (canReload()) {
			if (isReloadNotOnCooldown ()) {
				reloadLastTime = Time.fixedTime;

				for (int i = 0; i < missiles.Length; i++) {
					if (missilesReady [i]==false) {
						missiles [i] = (GameObject) Instantiate (missilePrefab, shootPoints [i].transform.position, shootPoints [i].transform.rotation, shootPoints [i].transform);
						//missiles [i].transform.localScale = transform.localScale*1.8f;
						missiles [i].GetComponent<MissileBehaviourScript> ().startReloadingAnimation ();
						missilesReady [i] = true;
						//Debug.Log ("reloaded missile "+i);
					}
				}

				Invoke ("setLamps",reloadCooldown+0.1f);
			} else {
				//Debug.Log ("reloading on cooldown");
			}
		} else {
			//Debug.Log ("reloading NOT needed, all missiles in position");
		}

		setLamps ();
	}

	private void setLamps(){
		for (int i = 0; i < lampsColor.Length; i++) lampsColor [i] = Color.red;

		if (isReloadNotOnCooldown () == false) {
			for (int i = 0; i < lampsColor.Length; i++) lampsColor [i] = Color.yellow;
		}

		if (isReloadNotOnCooldown () && canShoot ()) {
			int missilesReady = numberOfMissilesReady ();
			for (int i = 0; i < lampsColor.Length; i++) if(missilesReady>=i+1) lampsColor [lampsColor.Length-1-i] = Color.green;
		}

		for (int i = 0; i < lamps.Length; i++) lamps [i].material.SetColor("_EmissionColor", lampsColor [i]);
	}

	private bool isReloadNotOnCooldown(){
		return reloadLastTime + reloadCooldown <= Time.fixedTime;
	}

	private bool isShootNotOnCooldown(){
		return shootLastTime + shootCooldown <= Time.fixedTime;
	}

	private bool canShoot(){
		return numberOfMissilesReady() > 0;
	}

	private bool canReload(){
		for (int i = 0; i < missilesReady.Length; i++) {
			if (missilesReady [i] == false) return true;
		}
		return false;
	}

	private int numberOfMissilesReady(){
		int ready = 0;
		for (int i = 0; i < missilesReady.Length; i++) {
			if (missilesReady [i]) ready++;
		}
		return ready;
	}

	public void shoot(GameObject target){
		bool shootRandomMissile = true;
		if (isShootNotOnCooldown () && isReloadNotOnCooldown() && canShoot()) {
			shootLastTime = Time.fixedTime;

			int missileIndex = -1;
			if (shootRandomMissile) {
				int[] readyIndexes = new int[numberOfMissilesReady()];
				int readyIndex = 0;
				for (int i = 0; i < missilesReady.Length; i++) {
					if(missilesReady [i]){
						readyIndexes [readyIndex] = i;
						readyIndex++;
					}
				}
				missileIndex = readyIndexes[Random.Range(0,readyIndexes.Length)];
			} else {
				for (int i = 0; i < missilesReady.Length; i++) {
					if(missilesReady [i]){
						missileIndex = i;
						break;
					}
				}
			}

			if (missileIndex>=0) {
				missiles [missileIndex].transform.parent = null;
				missiles [missileIndex].GetComponent<MissileBehaviourScript> ().setInitialVelocity (spaceShip.GetComponent<Rigidbody>().velocity);
				missiles [missileIndex].GetComponent<MissileBehaviourScript> ().setLaunchRandomRotation (10f);
				missiles [missileIndex].GetComponent<MissileBehaviourScript> ().launch ();
				missiles [missileIndex].GetComponent<MissileBehaviourScript> ().setTarget (target);
				missilesReady [missileIndex] = false;
				audio.Play ();
			}
		} else {
			//Debug.Log ("CANT shoot");
		}

		setLamps ();
	}




	private void testShoot(){
		for (int i = 0; i < missiles.Length; i++) {
			missiles [i].GetComponent<MissileBehaviourScript> ().launch ();
			missilesReady [i] = false;
		}
	}
}
