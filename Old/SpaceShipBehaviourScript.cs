using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipBehaviourScript : MonoBehaviour {

	public UnityEngine.UI.Text log;

	private ParticleSystem particleSystem;
	private ParticleSystem.MainModule particleSystemMainModule;
	private ParticleSystem.EmissionModule particleSystemEmissionModule;
	private Rigidbody rigidbody;
	private AudioSource audio;
	private TurretBehaviourScript[] turretBehaviour;
	private GameObject target;

	private float engineIntensity = 0f;
	private float rotationPerSecond = 30f;
	private Vector3 engineForceVector = new Vector3 (0f,0f,0f);
	private float engineDragPower = 300f;
	private float shipMaxSpeed = 50f;

	// Use this for initialization
	void Start () {
		particleSystem = transform.Find ("EngineParticleSystem").GetComponent<ParticleSystem>();
		particleSystemMainModule = particleSystem.main;
		particleSystemEmissionModule = particleSystem.emission;

		rigidbody = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
		increaseEngineIntensity(0f);
		target = null;

		Transform missileLaunchers = transform.Find ("MissileLaunchers");
		int numberOfMissileLaunchers = 0;
		for (int i = 0; i < missileLaunchers.transform.childCount; i++) {
			if(missileLaunchers.transform.GetChild(i).tag=="MissileLauncher"){
				numberOfMissileLaunchers++;
			}
		}

		turretBehaviour = new TurretBehaviourScript[numberOfMissileLaunchers];
		int numberOfMissileLaunchersIndex = 0;
		for (int i = 0; i < missileLaunchers.transform.childCount; i++) {
			if(missileLaunchers.transform.GetChild(i).tag=="MissileLauncher"){
				turretBehaviour [numberOfMissileLaunchersIndex] = missileLaunchers.transform.GetChild(i).GetComponent<TurretBehaviourScript> ();
				numberOfMissileLaunchersIndex++;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (Input.GetKeyDown (KeyCode.R)) rigidbody.velocity = rigidbody.transform.forward*5000f;

		engineForceVector = rigidbody.transform.forward * engineDragPower * engineIntensity;
		rigidbody.AddForce(engineForceVector,ForceMode.Acceleration);

		if(rigidbody.velocity.magnitude<1f && engineIntensity==0f){
			rigidbody.velocity = Vector3.zero;
		}

		clearText ();
		addText("Velocity = "+rigidbody.velocity);
		addText("Velocity = "+rigidbody.velocity.magnitude);
	}

	public float getEngineIntensity(){ return engineIntensity; }

	public void increaseEngineIntensity(float i){
		engineIntensity += i;
		if (engineIntensity > 1f) engineIntensity = 1f;
		if (engineIntensity < 0f) engineIntensity = 0f;

		particleSystemMainModule.startSize = 2f + 8f * engineIntensity;
		particleSystemEmissionModule.rateOverTime = 50f + 450.0f * engineIntensity;
		audio.pitch = 0.25f + 2f * engineIntensity;
	}

	public void shipRotatePitchUp	(){ rigidbody.transform.Rotate (new Vector3 (rotationPerSecond * Time.fixedDeltaTime,0f,0f)); }
	public void shipRotatePitchDown	(){ rigidbody.transform.Rotate (new Vector3 (-rotationPerSecond * Time.fixedDeltaTime,0f,0f)); }
	public void shipRotateTurnLeft	(){ rigidbody.transform.Rotate (new Vector3 (0f,-rotationPerSecond * Time.fixedDeltaTime,0f)); }
	public void shipRotateTurnRight	(){ rigidbody.transform.Rotate (new Vector3 (0f,rotationPerSecond * Time.fixedDeltaTime,0f)); }
	public void shipRotateRollLeft	(){ rigidbody.transform.Rotate (new Vector3 (0f,0f,rotationPerSecond * Time.fixedDeltaTime)); }
	public void shipRotateRollRight	(){ rigidbody.transform.Rotate (new Vector3 (0f,0f,-rotationPerSecond * Time.fixedDeltaTime)); }

	public void shootTurrets(){
		for(int i=0;i<turretBehaviour.Length;i++){
			turretBehaviour [i].shoot (target);
		}
	}

	public void reloadTurrets(){
		for(int i=0;i<turretBehaviour.Length;i++){
			turretBehaviour [i].reload ();
		}
	}

	public void setTarget(GameObject t){
		target = t;
	}




	private void clearText(){
		log.text = "";
	}

	private void addText(string text){
		log.text += text+"\n";
	}
}













