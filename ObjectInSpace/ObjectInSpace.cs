using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InitialProperties
{
    public bool enable = false;

    public float mass = 0;
    public float drag = 0;
    public float dragAngular = 0;
    public float engineThrust = 0;
    public float maneuverability = 0;

    public float maxShield = 0;
    public float maxArmor = 0;
    public float maxHull = 0;
    public float maxEnergy = 0;

    public float energyProduction = 0;
    public float energyDrainPassive = 0;
    public float energyDrainActive = 0;

    public float shieldBalance = 0;
    public float armorBalance = 0;
    public float hullBalance = 0;

    public int maxNumberOfTargets = 0;
}

public class ObjectInSpace : MonoBehaviour {

	public string niceName = "ObjectInSpace";
	public bool canBeTargetted = true;
	public bool canBeDamaged = true;

    public bool isFixedUpdateMovement = false;
    public bool isFixedUpdateNormalizeRotation = false;
    public bool isFixedUpdateEquipmentUse = false;
    public bool isFixedUpdateEquipmentProperties = false;
    public bool isFixedUpdateTargets = false;

    public List<Faction.Factions> factions = new List<Faction.Factions>();

    public InitialProperties initialProperties = new InitialProperties();

    protected Rigidbody rigidBody;
	protected ObjectInSpace owner = null;

	protected float maxShield;
	protected float maxArmor;
	protected float maxHull;
	protected float maxEnergy;

	protected float currentShield;
	protected float currentArmor;
	protected float currentHull;
	protected float currentEnergy;

	protected float shieldBalance = 0f;
	protected float armorBalance = 0f;
	protected float hullBalance = 0f;
	protected float energyProduction = 0f;
	protected float energyDrainPassive = 0f;
	protected float energyDrainActive = 0f;
	protected float energyDrainActiveCoefficient = 1f;
    protected float maxVelocity = 0f;
    protected float maxCurrentVelocity = 0f;
    
    protected float mass;
    protected double volume;
    protected float drag;
    protected float dragAngular;
	protected float engineThrust = 0f;
	protected float maneuverability = 0f;
    protected float engineIntensity = 0f;

    protected int maxNumberOfTargets = 0;
    protected float distanceOffset = 0f;
    protected bool isDebris = false;

    public string getName(){ return niceName; }

	public bool getCanBeTargetted(){ return canBeTargetted; }
	public bool getCanBeDamaged(){ return canBeDamaged; }

	public float getMaxShield(){ return maxShield; }
	public float getMaxArmor(){ return maxArmor; }
	public float getMaxHull(){ return maxHull; }
	public float getMaxEnergy(){ return maxEnergy; }

	public float getCurrentShield(){ return currentShield; }
	public float getCurrentArmor(){ return currentArmor; }
	public float getCurrentHull(){ return currentHull; }
	public float getCurrentEnergy(){ return currentEnergy; }

	public float getShieldBalance(){ return shieldBalance; }
	public float getArmorBalance(){ return armorBalance; }
	public float getHullBalance(){ return hullBalance; }
	public float getEnergyProduction(){ return energyProduction; }
	public float getEnergyDrainPassive(){ return energyDrainPassive; }
    public float getEnergyDrainActive() { return energyDrainActive * energyDrainActiveCoefficient; }
    public float getEnergyDrainActiveAbsolute() { return energyDrainActive; }
    public float getEnergyDrainActiveCoefficient() { return energyDrainActiveCoefficient; }

    public float getMass() { return mass; }
    public double getVolume() { return volume; }
    public double getVolumeMeterCubic() { return unitsToMetersCubic(volume); }
    public float getDrag() { return drag; }
    public float getDragAngular() { return dragAngular; }
    public float getEngineIntensity(){ return engineIntensity; }
	public float getEngineThrust(){ return engineThrust; }
	public float getManeuverability(){ return maneuverability; }
	public float getCurrentVelocity(){ if(rigidBody) return rigidBody.velocity.magnitude; return 0f; }
    public float getMaxVelocity() { return maxVelocity; }
    public float getMaxCurrentVelocity() { return maxCurrentVelocity; }
    public float getDistanceOffset() { return distanceOffset; }

    public ObjectInSpace getOwner(){ return owner; }

	public void setName(string _name){ niceName = _name; }
	public void setMass(float _mass){ mass = _mass; if(rigidBody) rigidBody.mass = _mass; }
	//public void setSize(float _size){ size = _size; }
	public void setDrag(float _drag){ drag = _drag; if(rigidBody) rigidBody.drag = _drag; calculateMaxVelocity (); }
	public void setDragAngular(float _dragAngular){ dragAngular = _dragAngular; if(rigidBody) rigidBody.angularDrag = _dragAngular; }

	public void setCanBeTargetted(bool _canBeTargetted){ canBeTargetted = _canBeTargetted; }
	public void setCanBeDamaged(bool _canBeDamaged){ canBeDamaged = _canBeDamaged; }

	public void setMaxShield(float _maxShield){ maxShield = _maxShield; currentShield = Mathf.Min (maxShield, currentShield); }
	public void setMaxArmor(float _maxArmor){ maxArmor = _maxArmor; currentArmor = Mathf.Min (maxArmor, currentArmor); }
	public void setMaxHull(float _maxHull){ maxHull = _maxHull; currentHull = Mathf.Min (maxHull, currentHull); }
	public void setMaxEnergy(float _maxEnergy){ maxEnergy = _maxEnergy; currentEnergy = Mathf.Min (maxEnergy, currentEnergy); }

