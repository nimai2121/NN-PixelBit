//==============================================================
// Demo Buttons
//==============================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Demo : MonoBehaviour // make a bool condition for if the player is guarding or not; then, reduce the amount of damage taken by a random chance of 50-75%. program ts at some point. - nimai 10/11/25
{
    public GameObject guardButton; //two main menu buttons besides the atk button. 
    public GameObject itemButton; //should be disappearing once in attack menu to make way for attack buttons to spawn in. use gameObject.setActive(false) or sm shi like that ykyk. - nimai 10/11/25

    public GameObject atkMenuButton;
    public GameObject atkBackButton;
    public GameObject atkButton; 
    public GameObject atkButton2;
    public GameObject atkButton3;
    public GameObject atkButton4;
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    
    public InterfaceManager manager;
    public WeaknessSystem weaknessSystem;
    public HealthSystem healthSystem;
    float currentHealth;
    float newHealth;

    public float enemyHealth = 100f; // Track enemy health

    private bool isGuarding = false;//
    private bool playerTurn = true; //going to be used in the future to signal the enemy CPU to attack when playerTurn = false.
    public int levelCounter;

    public struct Attack
    {
        public float dmg;    
        public Element element;       
    }

    public Dictionary<string, Attack> attackDamage = new Dictionary<string, Attack>()
    {
        { "Ragnarok", new Attack{dmg = 10f, element = Element.Fire }  },
        { "Hurricane", new Attack{dmg = 15f, element = Element.Wind } },
        { "Wrath of Zeus", new Attack{dmg = 20f, element = Element.Lightning } },
        { "Glacier", new Attack{dmg = 25f, element = Element.Ice } }
    };

private void Start()
    {
        dialogueBox.SetActive(false);
        levelCounter = 0;
        enemyHealth = 100f; // Initialize enemy health
        atkMenuButton.gameObject.SetActive(true);
        atkButton.gameObject.SetActive(false);
        atkButton2.gameObject.SetActive(false);
        atkButton3.gameObject.SetActive(false);
        atkButton4.gameObject.SetActive(false);
        atkBackButton.gameObject.SetActive(false);
    }
    

    public void OnButtonClick()
    {
        if (!playerTurn) return; // Prevent action if not player's turn

        // HealthSystem.Instance.TakeDamage(15f); // Take damage 15 points
        //HealthSystem.Instance.UseMana(10f);
        //print("doing 10 damage!"); 

        atkMenuButton.gameObject.SetActive(false);
        atkButton.gameObject.SetActive(true);
        atkButton2.gameObject.SetActive(true);
        atkButton3.gameObject.SetActive(true);
        atkButton4.gameObject.SetActive(true);
        atkBackButton.gameObject.SetActive(true);

    }

    /*
    public void Attack1() {
        if (!playerTurn) return;
        switch (levelCounter)
        {
            case 0:
                enemyHealth -= 10f;
                HealthSystem.Instance.UseMana(10f);
                ShowBox("Player did 10 damage to enemy!");
                print("Player did 10 damage to enemy!");
                break;
            default:
                break;
        }
        EndPlayerTurn();
    }

    public void Attack2()
    {
        if (!playerTurn) return;
        switch (levelCounter)
        {
            case 0:
                enemyHealth -= 15f;
                HealthSystem.Instance.UseMana(15f);
                ShowBox("Player did 15 damage to enemy!");
                print("Player did 15 damage to enemy!");
                break;
            default:
                break;
        }
        EndPlayerTurn();
    }

    public void Attack3()
    {
        if (!playerTurn) return;
        switch (levelCounter)
        {
            case 0:
                enemyHealth -= 20f;
                HealthSystem.Instance.UseMana(20f);
                ShowBox("Player did 20 damage to enemy!");
                print("Player did 20 damage to enemy!");
                break;
            default:
                break;
        }
        EndPlayerTurn();
    }

    public void Attack4()
    {
        if (!playerTurn) return;
        switch (levelCounter) //
        {
            case 0:
                enemyHealth -= 25f;
                HealthSystem.Instance.UseMana(25f);
                ShowBox("Player did 25 damage to enemy!");
                print("Player did 25 damage to enemy!");
                break;
            default:
                break;
        }
        EndPlayerTurn();
    }*/

    public void AttackP(string attackName)
    {
        if (!playerTurn) return;

        attackDamage.TryGetValue(attackName, out Attack attack);
        float damage = weaknessSystem.CalculateDamage(attack.dmg, attack.element, levelCounter);

        enemyHealth -= damage;
        HealthSystem.Instance.UseMana(damage);
        ShowBox("Player did " + damage + " damage to enemy!");
        print("Player did " + damage + " damage to enemy!");
        EndPlayerTurn();
    }
    //
    public void EnemyAttack() //test function made by nimai 11/8/25.  
    {
        switch (levelCounter) //
        {
            case 0:
                int multiplier = 5; // the factor by how much damage it reduces per lvl. may be changed for each level/case to be harder n harder as it goes on. 
                
                // AI Random Move Selection (0, 1, or 2)
                int aiChoice = UnityEngine.Random.Range(0, 3);
                float damageToDeal = 0f;
                string moveName = "";

                if (aiChoice == 0)
                {
                    damageToDeal = 5f;
                    moveName = "Light Attack";
                }
                else if (aiChoice == 1)
                {
                    damageToDeal = 10f;
                    moveName = "Slightly Less Light Attack";
                }
                else if (aiChoice == 2)
                {
                    damageToDeal = 15f;
                    moveName = "More than Slightly Less (MTSL) Smash";
                }

                if (isGuarding == true)
                {
                    float dmgAmount = damageToDeal / multiplier;
                    HealthSystem.Instance.TakeDamage(dmgAmount);
                    updateHealth(dmgAmount); //change health value so its changed now.
                    
                    string guardMsg = "Enemy used " + moveName + " and did " + dmgAmount + " damage! (Guarded)";
                    ShowBox(guardMsg);
                    print(guardMsg);
                    print("Player is no longer guarding.");
                    isGuarding = false;
                }
                else
                {
                    HealthSystem.Instance.TakeDamage(damageToDeal);

                    string attackMsg = "Enemy used " + moveName + " and did " + damageToDeal + " damage to Player!";
                    ShowBox(attackMsg);
                    print(attackMsg);
                }
                break;
            default:
                break;
        }
    }

    public void updateHealth(float num)
    {
        newHealth = currentHealth - num;
    }

    public void someGuardFunctionality() //helps guard method, CHANGE NAME WHEN DONE TS NAME IS HORRIBLE nimai 11/8/25.  
    {
        if (!playerTurn) return; // Prevent guarding out of turn

        currentHealth = healthSystem.getHealth(); 
        newHealth = healthSystem.getHealth();

        ShowBox("Player is preparing to guard!");
        print("Player is guarding!"); 

        isGuarding = true; 
        EndPlayerTurn();
        
        //CAM ADVICE NOTE FOR NEXT TIME IM HERE(anytime after 12/6/25): 
        //do ts in a reverse order. ie set isGuarding to true WITHOUT all this health stuff, and then change it so that when the enemy attacks,
        //it'll do 0 or reduced damage, instead of having this while loop. after allat, then you can disable isGuarding again!! - nimai 12/6/25
                
        //btw future me i took SAT today, so my brains a lil fried. what did i get. nimai 12/20/25: THE SAME THING :broken_heart: at least finals was fine
        
    }

    private void EndPlayerTurn()
    {
        playerTurn = false;
        
        // Hide ALL buttons during turn sequencing so it feels like a cutscene
        atkMenuButton.gameObject.SetActive(false);
        atkButton.gameObject.SetActive(false);
        atkButton2.gameObject.SetActive(false);
        atkButton3.gameObject.SetActive(false);
        atkButton4.gameObject.SetActive(false);
        atkBackButton.gameObject.SetActive(false);

        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        // 1. Wait for 1.5 seconds to simulate enemy making a decision
        yield return new WaitForSeconds(1.5f);
        
        // 2. Check for win before enemy gets to attack
        if (enemyHealth <= 0)
        {
            ShowBox("<color=yellow>You won the battle!</color>");
            yield break; // End sequence completely
        }

        // 3. Enemy chooses an attack, applies damage, and updates Text Box
        EnemyAttack();
        
        // 4. Wait so the player can read what the enemy did
        yield return new WaitForSeconds(2.0f);

        // 5. Check if the player died from the attack
        if (healthSystem.getHealth() <= 0)
        {
            ShowBox("<color=red>You lost the battle...</color>");
            yield break; // End sequence completely
        }
        
        // 6. Return control to the player, hide dialogue and show main menu
        dialogueBox.SetActive(false);
        BackButton();
        playerTurn = true;
    }

    public void BackButton()
    {
        atkMenuButton.gameObject.SetActive(true);
        atkButton.gameObject.SetActive(false);
        atkButton2.gameObject.SetActive(false);
        atkButton3.gameObject.SetActive(false);
        atkButton4.gameObject.SetActive(false);
        atkBackButton.gameObject.SetActive(false);
    }

    public void ShowBox(string dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }

    /* public void isPlayerGuarding()
     {
         if ()
     } */


    /* public void Button1()
    {
        HealthSystem.Instance.TakeDamage(10f); // Take damage 10 points
        print("doing 10 damage!");
    }
    public void Button2()
    {
        HealthSystem.Instance.HealDamage(10f); // Heal damage 10 points
    }
    public void Button3()
    {
        HealthSystem.Instance.UseMana(10f); // Decrease mana 10 points
    }
    public void Button4()
    {
        HealthSystem.Instance.RestoreMana(10f); // Increase mana 10 points
    }
    public void Button5()
    {
        HealthSystem.Instance.SetMaxHealth(10f); // Add 10 % to max health
    }
    public void Button6()
    {
        HealthSystem.Instance.SetMaxMana(10f); // Add 10 % to max mana
    } */
}
