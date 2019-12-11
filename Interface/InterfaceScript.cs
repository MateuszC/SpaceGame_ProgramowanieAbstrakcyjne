using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceScript : MonoBehaviour{

    public Camera mainCamera;
    public ObjectInSpace playerShip;
    public GameObject equipmentTilePrefab;
    public GameObject overviewItemPrefab;
    public GameObject overviewDropdownPrefab;
    public GameObject targetTilePrefab;
    public GameObject damageTextPrefab;
    public GameObject messagePrefab;

    private ObjectInSpace owner = null;

    //Debug Text ----------------------------------------
    private UnityEngine.UI.Text debugText = null;

    //Equipment Panel and Tiles ----------------------------------------
    private RectTransform equipmentTilesHolder = null;
    private Equipment[] equipment = new Equipment[0];
    private GameObject[] equipmentTilesGameObjects = new GameObject[0];
    private InterfaceEquipmentTile[] equipmentTiles = new InterfaceEquipmentTile[0];
    private UnityEngine.UI.Button tileScaleAddButton;
    private UnityEngine.UI.Button tileScaleSubButton;
    private UnityEngine.UI.Button topPanelButton1;
    private UnityEngine.UI.Button topPanelButton2;
    private UnityEngine.UI.Button topPanelButton3;

    //Overview ----------------------------------------
    private Transform overviewTransform = null;

    //Target Tiles ----------------------------------------
    private RectTransform targetTilesHolder = null;

    //Messages ----------------------------------------
    private Transform messagesHolder = null;

    //Colors ----------------------------------------
    public static Color32 colorTransparent = new Color32(255, 255, 255, 0);
    public static Color32 colorNormal = new Color32(125, 255, 255, 100);
    public static Color32 colorHover = new Color32(125, 200, 255, 100);
    public static Color32 colorHighlighted = new Color32(125, 150, 255, 100);
    public static Color32 colorSelected = new Color32(125, 100, 255, 100);

    private bool interfaceReady = true;

    void Start () {
        Invoke("startInterface", 0.2f);
    }

    private void startInterface()
    {
        if (playerShip)
        {
            Equipment[] eq = playerShip.getEquipment();
            if (eq.Length > 0)
            {

                debugText = transform.Find("DebugLog/Text").GetComponent<UnityEngine.UI.Text>();
                equipmentTilesHolder = transform.Find("ShipEquipment").GetComponent<RectTransform>();
                tileScaleAddButton = transform.Find("ShipEquipment/TopPanel/TileScaleAddButton").GetComponent<UnityEngine.UI.Button>();
                tileScaleSubButton = transform.Find("ShipEquipment/TopPanel/TileScaleSubButton").GetComponent<UnityEngine.UI.Button>();
                topPanelButton1 = transform.Find("ShipEquipment/TopPanel/TopPanelButton1").GetComponent<UnityEngine.UI.Button>();
                topPanelButton2 = transform.Find("ShipEquipment/TopPanel/TopPanelButton2").GetComponent<UnityEngine.UI.Button>();
                topPanelButton3 = transform.Find("ShipEquipment/TopPanel/TopPanelButton3").GetComponent<UnityEngine.UI.Button>();
                tileScaleAddButton.onClick.AddListener(tileScaleAddButtonClick);
                tileScaleSubButton.onClick.AddListener(tileScaleSubButtonClick);
                topPanelButton1.onClick.AddListener(topPanelButton1Click);
                topPanelButton2.onClick.AddListener(topPanelButton2Click);
                topPanelButton3.onClick.AddListener(topPanelButton3Click);

                overviewTransform = transform.Find("Overview/Viewport/Content");

                targetTilesHolder = transform.Find("TargetTilesHolder").GetComponent<RectTransform>();

                messagesHolder = transform.Find("Messages/Viewport/Content");

                setEquipment(eq);
                setOwner(playerShip);

                InvokeRepeating("refreshOverview", 0f, 1f);
            }
        }
    }

    void FixedUpdate()
    {

    }

    public void setOwner(ObjectInSpace _owner)
    {
        owner = _owner;
    }

    public Camera getCamera()
    {
        return mainCamera;
    }




















    //Equipment Panel and Tiles -----------------------------------------------------------------------------------------------------------------


    private float scale = 0.625f;
    private int indexStart = 0;
    private int indexEnd = 1;
    private int indexHover = 0;
    private List<long> uniqueIds = new List<long>();

    public void setEquipment(Equipment[] eq)
    {
        for (int i = 0; i < equipmentTilesGameObjects.Length; i++)
        {
            Destroy(equipmentTilesGameObjects[i]);
        }

        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].remove();
        }

        List<Equipment> newEquipmentList = new List<Equipment>();
        List<InterfaceEquipmentTile> tileList = new List<InterfaceEquipmentTile>();
        List<GameObject> tileGOList = new List<GameObject>();
        for (int i = 0; i < eq.Length; i++)
        {
            if (eq[i].isInoperative() == false)
            {
                GameObject equipmentElement = (GameObject)Instantiate(equipmentTilePrefab, equipmentTilesHolder.transform.position, equipmentTilesHolder.transform.rotation, equipmentTilesHolder.transform);
                InterfaceEquipmentTile equipmentTile = equipmentElement.GetComponent<InterfaceEquipmentTile>();
                equipmentTile.setEquipment(eq[i]);
                equipmentTile.setIndex(i + 1);
                equipmentTile.setMaster(this);

                newEquipmentList.Add(eq[i]);
                tileGOList.Add(equipmentElement);
                tileList.Add(equipmentTile);
            }
        }
        equipment = newEquipmentList.ToArray();
        equipmentTiles = tileList.ToArray();
        equipmentTilesGameObjects = tileGOList.ToArray();

        drawEquipment();
    }

    private void drawEquipment()
    {
        float eqDetailWidth = 81f * scale;
        float eqDetailHeight = 61f * scale;
        float offsetStartX = (-equipmentTilesHolder.rect.width / 2f + eqDetailWidth / 2f);
        float offsetStartY = (equipmentTilesHolder.rect.height / 2f - eqDetailHeight / 2f) - 20f;
        float offsetX = offsetStartX;
        float offsetY = offsetStartY;
        int eqInRow = (int)Mathf.Floor(equipmentTilesHolder.rect.width / eqDetailWidth);

        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].gameObject.transform.localPosition = new Vector3(offsetX, offsetY, equipmentTiles[i].gameObject.transform.position.z);
            equipmentTiles[i].gameObject.transform.localScale = new Vector3(scale, scale, scale);

            offsetX += eqDetailWidth;
            if ((i + 1) % eqInRow == 0)
            {
                offsetX = offsetStartX;
                offsetY -= eqDetailHeight;
            }
        }
        equipmentDetailElementRefreshColor();
    }

    private void tileScaleAddButtonClick() { increseTilesScale(0.025f); drawEquipment(); }
    private void tileScaleSubButtonClick() { increseTilesScale(-0.025f); drawEquipment(); }

    public void increseTilesScale(float s)
    {
        scale += s;
    }




    public void equipmentDetailElementClickedOn(int index)
    {
        if (indexEnd > 0)
        {
            indexStart = index;
            indexEnd = 0;
        }
        else
        {
            if(index >= indexStart)
            {
                indexEnd = index;
            }
            else
            {
                indexEnd = indexStart;
                indexStart = index;
            }

            uniqueIds = new List<long>();
            for (int i = 0; i < equipmentTiles.Length; i++)
            {
                if (equipmentTiles[i].index >= indexStart && equipmentTiles[i].index <= indexEnd) uniqueIds.Add(equipmentTiles[i].getEquipment().getUniqueId());
            }

            /*
            string str = "";
            for (int i = 0; i < uniqueIds.Count; i++)
            {
                str += uniqueIds[i]+", ";
            }
            Debug.Log(str);
            */
        }
        equipmentDetailElementRefreshColor();
        //Debug.Log("indexStart = " + indexStart + ", indexEnd = " + indexEnd + ", indexHover = " + indexHover);
    }

    public void equipmentDetailElementMouseEnter(int index)
    {
        indexHover = index;
        equipmentDetailElementRefreshColor();
        //Debug.Log("indexStart = " + indexStart + ", indexEnd = " + indexEnd + ", indexHover = " + indexHover);
    }

    public void equipmentDetailElementMouseLeave(int index)
    {
        indexHover = 0;
        equipmentDetailElementRefreshColor();
        //Debug.Log("indexStart = " + indexStart + ", indexEnd = " + indexEnd + ", indexHover = " + indexHover);
    }

    public void equipmentSetState(Equipment.State newState)
    {
        if (uniqueIds.Count > 0)
        {
            owner.setStateToEquipment(uniqueIds, newState);
            clearSelection();
        }
    }

    public void equipmentSetTarget(ObjectInSpace target)
    {
        if (uniqueIds.Count > 0)
        {
            owner.addTargetToEquipment(uniqueIds, target);
            clearSelection();
        }
    }


    public void clearSelection()
    {
        indexStart = 0;
        indexEnd = 1;
        uniqueIds = new List<long>();
        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].setSelected(false);
        }
        equipmentDetailElementRefreshColor();
    }


    public void equipmentDetailElementRefreshColor()
    {
        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].setNextColor(InterfaceEquipmentTile.colorNormal);
            if (equipmentTiles[i].index == indexHover) equipmentTiles[i].setNextColor(InterfaceEquipmentTile.colorHover);
        }

        if (indexStart > 0 && indexEnd == 0) //highlight from indexStart to indexHover
        {
            for (int i = 0; i < equipmentTiles.Length; i++)
            {
                if (indexStart >= indexHover)
                {
                    if (equipmentTiles[i].index >= indexHover && equipmentTiles[i].index <= indexStart) equipmentTiles[i].setNextColor(InterfaceEquipmentTile.colorHighlighted);
                }
                else
                {
                    if (equipmentTiles[i].index >= indexStart && equipmentTiles[i].index <= indexHover) equipmentTiles[i].setNextColor(InterfaceEquipmentTile.colorHighlighted);
                }
            }
        }

        if (indexStart > 0 && indexEnd > 0) //select from indexStart to indexEnd
        {
            bool selectElement;
            for (int i = 0; i < equipmentTiles.Length; i++)
            {
                selectElement = false;
                if (equipmentTiles[i].index >= indexStart && equipmentTiles[i].index <= indexEnd) selectElement = true;
                if (selectElement)
                {
                    equipmentTiles[i].setNextColor(InterfaceEquipmentTile.colorSelected);
                    equipmentTiles[i].setSelected(true);
                }
                else
                {
                    equipmentTiles[i].setSelected(false);
                }
            }
        }

        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].applyColor();
        }
    }



    /*
        id;iconName;name;description;types;mass;size;requireTarget;canBeTurnOn;canBeActivated;canBeOverdrive;poweringUpTime;poweringDownTime;durabilityMax;heatMax;
        1;2_64_11;Ship Core;Core of the ship with basic defenses;ShipCore,Mobility, ManeuverabilityEnchancer;5000;Technocratic;FALSE;FALSE;FALSE;FALSE;0;0;0;0;
        2;1_64_1;Nuclear Generator;Energy generator that creates power.;Energy, EnergyGenerator;1000;Amateurish;FALSE;TRUE;FALSE;TRUE;2;5;100;500;
        3;1_64_7;Standard Battery;Battery that holds power;Energy, EnergyBattery;500;Amateurish;FALSE;TRUE;FALSE;FALSE;2;5;50;50;
        4;3_64_2;Fuel Engine Booster;Boost your engine;Mobility, Engine;500;Amateurish;FALSE;TRUE;FALSE;TRUE;2;5;50;50;
        5;1_64_15;Shield Generator;System that creates barrier around the ship.;Defence, Endurance, ShieldGenerator;500;Amateurish;FALSE;TRUE;FALSE;TRUE;2;5;100;50;
        6;2_64_3;Shield Booster;System for supporting shield regeneration;Defence, RepairSystem, ShieldRepair;50;Amateurish;FALSE;TRUE;TRUE;TRUE;2;5;50;50;
        7;2_64_2;Armor Reinforcer;System for supporting armor reinforcement;Defence, RepairSystem, ArmorRepair;50;Amateurish;FALSE;TRUE;TRUE;TRUE;2;5;50;50;
        8;2_64_4;Hull Repairer;System for supporting hull repairs;Defence, RepairSystem, HullRepair;50;Amateurish;FALSE;TRUE;TRUE;TRUE;2;5;50;50;
        9;12_64_16;Missile Launcher;Basic missile launcher;Weapon, MissileLauncher;500;Amateurish;TRUE;TRUE;TRUE;TRUE;2;5;100;50;
        10;5_64_17;Agility Plates;Wings that increase ability to maneuver;Mobility, ManeuverabilityEnchancer;100;Amateurish;FALSE;FALSE;FALSE;FALSE;0;0;0;0;
        11;3_64_9;Targetting Antena;Can see targets further;Targetting, TargetRange, TargetNumber;100;Amateurish;FALSE;TRUE;FALSE;FALSE;5;5;0;0;
        12;3_64_9;Energy Generator;Drain your shield to produce energy;Energy, EnergyGenerator;100;Amateurish;FALSE;TRUE;FALSE;FALSE;2;2;50;50;
        13;;Hull Block;Hull;HullReinforcement;20;Amateurish;FALSE;FALSE;FALSE;FALSE;0;0;0;0;
        99;12_64_16;Test Missile Launcher;Basic missile launcher;Weapon, MissileLauncher;500;Amateurish;TRUE;TRUE;TRUE;TRUE;0.1;0.1;0;0;         
     */

    private void topPanelButton1Click()
    {
        List<Equipment> equipmentList1 = new List<Equipment>();
        List<Equipment> equipmentList2 = new List<Equipment>();
        List<Equipment> equipmentList3 = new List<Equipment>();
        equipmentList1.AddRange(owner.getEquipmentWithType(Equipment.Type.Engine));
        equipmentList2.AddRange(owner.getEquipmentWithType(Equipment.Type.EnergyBattery));
        equipmentList3.AddRange(owner.getEquipmentWithType(Equipment.Type.EnergyGenerator));

        foreach (Equipment e in equipmentList1) e.turnOn();
        foreach (Equipment e in equipmentList2) e.turnOn();
        foreach (Equipment e in equipmentList3) e.overdrive();
    }

    private void topPanelButton2Click()
    {
        List<Equipment> equipmentList = new List<Equipment>();
        equipmentList.AddRange(owner.getEquipmentWithType(Equipment.Type.MissileLauncher));

        foreach (Equipment e in equipmentList)
        {
            e.activate();
        }
    }

    private List<Equipment> topPanelButton3ClickEquipmentList;
    private void topPanelButton3Click()
    {
        List<Equipment> equipmentList = new List<Equipment>();
        equipmentList.AddRange(owner.getEquipmentWithType(Equipment.Type.MissileLauncher));
        topPanelButton3ClickEquipmentList = new List<Equipment>();
        int randomIndex = 0;
        while (equipmentList.Count>0)
        {
            randomIndex = Random.Range(0, equipmentList.Count-1);
            topPanelButton3ClickEquipmentList.Add(equipmentList[randomIndex]);
            equipmentList.RemoveAt(randomIndex);
        }

        float timeMax = 4f;
        float timeDifference = timeMax / topPanelButton3ClickEquipmentList.Count;

        for (int i = 0; i < topPanelButton3ClickEquipmentList.Count; i++)
        {
            Invoke("topPanelButton3Click_activateLauncher", i * timeDifference);
        }
    }

    private void topPanelButton3Click_activateLauncher()
    {
        topPanelButton3ClickEquipmentList[0].activate();
        topPanelButton3ClickEquipmentList.RemoveAt(0);
    }










































    //Overview -----------------------------------------------------------------------------------------------------------------
    private List<InterfaceOverviewItemScript> overviewItemList = new List<InterfaceOverviewItemScript>();

    private void refreshOverview()
    {
        overviewListRemoveObjects();

        GameObject[] objectsInSpace = GameObject.FindGameObjectsWithTag("ObjectInSpace");

        ObjectInSpace checkedSpaceObject = null;
        bool notOwner = true;
        bool canBeTargetted = true;
        bool inNotOnList = true;
        bool isInRange = true;
        for (int i = 0; i < objectsInSpace.Length; i++)
        {
            notOwner = false;
            canBeTargetted = false;
            inNotOnList = false;
            isInRange = false;
            checkedSpaceObject = (ObjectInSpace)objectsInSpace[i].GetComponent<ObjectInSpace>();
            if (checkedSpaceObject)
            {
                notOwner = (owner.GetInstanceID() != checkedSpaceObject.GetInstanceID());
                canBeTargetted = checkedSpaceObject.getCanBeTargetted();
                inNotOnList = !isThisObjectOnOverviewList(checkedSpaceObject);
                isInRange = owner.isInTargettingRange(checkedSpaceObject);
                if (notOwner && canBeTargetted && inNotOnList && isInRange)
                {
                    addOverviewItemToOverview(checkedSpaceObject);
                }
            }
        }
    }

    private void overviewListRemoveObjects()
    {
        for (int i = 0; i < overviewItemList.Count; i++)
        {
            if (overviewItemList[i]){
                //if(overviewList[i].gameObject.activeSelf == false) overviewList.RemoveAt(i);
            }
            else
            {
                overviewItemDestroy(i);
            }
        }
    }

    private void overviewItemDestroy(int index)
    {
        InterfaceOverviewItemScript item = overviewItemList[index];
        if(item) item.destroy();
        else
        {
            overviewItemList.RemoveAt(index);
        }
    }

    private bool isThisObjectOnOverviewList(ObjectInSpace obj)
    {
        for (int i = 0; i < overviewItemList.Count; i++)
        {
            if (overviewItemList[i].getTarget().GetInstanceID() == obj.GetInstanceID()) return true;
        }
        return false;
    }

    private void addOverviewItemToOverview(ObjectInSpace target)
    {
        GameObject overviewItem = (GameObject) Instantiate(overviewItemPrefab, overviewTransform.position, overviewTransform.rotation, overviewTransform);
        InterfaceOverviewItemScript overviewItemScript = overviewItem.GetComponent<InterfaceOverviewItemScript>();
        if (overviewItemScript)
        {
            overviewItemScript.setData(this, owner, target);
            overviewItemList.Add(overviewItemScript);
        }
    }



    public void InterfaceOverviewItemMouseEnter(InterfaceOverviewItemScript interfaceOverviewItem)
    {
        interfaceOverviewItem.setColorHighlighted();
    }

    public void InterfaceOverviewItemMouseLeave(InterfaceOverviewItemScript interfaceOverviewItem)
    {
        interfaceOverviewItem.setColorNormal();
    }

    private InterfaceOverviewDropdownScript overviewDropdown = null;

    public void overviewDropdownAdd(InterfaceOverviewItemScript interfaceOverviewItem, PointerEventData eventData)
    {
        overviewDropdownDestroy();
        overviewDropdown = ((GameObject)Instantiate(overviewDropdownPrefab, interfaceOverviewItem.gameObject.transform.position, interfaceOverviewItem.gameObject.transform.rotation, gameObject.transform)).GetComponent<InterfaceOverviewDropdownScript>();
        overviewDropdown.setData(this, interfaceOverviewItem.getOwner(), interfaceOverviewItem.getTarget(), interfaceOverviewItem);
        overviewDropdown.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 pos = new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, overviewDropdown.gameObject.transform.position.z);
        pos = new Vector3(pos.x, pos.y-(overviewDropdown.gameObject.GetComponent<RectTransform>().sizeDelta.y*0.6f), pos.z);
        overviewDropdown.gameObject.transform.position = pos;

    }

    public void overviewDropdownDestroy()
    {
        if (overviewDropdown)
        {
            overviewDropdown.destroy();
            overviewDropdown = null;
        }
    }



































    //Target Tiles -----------------------------------------------------------------------------------------------------------------
    //targetTilesHolder
    private List<InterfaceTargetTileScript> targetTilesList = new List<InterfaceTargetTileScript>();

    public void addTarget(ObjectInSpace target, InterfaceOverviewItemScript interfaceOverviewItem)
    {
        bool addTargetSucess = owner.addTarget(target);
        if (addTargetSucess)
        {
            GameObject targetTile = (GameObject)Instantiate(targetTilePrefab, targetTilesHolder.position, targetTilesHolder.rotation, targetTilesHolder);
            InterfaceTargetTileScript targetTileScript = targetTile.GetComponent<InterfaceTargetTileScript>();
            if (targetTileScript)
            {
                targetTileScript.setData(this, owner, target, interfaceOverviewItem);
                interfaceOverviewItem.setColorTarget();
                targetTilesList.Add(targetTileScript);
                refreshTargetTiles();
            }
        }
    }

    public void refreshTargetTiles()
    {
        for (int i = 0; i < targetTilesList.Count; i++)
        {
            if (targetTilesList[i].canDelete())
            {
                targetTilesList.RemoveAt(i);
                i = -1;
            }
        }
        for (int i = 0; i < targetTilesList.Count; i++)
        {
            if (!targetTilesList[i])
            {
                targetTilesList.RemoveAt(i);
                i = -1;
            }
        }
        for (int i = 0; i < targetTilesList.Count; i++)
        {
            if (!targetTilesList[i].getTarget())
            {
                targetTilesList.RemoveAt(i);
                i = -1;
            }
        }
        for (int i = 0; i < targetTilesList.Count; i++)
        {
            if (targetTilesList[i].getOwner().isInTargettingRange(targetTilesList[i].getTarget())==false)
            {
                targetTilesList.RemoveAt(i);
                i = -1;
            }
        }

        float scale = 0.7f;
        for (int i = 0; i < targetTilesList.Count; i++)
        {
            if (targetTilesList[i])
            {
                targetTilesList[i].gameObject.transform.localScale = new Vector3(scale, scale, scale);

                float width = targetTilesList[i].gameObject.GetComponent<RectTransform>().sizeDelta.x;
                float height = targetTilesList[i].gameObject.GetComponent<RectTransform>().sizeDelta.y;

                targetTilesList[i].gameObject.transform.localPosition = new Vector3(width * (0.5f + i * 1.05f) * scale, (-height / 2) * scale, 0f);
            }
        }
    }

    public int isOnTargetList(ObjectInSpace target)
    {
        for (int i = 0; i < targetTilesList.Count; i++)
        {
            if (targetTilesList[i].getTarget() == target)
            {
                return i;
            }
        }
        return -1;
    }

    public void clickedOnInterfaceTargetTile(ObjectInSpace target)
    {
        equipmentSetTarget(target);
        //Debug.Log("clickedOnInterfaceTargetTile");
    }



























    //Damage -----------------------------------------------------------------------------------------------------------------

    public void createDamageText(Vector3 point, string text)
    {
        GameObject damageText = (GameObject)Instantiate(damageTextPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        damageText.GetComponent<DamageTextScript>().setData(point, text);
    }

    public void addMesage(string text)
    {
        System.DateTime now = System.DateTime.Now;
        GameObject messageText = (GameObject)Instantiate(messagePrefab, messagesHolder.position, messagesHolder.rotation, messagesHolder);
        messageText.GetComponent<UnityEngine.UI.Text>().text = "[" + now.Hour + ":" + now.Minute + ":" + now.Second + "] " + text;
        messageText.transform.SetSiblingIndex(0);
    }





















































    private List<string> logs = new List<string>();
    public void addLog(string txt)
    {
        if (logs.Count == 0)
        {
            logs.Add(txt);
        }
        else
        {
            logs[logs.Count - 1] = logs[logs.Count - 1]+txt;
        }
    }
    public void addLogLn(string txt)
    {
        logs.Add(txt + "\n");
    }

    public void displayText()
    {
        string[] texts = logs.ToArray();
        logs = new List<string>();
        debugText.text = "";
        for (int i = 0; i < texts.Length; i++)
        {
            debugText.text += texts[i];
        }
    }
}
