using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MinionType { Skeleton}
public class Minion : NPC
{

    private static Minion instance;

    public static Minion MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Minion>();
            }
            return instance;
        }
    }

    private IState currentState;


    [SerializeField]
    private float IntialDamage;

    public float MyIntialDamge {
        get {
            return IntialDamage;
        }
    }

    private Stat Damage;

    public Stat MyDamage
    {
        get {
 
            return Damage;
        }
    }

    [SerializeField]
    private int level;

    public int MyLevel { get { return level; } set { level = value; } }

    [SerializeField]
    private string requirements;
    public string MyRequirements { get { return requirements; } set { requirements = value; } }



    private bool canDie = false;

    [SerializeField]
    private Transform target_player;

    public Transform Mytarget_player
    {
        get { return target_player; }
        set { target_player = value; }
    }
    
    [SerializeField]
    private Transform target_Enemy;

    public Transform Mytarget_Enemy
    {
        get { return target_Enemy; }
        set { target_Enemy = value; }
    }

    
    public float MyAttackRange { get; set; }
    public float MyFollowRange { get; set; }

    public float MyAttackTime { get; set; }

    [SerializeField]
    private CanvasGroup canvasGroup;


    private Camera mainCamera;

    private Canvas canvas_go;

    [SerializeField]
    private Text nameTxt;

    [SerializeField]
    private Text damaageTxt;

    [SerializeField]
    private Text levelTxt;

    [SerializeField]
    private Text nameTxt_1;


    


    protected void Awake()
    {

        MyAttackRange = 3;
        MyFollowRange = 9;
        ChangeState(new IdleState());

        canvas_go = gameObject.transform.Find("Canvas").GetComponent<Canvas>();
    }

    protected override void Start()
    {
        mainCamera = Camera.main;

        canvas_go.worldCamera = mainCamera;

        if (healthStat == null)
        {
            healthStat = this.gameObject.AddComponent<Stat>();
            healthStat.isCharPanelStat = false;
            healthStat.hasSlider = false;
            healthStat.Initialize(InitialHealth, InitialHealth);
        }

        if (movementSpeedStat == null)
        {
            movementSpeedStat = this.gameObject.AddComponent<Stat>();
            movementSpeedStat.isCharPanelStat = false;
            movementSpeedStat.hasSlider = false;
            movementSpeedStat.Initialize(intialSpeed,intialSpeed);

        }
        Damage = this.gameObject.AddComponent<Stat>();
        Damage.isCharPanelStat = false;
        Damage.hasSlider = false;
        Damage.Initialize(IntialDamage, IntialDamage);
        base.Start();

        if (requirements == null)
        {
            requirements = "none";
        }
       



    }

   
    protected override void Update()
    {

        if (isMoving)
        {
            StopInteract();
        }

        if (!MyIsAttack_Basic)
        {
            MyAttackTime += Time.deltaTime;
        }

        currentState.Update();
        base.Update();

        nameTxt.text = nameTxt_1.text = this.MyCharacterName;
        damaageTxt.text = this.Damage.MyMaxValue + "";
        levelTxt.text = this.level + "";


    }

  

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }


    public override void Interact()
    {
        Debug.Log("Mininon is click");
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;

    }

    public override void StopInteract()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

}
