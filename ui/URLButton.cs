using UnityEngine;
using System.Runtime.InteropServices;
using com.super2games.idle.utilities;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using com.super2games.idle.utilties;

public class URLButton : MonoBehaviour, IPointerDownHandler 
{
    public string url;

    [Serializable]
    public class ButtonPressEvent : UnityEvent { }

    public void open()
    {
        URLUtility.openURL(url);
    }

    public ButtonPressEvent OnPress = new ButtonPressEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPress.Invoke();
    }
}