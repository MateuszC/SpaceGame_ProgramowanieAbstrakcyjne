using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainShipStatus : MonoBehaviour {

	public GameObject shipStatus;

	private ObjectInSpace ship = null;

	private UnityEngine.UI.Slider sliderShield;
	private UnityEngine.UI.Slider sliderArmor;
	private UnityEngine.UI.Slider sliderHull;
    private UnityEngine.UI.Slider sliderSpeed;
    private UnityEngine.UI.Slider sliderEnergy;
    private UnityEngine.UI.Text sliderShieldText;
	private UnityEngine.UI.Text sliderArmorText;
    private UnityEngine.UI.Text sliderHullText;
    private UnityEngine.UI.Text sliderSpeedText;
    private UnityEngine.UI.Text sliderEnergyText;
    private UnityEngine.UI.Text sliderEnergyTextPassive;
	private UnityEngine.UI.Text sliderEnergyTextActive;
    private UnityEngine.UI.Text sliderEnergyTextBalance;
    private UnityEngine.UI.Text sliderEnergyTextEfficiency;

    private UnityEngine.UI.Text textMass;
	private UnityEngine.UI.Text textDrag;
	private UnityEngine.UI.Text textEngineThrust;
    private UnityEngine.UI.Text textManouverability;
    private UnityEngine.UI.Text textVolume;

    private Dictionary<string,float> values = null;
	private Dictionary<string,float> valuesPrevious = null;

	void Start () {
		ship = shipStatus.GetComponent<ObjectInSpace> ();

		sliderShield = transform.Find ("SliderShield").GetComponent<UnityEngine.UI.Slider> ();
		sliderArmor = transform.Find ("SliderArmor").GetComponent<UnityEngine.UI.Slider> ();
		sliderHull = transform.Find ("SliderHull").GetComponent<UnityEngine.UI.Slider> ();
        sliderSpeed = transform.Find("SliderSpeed").GetComponent<UnityEngine.UI.Slider>();
        sliderEnergy = transform.Find ("SliderEnergy").GetComponent<UnityEngine.UI.Slider> ();
        sliderShieldText = transform.Find ("SliderShield/Text").GetComponent<UnityEngine.UI.Text> ();
		sliderArmorText = transform.Find ("SliderArmor/Text").GetComponent<UnityEngine.UI.Text> ();
		sliderHullText = transform.Find ("SliderHull/Text").GetComponent<UnityEngine.UI.Text> ();
        sliderSpeedText = transform.Find("SliderSpeed/Text").GetComponent<UnityEngine.UI.Text>();
        sliderEnergyText = transform.Find("SliderEnergy/Text").GetComponent<UnityEngine.UI.Text>();
        sliderEnergyTextPassive = transform.Find ("SliderEnergy/TextPassive").GetComponent<UnityEngine.UI.Text> ();
		sliderEnergyTextActive = transform.Find ("SliderEnergy/TextActive").GetComponent<UnityEngine.UI.Text> ();
        sliderEnergyTextBalance = transform.Find("SliderEnergy/TextBalance").GetComponent<UnityEngine.UI.Text>();
        sliderEnergyTextEfficiency = transform.Find("SliderEnergy/TextEfficiency").GetComponent<UnityEngine.UI.Text>();

        textMass = transform.Find ("Mass").GetComponent<UnityEngine.UI.Text> ();
		textDrag = transform.Find ("Drag").GetComponent<UnityEngine.UI.Text> ();
		textEngineThrust = transform.Find ("EngineThrust").GetComponent<UnityEngine.UI.Text> ();
        textManouverability = transform.Find("Manouverability").GetComponent<UnityEngine.UI.Text>();
        textVolume = transform.Find("Volume").GetComponent<UnityEngine.UI.Text>();

        setValues ();
		setValues ();

		updateSliderShield ();
		updateSliderArmor ();
		updateSliderHull ();
        updateSliderSpeed();
        updateSliderEnergy ();

        updateSliderEnergyTextPassive ();
		updateSliderEnergyTextActive ();
		updateSliderEnergyTextBalance ();

		updateShipParameters ();

		compare ();
	}

	void Update () {
		compare ();
	}

	private void setValues(){
		valuesPrevious = values;
		values = new Dictionary<string,float> ();
        values["shieldMax"] = ship.getMaxShield();
        values["shieldCurrent"] = ship.getCurrentShield();
        values["shieldBalance"] = ship.getShieldBalance();

        values["armorMax"] = ship.getMaxArmor();
        values["armorCurrent"] = ship.getCurrentArmor();
        values["armorBalance"] = ship.getArmorBalance();

        values["hullMax"] = ship.getMaxHull();
        values["hullCurrent"] = ship.getCurrentHull();
        values["hullBalance"] = ship.getHullBalance();

        values["energyMax"] = ship.getMaxEnergy();
        values["energyCurrent"] = ship.getCurrentEnergy();
        values["energyProduction"] = ship.getEnergyProduction();
        values["energyPassive"] = ship.getEnergyDrainPassive();
        values["energyActive"] = ship.getEnergyDrainActive();
        values["energyBalance"] = values["energyProduction"] - values["energyPassive"] - values["energyActive"];
        values["energyActiveCoefficient"] = ship.getEnergyDrainActiveCoefficient();
        values["energyIsStable"] = 0f;
        if (ship.isEnergyStable()) values["energyIsStable"] = 1f;

        values["mass"] = ship.getMass();
        values["drag"] = ship.getDrag();
        values["velocityCurrent"] = ship.getCurrentVelocity();
        values["velocityMax"] = ship.getMaxVelocity();
        values["velocityMaxCurrent"] = ship.getMaxCurrentVelocity();
        values["engineThrust"] = ship.getEngineThrust();
        values["maneuverability"] = ship.getManeuverability();
        values["volume"] = (float)ship.getVolume();
    }

	private void compare(){
		setValues ();

		if(values ["shieldMax"] != valuesPrevious ["shieldMax"] || values ["shieldCurrent"] != valuesPrevious ["shieldCurrent"] ||  values ["shieldBalance"] != valuesPrevious ["shieldBalance"]){
			updateSliderShield ();
		}

		if(values ["armorMax"] != valuesPrevious ["armorMax"] || values ["armorCurrent"] != valuesPrevious ["armorCurrent"] || values ["armorBalance"] != valuesPrevious ["armorBalance"]){
			updateSliderArmor ();
		}

		if(values ["hullMax"] != valuesPrevious ["hullMax"] || values ["hullCurrent"] != valuesPrevious ["hullCurrent"] || values ["hullBalance"] != valuesPrevious ["hullBalance"]){
			updateSliderHull ();
        }

        if (values["energyMax"] != valuesPrevious["energyMax"] || values["energyCurrent"] != valuesPrevious["energyCurrent"] || values["energyProduction"] != valuesPrevious["energyProduction"])
        {
            updateSliderEnergy();
        }

        if (values["velocityCurrent"] != valuesPrevious["velocityCurrent"] || values["velocityMaxCurrent"] != valuesPrevious["velocityMaxCurrent"] || values["velocityMax"] != valuesPrevious["velocityMax"])
        {
            updateSliderSpeed();
        }

        if (values ["energyPassive"] != valuesPrevious ["energyPassive"]){
			updateSliderEnergyTextPassive ();
		}

		if(values["energyActive"] != valuesPrevious["energyActive"] || values["energyActiveCoefficient"] != valuesPrevious["energyActiveCoefficient"]){
			updateSliderEnergyTextActive ();
		}

		if(values ["energyBalance"] != valuesPrevious ["energyBalance"]){
			updateSliderEnergyTextBalance ();
		}

		if(values ["engineThrust"] != valuesPrevious ["engineThrust"] || values ["maneuverability"] != valuesPrevious ["maneuverability"] || values ["mass"] != valuesPrevious ["mass"] || values["drag"] != valuesPrevious["drag"] || values["volume"] != valuesPrevious["volume"])
        {
			updateShipParameters ();
		}

		valuesPrevious = new Dictionary<string,float> (values);
	}

	private void updateSliderShield(){
		if (values ["shieldMax"] < 1f) {
			sliderShield.value = 0f;
			sliderShieldText.text = "";
		}else {
			sliderShield.value = values ["shieldCurrent"] / values ["shieldMax"];
			sliderShieldText.text = Mathf.Floor(values ["shieldCurrent"]) + "/" + Mathf.Floor(values ["shieldMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["shieldCurrent"])/values ["shieldMax"])+"%)";
			if (values ["shieldBalance"] > 0f) {
				sliderShieldText.text += " (<color=#000000>+" + values ["shieldBalance"] + "</color>)";
			}
		}
	}

	private void updateSliderArmor(){
		if (values ["armorMax"] < 1f) {
			sliderArmor.value = 0f;
			sliderArmorText.text = "";
		}else {
			sliderArmor.value = values ["armorCurrent"] / values ["armorMax"];
			sliderArmorText.text = Mathf.Floor(values ["armorCurrent"]) + "/" + Mathf.Floor(values ["armorMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["armorCurrent"])/values ["armorMax"])+"%)";
			if (values ["armorBalance"] > 0f) {
				sliderArmorText.text += " (<color=#000000>+" + values ["armorBalance"] + "</color>)";
			}
		}
	}

	private void updateSliderHull(){
		if (values ["hullMax"] < 1f) {
			sliderHull.value = 0f;
			sliderHullText.text = "";
		}else {
			sliderHull.value = values ["hullCurrent"] / values ["hullMax"];
			sliderHullText.text = Mathf.Floor(values ["hullCurrent"]) + "/" + Mathf.Floor(values ["hullMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["hullCurrent"])/values ["hullMax"])+"%)";
			if (values ["hullBalance"] > 0f) {
				sliderHullText.text += " (<color=#000000>+" + values ["hullBalance"] + "</color>)";
			}
		}
	}

	private void updateSliderEnergy(){
		if (values ["energyMax"] < 1f) {
			sliderEnergy.value = 0f;
			sliderEnergyText.text = "";
		}else {
			sliderEnergy.value = values ["energyCurrent"] / values ["energyMax"];
			sliderEnergyText.text = Mathf.Floor(values ["energyCurrent"]) + "/" + Mathf.Floor(values ["energyMax"]) + " ("+Mathf.Floor(100f*Mathf.Floor(values ["energyCurrent"])/values ["energyMax"])+"%)";
			if (values ["energyProduction"] > 0f) {
				sliderEnergyText.text += " (+" + values ["energyProduction"] + ")";
			}else if (values ["energyProduction"] < 0f){
				sliderEnergyText.text += " (" + values ["energyProduction"] + ")";
			}
		}
	}

    private void updateSliderSpeed()
    {
        if (values["velocityCurrent"] < 1f){
            sliderSpeed.value = 0f;
            sliderSpeedText.text = "";
        }else{
            if (values["velocityMaxCurrent"] > 1f)
            {
                sliderSpeed.value = values["velocityCurrent"] / values["velocityMaxCurrent"];
                sliderSpeedText.text = ship.getCurrentVelocityInMetersStr() + " | " + ship.getMaxCurrentVelocityInMetersStr();
            }
            else
            {
                sliderSpeed.value = values["velocityCurrent"] / values["velocityMax"];
                sliderSpeedText.text = ship.getCurrentVelocityInMetersStr() + " | " + ship.getMaxVelocityInMetersStr();
            }
        }
    }

    private void updateSliderEnergyTextPassive(){
		if (values ["energyPassive"] > 0f) {
			sliderEnergyTextPassive.text = "Passive <color=#ff0000>-" + Mathf.Floor(values ["energyPassive"]) + "</color>";
		} else {
			sliderEnergyTextPassive.text = "Passive ---";
		}
	}

	private void updateSliderEnergyTextActive(){
		if (values ["energyActive"] > 0f){
            sliderEnergyTextActive.text = "Active <color=#ff0000>-" + Mathf.Floor(values["energyActive"]) + "</color>";
            sliderEnergyTextEfficiency.text = "<color=#ff0000>" + "(" + ((Mathf.Floor(100f * values["energyActiveCoefficient"] * 10f) / 10f)) + "%)</color>";
        } else {
			sliderEnergyTextActive.text = "Active ---";
            sliderEnergyTextEfficiency.text = "";
        }
    }

    private void updateSliderEnergyTextBalance(){
		if (values ["energyIsStable"] >= 0f) {
			sliderEnergyTextBalance.text = "Balance ";
		} else {
			sliderEnergyTextBalance.text = "<color=#ff0000>Unbalanced! </color>";
		}


		if (values ["energyBalance"] >= 1f) {
			sliderEnergyTextBalance.text += "<color=#000000>+" + Mathf.Floor(values ["energyBalance"]) + "</color>";
		} else if (values ["energyBalance"] <= -1f) {
			sliderEnergyTextBalance.text += "<color=#ff0000>" + Mathf.Floor(values ["energyBalance"]) + "</color>";
		} else {
			sliderEnergyTextBalance.text += "0";
		}
	}

    private void updateShipParameters(){
		textMass.text = "Mass: "+values ["mass"]+"t";
		textDrag.text = "Drag: "+values ["drag"];
		textEngineThrust.text = "Engine Thrust: "+values ["engineThrust"];
        textManouverability.text = "Maneuverability: " + values["maneuverability"];
        textVolume.text = "Volume: " + ((long)ship.getVolumeMeterCubic()) + "m^3";
        //textVolume.text = "Volume: " + ship.getVolumeMeterCubic() + "km^3";
        //textVolume.text = "Volume: " + ObjectInSpace.mCubicToKmCubic(ship.getVolumeMeterCubic()) + "km^3";
    }






    private string strrrrr = "";
	private void addText(string txt){
		strrrrr += txt+"\n";
	}

	private void sendText(){
		GameObject.Find ("Interface/DebugLog/Text").GetComponent<UnityEngine.UI.Text> ().text = strrrrr;
		strrrrr = "";
	}



}
