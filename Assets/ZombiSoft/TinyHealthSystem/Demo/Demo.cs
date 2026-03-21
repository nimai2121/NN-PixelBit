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

    public HealthSystem healthSystem;
    float currentHealth;
    float newHealth;

    private bool isGuarding = false;//
   // private bool playerTurn = true; //going to be used in the future to signal the enemy CPU to attack when playerTurn = false.
    public int levelCounter;

    private void Start()
    {
        dialogueBox.SetActive(false);
        levelCounter = 0;
        atkMenuButton.gameObject.SetActive(true);
        atkButton.gameObject.SetActive(false);
        atkButton2.gameObject.SetActive(false);
        atkButton3.gameObject.SetActive(false);
        atkButton4.gameObject.SetActive(false);
        atkBackButton.gameObject.SetActive(false);
    }
    

    public void OnButtonClick()
    {
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

    
    public void Attack1() {
        switch (levelCounter)
        {
            case 0:
                HealthSystem.Instance.UseMana(10f);
                ShowBox("Player did 10 damage to enemy!");
                print("Player did 10 damage to enemy!");
                break;
            default:
                break;
        }
    }

    public void Attack2()
    {
        switch (levelCounter)
        {
            case 0:
                HealthSystem.Instance.UseMana(15f);
                print("Player did 15 damage to enemy!");
                break;
            default:
                break;
        }
    }

    public void Attack3()
    {
        switch (levelCounter)
        {
            case 0:
                HealthSystem.Instance.UseMana(20f);
                print("Player did 20 damage to enemy!");
                break;
            default:
                break;
        }
    }

    public void Attack4()
    {
        switch (levelCounter) //
        {
            case 0:
                HealthSystem.Instance.UseMana(25f);
                print("Player did 25 damage to enemy!");
                break;
            default:
                break;
        }
    }

    public void EnemyAttack() //test function made by nimai 11/8/25.  
    {
        switch (levelCounter) //
        {
            case 0:
                int multiplier = 5; // the factor by how much damage it reduces per lvl. may be changed for each level/case to be harder n harder as it goes on. 
                if (isGuarding == true)
                {
                    int thing = 20 / multiplier;
                    HealthSystem.Instance.TakeDamage(thing);
                    updateHealth(thing); //change health value so its changed now.
                    print("Enemy did " + thing + " damage to Player!");
                    print("Player is no longer guarding.");
                    isGuarding = false;
                }
                else
                {
                    HealthSystem.Instance.TakeDamage(20f);
                    print("Enemy did 20 damage to Player!");
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
        currentHealth = healthSystem.getHealth(); 
        newHealth = healthSystem.getHealth();

        print("Player is guarding!"); 

        isGuarding = true; 

        
        
        //CAM ADVICE NOTE FOR NEXT TIME IM HERE(anytime after 12/6/25): 
        //do ts in a reverse order. ie set isGuarding to true WITHOUT all this health stuff, and then change it so that when the enemy attacks,
        //it'll do 0 or reduced damage, instead of having this while loop. after allat, then you can disable isGuarding again!! - nimai 12/6/25

        //btw future me i took SAT today, so my brains a lil fried. what did i get. nimai 12/20/25: THE SAME THING :broken_heart: at least finals was fine
        
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
