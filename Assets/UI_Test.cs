using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Test : MonoBehaviour, IPointerClickHandler
{
    Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnClick() {
        image.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
   
    void Update()
    {
        
    }
}
