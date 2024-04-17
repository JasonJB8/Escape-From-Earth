using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public int currentHealth;
    public AudioClip deathSFX;

    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if(currentHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(LevelManager.playerGunDamageAmount);
        }
    }

}
