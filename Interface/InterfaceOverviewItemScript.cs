using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceOverviewItemScript : MonoBehaviour, IPointerClickHandler{
    private UnityEngine.UI.Text name = null;
    private UnityEngine.UI.Text distance = null;
    private UnityEngine.UI.Image image = null;

    private InterfaceScript master = null;
    private ObjectInSpace owner = null;
    private ObjectInSpace target = null;

    void Start () {
        name = transform.Find("Name").GetComponent<UnityEngine.UI.Text>();
        distance = transform.Find("Distance").GetComponent<UnityEngine.UI.Text>();
        image = GetComponent<UnityEngine.UI.Image>();

        refreshItem();
        setColorNormal();
    }
	
	void Update () {
        refreshItem();
    }

    public InterfaceScript getMaster() { return master; }
    public ObjectInSpace getOwner() { return owner; }
    public ObjectInSpace getTarget() { return target; }
    public void setData(InterfaceScript _master, ObjectInSpace _owner, ObjectInSpace _target)
    {
        master = _master;
        owner = _owner;
        target = _target;
    }

    private void refreshItem()
    {
        if (master && owner && target)
        {
            name.text = target.getName();
            distance.text = owner.distanceInMetersStr(target);
            if (owner.isInTargettingRange(target) == false)
            {
                destroy();
            }
        }
        else
        {
            destroy();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { master.InterfaceOverviewItemMouseEnter(this); }
    public void OnPointerExit(PointerEventData eventData) { master.InterfaceOverviewItemMouseLeave(this); }
    public void OnPointerClick(PointerEventData eventData)
    {
        master.overviewDropdownDestroy();
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 1)
        {
            //Debug.Log("Single Click");
        }

        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            master.addTarget(target, this);
        }

        if (eventData.button == PointerEventData.InputButton.Right && eventData.clickCount == 1)
        {
            master.overviewDropdownAdd(this, eventData);
        }
    }


    public static Color32 colorNormal = new Color32(255, 255, 255, 255);
    public static Color32 colorTarget = new Color32(125, 100, 255, 255);
    public static Color32 colorHighlighted = new Color32(225, 200, 255, 255);

    public void setColorNormal() { image.color = colorNormal; }
    public void setColorTarget() { image.color = colorTarget; }
    public void setColorHighlighted() { image.color = colorHighlighted; }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
