//==============================================================
// HealthSystem
// HealthSystem.Instance.TakeDamage (float Damage);
// HealthSystem.Instance.HealDamage (float Heal);
// HealthSystem.Instance.UseMana (float Mana);
// HealthSystem.Instance.RestoreMana (float Mana);
// Attach to the Hero.
//==============================================================

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public class HealthSystem : MonoBehaviour
{
	
	public static HealthSystem Instance; 

	public Image currentHealthBar;
	public Image currentHealthGlobe;
	public Text healthText;
	public float hitPoint = 100f;
	public float maxHitPoint = 100f;
	public GameObject Enemy;
	public GameObject Player;

	public Image currentManaBar;
	public Image currentManaGlobe;
	public Text manaText;
	public float manaPoint = 100f;
	public float maxManaPoint = 100f;

	//==============================================================
	// Regenerate Health & Mana
	//==============================================================
	// public bool Regenerate = false; // Set to false to disable all auto-regeneration
	// public float regen = 0.1f;
	// private float timeleft = 0.0f;	// Left time for current interval
	// public float regenUpdateInterval = 1f;

	public bool GodMode;

	//==============================================================
	// Awake
	//==============================================================
	void Awake()
	{
		Instance = this;
	}
	
	//==============================================================
	// Start
	//==============================================================
  	void Start()
	{
		UpdateGraphics();
		Enemy.SetActive(true);
		Player.SetActive(true);
	}

	//==============================================================
	// Update
	//==============================================================
	void Update()
	{
		// if (Regenerate)
		// 	Regen();
	}

	//==============================================================
	// Regenerate Health & Mana
	//==============================================================
	/*
	private void Regen()
	{
		timeleft -= Time.deltaTime;

		if (timeleft <= 0.0) // Interval ended - update health & mana and start new interval
		{
			// Debug mode
			if (GodMode)
			{
				HealDamage(maxHitPoint);
				RestoreMana(maxManaPoint);
			}
			else
			{
				HealDamage(regen);
				RestoreMana(regen);				
			}

			UpdateGraphics();

			timeleft = regenUpdateInterval;
		}
	}
	*/

	//==============================================================
	// Getters to be implemented in other classes(demo mainly).
	//==============================================================

	public float getHealth()
	{
		return hitPoint;
	}

    //==============================================================
    // Health Logic
    //==============================================================
    private void UpdateHealthBar()
	{
		float ratio = hitPoint / maxHitPoint;
		currentHealthBar.rectTransform.localPosition = new Vector3(currentHealthBar.rectTransform.rect.width * ratio - currentHealthBar.rectTransform.rect.width, 0, 0);
		healthText.text = hitPoint.ToString ("0") + "/" + maxHitPoint.ToString ("0");
	}

	private void UpdateHealthGlobe()
	{
		float ratio = hitPoint / maxHitPoint;
		currentHealthGlobe.rectTransform.localPosition = new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);
		healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
	}

	public void TakeDamage(float Damage)
	{
		hitPoint -= Damage;
		if (hitPoint < 1)
			hitPoint = 0;

		UpdateGraphics();

		StartCoroutine(PlayerHurts());
	}

	public void HealDamage(float Heal)
	{
		hitPoint += Heal;
		if (hitPoint > maxHitPoint) 
			hitPoint = maxHitPoint;

		UpdateGraphics();
	}
	public void SetMaxHealth(float max)
	{
		maxHitPoint += (int)(maxHitPoint * max / 100);

		UpdateGraphics();
	}

	//==============================================================
	// Mana Logic
	//==============================================================
	private void UpdateManaBar()
	{
		float ratio = manaPoint / maxManaPoint;
		currentManaBar.rectTransform.localPosition = new Vector3(currentManaBar.rectTransform.rect.width * ratio - currentManaBar.rectTransform.rect.width, 0, 0);
		manaText.text = manaPoint.ToString ("0") + "/" + maxManaPoint.ToString ("0");
	}

	private void UpdateManaGlobe()
	{
		float ratio = manaPoint / maxManaPoint;
		currentManaGlobe.rectTransform.localPosition = new Vector3(0, currentManaGlobe.rectTransform.rect.height * ratio - currentManaGlobe.rectTransform.rect.height, 0);
		manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");
	}

	public void UseMana(float Mana)
	{
		manaPoint -= Mana;
		if (manaPoint < 1) // Mana is Zero!!
			manaPoint = 0;

		UpdateGraphics();

		// Trigger enemy hurt sequence so we process death and popups for the enemy
		StartCoroutine(EnemyHurts());
	}

	public void RestoreMana(float Mana)
	{
		manaPoint += Mana;
		if (manaPoint > maxManaPoint) 
			manaPoint = maxManaPoint;

		UpdateGraphics();
	}
	public void SetMaxMana(float max)
	{
		maxManaPoint += (int)(maxManaPoint * max / 100);
		
		UpdateGraphics();
	}

	//==============================================================
	// Update all Bars & Globes UI graphics
	//==============================================================
	private void UpdateGraphics()
	{
		UpdateHealthBar();
		UpdateHealthGlobe();
		UpdateManaBar();
		UpdateManaGlobe();
	}

	//==============================================================
	// Coroutine Player Hurts
	//==============================================================
	IEnumerator PlayerHurts()
	{
		// Player gets hurt. Do stuff.. play anim, sound..

		//PopupText.Instance.Popup("Ouch!", 1f, 1f); // Demo stuff!

		if (hitPoint < 1) // Health is Zero!!
		{
			yield return StartCoroutine(PlayerDied()); // Hero is Dead
		}

		else
			yield return null;
	}

	//========================
	// Coroutine Enemy Hurts
	//========================

    IEnumerator EnemyHurts()
    {
        // Enemy gets hurt. Do stuff.. play anim, sound..

        PopupText.Instance.Popup("Ouch!", 1f, 1f); // Demo stuff!

        if (manaPoint < 1) // Health is Zero!!
        {
            yield return StartCoroutine(EnemyDied()); // enemy is dead
        }

        else
            yield return null;
    }

    //==============================================================
    // Hero is dead
    //==============================================================
    IEnumerator PlayerDied()
	{
		// Player is dead. Safely check if PopupText exists so it doesn't break the Coroutine!
		if (PopupText.Instance != null)
		{
			PopupText.Instance.Popup("You have died!", 1f, 1f);
		}

        // Debug.Log("Loading Main Menu...");
        // Bulletproof delay loop using unscaled time
        float timer = 0f;
		while (timer < 2.0f)
		{
			timer += Time.unscaledDeltaTime;
			yield return null;
		}

		Debug.Log("Delay finished. Loading Main Menu...");
		
		// Load Main Menu scene
		SceneManager.LoadScene(0);
	}							

    //==============================================================
    // Enemy is dead
    //==============================================================
    IEnumerator EnemyDied()
    {
        // Enemy is dead. Safely check if PopupText exists!
		if (PopupText.Instance != null)
		{
        	PopupText.Instance.Popup("Enemy is dead!", 1f, 1f);
		}

        yield return null;
    }

}
