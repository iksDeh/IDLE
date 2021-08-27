using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectHandler : MonoBehaviour
{
    public RectTransform rt;

    void Start()
    {
       
    }

    public void OnValueChanged()
    {
        if (rt.position.y < 0)
            rt.position = new Vector3(rt.position.x, 0, 0);
    }


}