	public void setCurrentShield(float amount){ currentShield = Mathf.Min (maxShield, amount); }
	public void setCurrentArmor(float amount){ currentArmor = Mathf.Min (maxArmor, amount); }
	public void setCurrentHull(float amount){ currentHull = Mathf.Min (maxHull, amount); }
	public void setCurrentEnergy(float amount){ currentEnergy = Mathf.Min (maxEnergy, amount); }

	public void setEngineThrust(float _engineThrust){ engineThrust = _engineThrust; calculateMaxVelocity (); }
	public void setManeuverability(float _maneuverability){ maneuverability = _maneuverability; }

	public void addShield(float amount){ currentShield = Mathf.Min (maxShield, currentShield + amount); }
	public void addArmor(float amount){ currentArmor = Mathf.Min (maxArmor, currentArmor + amount); }
	public void addHull(float amount){ currentHull = Mathf.Min (maxHull, currentHull + amount); }
	public void addEnergy(float amount){ currentEnergy = Mathf.Min (maxEnergy, currentEnergy + amount); }

    public void setOwner(ObjectInSpace _owner){ owner = _owner; }

	protected void objectInSpaceStart () {
		rigidBody = GetComponent<Rigidbody> ();
        rigidBody.useGravity = false;

        increaseEngineIntensity(-1f);

        getShipPartsFromGameObject();
        getShipPartsColliders();

        setNeedRecalculateEquipment();
        equipmentRecalculate();
        setBasicEquipment();

        setCurrentShield(getMaxShield());
        setCurrentArmor(getMaxArmor());
        setCurrentHull(getMaxHull());
    }

    void Start()
    {
        objectInSpaceStart();
    }


    void FixedUpdate()
    {
        if(isFixedUpdateMovement) fixedUpdateMovement();
        if(isFixedUpdateNormalizeRotation) fixedUpdateNormalizeRotation();
        if(isFixedUpdateEquipmentUse) fixedUpdateEquipmentUse();
        if(isFixedUpdateEquipmentProperties) fixedUpdateEquipmentProperties();
        if(isFixedUpdateTargets) fixedUpdateTargets();
    }

    protected void setBasicEquipment()
    {
        if (initialProperties.enable)
        {
            setMaxShield(initialProperties.maxShield);
            setMaxArmor(initialProperties.maxArmor);
            setMaxHull(initialProperties.maxHull);
            setMaxEnergy(initialProperties.maxEnergy);

            setCurrentShield(initialProperties.maxShield);
            setCurrentArmor(initialProperties.maxArmor);
            setCurrentHull(initialProperties.maxHull);
            setCurrentEnergy(initialProperties.maxEnergy);

            energyProduction = initialProperties.energyProduction;
            energyDrainPassive = initialProperties.energyDrainPassive;
            energyDrainActive = initialProperties.energyDrainActive;

            shieldBalance = initialProperties.shieldBalance;
            armorBalance = initialProperties.armorBalance;
            hullBalance = initialProperties.hullBalance;

            setMass(initialProperties.mass);
            setDrag(initialProperties.drag);
            setDragAngular(initialProperties.dragAngular);
            setEngineThrust(initialProperties.engineThrust);
            setManeuverability(initialProperties.maneuverability);

            maxNumberOfTargets = initialProperties.maxNumberOfTargets;
            calculateMaxVelocity();
            calculateCurrentMaxVelocity();
        }
    }















































    //Movement -----------------------------------------------------------------------------------------------------------------

    public static float oneUnitIsThisMeters = 10f;

    protected ObjectInSpace alignTarget = null; 
    protected float timeSinceLastRotation = -100f;
    protected Collider hotspot = null;
    protected List<Collider> shipPartsColliders = new List<Collider>();

