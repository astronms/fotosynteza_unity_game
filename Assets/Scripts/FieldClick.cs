﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        GameObject fieldNameHolder = GameObject.Find("/GameUI/FieldMenu/Panel/FieldName");
        Transform field = gameObject.transform.parent;
        Vector3 mousePos = Input.mousePosition;
        Vector3 shift = new Vector3(50, 0, 0);

        if (name == "Cylinder" && !IsPointerOverUIElement())
        {
            fieldMenu.SetActive(true);
            fieldMenu.transform.position = mousePos + shift;
            fieldNameHolder.GetComponent<UnityEngine.UI.Text>().text = field.name;
        }
    }

    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
