using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public Text powerupCooldownText; // Assign UI Text for the powerup cooldown in inspector
    public GameObject powerupReadyIndicator; // Assign a UI element to show powerup is ready

    private float powerupCooldown = 20f; // Cooldown lasts for 20 seconds
    private float currentCooldownTime;

    void Start()
    {
        powerupCooldownText.gameObject.SetActive(false); // Initially hide the cooldown timer
        powerupReadyIndicator.SetActive(true); // Initially, powerup is ready
    }

    void Update()
    {
        currentCooldownTime -= Time.deltaTime;
        if (currentCooldownTime <= 0)
        {
            currentCooldownTime = 0;
            powerupReadyIndicator.SetActive(true); // Show powerup is ready
            powerupCooldownText.gameObject.SetActive(false); // Hide the cooldown timer
        }
        else
        {
            powerupCooldownText.text = "Powerup Cooldown: " + currentCooldownTime.ToString("F1") + "s";
        }
    }

    // Call this method when a powerup is picked up
    public void PickupPowerup()
    {
        currentCooldownTime = powerupCooldown;
        powerupReadyIndicator.SetActive(false); // Hide powerup ready indicator
        powerupCooldownText.gameObject.SetActive(true); // Show the cooldown timer
    }
}
