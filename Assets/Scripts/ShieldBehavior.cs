using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldBehavior : MonoBehaviour
{
    public GameObject shieldGameObject; // Assign in inspector
    public TMP_Text shieldTimerText; // Assign UI Text for the shield timer in inspector
    public GameObject shieldReadyIndicator; // Assign a UI element to show shield is ready

    private bool isShieldActive = false;
    public float shieldDuration = 5f; // Shield lasts for 5 seconds
    public float shieldCooldown = 10f; // Cooldown lasts for 10 seconds

    private float currentShieldTime;
    private float currentCooldownTime;

    void Start()
    {
        shieldTimerText.gameObject.SetActive(false); // Initially hide the timer
        shieldReadyIndicator.SetActive(true); // Initially, shield is ready
    }

    void Update()
    {
        if (!PauseMenuBehavior.isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!isShieldActive)
                {
                    ActivateShield();
                }
                else
                {
                    DeactivateShield();
                }
                
            }

            if (isShieldActive)
            {
                currentShieldTime -= Time.deltaTime;
                shieldTimerText.text = "Shield Time: " + currentShieldTime.ToString("F1") + "s";
            }
            else
            {
                currentCooldownTime -= Time.deltaTime;
                if (currentCooldownTime <= 0)
                {
                    shieldReadyIndicator.SetActive(true); // Show shield is ready
                }
            }
        }
    }

    void ActivateShield()
    {
        if (!isShieldActive && currentCooldownTime <= 0)
        {
            shieldGameObject.SetActive(true);
            isShieldActive = true;
            currentShieldTime = shieldDuration;

            shieldTimerText.gameObject.SetActive(true); // Show the timer
            shieldReadyIndicator.SetActive(false); // Hide shield ready indicator

            Invoke("DeactivateShield", shieldDuration);
        }
    }

    void DeactivateShield()
    {
        shieldGameObject.SetActive(false);
        isShieldActive = false;
        currentCooldownTime = shieldCooldown;

        shieldTimerText.gameObject.SetActive(false); // Hide the timer

        // We will show the shield ready indicator again when cooldown finishes
    }
}
