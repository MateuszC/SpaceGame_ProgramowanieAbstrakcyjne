using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviourScript : MonoBehaviour {

	public GameObject explosionPrefab;

	private Rigidbody rigidBody;
	private AudioSource audio;
	private CapsuleCollider capsuleCollider;
	private ParticleSystem particleSystem;
	private ParticleSystem.MainModule particleSystemMainModule;
	private ParticleSystem.EmissionModule particleSystemEmissionModule;
	private Transform missileBody;
	private Transform missileHead;
	private Transform missileBodyPositionStart;
	private Transform missileBodyPositionEnd;
	private Transform missileHeadPositionStart;
	private Transform missileHeadPositionEnd;

	private GameObject target;
	private Vector3 targetRandomPointInside;
	private bool isLaunched = false;
	private bool isActivated = false;
	private bool isTriggered = false;
	private bool canDetonate = false;
	private bool reloadAnimationActive = false;
	private bool reloadAnimationInProgress = false;
	private float launchRndRotation = 0f;
	private Vector3 initialVelocity = new Vector3(0f,0f,0f);
	private float flightTime = 10f;
	private float flightTimeStart = 0f;
	private float animationSpeedTime = 2.5f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		particleSystem = transform.Find ("MissileParticleSystem").GetComponent<ParticleSystem>();
		particleSystemMainModule = particleSystem.main;
		particleSystemEmissionModule = particleSystem.emission;
		particleSystemEmissionModule.rateOverTime = 0f;

		missileBody = transform.Find ("Body");
		missileHead = transform.Find ("Head");
		missileBodyPositionStart = transform.Find ("BodyPositionStart");
		missileBodyPositionEnd = transform.Find ("BodyPositionEnd");
		missileHeadPositionStart = transform.Find ("HeadPositionStart");
		missileHeadPositionEnd = transform.Find ("HeadPositionEnd");

		if (reloadAnimationActive) {
			animationStartPosition ();
		}

		InvokeRepeating ("animationStartPosition", 1f, 5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (reloadAnimationActive) {
			animationStartPosition ();
			reloadAnimationActive = false;
		}

		if (reloadAnimationInProgress) {
			animationFrame ();
		}
		
		if (isLaunched) {
			particleSystemEmissionModule.rateOverTime = 20f;
			rigidBody.isKinematic = false;
			rigidBody.velocity = initialVelocity;
			rigidBody.AddForce (rigidBody.transform.forward*5000f);
			transform.Rotate (new Vector3(Random.Range(-launchRndRotation,launchRndRotation),Random.Range(-launchRndRotation,launchRndRotation),Random.Range(-launchRndRotation,launchRndRotation)));
			canDetonate = true;
			isLaunched = false;
		}

		if (isActivated) {
			rigidBody.AddForce (rigidBody.transform.forward*500f);

			if (target) {
				Vector3 newDir = Vector3.RotateTowards (transform.forward,targetRandomPointInside-transform.position,2f*Time.fixedDeltaTime,0f);
				transform.rotation = Quaternion.LookRotation (newDir);
			}

			if(flightTimeStart+flightTime<=Time.fixedTime){
				trigger ();
			}
		}

		if (isTriggered) {
			rigidBody.velocity = new Vector3(0f,0f,0f);
			GameObject explosion = Instantiate (explosionPrefab,transform.position,transform.rotation);
			float explosionScale = Random.Range (12f,20f);
			explosion.transform.localScale = new Vector3 (explosionScale,explosionScale,explosionScale);
			explosion.GetComponent<ParticleSystemBehaviourScript> ().startDestroyCountdown (10f);

			particleSystem.GetComponent<ParticleSystemBehaviourScript> ().stopGeneratingNewParticles ();
			particleSystem.GetComponent<ParticleSystemBehaviourScript> ().startDestroyCountdown (10f);
			particleSystem.transform.parent = null;

			Destroy (gameObject);
		}
	}

	public void startReloadingAnimation(){
		reloadAnimationActive = true;
	}

	private void animationStartPosition(){
		missileHead.transform.position = missileHeadPositionStart.position;
		missileBody.transform.position = missileBodyPositionStart.position;
		missileBody.transform.localScale = new Vector3 (10f,100f,100f);
		reloadAnimationInProgress = true;
	}

	private void animationFrame(){
		float distPrev1 = Vector3.Distance (missileHead.transform.position, missileHeadPositionEnd.position);
		float distPrev2 = Vector3.Distance (missileBody.transform.position, missileBodyPositionEnd.position);

		missileHead.transform.position = Vector3.MoveTowards (missileHead.transform.position, missileHeadPositionEnd.position, Vector3.Distance(missileHeadPositionStart.position,missileHeadPositionEnd.position)*Time.fixedDeltaTime/animationSpeedTime);
		missileBody.transform.position = Vector3.MoveTowards (missileBody.transform.position, missileBodyPositionEnd.position, Vector3.Distance(missileBodyPositionStart.position,missileBodyPositionEnd.position)*Time.fixedDeltaTime/animationSpeedTime);

		float distNow1 = Vector3.Distance (missileHead.transform.position, missileHeadPositionEnd.position);
		float distNow2 = Vector3.Distance (missileBody.transform.position, missileBodyPositionEnd.position);

		float bodyDistMax = Vector3.Distance (missileBodyPositionStart.position, missileBodyPositionEnd.position);
		missileBody.transform.localScale = new Vector3 (10f+90f*(1f-(distNow2/bodyDistMax)),100f,100f);

		if(distNow1==distPrev1 && distNow2==distPrev2){
			reloadAnimationInProgress = false;
			missileHead.transform.position = missileHeadPositionEnd.position;
			missileBody.transform.position = missileBodyPositionEnd.position;
			missileBody.transform.localScale = new Vector3 (100f,100f,100f);
		}
	}

	public void setTarget(GameObject t){
		target = t;
		if(target){
			Vector3 targetCenter = target.GetComponent<Renderer>().bounds.center;
			Vector3 targetExtents = target.GetComponent<Renderer>().bounds.extents*0.3f;
			targetRandomPointInside = new Vector3 (targetCenter.x+Random.Range(-targetExtents.x,targetExtents.x),targetCenter.y+Random.Range(-targetExtents.y,targetExtents.y),targetCenter.z+Random.Range(-targetExtents.z,targetExtents.z));
			//targetRandomPoint = targetCenter;
			Debug.Log (target.name);
			Debug.Log (targetCenter);
			Debug.Log (targetExtents);
			Debug.Log (targetRandomPointInside);
		}
	}

	public void setInitialVelocity(Vector3 iv){
		initialVelocity = iv;
	}

	public void setLaunchRandomRotation(float variation){
		launchRndRotation = variation;
	}

	public void launch(){
		isLaunched = true;
		Invoke ("activate",2f);
	}

	public void activate(){
		isActivated = true;
		flightTimeStart = Time.fixedTime;
		particleSystemEmissionModule.rateOverTime = 500f;
		audio.Play ();
	}

	public void trigger(){
		if(canDetonate) isTriggered = true;
	}

	public void  OnTriggerEnter(Collider c) {
		//Debug.Log ("Trigger = "+c.gameObject.tag);
		if(c.gameObject.tag=="Asteroid"){
			trigger ();
		}
		if(c.gameObject.tag=="SpaceTarget"){
			trigger ();
		}
	}
}
