using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMS{

	public static GMS _instance = null;

	public static GMS instance(){
		if(_instance==null){
			_instance = new GMS ();
		}
		return _instance;
	}







	private long highestIndex = 123;
	private Equipment.EquipmentDatabase eDB = new Equipment.EquipmentDatabase ();

	private GMS(){
		
	}


	public long getUniqueId(){
		highestIndex++;
		//Debug.Log ("unique id is "+(highestIndex - 1));
		return highestIndex - 1;
	}

	public Equipment getEquipment(int id){
		return eDB.getEquipment(id);
	}





    public static Vector3 randomVector() { return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); }
    public static Vector3 randomVector(float range) { return randomVector() * range; }
    public static Vector3 randomVectorRange(float rangeFrom, float rangeTo) { return randomVector(Random.Range(Mathf.Min(rangeFrom, rangeTo), Mathf.Max(rangeFrom, rangeTo))); }










    public static bool fileWrite(string fileName, string text) { return GMS.fileWrite(fileName, text, false); }
    public static bool fileWriteLn(string fileName, string text) { return GMS.fileWrite(fileName, text, true); }
    public static void fileDelete(string fileName) { System.IO.File.Delete(Application.dataPath + "/GameFiles/" + fileName); }
    public static void fileClear(string fileName) { System.IO.File.WriteAllText(Application.dataPath + "/GameFiles/" + fileName, ""); }

    private static bool fileWrite(string fileName, string text, bool writeLine)
    {
        //text = text.Trim();
        if (text.Length > 0)
        {
            string path = Application.dataPath + "/GameFiles/" + fileName;
            if (System.IO.File.Exists(path) == false)
            {
                System.IO.File.WriteAllText(path, "");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                if (writeLine) file.WriteLine(text);
                else file.Write(text);
                return true;
            }
        }
        return false;
    }

    public static List<GameObject> getChildrenOfThisGameObject(GameObject g)
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        for (int i = 0; i < g.transform.childCount; i++)
        {
            gameObjectList.Add(g.transform.GetChild(i).gameObject);
            gameObjectList.AddRange(getChildrenOfThisGameObject(g.transform.GetChild(i).gameObject));
        }

        return gameObjectList;
    }

    public static List<GameObject> getParentsOfThisGameObject(GameObject g)
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        while (g.transform.parent)
        {
            gameObjectList.Add(g.transform.parent.gameObject);
            g = g.transform.parent.gameObject;
        }

        return gameObjectList;
    }

    public static string getParentsOfThisGameObjectStr(GameObject g)
    {
        List<GameObject> parents = getParentsOfThisGameObject(g);
        parents.Reverse();
        string str = "";
        foreach (GameObject p in parents)
        {
            str += p.name + ", ";
        }
        str = str.Trim();
        str = str.Substring(0, str.Length-1);

        return str;
    }

    public static float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    public static float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }

    public static float VolumeOfMeshLossyScale(MeshFilter meshFilter)
    {
        return VolumeOfMesh(meshFilter.mesh) * (meshFilter.gameObject.transform.lossyScale.x * meshFilter.gameObject.transform.lossyScale.y * meshFilter.gameObject.transform.lossyScale.z);
    }



    public static float getShipPartVolume(ShipPart shipPart)
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        gameObjectList.Add(shipPart.gameObject);
        for (int i = 0; i < shipPart.gameObject.transform.childCount; i++)
        {
            gameObjectList.Add(shipPart.gameObject.transform.GetChild(i).gameObject);
            gameObjectList.AddRange(getChildrenOfThisGameObject(shipPart.gameObject.transform.GetChild(i).gameObject));
        }

        GMS.fileWriteLn("MeshSizes", gameObjectList.Count+"");
        GMS.fileWriteLn("MeshSizes", "Master: " + shipPart.gameObject.name);
        float volumeOfMesh = 0;
        float volumeOfCollider = 0;
        float volumeOfColliderSum = 0;
        Vector3 dimensions = Vector3.zero;
        for (int i = 0; i < gameObjectList.Count; i++)
        {
            volumeOfMesh = 0;
            volumeOfCollider = 0;
            dimensions = Vector3.zero;

            if (gameObjectList[i].GetComponent<MeshFilter>())
            {
                volumeOfMesh = GMS.VolumeOfMesh(gameObjectList[i].GetComponent<MeshFilter>().mesh);
                dimensions = gameObjectList[i].transform.lossyScale * Mathf.Pow(volumeOfMesh, 1f / 3f);
            }

            GMS.fileWrite("MeshSizes", "name: " + gameObjectList[i].name + "; ");
            GMS.fileWrite("MeshSizes", "parents: "+GMS.getParentsOfThisGameObjectStr(gameObjectList[i]) + "; ");
            GMS.fileWrite("MeshSizes", "volumeOfMesh = " + volumeOfMesh + "u; ");
            GMS.fileWrite("MeshSizes", "dimensions = " + dimensions + "u; ");
            GMS.fileWrite("MeshSizes", "dimensionsMeters = " + ObjectInSpace.unitsToMeters(dimensions) + "m; ");
            GMS.fileWrite("MeshSizes", "dimensionsMetersVolume = " + (ObjectInSpace.unitsToMeters(dimensions).x * ObjectInSpace.unitsToMeters(dimensions).y * ObjectInSpace.unitsToMeters(dimensions).z) + "m^3; ");
            GMS.fileWrite("MeshSizes", "volumeOfCollider = " + volumeOfCollider + "u; ");
            GMS.fileWriteLn("MeshSizes", " ");
        }

        volumeOfColliderSum = Mathf.Round(volumeOfColliderSum);

        GMS.fileWrite("MeshSizes", "Master: " + shipPart.gameObject.name+", ");
        GMS.fileWrite("MeshSizes", "volumeOfColliderSum = " + volumeOfColliderSum + "u; ");
        GMS.fileWrite("MeshSizes", "volumeOfColliderSum = " + ObjectInSpace.unitsToMetersCubic(volumeOfColliderSum) + "m^3; ");
        GMS.fileWriteLn("MeshSizes", " ");

        return 0f;
    }






    //https://math.stackexchange.com/questions/1599095/how-to-find-the-equidistant-middle-point-for-3-points-on-an-arbitrary-polygon
    public static Vector3 calculateEquidistant(List<Vector3> list)
    {


        return Vector3.zero;
    }


}
