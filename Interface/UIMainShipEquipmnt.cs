using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainShipEquipmnt : MonoBehaviour {

	public GameObject equipmentDetailsPrefab;

	private Equipment[] equipment = new Equipment[0];
	private InterfaceEquipmentTile[] equipmentTiles = new InterfaceEquipmentTile[0];

    private int eqInRow = 0;

    // Use this for initialization
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void setEquipment(Equipment[] eq){
		equipment = eq;
		RectTransform rt = GetComponent<RectTransform> ();

		float scale = 0.88f;
		float eqDetailWidth = 81f * scale;
		float eqDetailHeight = 61f * scale;
		float offsetStartX = -rt.rect.width / 2f + eqDetailWidth / 2f;
		float offsetStartY = rt.rect.height / 2f - eqDetailHeight / 2f;
		float offsetX = offsetStartX;
        float offsetY = offsetStartY;

		eqInRow = (int)Mathf.Floor(rt.rect.width / eqDetailWidth);
        List<InterfaceEquipmentTile> list = new List<InterfaceEquipmentTile>();
        for (int i = 0; i < equipment.Length; i++)
        {
            InterfaceEquipmentTile equipmentTile = null;
            GameObject equipmentElement = Instantiate (equipmentDetailsPrefab, transform.position, transform.rotation, transform);

			equipmentElement.transform.localPosition = new Vector3 (offsetX, offsetY, equipmentElement.transform.position.z);
			equipmentElement.transform.localScale = new Vector3 (scale,scale,scale);
            equipmentTile = equipmentElement.GetComponent<InterfaceEquipmentTile>();
            equipmentTile.setEquipment(equipment[i]);
            equipmentTile.index = i+1;
            //equipmentTile.master = this;

            offsetX += eqDetailWidth;
			if ((i+1)%eqInRow == 0) {
				offsetX = offsetStartX;
				offsetY -= eqDetailHeight;
			}

			list.Add (equipmentTile);
        }
        equipmentTiles = list.ToArray();
    }

    public void refreshEquipment() {

    }

    private int indexStart = 0;
    private int indexEnd = 1;
    private int indexHover = 0;

    public void equipmentDetailElementClickedOn(int index)
    {
        //Debug.Log("clickedOnEquipmentDetailElement " + index);
        if (indexEnd > 0)
        {
            indexStart = index;
            indexEnd = 0;
        }
        else {
            indexEnd = index;
        }
        if (indexStart > 0 && indexEnd > 0 && indexEnd > indexStart) {
            //int swap = indexStart;
            //indexStart = indexEnd;
            //indexEnd = swap;
        }
        equipmentDetailElementRefreshColor();
    }

    public void equipmentDetailElementMouseEnter(int index)
    {
        //Debug.Log("equipmentDetailElementMouseEnter " + index);
        indexHover = index;
        equipmentDetailElementRefreshColor();
    }

    public void equipmentDetailElementMouseLeave(int index)
    {
        //Debug.Log("equipmentDetailElementMouseLeave " + index);
        indexHover = 0;
        equipmentDetailElementRefreshColor();
    }

    public void equipmentDetailElementRefreshColor()
    {
        string selectedStr = "";

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
                if (indexStart >= indexEnd)
                {
                    if (equipmentTiles[i].index >= indexEnd && equipmentTiles[i].index <= indexStart) selectElement = true;
                }
                else
                {
                    if (equipmentTiles[i].index >= indexStart && equipmentTiles[i].index <= indexEnd) selectElement = true;
                }
                if (selectElement)
                {
                    equipmentTiles[i].setNextColor(InterfaceEquipmentTile.colorSelected);
                    equipmentTiles[i].setSelected(true);
                    selectedStr += (i+1)+",";
                }
                else {
                    equipmentTiles[i].setSelected(false);
                }
            }
        }

        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].applyColor();
        }

        //Debug.Log("indexStart " + indexStart + ", indexEnd " + indexEnd + ", indexHover " + indexHover + ", equipmentTiles " + equipmentTiles.Length + ", selected " + selectedStr.Substring(0, Mathf.Max(0, selectedStr.Length - 1)));
    }

    public void clearSelection()
    {
        indexStart = 0;
        indexEnd = 1;
        for (int i = 0; i < equipmentTiles.Length; i++)
        {
            equipmentTiles[i].setSelected(false);
        }
        equipmentDetailElementRefreshColor();
    }

    public void equipmentSetState(Equipment.State newState)
    {
        bool reqRecalculate = false;
        bool doReqRecalculate = false;
        if (indexStart > 0 && indexEnd > 0)
        {
            if (indexStart >= indexEnd)
            {
                for (int i = 0; i < equipmentTiles.Length; i++)
                {
                    if (equipmentTiles[i].index >= indexEnd && equipmentTiles[i].index <= indexStart) doReqRecalculate = equipmentTiles[i].getEquipment().setState(newState);
                    if (doReqRecalculate) reqRecalculate = true;
                }
            }
            else
            {
                for (int i = 0; i < equipmentTiles.Length; i++)
                {
                    if (equipmentTiles[i].index >= indexStart && equipmentTiles[i].index <= indexEnd) doReqRecalculate = equipmentTiles[i].getEquipment().setState(newState);
                    if (doReqRecalculate) reqRecalculate = true;
                }
            }


            if (reqRecalculate) equipmentTiles[0].getEquipment().getOwner().setNeedRecalculateEquipment();
            clearSelection();
        }
    }





}
