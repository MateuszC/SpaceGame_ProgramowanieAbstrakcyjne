using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextScript : MonoBehaviour {

    private float startTime = 0f;
    private float animationTime = 2f;
    private InterfaceScript master;
    private Vector3 point = Vector3.zero;

    void Start () {
        if (point == Vector3.zero)
        {
            destroy();
        }
        startTime = Time.time;
        master = GameObject.Find("InterfaceGame").GetComponent<InterfaceScript>();
    }

	void Update () {
        if (startTime + animationTime > Time.time)
        {
            updateFrame();
        }
        else
        {
            destroy();
        }
    }

    public void setData(Vector3 _point, string text)
    {
        point = _point;
        GetComponent<UnityEngine.UI.Text>().text = text;
        setColorRed();
    }

    private void updateFrame()
    {

        float x = (Time.time - startTime) / animationTime;

        Vector3 camPos = master.getCamera().WorldToScreenPoint(point);
        camPos.y = camPos.y + 25f*x;
        camPos.z = Mathf.Abs(camPos.z);
        transform.position = camPos;

        GetComponent<UnityEngine.UI.Text>().fontSize = (int)(5f + 10f * fontSizeFunction(x));
        Color32 c = GetComponent<UnityEngine.UI.Text>().color;
        float opacity = 255 * fontOpacityFunction(x);
        c = new Color32(c.r, c.g, c.b, (byte)((int)opacity));
        GetComponent<UnityEngine.UI.Text>().color = c;
    }

    private float fontSizeFunction(float x)
    {
        if (x <= 0.2f) return Mathf.Sin(8f * x);
        return 1f;
    }

    private float fontOpacityFunction(float x)
    {
        if (x <= 0.8f) return 1f;
        return 1f - ((x - 0.8f) / 0.2f);
    }

    private void setColorRed() { setColor(new Color32(255, 0, 0, 255)); }
    private void setColorGreen() { setColor(new Color32(0, 255, 0, 255)); }
    private void setColorBlue() { setColor(new Color32(0, 0, 255, 255)); }
    private void setColor(Color32 c) { GetComponent<UnityEngine.UI.Text>().color = c; }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
