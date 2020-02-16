using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour

{
    //The gameobject image
    private Image content;

    //The gameobject  text
    [SerializeField]
    private Text statValue;

    [SerializeField]
    private string statName;

    //the ui fill
    private float currentFill;

    //the lerp speed
    [SerializeField]
    private float lerpSpeed;

    //the current value
    private float currentValue;    

    //the regeneration time
    protected float regenerationTime;

    //the regeneration amount
    protected float regenerationAmount;

    public float MyRegenearationAmount
    {
        get { return regenerationAmount; }
        set { regenerationAmount = value; }
    }

    //if the stat has a slider or not
    [SerializeField]
    public bool hasSlider = true;

    [SerializeField]
    public bool isCharPanelStat = true;





    //get and set max value
    public float MyMaxValue { get; set; }


    //a property to get and set current value
    public float MyCurrentValue    
    {
        get
        {
            return currentValue;
        }
        set
        {
            //make sure current value never go over max value
            if (value > MyMaxValue)
            {       
                currentValue = MyMaxValue;
            }
            //make sure current value never go lower than zero 
            else if (value < 0)
            {       
                currentValue = 0;
            }
            //use the current value if pass all checks
            else
            { 
                currentValue = value;
                
            }

            //Calculates the currentFill so that we can lerp 
            currentFill = currentValue / MyMaxValue;

            //if health have a text 
            if (statValue != null)
            {
                //set the ui text
                statValue.text = currentValue + "/" + MyMaxValue;
            }
            if (isCharPanelStat)
            {
                statValue.text = statName + ": "+  MyMaxValue;
            }
          
        
           
            
        }
    }

 

    // Start is called before the first frame update
    void Start()
    {
        if (hasSlider == true)
        {
            content = GetComponent<Image>();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hasSlider)
        {
            //control the stat bar fill with lerping
            if (currentFill != content.fillAmount)
            {
                //this is a incorrent way to use larping: non-linear and speed varies base on user framerate, and never reach start paramenter
                content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }
      
    }

    //initialize te max vale
    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;

    }



    public void InitializeRegeneration(float amount, int time)
    {
        regenerationAmount = amount;
        regenerationTime = time;

        //repeat the method by regeneartion time with no delay
        InvokeRepeating("RegenerateCalculation", 0.0f, regenerationTime);
    }

    public void RegenerateCalculation()
    {
        if (MyCurrentValue < MyMaxValue)
        {
            MyCurrentValue += regenerationAmount;
        }

      
    }
}
