using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterCombat : MonoBehaviour
{
    public CharacterDisplay display;

    public float lightAttackCooldown = 0.5f;
    public float heavyAttackCooldown = 1.2f;
    public float specialCooldown = 5f;

    private float lightTimer = 0f;
    private float heavyTimer = 0f;
    private float[] specialTimers = new float[3];

    private CharacterMovement movement;

    void Start()
    {
        display = GetComponent<CharacterDisplay>();
        movement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (display.isActive)
        {
            HandlePlayerCombat();
        }
        else
        {
            HandleAICombat();
        }
    }

    void HandlePlayerCombat()
    {
        lightTimer -= Time.deltaTime;
        heavyTimer -= Time.deltaTime;
        for (int i = 0; i < 3; i++)
            specialTimers[i] -= Time.deltaTime;

        // Light Attack
        if (Input.GetButtonDown("Fire1") && lightTimer <= 0f)
        {
            LightAttack();
            lightTimer = lightAttackCooldown;
        }

        // Heavy Attack
        if (Input.GetButtonDown("Fire2") && heavyTimer <= 0f)
        {
            HeavyAttack();
            heavyTimer = heavyAttackCooldown;
        }

        // Special Abilities
        if (Input.GetKeyDown(KeyCode.Alpha1) && specialTimers[0] <= 0f)
        {
            UseSpecial(0);
            specialTimers[0] = specialCooldown;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && specialTimers[1] <= 0f)
        {
            UseSpecial(1);
            specialTimers[1] = specialCooldown;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && specialTimers[2] <= 0f)
        {
            UseSpecial(2);
            specialTimers[2] = specialCooldown;
        }
    }

    void HandleAICombat()
    {
        lightTimer -= Time.deltaTime;
        heavyTimer -= Time.deltaTime;

        Transform enemy = movement.currentEnemy;

        if (enemy == null) return;

        float distance = Vector3.Distance(transform.position, enemy.position);
        if (distance <= display.atkRange)
        {
            // Alternate attacks
            if (lightTimer <= 0f)
            {
                LightAttack();
                lightTimer = lightAttackCooldown;
            }
            else if (heavyTimer <= 0f)
            {
                HeavyAttack();
                heavyTimer = heavyAttackCooldown;
            }

            // Optional: Random chance for special
            for (int i = 0; i < 3; i++)
            {
                specialTimers[i] -= Time.deltaTime;
                if (Random.value < 0.01f && specialTimers[i] <= 0f)
                {
                    UseSpecial(i);
                    specialTimers[i] = specialCooldown;
                }
            }
        }
    }

    void LightAttack()
    {
        Debug.Log($"{gameObject.name} performs a Light Attack!");
        // Trigger animation or damage here
    }

    void HeavyAttack()
    {
        Debug.Log($"{gameObject.name} performs a Heavy Attack!");
        // Trigger animation or damage here
    }

    void UseSpecial(int index)
    {
        Debug.Log($"{gameObject.name} uses Special Ability {index + 1}!");
        // Add ability effect here
    }
}
