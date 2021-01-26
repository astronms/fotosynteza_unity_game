using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuBox : ModalBox
{
    /// <summary>
    ///     Set this to the name of the prefab that should be loaded when a menu box is shown.
    /// </summary>
    [Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
    public static string PrefabResourceName = "Menu Box";

    public static bool hasMenu()
    {
        return GameObject.Find("Menu Box(Clone)") != null;
    }

    /// <summary>
    ///     Display a menu box.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="onFinished"></param>
    /// <param name="title"></param>
    public static MenuBox Show(IEnumerable<string> options, IEnumerable<UnityAction> actions, string title = "")
    {
        if (options.Count() != actions.Count())
            throw new Exception("MenuBox.Show must be called with an equal number of options and actions.");

        var box = Instantiate(Resources.Load<GameObject>(PrefabResourceName)).GetComponent<MenuBox>();

        box.SetText(null, title);
        box.SetUpButtons(options, actions);

        return box;
    }

    private void SetUpButtons(IEnumerable<string> options, IEnumerable<UnityAction> actions)
    {
        for (var i = 0; i < options.Count(); i++)
            CreateButton(Button.gameObject, options.ElementAt(i), actions.ElementAt(i));

        Destroy(Button.gameObject);
    }

    private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction action)
    {
        var button = Instantiate(buttonToClone);

        button.GetComponentInChildren<Text>().text = label;
        button.GetComponent<Button>().onClick.AddListener(action);
        button.GetComponent<Button>().onClick.AddListener(() => { Close(); });

        button.transform.SetParent(buttonToClone.transform.parent, false);

        return button;
    }
}