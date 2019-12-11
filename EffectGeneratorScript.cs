using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGeneratorScript : MonoBehaviour {

    public GameObject effect;

	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnEffect",0f,2f);
	}

    private GameObject newEffect;

    void spawnEffect ()
    {
        if (newEffect)
        {
            newEffect.GetComponent<EffectScript>().destroy();
        }
        newEffect = (GameObject)Instantiate(effect, transform.position, transform.rotation, transform);
        newEffect.transform.parent = null;
    }
}
