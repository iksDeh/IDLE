using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectHandler : MonoBehaviour
{
    public RectTransform handle;
    public RectTransform content;
    private Vector3 lastPos;
    private bool outOfBorder = false;

    public void OnValueChanged()
    {
        if (handle != null)
            if (content.anchoredPosition.y > 0 && handle.anchorMin.y > 0)
            {
                lastPos = content.anchoredPosition;
                outOfBorder = false;
            }

            else if (content.anchoredPosition.y <= 0)
            {
                lastPos.y = 0;
                content.anchoredPosition = lastPos;
            }
            else if (handle.anchorMin.y == 0)
            {
                if (!outOfBorder)
                {
                    outOfBorder = true;
                    lastPos = content.anchoredPosition;
                }
                else
                    content.anchoredPosition = lastPos;
            }
            else
                content.anchoredPosition = lastPos;
    }


}
