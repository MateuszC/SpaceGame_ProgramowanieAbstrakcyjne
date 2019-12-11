using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineScript : MonoBehaviour {

	public GameObject enginePoint;
	public GameObject engineFuelDrop;

	[Range(0f,10f)]
	public float intensity;
	public float engineFuelDropForce;
	public float engineFuelDropParticleConstant;

	private float enginePointRadius;
	private int numberOfDropsPerSecond;
	private float engineDropForce;
	private float dropsToCreatePerUpdate;


	void Start () {
		enginePointRadius = 0.5f;
		dropsToCreatePerUpdate = 0f;
		setIntensity (intensity);
		//InvokeRepeating ("setRandomIntensity",0f,2f);
	}

	void Update () {
		dropsToCreatePerUpdate += numberOfDropsPerSecond * Time.deltaTime;
		while(dropsToCreatePerUpdate>1){
			createEngineFuelDrop ();
			dropsToCreatePerUpdate -= 1;
		}

		//Debug.Log ("intensity: "+intensity+", engineDropForce: "+engineDropForce+", numberOfDropsPerSecond: "+numberOfDropsPerSecond);
	}

	private void setRandomIntensity(){
		setIntensity(Random.Range(0f,10f));
	}

	public void setIntensity(float i){
		engineDropForce = engineFuelDropForce * calculateForcePercent (i);
		numberOfDropsPerSecond = (int)Mathf.Floor(engineFuelDropParticleConstant * calculateParticlesPercent (i));
	}

	private float calculateForcePercent(float intens){
		if (intens >= 7) return 1f;
		float max = 3 * Mathf.Log (8);
		float val = 3 * Mathf.Log (intensity+1);

		return (val/max);
	}

	private float calculateParticlesPercent(float intens){
		return (0.1f * intens * intens) / 10f;
	}

	private void createEngineFuelDrop(){
		float scale = Mathf.Min(transform.localScale.x,transform.localScale.y);
		float randomAngle = Random.Range (0f, 2f) * Mathf.PI;
		float randomRadius = Random.Range (0f, enginePointRadius)*scale;
		Vector3 newPostion = enginePoint.transform.position;
		newPostion.x = newPostion.x + randomRadius * Mathf.Cos (randomAngle);
		newPostion.y = newPostion.y + randomRadius * Mathf.Sin (randomAngle);

		GameObject engineFuelDropClone = (GameObject) Instantiate(engineFuelDrop, newPostion, enginePoint.transform.rotation);

		engineFuelDropClone.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity;
		engineFuelDropClone.GetComponent<Rigidbody>().AddForce (enginePoint.transform.forward * engineDropForce);
		//engineFuelDropClone.transform.localScale = new Vector3 (scale,scale,scale);
		engineFuelDropClone.transform.localScale = engineFuelDropClone.transform.localScale*scale;
	}
}


















