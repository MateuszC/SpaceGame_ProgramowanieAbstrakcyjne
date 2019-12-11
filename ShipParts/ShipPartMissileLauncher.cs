using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartMissileLauncher : ShipPart {

	private Transform shootPoint = null;
	private AudioSource audio = null;

	public GameObject missileKinetic;

	void Start () {
        setEquipment();
        calculateVolume();

        shootPoint = transform.Find ("Body/Launcher/LauncherHead/ShootPoint");
		audio = GetComponent<AudioSource> ();
	}
	
	void FixedUpdate () {
		
	}

    public override void actionOnActive(){
        if (equipment.getTarget())
        {
            GameObject missile = Instantiate(missileKinetic, shootPoint.transform.position, shootPoint.transform.rotation, shootPoint.transform);
            MissileScript missileScript = missile.GetComponent<MissileScript>();
            missile.transform.parent = null;
            missile.transform.Rotate(0f, 90f, 0f);
            missileScript.setOwner(equipment.getOwner());
            missileScript.setTarget(equipment.getTarget());
            audio.Play();
        }
    }


}
