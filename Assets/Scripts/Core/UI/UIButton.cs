using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("ShadowRun/UIButton")]
public class UIButton : Button
{
    [SerializeField] private OnButtonUpEvent m_OnButtonUpEvent = new OnButtonUpEvent();
    [SerializeField] private OnButtonDownEvent m_OnButtonDownEvent = new OnButtonDownEvent();

    protected UIButton() { }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        m_OnButtonUpEvent.Invoke();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        m_OnButtonDownEvent.Invoke();
    }

    public OnButtonUpEvent OnButtonUp => m_OnButtonUpEvent;
    public OnButtonDownEvent OnButtonDown => m_OnButtonDownEvent;

}

[System.Serializable]
public class OnButtonUpEvent : UnityEvent
{

}
[System.Serializable]
public class OnButtonDownEvent : UnityEvent
{

}
