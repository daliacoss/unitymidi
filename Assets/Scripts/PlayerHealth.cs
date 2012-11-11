using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float healthRechargeDelay = 3f;
    public float healthRechargePerSecond = 30f;
    public LayerMask layerToTakeDamageFrom = 1 << 9;
    public float CurrentHealth;
    public Vector2 healthBarPosition = new Vector2(5, 5);
    public int healthBarThickness = 20;
    public float healthBarWidthScale = 1f;
    public Texture healthbarImage;

    private float lastHitTime = -1f;
    private bool recharging = false;
    private Rect healthBar;

    void Awake()
    {
        CurrentHealth = maxHealth;
        healthBar = new Rect(healthBarPosition.x, healthBarPosition.y, maxHealth, healthBarThickness);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.LogWarning("HIT!!!!");
        if ((( 1 << collisionInfo.gameObject.layer) & layerToTakeDamageFrom) != 0) {
            var damage = collisionInfo.gameObject.GetComponent<DamageDealer>();
            if (damage != null) {
                lastHitTime = Time.time;
                CurrentHealth = Math.Max(0f, CurrentHealth - damage.DealDamage());
                if (CurrentHealth == 0f) {
                    Application.LoadLevel("LevelSelect");
                }
                recharging = false;
            } else {
                Debug.LogError("Collided with Baddo, but Baddo not deal damage");
            }
        }
    }

    void Update()
    {
        if (!recharging) {
            if (Time.time - lastHitTime >= healthRechargeDelay) {
                recharging = true;
            }
        } else {
            if (CurrentHealth < maxHealth) {
                CurrentHealth = Math.Min(CurrentHealth + healthRechargePerSecond * Time.deltaTime, maxHealth);
            }
        }
    }

    void OnGUI()
    {
        healthBar.width = CurrentHealth * healthBarWidthScale;
        if (healthbarImage != null) {
            GUI.DrawTexture(healthBar, healthbarImage);
        } else {
            Debug.LogError("You should assign the player health component a healthbar image");
        }
    }
}
