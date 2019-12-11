using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGeneratorScript : MonoBehaviour {

	public GameObject[] asteroids;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
	public void instantiateRandomAsteroids(GameObject parentAsteroid){
		ObjectInSpace parentAsteroidScript = parentAsteroid.GetComponent<ObjectInSpace> ();
		string startType = parentAsteroid.name;
		if(startType.IndexOf("(")>=0) startType = startType.Substring (0,startType.IndexOf("(")).Trim();
		startType = parentAsteroid.name.Substring (0,parentAsteroid.name.Length-1).Trim();
		int numberOfNewAsteroids = (int)Mathf.Floor (parentAsteroidScript.getSize ()*Random.Range (0.05f, 0.1f));
		List<GameObject> asteroidsToCreate = new List<GameObject> ();

		for (int i = 0; i < asteroids.Length; i++) {
			if(asteroids[i].name.IndexOf(startType)>=0){
				asteroidsToCreate.Add (asteroids[i]);
			}
		}

		for (int i = 0; i < numberOfNewAsteroids && i<asteroidsToCreate.Count; i++) {
			Vector3 spawnOffset = new Vector3 (Random.Range(-parentAsteroidScript.getSize()/2,parentAsteroidScript.getSize()/2),Random.Range(-parentAsteroidScript.getSize()/2,parentAsteroidScript.getSize()/2),Random.Range(-parentAsteroidScript.getSize()/2,parentAsteroidScript.getSize()/2));
			GameObject newAsteroid = Instantiate (asteroidsToCreate [Random.Range (0, asteroidsToCreate.Count)], transform.position+spawnOffset, transform.rotation, null);
			ObjectInSpace newAsteroidScript = newAsteroid.GetComponent<ObjectInSpace> ();

			float newSizePercent = Random.Range (parentAsteroidScript.getSize ()*0.20f, parentAsteroidScript.getSize ()*0.60f) / parentAsteroidScript.getSize ();

			newAsteroid.transform.position += new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)) .normalized * Mathf.Floor (parentAsteroidScript.getSize () /2);
			newAsteroid.transform.localScale = Vector3.one * Mathf.Floor (parentAsteroidScript.getSize () * newSizePercent);

			newAsteroidScript.setName(parentAsteroidScript.getName());
			newAsteroidScript.setSize(Mathf.Floor(parentAsteroidScript.getSize() * newSizePercent));
			//newAsteroidScript.setTags(parentAsteroidScript.getTags());

			newAsteroidScript.setCanBeTargetted(parentAsteroidScript.getCanBeTargetted());
			newAsteroidScript.setCanBeDamaged(parentAsteroidScript.getCanBeDamaged());

			newAsteroidScript.setMaxShield(Mathf.Floor(parentAsteroidScript.getMaxShield() * newSizePercent));
			newAsteroidScript.setMaxArmor(Mathf.Floor(parentAsteroidScript.getMaxArmor() * newSizePercent));
			newAsteroidScript.setMaxHull(Mathf.Floor(parentAsteroidScript.getMaxHull() * newSizePercent));
			newAsteroidScript.setMaxEnergy(Mathf.Floor(parentAsteroidScript.getMaxEnergy() * newSizePercent));

			newAsteroidScript.increaseEngineIntensity(-1f);
			newAsteroidScript.setEngineThrust(parentAsteroidScript.getEngineThrust());
			newAsteroidScript.setManeuverability(parentAsteroidScript.getManeuverability());
		}
	}
    */
}
