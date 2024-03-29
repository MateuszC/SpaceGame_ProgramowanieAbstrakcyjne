using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour {

	public int equipmentId = 0;

	protected Equipment equipment = null;
    private double volume = 0f;
    private bool isVolumeCalculated = false;

    void Start ()
    {
        getEquipment();
        calculateVolume();
    }

    public Equipment getEquipment()
    {
        setEquipment();
        return equipment;
    }

    public double getVolume()
    {
        calculateVolume();
        return volume;
    }

    protected void setEquipment()
    {
        if (equipment == null)
        {
            equipment = GMS.instance().getEquipment(equipmentId);
            equipment.setShipPart(this);
            equipment.setTarget(null);
            equipment.setOwner(gameObject.transform.parent.transform.parent.gameObject.GetComponent<ObjectInSpace>());
        }
    }

    protected void calculateVolume()
    {
        if (isVolumeCalculated==false)
        {
            List<MeshFilter> meshFilterList = new List<MeshFilter>();
            List<GameObject> gameObjectList = new List<GameObject>();
            gameObjectList.Add(gameObject);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObjectList.Add(gameObject.transform.GetChild(i).gameObject);
                gameObjectList.AddRange(GMS.getChildrenOfThisGameObject(gameObject.transform.GetChild(i).gameObject));
            }

            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i].GetComponent<MeshFilter>())
                {
                    meshFilterList.Add(gameObjectList[i].GetComponent<MeshFilter>());
                }
            }

            Vector3 dimensions = Vector3.zero;
            double volumeSum = 0f;

            for (int i = 0; i < meshFilterList.Count; i++)
            {
                dimensions = meshFilterList[i].gameObject.transform.lossyScale * Mathf.Pow(GMS.VolumeOfMesh(meshFilterList[i].mesh), 1f / 3f);
                volumeSum += (double)(dimensions.x) * (double)(dimensions.y) * (double)(dimensions.z);
            }

            volume = volumeSum;
            isVolumeCalculated = true;
        }
    }

    public float getDistanceOffset()
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        gameObjectList.Add(gameObject);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObjectList.Add(gameObject.transform.GetChild(i).gameObject);
            gameObjectList.AddRange(GMS.getChildrenOfThisGameObject(gameObject.transform.GetChild(i).gameObject));
        }

        List<Collider> colliderFilterList = new List<Collider>();
        for (int i = 0; i < gameObjectList.Count; i++)
        {
            if(gameObjectList[i].GetComponent<Collider>()) colliderFilterList.Add(gameObjectList[i].GetComponent<Collider>());
        }

        float distanceOffset = 0f;
        GameObject gameObjectParent = getEquipment().getOwner().gameObject;
        for (int i = 0; i < colliderFilterList.Count; i++)
        {
            distanceOffset = Mathf.Max(distanceOffset, Vector3.Distance(gameObjectParent.transform.position, colliderFilterList[i].bounds.center) + colliderFilterList[i].bounds.extents.magnitude);
        }


        return distanceOffset;
    }

    public virtual void actionOnActive()
    {

    }

    public virtual void onStateChange()
    {

    }

    public void shutDown()
    {
        equipment.turnOff();
        equipment.setTarget(null);
        equipment.setOwner(null);
        onShutDown();
    }

    protected virtual void onShutDown()
    {

    }
}
