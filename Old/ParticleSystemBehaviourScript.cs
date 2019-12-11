using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemBehaviourScript : MonoBehaviour {

	private ParticleSystem particleSystem;
	private ParticleSystem.EmissionModule particleSystemEmissionModule;
	private float timeStarted;
	private float timeAlive = 5f;
	private bool startDestroy = false;
	private bool stopGeneratingParticles = false;

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
		particleSystemEmissionModule = particleSystem.emission;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(stopGeneratingParticles){
			particleSystemEmissionModule.rateOverTime = 0f;
		}
		if (startDestroy) {
			if(timeStarted+timeAlive<=Time.fixedTime){
				Destroy (gameObject);
			}
		}
	}

	public void startDestroyCountdown(float time){
		startDestroy = true;
		timeStarted = Time.fixedTime;
		timeAlive = time;
	}

	public void stopGeneratingNewParticles(){
		stopGeneratingParticles = true;
	}

}
