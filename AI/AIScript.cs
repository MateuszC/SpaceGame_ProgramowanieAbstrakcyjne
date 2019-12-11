using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TestTest
{
    [Tooltip("A camera must be associated with this variable. The camera that is associated here, will receive the settings of this index.")]
    public Camera _camera;
    public enum TipoRotacTipoRotac { LookAtThePlayer, FirstPerson, FollowPlayer, Orbital, Stop, StraightStop, OrbitalThatFollows, ETS_StyleCamera }
    [Tooltip("Here you must select the type of rotation and movement that camera will possess.")]
    public TipoRotacTipoRotac rotationType = TipoRotacTipoRotac.LookAtThePlayer;
    [Range(0.01f, 1.0f)]
    [Tooltip("Here you must adjust the volume that the camera attached to this element can perceive. In this way, each camera can perceive a different volume.")]
    public float volume = 1.0f;
}

[Serializable]
public class AIMovementSettings
{
    public enum MovementType { None, RandomMovement }
    public MovementType movementType = MovementType.None;
    public float distanceFormStartPoint = 0;
}

[Serializable]
public class AIAttackSettings
{
    public enum AttackType { None, AttackWhenInRange, AttackWhenInRangeAndFollow }
    public AttackType attackType = AttackType.None;
    public float distanceFormStartPoint = 0;
}

public class AIScript : MonoBehaviour {

    public AIMovementSettings AIMovement = new AIMovementSettings();

    private ObjectInSpace owner;

    void Start ()
    {
        owner = GetComponent<ObjectInSpace>();
        if (AIMovement.movementType == AIMovementSettings.MovementType.RandomMovement) start_RandomMovement();
    }
	
	void FixedUpdate()
    {
        if (AIMovement.movementType == AIMovementSettings.MovementType.RandomMovement) fixedUpdate_RandomMovement();
    }



    private void turnOnEngines()
    {
        ShipPartEngine shipPartEngine;
        Equipment[] eqWithType = owner.getEquipmentWithType(Equipment.Type.Engine);
        for (int eqI = 0; eqI < eqWithType.Length; eqI++)
        {
            eqWithType[eqI].turnOn();
        }
    }



    private Vector3 startPosition = Vector3.zero;
    private Vector3 moveToPosition = Vector3.zero;
    public void start_RandomMovement()
    {
        startPosition = owner.gameObject.transform.position;
        moveToPosition = owner.gameObject.transform.position;
    }

    public void fixedUpdate_RandomMovement()
    {
        if (AIMovement.distanceFormStartPoint > 0)
        {
            if (Vector3.Distance(owner.gameObject.transform.position, moveToPosition) <= 2f)
            {
                moveToPosition = startPosition + GMS.randomVector() * AIMovement.distanceFormStartPoint;
                owner.increaseEngineIntensity(-1f);
                owner.increaseEngineIntensity(0.2f);
                turnOnEngines();
            }
            owner.alignTowardsFixedUpdate(moveToPosition);
        }
    }




}
