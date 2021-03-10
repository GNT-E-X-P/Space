using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonTransitioner : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.white;

    private Image m_image = null;
    private void Awake()
    {
        m_image = GetComponent<Image>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_image.color = m_HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_image.color = m_NormalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_image.color = m_HoverColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_image.color = m_DownColor;
    }


}
