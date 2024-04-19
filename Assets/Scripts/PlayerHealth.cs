using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    int currentHealth;
    public static bool isDead = false; //Get component instead of static variable?
    void Start()
    {
        isDead = false;
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if(!LevelManager.gameOver)
        {
            if(currentHealth > 0)
            {
                currentHealth -= damageAmount;
                healthSlider.value = currentHealth;
            }
            else if(currentHealth <= 0)
            {
                PlayerDies();
            }
        }
    }

    void PlayerDies()
    {
        isDead = true;
        transform.Rotate(-90, 0, 0, Space.Self);
        FindObjectOfType<LevelManager>().LevelLost();
    }
}
