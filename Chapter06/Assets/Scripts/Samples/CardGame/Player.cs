using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private int maxHealth = 20;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private int lowHealthThreshold = 7;

    [Header("Ability Parameters")]
    private int minHealAmount = 2;
    private int maxHealAmount = 5;

    private int minDamage = 2;
    private int maxDamage = 5;

    private bool isBuffed = false;

    public int CurrentHealth {
        get { return currentHealth; }
    }

    public bool IsBuffed {
        get { return isBuffed; }
    }

    public bool HasLowHealth {
        get { return currentHealth < lowHealthThreshold; }
    }

    private void Awake() {
        currentHealth = maxHealth;
    }

    public bool Buff() {
        isBuffed = true;
        return isBuffed;
    }

    public int Heal() {
        int healAmount = Random.Range(minHealAmount, maxHealAmount);
        currentHealth += healAmount;
        return currentHealth;
    }

    public int Damage() {
        int damageAmount = Random.Range(minDamage, maxDamage);
        if(isBuffed) {
            damageAmount /= 2;
            isBuffed = false;
        } 
        currentHealth -= damageAmount;
        return currentHealth;
    }
}
