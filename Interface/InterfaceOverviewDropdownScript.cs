using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceOverviewDropdownScript : MonoBehaviour {

    private UnityEngine.UI.Button buttonTarget = null;
    private UnityEngine.UI.Button buttonAlign = null;
    private UnityEngine.UI.Button buttonInfo = null;

    private InterfaceScript master = null;
    private ObjectInSpace owner = null;
    private ObjectInSpace target = null;
    private InterfaceOverviewItemScript interfaceOverviewItem = null;

    // Use this for initialization
    void Start ()
    {
        buttonTarget = transform.Find("ButtonTarget").GetComponent<UnityEngine.UI.Button>();
        buttonAlign = transform.Find("ButtonAlign").GetComponent<UnityEngine.UI.Button>();
        buttonInfo = transform.Find("ButtonInfo").GetComponent<UnityEngine.UI.Button>();

        buttonTarget.onClick.AddListener(buttonTargetClick);
        buttonAlign.onClick.AddListener(buttonAlignClick);
        buttonInfo.onClick.AddListener(buttonInfoClick);

        if (target == null)
        {
            destroy();
        }
    }

    public InterfaceScript getMaster() { return master; }
    public ObjectInSpace getOwner() { return owner; }
    public ObjectInSpace getTarget() { return target; }
    public void setData(InterfaceScript _master, ObjectInSpace _owner, ObjectInSpace _target, InterfaceOverviewItemScript _interfaceOverviewItem)
    {
        master = _master;
        owner = _owner;
        target = _target;
        interfaceOverviewItem = _interfaceOverviewItem;
    }

    private void buttonTargetClick()
    {
        master.addTarget(target, interfaceOverviewItem);
        destroy();
    }

    private void buttonAlignClick()
    {
        owner.alignTowardsTarget(target);
        destroy();
    }

    private void buttonInfoClick()
    {
        destroy();
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
