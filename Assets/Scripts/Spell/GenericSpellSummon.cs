using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Summon Spell", menuName = "Spells/Summon", order = 1)]
public class GenericSpellSummon : Spell
{
    [SerializeField]
    private SpellType spellType = SpellType.SUMMON;

    private MinionType minionType = MinionType.Skeleton;

    [SerializeField]
    private float yOffset;

    [SerializeField]
    string parameter;

    [SerializeField]
    private GameObject magicCircle_prefab;

    [SerializeField]
    private GameObject Pet_prefab;

    public GameObject MyPet_prefab
    {
        get
        {
            return Pet_prefab;
        }
        set
        {
            Pet_prefab = value;
        }
    }

    Minion minion;

    public GameObject MymagicCircle_prefab
    {
        get
        {
            return magicCircle_prefab;
        }
        set
        {
            magicCircle_prefab = value;
        }
    }

    private static int mininonCount=0;
    
    public override IEnumerator Ability()
    {
        //distance player can cast spell
        float mousedist = (Input.mousePosition - Camera.main.WorldToScreenPoint(Player.MyInstance.transform.position)).magnitude;
        if (mousedist <= 200)
        {
            //subtract the spell magic cost
            Player.MyInstance.MyMana.MyCurrentValue = Player.MyInstance.MyMana.MyCurrentValue - MagicCost;

            //get the mouse position 
            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            //
            Player.MyInstance.MyAnimator.SetTrigger("summon");
            yield return new WaitForSeconds(MyCastTime);
            
            //instantiate the magic circle prefab
            SpellScriptSummon s = Instantiate(magicCircle_prefab, mousePos, Quaternion.Euler(120, 0, 0)).GetComponent<SpellScriptSummon>();

            //time it take for the magic circle to summon
            yield return new WaitForSeconds(MyCastTime);


            //create and instatiate of the pet prefab
            GameObject p = Instantiate(Pet_prefab, new Vector2(mousePos.x, mousePos.y + yOffset), Quaternion.identity);

            //set the name of the summon minion//
            mininonCount++;
            p.GetComponent<Minion>().name = minionType +""+ mininonCount;
            p.GetComponent<Minion>().MyCharacterName = minionType + "" + mininonCount;

            //add minion to dictionary<minion, minionMenuContainer>, minionMenuContainer =minion, name, damage, requirements
            MinionMenu.MyInstance.MyMainMenu.Add(p.GetComponent<Minion>(), MinionMenu.MyInstance.createContainer(p.GetComponent<Minion>(),
                                                                                                                 p.GetComponent<Minion>().MyCharacterName,
                                                                                                                 p.GetComponent<Minion>().MyIntialDamge,
                                                                                                                 p.GetComponent<Minion>().MyLevel,
                                                                                                                 p.GetComponent<Minion>().MyRequirements));

            //time it take for the pet to summon after the magic circle
            yield return new WaitForSeconds(MyCastTime);

            //set pet to target player
            p.GetComponent<Minion>().Mytarget_player = GameObject.FindWithTag("Player").transform;



         
        }
        else
        {
            //when the player try to summon at a greater distacne than it can
            Debug.Log("Can't cast summon spell");
        }
        Player.MyInstance.StopAttack();


    }


}
