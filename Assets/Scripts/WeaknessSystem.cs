using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Fire,       // Attack 1: Ragnarok
    Wind,       // Attack 2: Hurricane
    Lightning,  // Attack 3: Wrath of Zeus
    Ice,        // Attack 4: Glacier
    None
}

public class WeaknessSystem : MonoBehaviour
{
    [Header("Damage Multipliers")]
    public float weaknessMultiplier = 2.0f; // Double damage
    public float resistanceMultiplier = 0.5f; // Half damage

    // Returns the calculated damage based on the enemy's elemental properties
    public float CalculateDamage(float baseDamage, Element attackElement, int battleIndex)
    {
        Element weakness = GetWeakness(battleIndex);
        Element resistance = GetResistance(battleIndex);

        if (attackElement == weakness)
        {
            Debug.Log("It's super effective!");
            return baseDamage * weaknessMultiplier;
        }
        else if (attackElement == resistance)
        {
            Debug.Log("It's not very effective...");
            return baseDamage * resistanceMultiplier;
        }
        
        return baseDamage; // Normal damage if neither weak nor resistant
    }

    // Define the elemental weakness per battle (0, 1, or 2)
    public Element GetWeakness(int battleIndex)
    {
        switch (battleIndex)
        {
            case 0: return Element.Ice;        // Battle 1 is weak to Glacier
            case 1: return Element.Wind;       // Battle 2 is weak to Hurricane
            case 2: return Element.Lightning;  // Battle 3 is weak to Wrath of Zeus
            default: return Element.None;
        }
    }

    // Define the elemental resistance per battle (0, 1, or 2)
    public Element GetResistance(int battleIndex)
    {
        switch (battleIndex)
        {
            case 0: return Element.Fire;       // Battle 1 resists Ragnarok
            case 1: return Element.Lightning;  // Battle 2 resists Wrath of Zeus
            case 2: return Element.Wind;       // Battle 3 resists Hurricane
            default: return Element.None;
        }
    }

    // Generates a scan readout summarizing the enemy's properties
    public string ScanEnemy(int battleIndex)
    {
        Element weak = GetWeakness(battleIndex);
        Element resist = GetResistance(battleIndex);

        return $"<color=#00FFFF>Scan Complete!</color>\nWeakness: <b>{weak}</b>\nResistance: <b>{resist}</b>";
    }
}
                    