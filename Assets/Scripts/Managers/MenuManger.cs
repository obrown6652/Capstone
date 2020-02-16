using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManger : MonoBehaviour
{


    [SerializeField]
    private SubMenu[] subMenu;

    [SerializeField]
    private CanvasGroup mainCanvas;


    // Start is called before the first frame update
    void Start()
    {
        foreach (SubMenu canvas in subMenu)
        {
            Close(canvas);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
          
            OpenCloseSM(subMenu[0].MyCanvas);
            //OpenCloseMM(mainCanvas);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
           
            OpenCloseSM(subMenu[1].MyCanvas);
           // OpenCloseMM(mainCanvas);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            OpenCloseSM(subMenu[2].MyCanvas);
           // OpenCloseMM(mainCanvas);
        }

    }

 



    public void Close(SubMenu canvasGroup)
    {
        canvasGroup.MyCanvas.alpha = 0;
        canvasGroup.MyCanvas.blocksRaycasts = false;
    }

    public void Open(SubMenu canvasGroup)
    {
        canvasGroup.MyCanvas.alpha = 1;
        canvasGroup.MyCanvas.blocksRaycasts = true;
    }

    
    public void OpenCloseSM(CanvasGroup canvasgroup)
    {

       
        foreach (SubMenu menu in subMenu)
        {
            if (menu.MyCanvas != canvasgroup)
            {
                Close(menu);
            }
        }

        //f alpha is more than 0 make it 0 else make it 1
        canvasgroup.alpha = canvasgroup.alpha > 0 ? 0 : 1;
        canvasgroup.blocksRaycasts = canvasgroup.blocksRaycasts == true ? false : true;


    }

    public void OpenCloseSMBtn(CanvasGroup canvasgroup) {
        if (canvasgroup.alpha <=0 )
        {
            foreach (SubMenu menu in subMenu)
            {
                if (menu.MyCanvas != canvasgroup)
                {
                    Close(menu);
                }
            }

            //f alpha is more than 0 make it 0 else make it 1
            canvasgroup.alpha = canvasgroup.alpha > 0 ? 0 : 1;
            canvasgroup.blocksRaycasts = canvasgroup.blocksRaycasts == true ? false : true;
        }
    }
    public void OpenCloseMM(CanvasGroup canvasgroup)
    {
        foreach (SubMenu menu in subMenu)
        {
            if (menu.MyCanvas.alpha >0)
            {
                canvasgroup.alpha = 1;
                canvasgroup.blocksRaycasts = true;
                break;
            }
            else
            {
                canvasgroup.alpha = 0;
                canvasgroup.blocksRaycasts = false;
            }
        }
    }

    public void CloseAll(CanvasGroup canvasgroup) {
        

        foreach (SubMenu menu in subMenu)
        {
            Close(menu);
        }
        OpenCloseMM(mainCanvas);
    }

}
