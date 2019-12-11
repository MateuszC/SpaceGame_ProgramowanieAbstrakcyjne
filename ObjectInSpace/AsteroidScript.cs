using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : ObjectInSpace {

	[Space(15)]
	public GameObject asteroidAudioSourcePrefab;
	public GameObject asteroidsGeneratorPrefab;

	private int lastAudioSourcePlayed = -1;
	private GameObject asteroidsGenerator;
	private bool isDestroyed = false;

	void Start () {
		objectInSpaceStart ();
		asteroidsGenerator = Instantiate (asteroidsGeneratorPrefab, transform.position, transform.rotation, transform);
	}

	void FixedUpdate () {
		
	}

	protected override void damageEffect(){
		GameObject asteroidAudioSource = Instantiate (asteroidAudioSourcePrefab, transform.position, transform.rotation, null);
		AudioSource[] audioSources = asteroidAudioSource.GetComponents<AudioSource> ();

		int playAudio = lastAudioSourcePlayed;
		while(playAudio == lastAudioSourcePlayed){
			playAudio = Random.Range (0, audioSources.Length);
		}
		audioSources [playAudio].Play ();
		asteroidAudioSource.GetComponent<ShortLivedElement> ().invokeDestroyIn (5f);
		lastAudioSourcePlayed = playAudio;
	}

	protected override void destroyByDamage(){
		if (isDestroyed == false) {
			isDestroyed = true;
			try{
				//if(getSize()>=10f) asteroidsGenerator.GetComponent<AsteroidGeneratorScript> ().instantiateRandomAsteroids (gameObject);
			}catch (UnityException e){
				Debug.Log (e);
			}

			Destroy (gameObject);
		}
	}



}
