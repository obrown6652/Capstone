using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SubMenu
{
    [SerializeField]
    private CanvasGroup canvas;

    [SerializeField]
    private Button button;

    public CanvasGroup MyCanvas
    {
        get {
            return canvas;
        }
    }

    public Button MyButton
    {
        get
        {
            return button;
        }
    }

}
