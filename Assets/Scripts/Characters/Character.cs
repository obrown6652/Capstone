using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

/*
 This class is the basic class for all character in the game: feature include: 
    health, movement speed, moving the character, actvating/deactving different weight layers, 
    checking to see if character is alive or not, and take damage. 
     */




    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class Character : MonoBehaviour
    {

        private static Character instance;

        public static Character MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Character>();
                }
                return instance;
            }
        }


        //the character name
        [SerializeField]
        protected string characterName;

        public string MyCharacterName
        {
            get
            {
                return characterName;
            }
            set { characterName = value; }
        }


        //The initial character max health
        [SerializeField]
        protected float InitialHealth;

        //character health stat for ui
        [SerializeField]
        protected Stat healthStat;

        //get the character health
        public Stat MyHealth
        {
            get
            {
                return healthStat;
            }
            set
            {
                healthStat = value;
            }
        }

        //check if character is alive or not
        public bool isAlive
        {
            get
            {
                return healthStat.MyCurrentValue > 0;
            }
        }


        [SerializeField]
        protected float intialSpeed;

        // The  character movement speed
        [SerializeField]
        protected Stat movementSpeedStat;

        public Stat MyMovementSpeed
        {
            get { return movementSpeedStat; }
            set { movementSpeedStat = value; }
        }


        //Character hitbox
        [SerializeField]
        protected Transform hitBox;

        // The character direction 
        protected Vector2 direction;

        public Vector2 MyDirection
        {
            get { return direction; }
            set { direction = value; }
        }

        //control character animation 
        protected Animator animator;

        public Animator MyAnimator
        {
            get
            {
                return animator;
            }

        }

        //control character rigidbody
        protected Rigidbody2D Rigidbody;

        public Rigidbody2D MyRigidbody
        {
            get { return Rigidbody; }
            set { Rigidbody = value; }
        }

        //control attack coroutine
        protected Coroutine attackCoroutine;


        //Indicates if the character is attacking or not
        private bool isAttacking_Spells;

        public bool MyIsAttacking_Spells
        {
            get
            {
                return isAttacking_Spells;
            }
            set
            {
                isAttacking_Spells = value;
            }
        }

        private bool isAttack_basic;

        public bool MyIsAttack_Basic
        {
            get
            {
                return isAttack_basic;
            }
            set
            {
                isAttack_basic = value;
            }
        }


        //Indicates if the player is moving or not
        public bool isMoving
        {
            get
            {
                return direction != Vector2.zero;
            }


        }
        public bool canMove;

        //the character dust particle 
        [SerializeField]
        private ParticleSystem dustPS;


   


        // Start is called before the first frame update
        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();


            // set the player start health and start max health 
            if (healthStat != null)
            {
                healthStat.Initialize(InitialHealth, InitialHealth);

            }
            if (movementSpeedStat != null)
            {
                movementSpeedStat.Initialize(intialSpeed, intialSpeed);

            }

            isAttacking_Spells = false;
            isAttack_basic = false;

            canMove = true;

       

        }

        // Update is marked as virtual, so that we can override it in the subclasses
        protected virtual void Update()
        {
            if (Rigidbody != null)
            {
                handleAnimation();
            }

        }

        //FixedUpdate is called depending on how many physics frames per second are set in the time settings
        private void FixedUpdate()
        {
            if (Rigidbody != null)
            {
                Move();
            }

        }

        //Moves the character 
        public void Move()
        {
            if (isMoving && canMove)
            {

                //Makes sure that the character moves
                Rigidbody.velocity = new Vector2(direction.normalized.x, direction.normalized.y) * movementSpeedStat.MyMaxValue;
            }
            else
            {
                Rigidbody.velocity = Vector2.zero;

            }

        }

        public virtual void handleAnimation()
        {
            //Check if character is moving if so then play moving animation if not go to idle
            if (isMoving && canMove)
            {

                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);
                animator.SetBool("moving", true);

                if (dustPS != null)
                {
                    dustPS.Play();
                }

            }
            //Make sure that the character go back to idle when not pressing any keys
            else
            {

                ActivateLayer("BaseLayer");
                animator.SetBool("moving", false);

                if (dustPS != null)
                {
                    dustPS.Stop();
                }

            }
            //make it so that it can cast and move at the same time 
            if (isAttacking_Spells)
            {
                ActivateLayer("SpellLayer");
            }
            if (MyIsAttack_Basic)
            {
                ActivateLayer("AttackLayer");
            }



        }



        //set all the weight layer to inactive excpet one
        public virtual void ActivateLayer(string layerName)
        {
            //for all the layer set them to inactive
            for (int i = 0; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }
            //make only one layer active
            animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
        }



        public virtual void TakeDamage(float damage)
        {
            //reduce health
            healthStat.MyCurrentValue -= damage;
            if (healthStat.MyCurrentValue <= 0)
            {
                //die
                animator.SetTrigger("die");
            }
        }
    }