    protected void fixedUpdateMovement(){
		if (engineIntensity > 0f) {
			rigidBody.AddForce (transform.forward * engineThrust * engineIntensity, ForceMode.Force);
		} else {
			if (rigidBody.velocity.magnitude < 1f) rigidBody.velocity = Vector3.zero;
        }

        if (alignTarget)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (alignTarget.gameObject.transform.position) - transform.position, maneuverability * 0.02f * Time.fixedDeltaTime, 0f));
        }

        if (hotspot)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (hotspot.bounds.center) - transform.position, maneuverability * 0.02f * Time.fixedDeltaTime, 0f));
        }
    }

    public void alignTowardsFixedUpdate(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (target) - transform.position, maneuverability * 0.02f * Time.fixedDeltaTime, 0f));
    }

    protected void fixedUpdateNormalizeRotation(){
		if(alignTarget == null){
            if (Time.fixedTime - timeSinceLastRotation >= 1f) {
                if (transform.eulerAngles.z != 0)
                {
                    normalizeRotationZ(Mathf.Min(1f, (Time.fixedTime - timeSinceLastRotation - 1f) / 3f));
                }
            }
			if(engineIntensity == 0f){
				if(transform.eulerAngles.x != 0)
                {
                    normalizeRotationX(Mathf.Min(1f, (Time.fixedTime - timeSinceLastRotation - 1f) / 3f));
                }
			}
		}
	}

	protected void fixedUpdateNormalizeVelocity(){
		if(getMaxVelocity()>0f){
			if (Mathf.Ceil (getCurrentVelocity ()) == getMaxVelocity ()) {
				rigidBody.velocity = rigidBody.velocity.normalized * getMaxVelocity ();
			}
		}
    }

    public void calculateMaxVelocity()
    {
        maxVelocity = ((engineThrust * ((50f - drag) / 50f)) / drag) / mass;
    }

    public void calculateCurrentMaxVelocity()
    {
        maxCurrentVelocity = ((engineThrust * engineIntensity * ((50f - drag) / 50f)) / drag) / mass;
    }

    public virtual void increaseEngineIntensity(float increase){
        float engineIntensityPrev = engineIntensity;
        engineIntensity += increase;
		if (engineIntensity > 1f) engineIntensity = 1f;
		if (engineIntensity < 0f) engineIntensity = 0f;

        calculateMaxVelocity();
        calculateCurrentMaxVelocity();

        if (engineIntensity != engineIntensityPrev)
        {
            ShipPartEngine shipPartEngine;
            Equipment[] eqWithType = getEquipmentWithType(Equipment.Type.Engine);
            for (int eqI = 0; eqI < eqWithType.Length; eqI++)
            {
                shipPartEngine = (ShipPartEngine)eqWithType[eqI].getShipPart();
                shipPartEngine.setEngineIntensity(engineIntensity);
                //((ShipPartEngine)eqWithType[eqI].getShipPart()).setEngineIntensity(engineIntensity);
            }
        }
    }

    private bool rotationNotTorque = true;
    public virtual void turnUp()            { if (rotationNotTorque) maneuverRotation(maneuverability * Time.fixedDeltaTime, 0f, 0f);   else maneuverTorque(transform.right); }
    public virtual void turnDown()          { if (rotationNotTorque) maneuverRotation(-maneuverability * Time.fixedDeltaTime, 0f, 0f);  else maneuverTorque(-transform.right); }
    public virtual void turnLeft()          { if (rotationNotTorque) maneuverRotation(0f, -maneuverability * Time.fixedDeltaTime, 0f);  else maneuverTorque(-transform.up); }
    public virtual void turnRight()         { if (rotationNotTorque) maneuverRotation(0f, maneuverability * Time.fixedDeltaTime, 0f);   else maneuverTorque(transform.up); }
    public virtual void turnRollLeft()      { if (rotationNotTorque) maneuverRotation(0f, 0f, maneuverability * Time.fixedDeltaTime);   else maneuverTorque(-transform.forward); }
    public virtual void turnRollRight()     { if (rotationNotTorque) maneuverRotation(0f, 0f, -maneuverability * Time.fixedDeltaTime);  else maneuverTorque(transform.forward); }

    protected void maneuverRotation(float x, float y, float z)
    {
        transform.Rotate(new Vector3(x, y, z));
        timeSinceLastRotation = Time.fixedTime;
        alignTowardsTargetStop();
        alignTowardsHotspotStop();
    }

    protected void maneuverTorque(Vector3 vector)
    {
        rigidBody.AddTorque(vector * 1000f);
        timeSinceLastRotation = Time.fixedTime;
        alignTowardsTargetStop();
        alignTowardsHotspotStop();
    }

    public void alignTowardsTarget(ObjectInSpace target)
    {
        alignTarget = target;
    }

    public void alignTowardsHotspot(Collider _hotspot)
    {
        hotspot = _hotspot;
    }

    public void alignTowardsTargetStop()
    {
        alignTarget = null;
    }

    public void alignTowardsHotspotStop()
    {
        hotspot = null;
    }

    public bool isAligningTowardsTarget() {
        return alignTarget != null;
    }

    public float distanceToAlignTarget()
    {
        if (alignTarget)
        {
            return Vector3.Distance(transform.position, alignTarget.gameObject.transform.position);
        }
        return -1f;
    }

    protected void stopAllMovement()
    {
        alignTowardsTargetStop();
        alignTowardsHotspotStop();
        rigidBody.velocity = Vector3.zero;
		rigidBody.freezeRotation = true;
		rigidBody.freezeRotation = false;
		increaseEngineIntensity(-1f);
	}

    /*
	public Vector3 getInsideRandomOffset(){
		Vector3 offset = Vector3.zero;
		if (collider) {
			Vector3 extent = collider.bounds.extents;
			offset = new Vector3 (Random.Range(-extent.x,extent.x), Random.Range(-extent.y,extent.y), Random.Range(-extent.z,extent.z)) * 0.4f;
			//while(b.Contains (b.center + offset)==false) offset = new Vector3 (Random.Range(-b.extents.x,b.extents.x), Random.Range(-b.extents.y,b.extents.y), Random.Range(-b.extents.z,b.extents.z))*0.4f;
		}
		return offset;
	}
    */

    public void addForceFromPoint(Vector3 pos, float force){
		rigidBody.AddForce ((transform.position - pos).normalized * force);
	}


	protected void normalizeRotationZ(float multiplier){
		Vector3 angle = transform.eulerAngles;
		if (angle.z >= 0f && angle.z < 180f) {//is on left side, need to turn right
			angle.z -= maneuverability * Time.fixedDeltaTime * multiplier;
		}else if (angle.z >= 180f && angle.z < 360f) {//is on right side, need to turn left
			angle.z += maneuverability * Time.fixedDeltaTime * multiplier;
		}
		if (angle.z < 0.01f || angle.z >= 359.99f) angle.z = 0f;
		transform.eulerAngles = angle;
	}

	protected void normalizeRotationX(float multiplier){
		Vector3 angle = transform.eulerAngles;
		if (angle.x >= 0f && angle.x < 180f) {//is on left side, need to turn right
			angle.x -= maneuverability * Time.fixedDeltaTime * multiplier;
		}else if (angle.x >= 180f && angle.x < 360f) {//is on right side, need to turn left
			angle.x += maneuverability * Time.fixedDeltaTime * multiplier;
		}
		if (angle.x < 0.01f || angle.x >= 359.99f) angle.x = 0f;
		transform.eulerAngles = angle;
    }

    protected void getShipPartsColliders()
    {
        List<GameObject> objectsToFindCollidersInside = new List<GameObject>();
        objectsToFindCollidersInside.Add(gameObject);
        Collider collider;
        for (int i = 0; i < objectsToFindCollidersInside.Count; i++)
        {
            collider = objectsToFindCollidersInside[i].GetComponent<Collider>();
            if (collider) shipPartsColliders.Add(collider);

            for (int j = 0; j < objectsToFindCollidersInside[i].transform.childCount; j++)
            {
                objectsToFindCollidersInside.Add(objectsToFindCollidersInside[i].transform.GetChild(j).gameObject);
            }
        }

        //Calculating Distance Offset of the ship
        for (int i = 0; i < shipPartsColliders.Count; i++)
        {
            distanceOffset = Mathf.Max(distanceOffset, Vector3.Distance(transform.position, shipPartsColliders[i].bounds.center) + shipPartsColliders[i].bounds.extents.magnitude);
        }
    }

    public Collider getRandomHotspot()
    {
        if (shipPartsColliders.Count > 0)
        {
            return shipPartsColliders[Random.Range(0, shipPartsColliders.Count - 1)];
        }
        return null;
    }

    public class HotspotData
    {
        public Collider collider;
        public Vector3 offset;
    }

    public HotspotData getRandomHotspotWithOffset()
    {
        HotspotData hsData = new HotspotData();
        hsData.collider = getRandomHotspot();

        return hsData;
    }













    public static float           unitsToMeters(float units) { return units * oneUnitIsThisMeters; }
    public static float     unitsToMetersSquare(float units) { return units * oneUnitIsThisMeters * oneUnitIsThisMeters; }
    public static float      unitsToMetersCubic(float units) { return units * oneUnitIsThisMeters * oneUnitIsThisMeters * oneUnitIsThisMeters; }
    public static double         unitsToMeters(double units) { return units * (double)oneUnitIsThisMeters; }
    public static double   unitsToMetersSquare(double units) { return units * (double)oneUnitIsThisMeters * (double)oneUnitIsThisMeters; }
    public static double    unitsToMetersCubic(double units) { return units * (double)oneUnitIsThisMeters * (double)oneUnitIsThisMeters * (double)oneUnitIsThisMeters; }
    public static Vector3       unitsToMeters(Vector3 units) { return units * oneUnitIsThisMeters; }
    public static Vector3 unitsToMetersSquare(Vector3 units) { return units * oneUnitIsThisMeters * oneUnitIsThisMeters; }
    public static Vector3  unitsToMetersCubic(Vector3 units) { return units * oneUnitIsThisMeters * oneUnitIsThisMeters * oneUnitIsThisMeters; }

    public static double mCubicToKmCubic(double m) { return (double)m/1000000000; }


    public static string distanceStr(float distanceInMeters)
    {
        distanceInMeters = Mathf.Floor(distanceInMeters);
        if (distanceInMeters < 1000f) { return distanceInMeters + "m"; }
        else if (distanceInMeters < 10000f) { return (Mathf.Floor(distanceInMeters / 10) / 100).ToString("0.00") + "km"; }
        else if (distanceInMeters < 100000f) { return (Mathf.Floor(distanceInMeters / 100) / 10).ToString("0.0") + "km"; }
        else { return Mathf.Floor(distanceInMeters / 1000) + "km"; }

        return "0m";
    }

    public float distance(ObjectInSpace target) { return Mathf.Max(0f, Vector3.Distance(transform.position, target.gameObject.transform.position) - getDistanceOffset() - target.getDistanceOffset());  }
    //public float distance(ObjectInSpace target) { return Vector3.Distance(transform.position, target.gameObject.transform.position); }
    public float distanceInMeters(ObjectInSpace target) { return distance(target) * oneUnitIsThisMeters; }
    public float getCurrentVelocityInMeters() { return getCurrentVelocity() * oneUnitIsThisMeters; }
    public float getMaxVelocityInMeters() { return maxVelocity * oneUnitIsThisMeters; }
    public float getMaxCurrentVelocityInMeters(){ return maxCurrentVelocity * oneUnitIsThisMeters; }
    public string distanceInMetersStr(ObjectInSpace target) { return distanceStr(distanceInMeters(target)); }
    public string getCurrentVelocityInMetersStr() { return distanceStr(getCurrentVelocityInMeters()) + "/s"; }
    public string getMaxVelocityInMetersStr() { return distanceStr(getMaxVelocityInMeters()) + "/s"; }
    public string getMaxCurrentVelocityInMetersStr(){ return distanceStr(getMaxCurrentVelocityInMeters()) + "/s"; }


    public bool isInTargettingRange(ObjectInSpace target)
    {
        return distanceInMeters(target) <= getShipProperty(Equipment.PropertyName.TargetingRange);
    }





    void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionForce = (collision.impulse / Time.fixedDeltaTime);
        //collision.relativeVelocity.magnitude

        if (collisionForce.magnitude > 0f)
        {
            //Debug.Log(getName() + " collided with " + collision.gameObject + " with force " + collisionForce.magnitude);
        }
    }























































    //Equipment Management -----------------------------------------------------------------------------------------------------------------

    protected Equipment[] equipment = new Equipment[0];
	protected Equipment.EquipmentSum equipmentSum = new Equipment.EquipmentSum (new Equipment[0]);
    private bool needToRecalculate = false;

    public float getShipProperty(Equipment.PropertyName propertyName)
    {
        return equipmentSum.getPropertySum(propertyName);
    }

    public void setNeedRecalculateEquipment()
    {
        needToRecalculate = true;
    }

    protected void addEquipment(Equipment eq){
		List<Equipment> equipmentList = new List<Equipment>(equipment);
		equipmentList.Add (eq);
		equipment = equipmentList.ToArray ();
	}

	public Equipment[] getEquipment(){
		return equipment;
	}

    protected void removeEquipment(Equipment eq)
    {
        List<Equipment> equipmentList = new List<Equipment>(equipment);
        if (equipmentList.Contains(eq)){
            eq.setOwner(null);
            equipmentList.Remove(eq);
            equipment = equipmentList.ToArray();
        }
    }

	protected Equipment getEquipment(long uniqueId){
		for (int i = 0; i < equipment.Length; i++) {
			if (equipment [i].getUniqueId () == uniqueId) {
				return equipment [i];
			}
		}
		return null;
    }

    public Equipment[] getEquipmentWithType(Equipment.Type type){
        List<Equipment> eqWithTypeList = new List<Equipment>();
        for (int i = 0; i < equipment.Length; i++){
            if (equipment[i].getTypes().Contains(type)){
                eqWithTypeList.Add(equipment[i]);
            }
        }
        return eqWithTypeList.ToArray();
    }


    protected void getShipPartsFromGameObject()
    {
        Transform shipParts = transform.Find("ShipParts");

        Equipment eq;
        ShipPart shipPart;
        //float newDistanceOffset = 0f;
        if (shipParts)
        {
            for (int i = 0; i < shipParts.childCount; i++)
            {
                shipPart = shipParts.GetChild(i).GetComponent<ShipPart>();
                if (shipPart)
                {
                    volume += shipPart.getVolume();
                    //newDistanceOffset = Mathf.Max(newDistanceOffset, shipPart.getDistanceOffset());
                    addEquipment(shipPart.getEquipment());
                }
            }
            //distanceOffset = newDistanceOffset;
        }
        for (int i = 0; i < equipment.Length; i++)
        {
            equipment[i].setOwner(this);
        }
    }


    protected void equipmentRecalculate()
    {
        if (equipment.Length > 0 && needToRecalculate && initialProperties.enable == false)
        {
            needToRecalculate = false;
            equipmentSum = new Equipment.EquipmentSum(equipment);

            setMaxShield(equipmentSum.getPropertySum(Equipment.PropertyName.MaxShield));
            setMaxArmor(equipmentSum.getPropertySum(Equipment.PropertyName.MaxArmor));
            setMaxHull(equipmentSum.getPropertySum(Equipment.PropertyName.MaxHull));
            setMaxEnergy(equipmentSum.getPropertySum(Equipment.PropertyName.MaxEnergy));

            energyProduction = equipmentSum.getPropertySum(Equipment.PropertyName.EnergyProduction);
            energyDrainPassive = equipmentSum.getPropertySum(Equipment.PropertyName.EnergyConsumption);
            energyDrainActive = 0f;
            for (int i = 0; i < equipment.Length; i++) energyDrainActive += equipment[i].energyRequirementActive();

            shieldBalance = equipmentSum.getPropertySum(Equipment.PropertyName.ShieldRepair);
            armorBalance = equipmentSum.getPropertySum(Equipment.PropertyName.ArmorRepair);
            hullBalance = equipmentSum.getPropertySum(Equipment.PropertyName.HullRepair);

            setMass(equipmentSum.getMass());
            setDrag(equipmentSum.getPropertySum(Equipment.PropertyName.Drag));
            setDragAngular(equipmentSum.getPropertySum(Equipment.PropertyName.DragAngular));
            setEngineThrust(equipmentSum.getPropertySum(Equipment.PropertyName.EngineThrust));
            setManeuverability(equipmentSum.getPropertySum(Equipment.PropertyName.Maneuverability));

            maxNumberOfTargets = (int)equipmentSum.getPropertySum(Equipment.PropertyName.NumberOfTargets);

            calculateMaxVelocity();
            calculateCurrentMaxVelocity();
            checkIfTargetsAreInGoodNumber();

            onEquipmentRecalculate();
        }
    }

    public void setStateToEquipment(List<long> uniqueIds, Equipment.State newState)
    {
        bool reqRecalculate = false;
        for (int i = 0; i < equipment.Length; i++)
        {
            if (uniqueIds.Contains(equipment[i].getUniqueId()))
            {
                reqRecalculate = equipment[i].queueState(newState);
                if (reqRecalculate) setNeedRecalculateEquipment();
                //Debug.Log("Set state " + newState + " to " + equipment[i].getName() + "(" + equipment[i].getUniqueId() + ")");
            }
        }
        equipmentRecalculate();
    }

    protected virtual void onEquipmentRecalculate()
    {

    }


    public bool isEnergyStable(){
		return energyProduction >= energyDrainPassive;
	}

	protected void fixedUpdateEquipmentUse()
    {
        if (equipment.Length > 0)
        {
            equipmentRecalculate();

            bool needToRecalculateBecauseTime = false;
            for (int i = 0; i < equipment.Length; i++)
            {
                needToRecalculateBecauseTime = equipment[i].applyTime(Time.fixedDeltaTime);
                if (needToRecalculateBecauseTime)
                {
                    setNeedRecalculateEquipment();
                }
            }

            bool needToRecalculateBecauseChangedState = false;
            for (int i = 0; i < equipment.Length; i++)
            {
                needToRecalculateBecauseChangedState = equipment[i].attemptToChangeQueuedState();
                if (needToRecalculateBecauseChangedState)
                {
                    setNeedRecalculateEquipment();
                }
            }

            float energy = currentEnergy + energyProduction * Time.fixedDeltaTime;
            if (energy >= energyDrainPassive * Time.fixedDeltaTime)
            {
                //there is enought energy to power all systems
                energy -= energyDrainPassive * Time.fixedDeltaTime;
            }
            else
            {
                //there is not enough energy
                float energyRequirementPassive;
                for (int i = 0; i < equipment.Length; i++)
                {
                    energyRequirementPassive = equipment[i].energyRequirementPassive() * Time.fixedDeltaTime;
                    if (energyRequirementPassive > 0f)
                    {
                        if (energy - energyRequirementPassive > 0f)
                        {
                            energy -= energyRequirementPassive;
                        }
                        else
                        {
                            //equipment[i].setState(Equipment.State.TurnedOff);
                            equipment[i].turnOff();
                            setNeedRecalculateEquipment();
                        }
                    }
                }
            }

            if (energy > 0f)
            {
                float energyToSendSum = 0f;
                for (int i = 0; i < equipment.Length; i++)
                {
                    energyToSendSum += equipment[i].energyRequirementActive();
                }
                energyDrainActive = energyToSendSum;

                if (energyDrainActive > 0f)
                {
                    energyDrainActiveCoefficient = Mathf.Min(energy, energyDrainActive * Time.fixedDeltaTime) / (energyDrainActive * Time.fixedDeltaTime);
                }
                else
                {
                    energyDrainActiveCoefficient = 0f;
                }

                float energyToSend = 0f;
                float energyToReturn = 0f;
                if (energyDrainActiveCoefficient > 0f)
                {
                    for (int i = 0; i < equipment.Length; i++)
                    {
                        energyToSend = equipment[i].energyRequirementActive() * energyDrainActiveCoefficient * Time.fixedDeltaTime;
                        if (energyToSend > 0f)
                        {
                            energyToReturn = equipment[i].addEnergyLocalToBattery(energyToSend);
                            energy -= energyToSend;
                            energy += energyToReturn;
                        }
                    }
                }

                if (energy < 0f)
                {
                    energy = 0f;
                }
            }

            bool needToRecalculateBecauseHeat;
            bool needToRecalculateBecauseDurability;
            for (int i = 0; i < equipment.Length; i++)
            {
                needToRecalculateBecauseHeat = equipment[i].applyHeat(Time.fixedDeltaTime);
                needToRecalculateBecauseDurability = equipment[i].applyDurability(Time.fixedDeltaTime);
                if (needToRecalculateBecauseHeat || needToRecalculateBecauseDurability)
                {
                    setNeedRecalculateEquipment();
                }
            }

            setCurrentEnergy(energy);
        }
    }

	protected void fixedUpdateEquipmentProperties(){
		if(shieldBalance > 0f) addShield (shieldBalance * Time.fixedDeltaTime);
		if(armorBalance > 0f) addArmor (armorBalance * Time.fixedDeltaTime);
		if(hullBalance > 0f) addHull (hullBalance * Time.fixedDeltaTime);
	}
























































    //Targetting -----------------------------------------------------------------------------------------------------------------

    private List<ObjectInSpace> targets = new List<ObjectInSpace>();

    public int numberOfTargets() { return targets.Count; }
    public List<ObjectInSpace> getTargets() { return new List<ObjectInSpace>(targets.ToArray()); }
    public bool isTargetted(ObjectInSpace target) { return isTargetTargetted(target) >= 0; }

    public void fixedUpdateTargets()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i])
            {
                if (isInTargettingRange(targets[i]) == false)
                {
                    removeTarget(targets[i]);
                }
            }
            else
            {
                removeTargetFromEquipment(targets[i]);
                targets.RemoveAt(i);
            }
        }
    }

    public bool addTarget(ObjectInSpace target)
    {
        if (targets.Count < maxNumberOfTargets && isTargetTargetted(target) == -1)
        {
            targets.Add(target);
            //Debug.Log("ObjectInSpace Added target "+target.getName());
            return true;
        }
        return false;
    }

    public bool removeTarget(ObjectInSpace target)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i])
            {
                //if (targets[i].gameObject.GetInstanceID() == target.gameObject.GetInstanceID())
                if (targets[i] == target)
                {
                    removeTargetFromEquipment(target);
                    targets.RemoveAt(i);
                    return true;
                }
            }
        }
        return false;
    }

    public void addTargetToEquipment(List<long> uniqueIds, ObjectInSpace target)
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            if (uniqueIds.Contains(equipment[i].getUniqueId()))
            {
                equipment[i].setTarget(target);
                //Debug.Log("Added target " + target.getName() + " to " + equipment[i].getName() + "(" + equipment[i].getUniqueId() + ")");
            }
        }
    }

    private void removeTargetFromEquipment(ObjectInSpace target)
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            if (equipment[i].getTarget())
            {
                //if (equipment[i].getTarget().GetInstanceID() == target.GetInstanceID())
                if (equipment[i].getTarget() == target)
                {
                    equipment[i].setTarget(null);
                }
            }
        }
    }

    public int isTargetTargetted(ObjectInSpace target)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            //if (targets[i].gameObject.GetInstanceID() == target.gameObject.GetInstanceID())
            if (targets[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    public void checkIfTargetsAreInGoodNumber()
    {
        while (targets.Count > maxNumberOfTargets)
        {
            //Debug.Log("had to remove "+ targets[targets.Count - 1].getName());
            removeTarget(targets[targets.Count-1]);
        }
    }





































    //Damage -----------------------------------------------------------------------------------------------------------------


    public enum DamageType { Kinetic, Electric, Acid, Fire, Antimatter };


    public class DamageReport
    {
        public ObjectInSpace recepientOfDamage;
        public ObjectInSpace dealerOfDamage;
        public Vector3 damagePoint;
        public DamageType dmgType;
        public float maxShield;
        public float maxArmor;
        public float maxHull;
        public float currentShield;
        public float currentArmor;
        public float currentHull;
        public float damageShield;
        public float damageArmor;
        public float damageHull;
        public float damageSum;
        public float damageOverkill;
        public float remainingShield; //currentShield - damageShield
        public float remainingArmor; //currentArmor - damageArmor
        public float remainingHull; //currentHull - damageHull
        public bool isDestroyed;
        public float overkill;
        public DamageReport(ObjectInSpace _recepientOfDamage, ObjectInSpace _dealerOfDamage, DamageType _dmgType)
        {
            recepientOfDamage = _recepientOfDamage;
            dealerOfDamage = _dealerOfDamage;
            damagePoint = recepientOfDamage.gameObject.transform.position;
            dmgType = _dmgType;
            maxShield = recepientOfDamage.maxShield;
            maxArmor = recepientOfDamage.maxArmor;
            maxHull = recepientOfDamage.maxHull;
            currentShield = recepientOfDamage.currentShield;
            currentArmor = recepientOfDamage.currentArmor;
            currentHull = recepientOfDamage.currentHull;
            damageShield = 0f;
            damageArmor = 0f;
            damageHull = 0f;
            damageSum = 0f;
            damageOverkill = 0f;
            remainingShield = recepientOfDamage.currentShield;
            remainingArmor = recepientOfDamage.currentArmor;
            remainingHull = recepientOfDamage.currentHull;
            isDestroyed = false;
            overkill = 0f;
        }

        public string damageDealtReport()
        {
            return damageShield + ", " + damageArmor + ", " + damageHull;
        }

        public string damageMessage()
        {
            string str = "";

            if (damageSum > 0f)
            {
                if (isDestroyed)
                {
                    str = dealerOfDamage.getName() + " dealt " + Mathf.Floor(damageSum * 100f) / 100f + " " + dmgType + " damage to " + recepientOfDamage.getName() + " and destroyed it ";
                }
                else
                {
                    str = dealerOfDamage.getName() + " dealt " + Mathf.Floor(damageSum * 100f) / 100f + " " + dmgType + " damage to " + recepientOfDamage.getName() + " ";
                }

                List<string> dmgStrList = new List<string>();
                if (damageShield > 0f) dmgStrList.Add(Mathf.Floor(damageShield * 100f) / 100f + " to shield");
                if (damageArmor > 0f) dmgStrList.Add(Mathf.Floor(damageArmor * 100f) / 100f + " to armor");
                if (damageHull > 0f) dmgStrList.Add(Mathf.Floor(damageHull * 100f) / 100f + " to hull");

                str += "(";
                for (int i = 0; i < dmgStrList.Count; i++)
                {
                    str += dmgStrList[i];
                    if (i + 1 < dmgStrList.Count) str += ", ";
                }
                str += ")";

                if (damageOverkill > 0f)
                {
                    str += ", " + Mathf.Floor(damageOverkill * 100f) / 100f + " overkill";
                }
                str += ".";
            }
            else
            {
                str = dealerOfDamage.getName() + " dealt no damage to " + recepientOfDamage.getName() + ".";
            }

            return str;
        }
    }

    public DamageReport dealDamage(ObjectInSpace delerOfDamage, float dmgAmount, DamageType dmgType, Vector3 damagePoint)
    {
        DamageReport dmgReport = new DamageReport(this, delerOfDamage, dmgType);
        dmgReport.damagePoint = damagePoint;

        if (dmgAmount > 0f)
        {
            dmgAmount = dealDamageToShield(dmgReport, dmgAmount, dmgType);
            dmgAmount = dealDamageToArmor(dmgReport, dmgAmount, dmgType);
            dmgAmount = dealDamageToHull(dmgReport, dmgAmount, dmgType);

            if (dmgReport.damageSum > 0f)
            {
                damageEffect();
                GameObject.Find("InterfaceGame").GetComponent<InterfaceScript>().createDamageText(dmgReport.damagePoint, "" + Mathf.Ceil(dmgReport.damageSum));
                GameObject.Find("InterfaceGame").GetComponent<InterfaceScript>().addMesage(dmgReport.damageMessage());
            }

            if (dmgReport.remainingHull <= 0f)
            {
                dmgReport.isDestroyed = true;
                destroyByDamage();
            }
        }

        return dmgReport;
    }

    private float dealDamageToShield(DamageReport dmgReport, float dmgAmount, DamageType dmgType)
    {
        if (dmgAmount > 0f)
        {
            if (getCurrentShield() > 0f)
            {
                if (getCurrentShield() >= dmgAmount)
                {
                    addShield(-dmgAmount);
                    dmgReport.damageShield = dmgAmount;
                    dmgReport.damageSum += dmgAmount;
                    dmgAmount = 0f;
                }
                else
                {
                    float dmgAmountToZeroShield = getCurrentShield();
                    addShield(-dmgAmountToZeroShield);
                    dmgReport.damageShield = dmgAmountToZeroShield;
                    dmgReport.damageSum += dmgAmountToZeroShield;
                    dmgAmount = dmgAmount - dmgAmountToZeroShield;
                }
                dmgReport.remainingShield = dmgReport.currentShield - dmgReport.damageShield;
            }
        }

        return dmgAmount;
    }

    private float dealDamageToArmor(DamageReport dmgReport, float dmgAmount, DamageType dmgType)
    {
        if (dmgAmount>0f)
        {
            if (getCurrentArmor() > 0f)
            {
                if (getCurrentArmor() >= dmgAmount)
                {
                    addArmor(-dmgAmount);
                    dmgReport.damageArmor = dmgAmount;
                    dmgReport.damageSum += dmgAmount;
                    dmgAmount = 0f;
                }
                else
                {
                    float dmgAmountToZeroArmor = getCurrentArmor();
                    addArmor(-dmgAmountToZeroArmor);
                    dmgReport.damageArmor = dmgAmountToZeroArmor;
                    dmgReport.damageSum += dmgAmountToZeroArmor;
                    dmgAmount = dmgAmount - dmgAmountToZeroArmor;
                }
                dmgReport.remainingArmor = dmgReport.currentArmor - dmgReport.damageArmor;
            }
        }

        return dmgAmount;
    }

    private float dealDamageToHull(DamageReport dmgReport, float dmgAmount, DamageType dmgType)
    {
        if (dmgAmount > 0f)
        {
            if (getCurrentHull() > 0f)
            {
                if (getCurrentHull() >= dmgAmount)
                {
                    addHull(-dmgAmount);
                    dmgReport.damageHull = dmgAmount;
                    dmgReport.damageSum += dmgAmount;
                    dmgAmount = 0f;
                }
                else
                {
                    float dmgAmountToZeroHull = getCurrentHull();
                    addHull(-dmgAmountToZeroHull);
                    dmgReport.damageHull = dmgAmountToZeroHull;
                    dmgReport.damageSum += dmgAmountToZeroHull;
                    dmgReport.damageOverkill = dmgAmount - dmgAmountToZeroHull;
                    dmgReport.isDestroyed = true;
                    dmgAmount = 0f;
                }
                dmgReport.remainingHull = dmgReport.currentHull - dmgReport.damageHull;
            }
        }

        return dmgAmount;
    }












    protected void detachShipPartsAsDebris()
    {
        ShipPart shipPart = null;
        GameObject newDebris = null;
        GameObject newDebrisShipPart = null;
        while (equipment.Length>0)
        {
            shipPart = equipment[0].getShipPart();
            if (shipPart)
            {
                newDebris = new GameObject("ShipDebris");
                newDebris.tag = "ObjectInSpace";
                newDebris.AddComponent<Rigidbody>();
                newDebris.AddComponent<ObjectInSpace>();
                newDebris.transform.position = shipPart.gameObject.transform.position;

                newDebrisShipPart = new GameObject("ShipParts");
                newDebrisShipPart.transform.parent = newDebris.transform;

                shipPart.gameObject.transform.parent = newDebrisShipPart.transform;

                newDebris.GetComponent<ObjectInSpace>().niceName = "ShipDebris";
                newDebris.GetComponent<ObjectInSpace>().isDebris = true;
                newDebris.GetComponent<ObjectInSpace>().canBeTargetted = true;
                newDebris.GetComponent<ObjectInSpace>().canBeDamaged = true;
                newDebris.GetComponent<ObjectInSpace>().initialProperties = new InitialProperties();
                newDebris.GetComponent<ObjectInSpace>().initialProperties.enable = true;
                newDebris.GetComponent<ObjectInSpace>().initialProperties.maxHull = 500f;
                newDebris.GetComponent<ObjectInSpace>().initialProperties.mass = equipment[0].getMass();
                newDebris.GetComponent<ObjectInSpace>().initialProperties.drag = 1f;
                newDebris.GetComponent<ObjectInSpace>().initialProperties.dragAngular = 1f;

                newDebris.GetComponent<Rigidbody>().AddForce((newDebris.transform.position - transform.position).normalized * Random.Range(4000f, 7000f));

                shipPart.shutDown();
                removeEquipment(equipment[0]);
            }
        }
    }



    protected virtual void damageEffect(){
        //Debug.Log("damageEffect - " + getName());
    }

	protected virtual void destroyByDamage(){
        //Debug.Log ("destroyByDamage - "+getName());
        if (isDebris == false) detachShipPartsAsDebris();
        OnDestroy(); ;
        Destroy(gameObject);
    }

	protected virtual void OnDestroy() {
		//Debug.Log("OnDestroy " + getName());
	}


}

