using System.Collections.Generic;
using Assets.Scripts.Field;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FieldClick : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        GameObject fieldNameHolder = GameObject.Find("/GameUI/FieldMenu/Panel/FieldName");
        Transform field = gameObject.transform.parent;
        Vector3 mousePos = Input.mousePosition;

        float width = ((RectTransform) fieldMenu.transform).rect.width;
        Vector3 shift = new Vector3(width * (float) (Screen.width / 1283.0), 0, 0);

        if (name == "Cylinder" && !IsPointerOverUIElement())
        {
            var tmp = field.name.Split('[', ']')[1].Split(';');
            FieldVector fieldCoordinates = Field.GetCoordinates(tmp);
            actionType action = _gameManager.AvailableActionOnField(fieldCoordinates);

            GameObject updateButton = GameObject.Find("/GameUI/FieldMenu/Panel/UpgradeTreeButton");
            GameObject cutButton = GameObject.Find("/GameUI/FieldMenu/Panel/CutTreeButton");
            GameObject seedButton = GameObject.Find("/GameUI/FieldMenu/Panel/SowSeedButton");
            GameObject plantButton = GameObject.Find("/GameUI/FieldMenu/Panel/PlantTreeButton");

            updateButton.SetActive(false);
            seedButton.SetActive(false);
            cutButton.SetActive(false);
            plantButton.SetActive(false);

            if (action > actionType.none)
            {
                fieldMenu.SetActive(true);
                fieldMenu.transform.position = mousePos + shift;
                fieldNameHolder.GetComponent<Text>().text = field.name;
                switch (action)
                {
                    case actionType.plant:
                        plantButton.SetActive(true);
                        break;
                    case actionType.seed:
                        seedButton.SetActive(true);
                        break;
                    case actionType.upgrade:
                        updateButton.SetActive(true);
                        break;
                    case actionType.cut:
                        cutButton.SetActive(true);
                        break;
                }
            }
            else
            {
                fieldMenu.SetActive(false);
            }
        }
    }



    private static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }

        return false;
    }

    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}