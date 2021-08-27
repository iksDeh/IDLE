using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



[ExecuteInEditMode]
//[AddComponentMenu("Event/RightButtonEvent")]
public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    //public event System.Action OnLeftClick;
    //public event System.Action OnRightClick;
    public event System.Action OnMiddleClick;

    public delegate void OnLeftClick(PointerEventData eventData);
    public OnLeftClick onLeftClick;
    public delegate void OnRightClick(PointerEventData eventData);
    public OnLeftClick onRightClick;

    PointerEventData peventdata;
    public RectTransform rect;
    public void GetMousePosition()
    {

    }



    public void OnPointerClick(PointerEventData eventData)
    {
        rect = eventData.pointerDrag.GetComponent<RectTransform>();
        peventdata = eventData;
        Debug.Log(peventdata.pointerCurrentRaycast.ToString() + " || " + peventdata.position);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (onLeftClick != null)
                onLeftClick.Invoke(eventData);
        }

        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            if (OnMiddleClick != null)
                OnMiddleClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (onRightClick != null)
                onRightClick.Invoke(eventData);
        }
    }
}