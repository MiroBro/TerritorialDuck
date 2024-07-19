using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuPlayHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioSource aus;
    public AudioSource ausClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        ausClick.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        aus.Play();
        //References.Instance.soundHandler.PlayHoverBtn();
    }
}
