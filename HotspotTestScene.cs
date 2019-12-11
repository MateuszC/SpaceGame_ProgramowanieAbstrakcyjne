using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotTestScene : MonoBehaviour {

    public ObjectInSpace ship;
    public ObjectInSpace enemy;

    private Equipment[] equipment;
    private List<GameObject> cubeList = new List<GameObject>();

    void Start ()
    {
        GameObject cube;
        for (int i=0;i<10;i++)
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(1000f, 1000f, 1000f);
            Destroy(cube.GetComponent<Collider>());
            cubeList.Add(cube);
        }
        Invoke("startEq", 2f);
    }

    private void startEq()
    {
        equipment = ship.getEquipment();
        for (int i = 0; i < equipment.Length; i++)
        {
            equipment[i].setTarget(enemy);
            equipment[i].activate();
        }
    }

    private List<ObjectInSpace> missiles = new List<ObjectInSpace>();
	
	void FixedUpdate ()
    {
        enemy.gameObject.transform.Rotate(Vector3.forward * Time.fixedDeltaTime * Random.Range(5f, 10f));
        enemy.gameObject.transform.Rotate(     Vector3.up * Time.fixedDeltaTime * Random.Range(5f, 10f));
        enemy.gameObject.transform.Rotate(  Vector3.right * Time.fixedDeltaTime * Random.Range(5f, 10f));

        GameObject[] objectsInSpace = GameObject.FindGameObjectsWithTag("ObjectInSpace");
        List<GameObject> objectsInSpaceList = new List<GameObject>(objectsInSpace);
        objectsInSpaceList.RemoveAt(0);
        objectsInSpaceList.RemoveAt(0);
        objectsInSpace = objectsInSpaceList.ToArray();

        for (int i = 0; i < objectsInSpace.Length; i++)
        {
            if (objectsInSpace[i] != ship && objectsInSpace[i] != enemy)
            {
                if (missiles.Contains(objectsInSpace[i].GetComponent<ObjectInSpace>()) == false)
                {
                    missiles.Add(objectsInSpace[i].GetComponent<ObjectInSpace>());
                }
            }
        }
        for (int i = 0; i < missiles.Count; i++)
        {
            if (missiles[i] == null)
            {
                missiles.RemoveAt(i);
                i = 0;
            }
        }

        for (int i = 0; i < missiles.Count; i++)
        {
            cubeList[i].transform.position = new Vector3(1000f, 1000f, 1000f);
            if(missiles[i].GetComponent<MissileScript>().getHotspotHolder()) cubeList[i].transform.position = missiles[i].GetComponent<MissileScript>().getHotspotHolder().bounds.center;
        }
    }
}
