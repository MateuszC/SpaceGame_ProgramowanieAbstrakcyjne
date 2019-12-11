using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceEquipmentTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //InterfaceEquipmentTile
    private UnityEngine.UI.Button button;
    private UnityEngine.UI.Image backgroundImage;
    private UnityEngine.UI.RawImage icon = null;

    private UnityEngine.UI.Slider sliderEnergy = null;
	private UnityEngine.UI.Slider sliderHeat = null;
	private UnityEngine.UI.Slider sliderDurability = null;

	private UnityEngine.UI.Text sliderEnergyText = null;
	private UnityEngine.UI.Text sliderHeatText = null;
	private UnityEngine.UI.Text sliderDurabilityText = null;
	private UnityEngine.UI.Text textPassive = null;
    private UnityEngine.UI.Text textActive = null;
    private UnityEngine.UI.Text textPercent = null;
    private UnityEngine.UI.Text textState = null;

    private GameObject hasTarget = null;
    private GameObject needTarget = null;

    private Equipment equipment = null;
	private Dictionary<string,float> values = null;
	private Dictionary<string,float> valuesPrevious = null;

    public int index;
    private InterfaceScript master;
    private bool selected = false;

    // Use this for initialization
    void Start () {
        button = transform.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(clickedOnEquipmentDetails);

        backgroundImage = transform.GetComponent<UnityEngine.UI.Image>();
        icon = transform.Find ("Image/Icon").GetComponent<UnityEngine.UI.RawImage> ();

		sliderEnergy = transform.Find ("SliderEnergy").GetComponent<UnityEngine.UI.Slider> ();
		sliderHeat = transform.Find ("SliderHeat").GetComponent<UnityEngine.UI.Slider> ();
		sliderDurability = transform.Find ("SliderDurability").GetComponent<UnityEngine.UI.Slider> ();

		sliderEnergyText = transform.Find ("SliderEnergy/Text").GetComponent<UnityEngine.UI.Text> ();
		sliderHeatText = transform.Find ("SliderHeat/Text").GetComponent<UnityEngine.UI.Text> ();
		sliderDurabilityText = transform.Find ("SliderDurability/Text").GetComponent<UnityEngine.UI.Text> ();

		textPassive = transform.Find ("TextPassive").GetComponent<UnityEngine.UI.Text> ();
		textActive = transform.Find ("TextActive").GetComponent<UnityEngine.UI.Text> ();
        textPercent = transform.Find("TextPercent").GetComponent<UnityEngine.UI.Text>();
        textState = transform.Find("TextState").GetComponent<UnityEngine.UI.Text>();

        hasTarget = transform.Find("HasTarget").gameObject;
        needTarget = transform.Find("NeedTarget").gameObject;

        if (equipment != null) {
			setValues ();
			setValues ();

			updateEnergySlider ();
			updateHeatSlider ();
			updateDurabilitySlider ();
			updatePassiveActive ();

			compare ();
            updateTarget();
        }
        else
        {
            remove();
        }
	}

	// Update is called once per frame
	void Update () {
		compare ();
	}

    public void setIndex(int i) { index = i; }
    public void setMaster(InterfaceScript m) { master = m; }

    public void setEquipment(Equipment eq){
		equipment = eq;

		transform.Find ("TextName").GetComponent<UnityEngine.UI.Text> ().text = equipment.getName ();
		transform.Find ("Image/Icon").GetComponent<UnityEngine.UI.RawImage> ().texture = Resources.Load ("Icons/" + equipment.getIconName ()) as Texture;
	}

    public Equipment getEquipment() { return equipment; }

    private void setValues(){
		valuesPrevious = values;
		values = new Dictionary<string,float> ();
        values["energyCurrent"] = equipment.getLocalBatteryCurrentCapacity();
        values["energyMax"] = equipment.getPropertyValue(Equipment.PropertyName.EnergyLocalBatteryCapacity);

        values["heatCurrent"] = equipment.getHeat();
        values["heatMax"] = equipment.getHeatMax();

        values["durabilityCurrent"] = equipment.getDurability();
        values["durabilityMax"] = equipment.getDurabilityMax();

        values["energyPassive"] = equipment.energyRequirementPassive();
        values["energyActive"] = equipment.energyRequirementActive();

        values["poweringPercent"] = equipment.getPoweringPercent();

        values["state"] = equipment.getStateNumber();

        values["requireTarget"] = 0f;
        if (equipment.getRequireTarget()) values["requireTarget"] = 1f;

        values["hasTarget"] = 0f;
        if (equipment.hasTarget()) values["hasTarget"] = 1f;

        textState.text = equipment.getStateName() + "";
    }

	private void compare(){
		if(equipment!=null){
			setValues ();

			if(values ["energyCurrent"] != valuesPrevious ["energyCurrent"] || values ["energyMax"] != valuesPrevious ["energyMax"]){
				updateEnergySlider ();
			}

			if(values ["heatCurrent"] != valuesPrevious ["heatCurrent"] || values ["heatMax"] != valuesPrevious ["heatMax"]){
				updateHeatSlider ();
			}

			if(values ["durabilityCurrent"] != valuesPrevious ["durabilityCurrent"] || values ["durabilityMax"] != valuesPrevious ["durabilityMax"]){
				updateDurabilitySlider ();
            }

            if (values["energyPassive"] != valuesPrevious["energyPassive"] || values["energyActive"] != valuesPrevious["energyActive"])
            {
                updatePassiveActive();
            }

            if (values["poweringPercent"] != valuesPrevious["poweringPercent"])
            {
                updatePercent();
            }

            if (values["state"] != valuesPrevious["state"])
            {
                textState.text = equipment.getStateName() + "";
            }

            if (values["requireTarget"] != valuesPrevious["requireTarget"] || values["hasTarget"] != valuesPrevious["hasTarget"])
            {
                updateTarget();
            }

            valuesPrevious = new Dictionary<string,float> (values);
		}
	}


	private void updateEnergySlider(){
		if (values ["energyCurrent"] < 1f) {
			sliderEnergy.value = 0f;
			sliderEnergyText.text = "";
		}else {
			sliderEnergy.value = values ["energyCurrent"] / values ["energyMax"];
			sliderEnergyText.text = Mathf.Floor(values ["energyCurrent"]) + "/" + Mathf.Floor(values ["energyMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["energyCurrent"])/values ["energyMax"])+"%)";
		}
	}

	private void updateHeatSlider(){
		if (values ["heatCurrent"] < 1f) {
			sliderHeat.value = 0f;
			sliderHeatText.text = "";
		}else {
			sliderHeat.value = values ["heatCurrent"] / values ["heatMax"];
			sliderHeatText.text = Mathf.Floor(values ["heatCurrent"]) + "/" + Mathf.Floor(values ["heatMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["heatCurrent"])/values ["heatMax"])+"%)";
		}
	}

	private void updateDurabilitySlider(){
		if (values ["durabilityCurrent"] < 1f) {
			sliderDurability.value = 0f;
			sliderDurabilityText.text = "";
		}else {
			sliderDurability.value = values ["durabilityCurrent"] / values ["durabilityMax"];
			sliderDurabilityText.text = Mathf.Floor(values ["durabilityCurrent"]) + "/" + Mathf.Floor(values ["durabilityMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["durabilityCurrent"])/values ["durabilityMax"])+"%)";
		}
    }

    private void updatePassiveActive()
    {
        textPassive.text = "Passive: " + values["energyPassive"];
        textActive.text = "Active: " + values["energyActive"];
    }

    private void updatePercent()
    {
        if (values["poweringPercent"] >= 0f) textPercent.text = "" + (Mathf.Floor(1000f * values["poweringPercent"]) / 10f) + "%";
        else textPercent.text = "---";
    }

    private void updateTarget()
    {
        if (values["requireTarget"] == 1f)
        {
            if (values["hasTarget"] == 1f)
            {
                hasTarget.SetActive(true);
                needTarget.SetActive(false);
            }
            else
            {
                hasTarget.SetActive(false);
                needTarget.SetActive(true);
            }
            //Debug.Log(equipment.getName() + ", requireTarget = " + values["requireTarget"] + ",  hasTarget = " + values["hasTarget"]);
        }
        else
        {
            hasTarget.SetActive(false);
            needTarget.SetActive(false);
        }
    }



    private void               clickedOnEquipmentDetails() { master.equipmentDetailElementClickedOn(index);  }
    public void OnPointerEnter(PointerEventData eventData) { master.equipmentDetailElementMouseEnter(index); }
    public void OnPointerExit (PointerEventData eventData) { master.equipmentDetailElementMouseLeave(index); }

    public static Color32 colorNormal = new Color32(125, 255, 255, 100);
    public static Color32 colorHover = new Color32(125, 200, 255, 100);
    public static Color32 colorHighlighted = new Color32(125, 150, 255, 100);
    public static Color32 colorSelected = new Color32(125, 100, 255, 100);
    private Color32 colorNextColor = colorNormal;

    public void setNextColor(Color32 nextColor) { colorNextColor = nextColor; }
    public void applyColor() { if (backgroundImage) backgroundImage.color = colorNextColor; }

    public void       setColorNormal() { if (backgroundImage) backgroundImage.color = colorNormal;      }
    public void        setColorHover() { if (backgroundImage) backgroundImage.color = colorHover;       }
    public void  setColorHighlighted() { if (backgroundImage) backgroundImage.color = colorHighlighted; }
    public void     setColorSelected() { if (backgroundImage) backgroundImage.color = colorSelected;    }

    public void setSelected(bool set) {
        selected = set;
    }

    public bool isSelected() {
        return selected;
    }

    public void remove()
    {
        Destroy(gameObject);
    }



}
