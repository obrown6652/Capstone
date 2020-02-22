using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    { get { return instance; } }



 

    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private CanvasGroup keybindsMenu;

    [SerializeField]
    private CanvasGroup Inventory;

    private GameObject[] keybindButtons;

    [SerializeField]
    private GameObject tooltip;

    private Text tooltipText;

 

    [SerializeField]
    private RectTransform tooltipRect;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keybindsMenu);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
           // InventoryScript.MyInstance.OpenClose();

            OpenClose(Inventory);
           

        }
       
    }



 

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

 
    public void OpenClose(CanvasGroup canvasgroup)
    {
        //if alpha is more than 0 make it 0 else make it 1
        canvasgroup.alpha = canvasgroup.alpha > 0 ? 0 : 1;
        canvasgroup.blocksRaycasts = canvasgroup.blocksRaycasts == true ? false : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0,0,0,0);
            clickable.MyIcon.color = Color.white;
        }
        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0,0,0,0);
            clickable.MyStackText.color = new Color(0,0,0,0);
        }
    }

    public void ShowTooltip(Vector2 pivot, IDescribable description, Vector2 offset)
    {


        tooltipRect.pivot = offset;
        tooltip.transform.position = pivot;
        tooltip.SetActive(true);
        tooltipText.text = description.GetDescription();
    }
    public void ShowTooltip(Vector2 pivot, string description, Vector2 offset)
    {


        tooltipRect.pivot = offset;
        tooltip.transform.position = pivot;
        tooltip.SetActive(true);
        tooltipText.text = description;
    }
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void RefreashTooltip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
