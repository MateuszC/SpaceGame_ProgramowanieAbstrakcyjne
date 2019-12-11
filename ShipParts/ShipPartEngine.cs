using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartEngine : ShipPart {

    private AudioSource shipEngineAudio;
    private ParticleSystem shipEngineParticleSystem;
    private ParticleSystem.MainModule shipEngineParticleSystemMain;
    private ParticleSystem.EmissionModule shipEngineParticleSystemEmmision;
    private float engineIntensity = 0f;

    void Start ()
    {
        setEquipment();
        calculateVolume();

        shipEngineAudio = transform.Find("ShipEngineEffects").GetComponent<AudioSource>();
        shipEngineParticleSystem = transform.Find("ShipEngineEffects").GetComponent<ParticleSystem>();
        shipEngineParticleSystemMain = shipEngineParticleSystem.main;
        shipEngineParticleSystemEmmision = shipEngineParticleSystem.emission;
        setEngineIntensity(0f);
        setEngineEffects(0f);
    }

    public void setEngineIntensity(float newEngineIntensity){
        if (engineIntensity != newEngineIntensity)
        {
            engineIntensity = newEngineIntensity;
            refreshEngine(engineIntensity);
        }
    }

    private void refreshEngine(float newEngineIntensity)
    {
        if (equipment.getState() == Equipment.State.TurnedOn || equipment.getState() == Equipment.State.Active || equipment.getState() == Equipment.State.Overdrive)
        {
            setEngineEffects(engineIntensity);
        }
        else
        {
            setEngineEffects(0f);
        }
    }

    private void setEngineEffects(float intensity)
    {
        //Debug.Log("ShipPartEngine setEngineEffects "+ intensity);
        if (equipment.getState() == Equipment.State.Overdrive) intensity = intensity * 1.5f;
        shipEngineParticleSystemMain.startSize = 2f + 2f * intensity;
        shipEngineParticleSystemEmmision.rateOverTime = 50f + 450.0f * intensity;
        shipEngineAudio.pitch = 2.25f * intensity;
    }

    public override void onStateChange()
    {
        //Debug.Log("ShipPartEngine onStateChange, state = "+ equipment.getState());
        refreshEngine(engineIntensity);
    }

    protected override void onShutDown()
    {
        setEngineIntensity(0f);
        setEngineEffects(0f);
        shipEngineParticleSystemMain.startSize = 0f;
        shipEngineParticleSystemEmmision.rateOverTime = 0f;
        shipEngineAudio.pitch = 0f;
    }












}
