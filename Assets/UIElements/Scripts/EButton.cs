using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UIElements/EButton")]
public class EButton : Button
{
    [Serializable]
    public class ButtonDownEvent : UnityEvent { }
    [Serializable]
    public class ButtonUpEvent : UnityEvent { }

    public ButtonDownEvent onDown { get; protected set; } = new ButtonDownEvent();
    public ButtonUpEvent onUp { get; protected set; } = new ButtonUpEvent();
    public bool isHeldDown { get; protected set; } = false;
    public float holdTime { get; protected set; } = 0f;

    public ButtonDownEvent onRightDown { get; protected set; } = new ButtonDownEvent();
    public ButtonUpEvent onRightUp { get; protected set; } = new ButtonUpEvent();
    public ButtonClickedEvent onRightClick { get; protected set; } = new ButtonClickedEvent();
    public bool isRightHeldDown { get; protected set; } = false;
    public float rightHoldTime { get; protected set; } = 0f;

    public ButtonUpEvent onMiddleDown { get; protected set; } = new ButtonUpEvent();
    public ButtonUpEvent onMiddleUp { get; protected set; } = new ButtonUpEvent();
    public ButtonClickedEvent onMiddleClick { get; protected set; } = new ButtonClickedEvent();
    public bool isMiddleHeldDown { get; protected set; } = false;
    public float middleHoldTime { get; protected set; } = 0f;

    private void Update()
    {
        var dt = Time.deltaTime;

        if(isHeldDown)
        {
            holdTime += dt;
        }
        if(isRightHeldDown)
        {
            rightHoldTime += dt;
        }
        if(isMiddleHeldDown)
        {
            middleHoldTime += dt;
        }
    }

    protected EButton() { }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                isHeldDown = true;
                holdTime = 0f;
                onDown?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                isRightHeldDown = true;
                rightHoldTime = 0f;
                onRightDown?.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                isMiddleHeldDown = true;
                middleHoldTime = 0f;
                onMiddleDown?.Invoke();
                break;
            default:
                break;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                isHeldDown = false;
                onUp?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                isRightHeldDown = false;
                onRightUp?.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                isMiddleHeldDown = false;
                onRightUp?.Invoke();
                break;
            default:
                break;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        switch(eventData.button)
        {
            case PointerEventData.InputButton.Right:
                onRightClick?.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                onMiddleClick?.Invoke();
                break;
            default:
                break;
        }
    }
}