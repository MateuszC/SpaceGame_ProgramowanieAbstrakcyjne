using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceTargetTileScript : MonoBehaviour, IPointerClickHandler{

    private UnityEngine.UI.Image background;
    private UnityEngine.UI.Text title;
    private UnityEngine.UI.Text distance;
    private UnityEngine.UI.Button buttonClose;

    private UnityEngine.UI.Slider sliderShield;
	private UnityEngine.UI.Slider sliderArmor;
	private UnityEngine.UI.Slider sliderHull;
	private UnityEngine.UI.Slider sliderEnergy;
	private UnityEngine.UI.Text sliderShieldText;
	private UnityEngine.UI.Text sliderArmorText;
	private UnityEngine.UI.Text sliderHullText;
	private UnityEngine.UI.Text sliderEnergyText;

    private InterfaceScript master = null;
    private ObjectInSpace owner = null;
    private ObjectInSpace target = null;
    private InterfaceOverviewItemScript interfaceOverviewItem = null;

    void Start ()
    {
        background = GetComponent<UnityEngine.UI.Image>();
        title = transform.Find("Title").GetComponent<UnityEngine.UI.Text>();
        distance = transform.Find("Distance").GetComponent<UnityEngine.UI.Text>();
        buttonClose = transform.Find("ButtonClose").GetComponent<UnityEngine.UI.Button>();
        buttonClose.onClick.AddListener(buttonCloseAction);

        sliderShield = transform.Find ("SliderShield").GetComponent<UnityEngine.UI.Slider> ();
		sliderArmor = transform.Find ("SliderArmor").GetComponent<UnityEngine.UI.Slider> ();
		sliderHull = transform.Find ("SliderHull").GetComponent<UnityEngine.UI.Slider> ();
		//sliderEnergy = transform.Find ("SliderEnergy").GetComponent<UnityEngine.UI.Slider> ();
		sliderShieldText = transform.Find ("SliderShield/Fill Area/Text").GetComponent<UnityEngine.UI.Text> ();
		sliderArmorText = transform.Find ("SliderArmor/Fill Area/Text").GetComponent<UnityEngine.UI.Text> ();
		sliderHullText = transform.Find ("SliderHull/Fill Area/Text").GetComponent<UnityEngine.UI.Text> ();
		//sliderEnergyText = transform.Find ("SliderEnergy/Fill Area/Text").GetComponent<UnityEngine.UI.Text> ();


		refresh ();

        if (master && owner && target)
        {
            title.text = target.getName();
        }
    }

    public void Update()
    {
        refresh();
    }

    public InterfaceScript getMaster() { return master; }
    public ObjectInSpace getOwner() { return owner; }
    public ObjectInSpace getTarget() { return target; }
    public InterfaceOverviewItemScript getInterfaceOverviewItem() { return interfaceOverviewItem; }
    public void setData(InterfaceScript _master, ObjectInSpace _owner, ObjectInSpace _target, InterfaceOverviewItemScript _interfaceOverviewItem)
    {
        master = _master;
        owner = _owner;
        target = _target;
        interfaceOverviewItem = _interfaceOverviewItem;
    }
    private bool canBeDeleted = false;
    public bool canDelete()
    {
        return canBeDeleted;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        master.overviewDropdownDestroy();
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 1)
        {
            master.clickedOnInterfaceTargetTile(target);
            master.overviewDropdownDestroy();
        }

        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            //Debug.Log("Double Click");
        }

        if (eventData.button == PointerEventData.InputButton.Right && eventData.clickCount == 1)
        {
            owner.alignTowardsTarget(target);
        }
    }


    public void refresh()
    {
        if (master && owner && target)
        {
            if (owner.isInTargettingRange(target) == false)
            {
                destroy();
            }
            else
            {
                refreshNumbers();
            }
        }
        else {
            destroy();
        }
    }

    public void refreshNumbers()
    {

        distance.text = "Distance: " + ObjectInSpace.distanceStr(owner.distanceInMeters(target)) + ", Velocity: " + target.getCurrentVelocityInMetersStr();

        if (target.getMaxShield() > 0f)
        {
            sliderShield.value = target.getCurrentShield() / target.getMaxShield();
            sliderShieldText.text = "Shield: " + Mathf.Floor(target.getCurrentShield()) + "/" + target.getMaxShield();
            if (target.getShieldBalance() > 0) sliderShieldText.text += " (<color=#008000ff>+" + target.getShieldBalance() + "</color>)";
        }
        else
        {
            sliderShield.value = 0f;
            sliderShieldText.text = "";
        }

        if (target.getMaxArmor() > 0f)
        {
            sliderArmor.value = target.getCurrentArmor() / target.getMaxArmor();
            sliderArmorText.text = "Armor: " + Mathf.Floor(target.getCurrentArmor()) + "/" + target.getMaxArmor();
            if (target.getArmorBalance() > 0) sliderArmorText.text += " (<color=#008000ff>+" + target.getArmorBalance() + "</color>)";
        }
        else
        {
            sliderArmor.value = 0f;
            sliderArmorText.text = "";
        }

        if (target.getMaxHull() > 0f)
        {
            sliderHull.value = target.getCurrentHull() / target.getMaxHull();
            sliderHullText.text = "Hull: " + Mathf.Floor(target.getCurrentHull()) + "/" + target.getMaxHull();
            if (target.getHullBalance() > 0) sliderHullText.text += " (<color=#008000ff>+" + target.getHullBalance() + "</color>)";
        }
        else
        {
            sliderHull.value = 0f;
            sliderHullText.text = "";
        }
    }


    private void buttonCloseAction(){
        if (owner) owner.removeTarget(target);
        destroy();
	}

	public void destroy(){
        canBeDeleted = true;
        if (interfaceOverviewItem) interfaceOverviewItem.setColorNormal();
        if (master) master.refreshTargetTiles();
        Destroy (gameObject);
	}
}
