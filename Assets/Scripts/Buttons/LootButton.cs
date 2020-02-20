using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler ,IPointerClickHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text text;

    private LootWindow lootWindow;

    public Image MyIcon { get { return icon; } }

    public Text MyName { get { return text; } }

    public Item MyLoot { get; set; }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
     //   UIManager.MyInstance.ShowTooltip(transform.position, MyLoot,new Vector2(1.7f,0));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //loot the item 
        
        if (InventoryScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UIManager.MyInstance.HideTooltip();
        }
    }
}
