using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : ObjectInSpace{

    public GameObject[] effectsPrefabs;
    public ObjectInSpace.DamageType damageType;
    public float flightTimeMax = 10f;
    public float damageDealt = 100f;

    private float flightTimeStart = -100f;
    private bool isLaunched = false;
    private ObjectInSpace target = null;

    private ParticleSystem shipEngineParticleSystem;
    private ParticleSystem.MainModule shipEngineParticleSystemMain;
    private ParticleSystem.EmissionModule shipEngineParticleSystemEmmision;

    private void Start () {
		objectInSpaceStart();
        setEngineThrust(500f);
        setManeuverability(900f);

        shipEngineParticleSystem = transform.Find("MissileEngine").GetComponent<ParticleSystem>();
        shipEngineParticleSystemMain = shipEngineParticleSystem.main;
        shipEngineParticleSystemEmmision = shipEngineParticleSystem.emission;

        Invoke("launch", 2f);
        rigidBody.AddForce(transform.forward * 9000f, ForceMode.Force);
    }

    private void FixedUpdate () {
        fixedUpdateMovement();

        if (isLaunched)
        {
            if (flightTimeStart + flightTimeMax <= Time.time)
            {
                explode();
            }
        }
    }

    public void setTarget(ObjectInSpace _target)
    {
        target = _target;
    }

    private void launch()
    {
        increaseEngineIntensity(1f);
        flightTimeStart = Time.time;
        isLaunched = true;

        shipEngineParticleSystemMain.startSize = 5f;
        shipEngineParticleSystemEmmision.rateOverTime = 200f;

        if (target)
        {
            hotspotHolder = target.getRandomHotspot();
            alignTowardsHotspot(hotspotHolder);
        }
    }

    protected override void damageEffect(){
		
	}

	protected override void destroyByDamage(){
		
	}

	public void  OnTriggerEnter(Collider c)
    {
        if (isLaunched)
        {
            GameObject g = c.gameObject;
            ObjectInSpace targetOfCollision = g.GetComponent<ObjectInSpace>();

            while (g.transform.parent && !targetOfCollision)
            {
                g = g.transform.parent.gameObject;
                targetOfCollision = g.GetComponent<ObjectInSpace>();
            }

            if (targetOfCollision)
            {
                if (!(getOwner() != null && targetOfCollision.getOwner() != null && getOwner() == targetOfCollision.getOwner()))
                {
                    ObjectInSpace.DamageReport dmgReport = targetOfCollision.dealDamage(getOwner(), damageDealt, damageType, transform.position);
                    explode();
                }
            }
        }
    }

    public void dealDamage()
    {
        //damageType


    }

    public void explode()
    {
        isLaunched = false;
        //createExplosion();
        destroy();
    }

    private void createExplosion()
    {
        GameObject effect = Instantiate(effectsPrefabs[Random.Range(0, effectsPrefabs.Length - 1)], transform.position, transform.rotation, null);
        effect.transform.localScale = new Vector3(5f, 5f, 5f);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }



    //Testing
    private Collider hotspotHolder = null;
    public Collider getHotspotHolder()
    {
        return hotspotHolder;
    }

}

