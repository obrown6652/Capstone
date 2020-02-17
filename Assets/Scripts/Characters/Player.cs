using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

/*
 This class contains functionality that is specific to the player feature include:
 stats, AOE collision, armor management, basic attack, dashing, player input,
     */

public class Player : Character
{
    private static Player instance;

    public static Player  MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }


    #region stats 

    //The player Mana stat
    [SerializeField]
    private Stat magicStat_Frame;

    public Stat MyMana
    {
        get {
            return magicStat_Frame;
        }

    }

    [SerializeField]
    private Stat healthStat_characerPanel;
    [SerializeField]
    private Stat magicStat_characerPanel;


    [SerializeField]
    private Stat physicalStrengthStat_characterPanel;
    [SerializeField]
    private Stat magicStrengthStat_characterPanel;
    [SerializeField]
    private Stat magicRegenerationStat_characterPanel;
    [SerializeField]
    private Stat healthRegenerationStat_characterPanel;
    [SerializeField]
    private Stat resistanceStat_characterPanel;
    [SerializeField]
    private Stat spellKnowledge_characterPanel;
    //player speed
   // private float movementSpeed;

    [SerializeField]
    private int initalMagic = 100;
    [SerializeField]
    private int intialPhysicalStrength =0;
    [SerializeField]
    private int intialmagicStrength = 0;
    [SerializeField]
    private int intialMagicRegeneration = 2;
    [SerializeField]
    private int intialHealthRegeneration = 2;
    [SerializeField]
    private int intialResistance = 0;
  
    [SerializeField]
    private int intialSpellKnowledge = 0;

    //change the character panel ui
    private   int changeStats_mainhand = 0;
    private int changeStats_outfit = 0;
    private int changeStats_accessory1=0;
    private int changeStats_accessory2 = 0;
    private int changeStats_accessory3 = 0;
    #endregion


    //an array of blocks used for blocking the player sight
    [SerializeField]
    private SightBlock[] blocks;

    //spell exit points
    [SerializeField]
    private Transform[] exitPoints;

    public Transform[] MyexitPoints
    {
        get {
            return exitPoints;
        }
    }

    //Index that keep track of which exit point to use, set the exit points to down
    private int exitIndex = 2;

    public int MyexitIndex
    {
        get
        {
            return exitIndex;
        }
    }

    //
    [SerializeField]
    private Transform minimapIcon;

    //AoE collison
    [SerializeField]
    private GameObject AoE;
    
    public GameObject MyAoE
    {
        get {
            return AoE;
        }
        
    }

 

    //The player's target
    public Transform MyTarget { get; set; }

    //player light attack, set it to not attacking
    //player weapon attack, set it to not attacking
    private bool playerWeaponAttack = false;

    //The equipped weapon in the character weapon slot
    // String weapon = CharacterPanel.MyInstance.MyMainhand.MyEquippedArmor.MyTitle;

    //get weapon slot
    [SerializeField]
    private CharButton weapon;
    private Armor previousMainHand;
  
    //get outfit slot
    [SerializeField]
    private CharButton outfit;
    private Armor previousOutfit;
   
    //get acessory1 slot
    [SerializeField]
    private CharButton accessory1;
    private Armor previousAccessory1;
 
    //get acessory2 slot
    [SerializeField]
    private CharButton accessory2;
    private Armor previousAccessory2;
  
    //get acessory3 slot
    [SerializeField]
    private CharButton accessory3;
    private Armor previousAccessory3;

    //get the last equip armor
    private Armor getLastEquipment(Armor name) { return name; }
 




    //get each gear, this hole the visual gear to show up on player
    [SerializeField]
    private GearSocket[] gearSockets;



    //interact
    private IInteractable interactable;

    //get the interactable
    public IInteractable MyInteractable
    {
        get
        {
            return interactable;
        }
        set
        {
            interactable = value;
        }
    }

    //get the player current direction, set it to the down direction 
    private Vector3 lastDirection= Vector3.down;

    private bool isDashDownButton;
    [SerializeField]
    private float dashDistance = 2f;

    public float interactionRadius; //talk range
    

    private int weaponLayer;

    private void Awake()
    {
        this.weapon = weapon.GetComponent<CharButton>();
        this.outfit = outfit.GetComponent<CharButton>();
        this.accessory1 = accessory1.GetComponent<CharButton>();
        this.accessory2 = accessory2.GetComponent<CharButton>();
        this.accessory3 = accessory3.GetComponent<CharButton>();
    }
    protected override void Start()
    {
        //set the player start mana and start max mana
        magicStat_Frame.Initialize(initalMagic, initalMagic);
        //set and start the player mana regeneration by 2points every 2 seconds
        magicStat_Frame.InitializeRegeneration(intialMagicRegeneration, 2);
        //set and start the player health regeneration by 2points every 2 seconds
        healthStat.InitializeRegeneration(intialHealthRegeneration, 2);

        //set the player start mana and start max mana
        magicStat_characerPanel.Initialize(initalMagic, initalMagic);
        //set and start the player mana regeneration by 2points every 2 seconds
        magicRegenerationStat_characterPanel.Initialize(intialMagicRegeneration, intialMagicRegeneration);
        magicStat_characerPanel.InitializeRegeneration(magicRegenerationStat_characterPanel.MyCurrentValue, 2);
        // set the player start health and start max health 
        healthStat_characerPanel.Initialize(InitialHealth, InitialHealth);
        //set and start the player health regeneration by 2points every 2 seconds
        healthRegenerationStat_characterPanel.Initialize(intialHealthRegeneration, intialHealthRegeneration);
        healthStat_characerPanel.InitializeRegeneration(healthRegenerationStat_characterPanel.MyCurrentValue, 2);
        //
        physicalStrengthStat_characterPanel.Initialize(intialPhysicalStrength, intialPhysicalStrength);
        magicStrengthStat_characterPanel.Initialize(intialmagicStrength, intialmagicStrength);
        resistanceStat_characterPanel.Initialize(intialResistance, intialResistance);
        spellKnowledge_characterPanel.Initialize(intialSpellKnowledge, intialSpellKnowledge);


        base.Start();
    }

    //overriding the characters update function, so that we can execute our own function
    protected override void Update()
    {
        // Remove all player control when we're in dialogue
        if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
        {
            return;
        }


        //Executes the getInput function
        if (canMove)
        {
            getInput();
        }
        else
        {
        }
       
        HandelEquipStats();

        base.Update();

        //test code
        Debug.DrawRay(transform.position, direction, Color.green);

        int layermask = ~LayerMask.GetMask("Player");
       RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f,layermask);
        if (hit.collider != null)
        {
          //  Debug.Log("hit something");
        }
        else
        {
          //  Debug.Log("hit nothing");
        }
        //test code 

    }


    //object face camera good for gun
    private void faceCamera() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }


    //Listen to the players input 
    private void getInput() {
        direction = Vector3.zero;


        // Detect if we want to start a conversation
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForNearbyNPC();
        }

        //direction and control exit points  
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
        {
      
            exitIndex = 0;
            direction += new Vector3(0,1);
            lastDirection = Vector3.up;

            minimapIcon.eulerAngles = new Vector3(0,0,0);
            
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
        {
     
            exitIndex = 3;
            direction += new Vector3(-1, 0);
            lastDirection = Vector3.left;

            if (direction.y == 0)
            {
                minimapIcon.eulerAngles = new Vector3(0, 0, 90);
            }
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]) )
        {
          
            exitIndex = 2;
            direction += new Vector3(0, -1);
            lastDirection = Vector3.down;


            minimapIcon.eulerAngles = new Vector3(0, 0, 180);
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]) )
        {
            exitIndex = 1;
            direction += new Vector3(1, 0);
            lastDirection = Vector3.right;

            if (direction.y == 0)
            {
                minimapIcon.eulerAngles = new Vector3(0, 0, 270);
            }
        }
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //manage player dash
            isDashDownButton = true;
        }
        

        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }
        
        //weapons attack
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //check to see if not over an ui
        {
            if (!MyIsAttack_Basic && !MyIsAttacking_Spells)
            {
                //the mouse click position
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);
   
                //if the mouse pointer isn't over pickupable item
                if (!hit || hit.collider.tag == "Enemy")
                {

                    try
                    {
                        //check to see if we have a weapon in hand
                        CharButton weapon = this.weapon;

                        //check to see if there's a weapon equipped
                        if (weapon.MyEquippedArmor.MyTitle != null)
                        {
                            //change the character direction base on the mouse pointer direction
                            CheckMouseDir();
                        }
                        
                        playerWeaponAttack = true;

                    }
                    catch (NullReferenceException ex)
                    {

                        Debug.Log("Can't attack "+ ex);
                    }
                    
                }
              
            }

          
        }



        //test code 
        if (Input.GetKeyDown(KeyCode.P)) {
            InMemoryVariableStorage.MyInstance.SetValue("$open_Chest", true);
            healthStat.MyCurrentValue -= 10;
            magicStat_Frame.MyCurrentValue -= 20;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            healthStat.MyCurrentValue += 10;
            magicStat_Frame.MyCurrentValue += 20;
        }
        //test code
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isDashDownButton)
        {
            HandleDash();

        }

       
    }

    /// Find all DialogueParticipants
    /** Filter them to those that have a Yarn start node and are in range; 
     * then start a conversation with the first one
     */
    public void CheckForNearbyNPC()
    {
        List<NPC> allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
        var target = allParticipants.Find(delegate (NPC p) {
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
            Vector3.Distance(this.transform.position, p.transform.position) <= interactionRadius;// is in range? 
        });
        if (target != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
        }
    }






    //cast a spell from spellbook array
    public void CastSpell(string spellName)
    {
        //activate and deactivate base on player direction
        SightBlock();

        //can start attack if not already attacking
        if (!MyIsAttacking_Spells)
        {
           // attackCoroutine = StartCoroutine(Attack_Spells(spellName));

            
            AttackSpell(spellName);
        }
       
       
    }

    private void AttackSpell(string spellName)
    {

        if (!MyIsAttacking_Spells)
        {
            //get the name of the spell from the spell book array
            Spell newSpell = SpellInventory.MyInstance.CastSpell(spellName);

            


            if (magicStat_Frame.MyCurrentValue >= newSpell.MagicCost)
            {
                MyAnimator.SetBool("attack", MyIsAttacking_Spells = true);

                attackCoroutine = StartCoroutine(newSpell.Ability());

               
            }
        }


    }


    //Check if the target is in line of sight
    private bool InLineOfSight()
    {
        //calculates the target direction
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        //Throws a raycast in the direction of the target
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position,MyTarget.transform.position), 256);

        //if we didn't hit the block, then can cast a spell
        if (hit.collider == null)
        {
            return true;
        }
        //if we hit the block can't cast a spell
        return false;
    }

    //activate and deactivate sight blocks
    private void SightBlock()
    {
        //deactivate all sight blocks
        foreach (SightBlock b in blocks)
        {
            b.Deactivate();
        }

        //activate sight blocks base on player direction
        blocks[exitIndex].Activate();
    }

   

  

    public void StopAttack()
    {
        //if there's an attack stop coroutine and set attack to false
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            animator.SetBool("attack", MyIsAttacking_Spells = false);

        }

        //if there's no attack just set attack to false
        else if (attackCoroutine == null)
        {
            animator.SetBool("attack", MyIsAttacking_Spells = false);
        }
    }
    public override void handleAnimation()
    {
        base.handleAnimation();

        if (isMoving )
        {
            foreach (GearSocket gear in gearSockets)
            {
                gear.SetXAndY(direction.x, direction.y);
                gear.setMoving(true);
                
            }
           
        }
        else
        {
            foreach (GearSocket gear in gearSockets)
            {
                gear.setMoving(false);

            }
        }

        
        if (playerWeaponAttack)
        {
            try
            {
                //if the weapon in player hand is a sword
                if (this.weapon.MyEquippedArmor.MyWeaponType == weaponType.Sword)
                {
                    AttackSword();
                }
                else if (this.weapon.MyEquippedArmor.MyWeaponType == weaponType.Staff)
                {
                    Debug.Log("staff in hand");
                }
                
            }
            catch (NullReferenceException ex)
            {

                Debug.Log("Can't attack");
                MyIsAttack_Basic = false;
            }
            
        }
        
    }

    private void AttackSword()
    {
        String weapon = this.weapon.MyEquippedArmor.MyTitle;
        //list all the weapons name
        if (weapon == "Steel Sword")
        {
            ActivateLayer("SteelSwordWeaponAttackLayer");
            weaponLayer = 3; //weapon attack layer
        }
        else if (weapon == "Undead Sword")
        {
            ActivateLayer("UndeadSwordWeaponAttackLayer");

            weaponLayer = 4;
        }
        WeaponAttack();
    }

    //start weapon attack coroutine
    public void WeaponAttack()
    {
        if (!MyIsAttack_Basic)
        {
            //start coroutine
            attackCoroutine = StartCoroutine(Attack_Weapon());
        }
    }

    private IEnumerator Attack_Weapon()
    {

        if (!MyIsAttack_Basic)
        {
            //set varibles attck to true
            canMove = false;
            MyAnimator.SetBool("attacking", MyIsAttack_Basic = true);
            yield return new WaitForSeconds(weapon.MyEquippedArmor.MyWeaponAnimationSpeed);

            // StopAttack();
            MyAnimator.SetBool("attacking", MyIsAttack_Basic = false);
            playerWeaponAttack = false;
            canMove = true;
            StopCoroutine(Attack_Weapon());
        }
    }

    private void HandleDash()
    {

        
       
        if (isDashDownButton)
        {
            MyRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
           
            MyRigidbody.MovePosition(transform.position + CheckMouseDir(direction) * dashDistance);
            isDashDownButton = false;
            
        }
      
    }
    private bool CanMove(Vector3 direction, float distance )
    {
        int layerMask = ~LayerMask.GetMask("Player");


       
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 2f);

        foreach (var hit in hits)
        {
            if (!hit.transform.tag.Equals("Player"))
            {
               
                return false;

            }
         


        }


        return true;
    }
   
    public void CheckMouseDir()
    {

        //calculate the mouse pointer, get player direction of mouse pointer
        Vector2 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 towardsMouseFromPlayer = positionMouse - (Vector2)transform.position;
        Vector2 vectorAttack = towardsMouseFromPlayer.normalized;

        if (vectorAttack.x >= 0.7f && vectorAttack.y >= -0.7 && vectorAttack.y <= 0.7f)
        {
            //right direction

            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY",0);
            direction = new Vector3(1, 0).normalized;
        }
        else if (vectorAttack.x < 0.7f && vectorAttack.y >= -0.7 && vectorAttack.y <= 0.7f)
        {
            //left direction

            animator.SetFloat("moveX",-1);
            animator.SetFloat("moveY", 0);
            direction = new Vector3(-1, 0).normalized;

        }
        else if (vectorAttack.y > 0.7f && vectorAttack.x >= -0.7 && vectorAttack.x <= 0.7f)
        {
            //up direction
        
            animator.SetFloat("moveY", 1);
            animator.SetFloat("moveX", 0);
            direction = new Vector3(0, 1).normalized;
        }
        else if (vectorAttack.y < 0.7f && vectorAttack.x > -0.7 && vectorAttack.x < 0.7f)
        {
            //down direciton
      
            animator.SetFloat("moveY", -1);
            animator.SetFloat("moveX", 0);
            direction = new Vector3(0,-1);

        }
        direction = Vector3.zero;
       
    }

    public Vector3 CheckMouseDir(Vector3 direction)
    {

        //calculate the mouse pointer, get player direction of mouse pointer
        Vector2 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 towardsMouseFromPlayer = positionMouse - (Vector2)transform.position;
        Vector2 vectorAttack = towardsMouseFromPlayer.normalized;

        if (vectorAttack.x >= 0.7f && vectorAttack.y >= -0.7 && vectorAttack.y <= 0.7f)
        {
            //right direction

            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY", 0);
            direction = new Vector3(1, 0).normalized;
        }
        else if (vectorAttack.x < 0.7f && vectorAttack.y >= -0.7 && vectorAttack.y <= 0.7f)
        {
            //left direction

            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);
             direction = new Vector3(-1, 0).normalized;

        }
        else if (vectorAttack.y > 0.7f && vectorAttack.x >= -0.7 && vectorAttack.x <= 0.7f)
        {
            //up direction

            animator.SetFloat("moveY", 1);
            animator.SetFloat("moveX", 0);
             direction = new Vector3(0, 1).normalized;
        }
        else if (vectorAttack.y < 0.7f && vectorAttack.x > -0.7 && vectorAttack.x < 0.7f)
        {
            //down direciton

            animator.SetFloat("moveY", -1);
            animator.SetFloat("moveX", 0);
            direction = new Vector3(0, -1);

        }
        return direction ;
    }

    public override void ActivateLayer(string layerName)
    {
        base.ActivateLayer(layerName);

        foreach (GearSocket gear in gearSockets)
        {
            gear.ActivateLayer(layerName);
        }
    }


    public void Interact()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "PickUp" || collision.tag == "Spellbook" || collision.tag == "Interactable" || collision.tag == "Chest")
        {
            if (Interactiveui.MyInstance != null)
            {
                Interactiveui.MyInstance.Activate();
            }
            interactable = collision.GetComponent<IInteractable>();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "PickUp" || collision.tag == "Spellbook" || collision.tag == "Interactable" || collision.tag == "Chest")
        {
            if (interactable != null)
            {
                if (Interactiveui.MyInstance != null)
                {
                    Interactiveui.MyInstance.DeActivate();
                }

                interactable.StopInteract();
                interactable = null;

            }
        }
    }


    //change the character panel stats base on equip armor
    public void ChangeStatsCharacterPanel(CharButton equipment)
    {
       
           
        healthStat_characerPanel.MyMaxValue += equipment.MyEquippedArmor.MyHealth;
        healthStat_characerPanel.MyCurrentValue += equipment.MyEquippedArmor.MyHealth;

        magicStat_characerPanel.MyMaxValue += equipment.MyEquippedArmor.MyMagic;
        magicStat_characerPanel.MyCurrentValue += equipment.MyEquippedArmor.MyMagic;

        movementSpeedStat.MyMaxValue += equipment.MyEquippedArmor.MySpeed;
        movementSpeedStat.MyCurrentValue += equipment.MyEquippedArmor.MySpeed;

        resistanceStat_characterPanel.MyMaxValue += equipment.MyEquippedArmor.MyResistance;
        resistanceStat_characterPanel.MyCurrentValue += equipment.MyEquippedArmor.MyResistance;

        magicRegenerationStat_characterPanel.MyMaxValue += equipment.MyEquippedArmor.MyMagicRecovery;
        magicRegenerationStat_characterPanel.MyCurrentValue += equipment.MyEquippedArmor.MyMagicRecovery;

        healthRegenerationStat_characterPanel.MyMaxValue += equipment.MyEquippedArmor.MyHealthRecovery;
        healthRegenerationStat_characterPanel.MyCurrentValue += equipment.MyEquippedArmor.MyHealthRecovery;

        physicalStrengthStat_characterPanel.MyMaxValue += equipment.MyEquippedArmor.MyphsicalDamage;
        physicalStrengthStat_characterPanel.MyCurrentValue += equipment.MyEquippedArmor.MyphsicalDamage;


        magicStrengthStat_characterPanel.MyMaxValue += equipment.MyEquippedArmor.myMagicDamage;
        magicStrengthStat_characterPanel.MyCurrentValue += equipment.MyEquippedArmor.myMagicDamage;

         

    }

    //revert the changes on the character panel 
    public void RevertChangeStateCharacterPanel(Armor armor)
    {
        healthStat_characerPanel.MyMaxValue -= armor.MyHealth;
        healthStat_characerPanel.MyCurrentValue -= armor.MyHealth;

        magicStat_characerPanel.MyMaxValue -= armor.MyMagic;
        magicStat_characerPanel.MyCurrentValue -= armor.MyMagic;

        movementSpeedStat.MyMaxValue -= armor.MySpeed;
        movementSpeedStat.MyCurrentValue -= armor.MySpeed;

        resistanceStat_characterPanel.MyMaxValue -= armor.MyResistance;
        resistanceStat_characterPanel.MyCurrentValue -= armor.MyResistance;

        magicRegenerationStat_characterPanel.MyMaxValue -= armor.MyMagicRecovery;
        magicRegenerationStat_characterPanel.MyCurrentValue -= armor.MyMagicRecovery;

        healthRegenerationStat_characterPanel.MyMaxValue -= armor.MyHealthRecovery;
        healthRegenerationStat_characterPanel.MyCurrentValue -= armor.MyHealthRecovery;

        physicalStrengthStat_characterPanel.MyMaxValue -= armor.MyphsicalDamage;
        physicalStrengthStat_characterPanel.MyCurrentValue -= armor.MyphsicalDamage;


        magicStrengthStat_characterPanel.MyMaxValue -= armor.myMagicDamage;
        magicStrengthStat_characterPanel.MyCurrentValue -= armor.myMagicDamage;
    }

    //change the Player health/magic ui stats base on equip armor
    public void ChangeStatFrame(CharButton equipment)
    {
        //player health/magic UI
        healthStat.MyMaxValue += equipment.MyEquippedArmor.MyHealth;
        healthStat.MyCurrentValue += equipment.MyEquippedArmor.MyHealth;
        healthStat.MyRegenearationAmount += equipment.MyEquippedArmor.MyHealthRecovery;

        //player health/magic UI
        magicStat_Frame.MyMaxValue += equipment.MyEquippedArmor.MyMagic;
        magicStat_Frame.MyCurrentValue += equipment.MyEquippedArmor.MyMagic;
        magicStat_Frame.MyRegenearationAmount += equipment.MyEquippedArmor.MyMagicRecovery;
    }

    public void RevertChangeStateFrame(Armor armor)
    {
        //player health/magic UI
        healthStat.MyMaxValue -= armor.MyHealth;
        healthStat.MyCurrentValue -= armor.MyHealth;
        healthStat.MyRegenearationAmount -= armor.MyHealthRecovery;
        //player health/magic UI
        magicStat_Frame.MyMaxValue -= armor.MyMagic;
        magicStat_Frame.MyCurrentValue -= armor.MyMagic;
        magicStat_Frame.MyRegenearationAmount -= armor.MyMagicRecovery;
    }
    private void HandelEquipStats()
    {
          
        //MainHand start
        if (this.weapon.MyEquippedArmor != null)
        {
            if (changeStats_mainhand == 0)
            {
                //player health/magic UI
                ChangeStatFrame(this.weapon);
                ChangeStatsCharacterPanel(this.weapon);
                this.changeStats_mainhand = 1;
                this.previousMainHand = getLastEquipment(this.weapon.MyEquippedArmor);
  
            }
        }
        else if (this.weapon.MyEquippedArmor == null)
        {
            if (changeStats_mainhand == 1)
            {
                RevertChangeStateFrame(this.previousMainHand);
                RevertChangeStateCharacterPanel(this.previousMainHand);
                this.changeStats_mainhand = 0;
            }
        }
        //MainHand End

        //Outfit start
        if (this.outfit.MyEquippedArmor != null)
        {
            if (changeStats_outfit == 0)
            {

                ChangeStatFrame(this.outfit);
                ChangeStatsCharacterPanel(this.outfit);
                this.changeStats_outfit = 1;
                this.previousOutfit = getLastEquipment(this.outfit.MyEquippedArmor);

            }
        }
        else if (this.outfit.MyEquippedArmor == null)
        {
            if (changeStats_outfit == 1)
            {

                RevertChangeStateFrame(this.previousOutfit);
                RevertChangeStateCharacterPanel(this.previousOutfit);
                this.changeStats_outfit = 0;
            }
        }
        //Outfit end

        //Accessory 1 start
        if (this.accessory1.MyEquippedArmor != null)
        {
            if (changeStats_accessory1 == 0)
            {

                ChangeStatFrame(this.accessory1);
                ChangeStatsCharacterPanel(this.accessory1);
                this.changeStats_accessory1 = 1;
                this.previousAccessory1 = getLastEquipment(this.accessory1.MyEquippedArmor);

            }
        }
        else if (this.accessory1.MyEquippedArmor == null)
        {
            if (changeStats_accessory1 == 1)
            {

                RevertChangeStateFrame(this.previousAccessory1);
                RevertChangeStateCharacterPanel(this.previousAccessory1);
                this.changeStats_accessory1 = 0;
            }
        }
        //Accessory 1 end

        //Accessory 2 start
        if (this.accessory2.MyEquippedArmor != null)
        {
            if (changeStats_accessory2 == 0)
            {

                ChangeStatFrame(this.accessory2);
                ChangeStatsCharacterPanel(this.accessory2);
                this.changeStats_accessory2 = 1;
                this.previousAccessory2 = getLastEquipment(this.accessory2.MyEquippedArmor);

            }
        }
        else if (this.accessory2.MyEquippedArmor == null)
        {
            if (changeStats_accessory2 == 1)
            {

                RevertChangeStateFrame(this.previousAccessory2);
                RevertChangeStateCharacterPanel(this.previousAccessory2);
                this.changeStats_accessory2 = 0;
            }
        }
        //Accessory 2 end


        //Accessory 3 start
        if (this.accessory3.MyEquippedArmor != null)
        {
            if (changeStats_accessory3 == 0)
            {

                ChangeStatFrame(this.accessory3);
                ChangeStatsCharacterPanel(this.accessory3);
                this.changeStats_accessory3 = 1;
                this.previousAccessory3 = getLastEquipment(this.accessory3.MyEquippedArmor);

            }
        }
        else if (this.accessory3.MyEquippedArmor == null)
        {
            if (changeStats_accessory3 == 1)
            {

                RevertChangeStateFrame(this.previousAccessory3);
                RevertChangeStateCharacterPanel(this.previousAccessory3);
                this.changeStats_accessory3 = 0;
            }
        }
        //Accessory 3 end

       
    }

    

}
