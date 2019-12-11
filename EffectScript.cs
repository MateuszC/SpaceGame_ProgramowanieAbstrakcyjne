using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{

    public float destroyTime = 10f;

    private ParticleSystem particleSystem;
    private AudioSource audioSource;
    private float startTime = 0f;

    void Start ()
    {
        startTime = Time.time;
    }

	void Update ()
    {
        if (startTime + destroyTime <= Time.time)
        {
            destroy();
        }
	}

    public void destroy()
    {
        Destroy(gameObject);
    }

}
