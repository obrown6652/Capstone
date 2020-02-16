using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{

    private static LootWindow instance;

    public static LootWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LootWindow>();
            }
            return instance;
        }
    }

    private CanvasGroup canvasGroup;

    [SerializeField]
    private LootButton[] lootButtons;

    private List<List<Item>> pages = new List<List<Item>>();

    private List<Item> droppedLoot = new List<Item>();

    private int pageIndex =0;

    [SerializeField]
    private GameObject nextBtn, previousBtn;

    [SerializeField]
    private Text pageNumber;

    //only for debuging 
    [SerializeField]
    private Item[] items;

    public bool IsOpen
    {
        get { return canvasGroup.alpha > 0; }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    private void Start()
    {
        //only for debug
       /* List<Item> tmp = new List<Item>();
        for (int i = 0; i < items.Length; i++)
        {
            tmp.Add(items[i]);
        }
        CreatePage(tmp);*/
    }

    public void CreatePage(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 8 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }
            AddLoot();
            Open();
        }
       
    }

    public void AddLoot()
    {

        if (pages.Count > 0)
        {
            //handle page numbers
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            //handle next and prev buttons actives
            previousBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count -1);

            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    //set icon
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;

                    lootButtons[i].MyLoot = pages[pageIndex][i];
                    //make lootbutotn visable
                    lootButtons[i].gameObject.SetActive(true);
                    //set the name
                    string name = string.Format("<color={0}>{1}</color>", QualityColor.MyColor[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);
                    lootButtons[i].MyName.text = name;
                }

            }
        }
      
     
    }

    public void ClearButtons()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        //check to see if we have more pages
        if (pageIndex < pages.Count -1)
        {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }     
    }
    public void PrevPage()
    {
        //checking if we habe more pages
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);

        droppedLoot.Remove(loot);

        if (pages[pageIndex].Count == 0)
        {
            pages.Remove(pages[pageIndex]);
            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }
            AddLoot();
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

   
    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();
    }
}
