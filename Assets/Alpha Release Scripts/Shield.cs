using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shieldGameObject; // Assign in inspector
    private bool isShieldActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ActivateShield();
        }
    }

    void ActivateShield()
    {
        if (!isShieldActive)
        {
            shieldGameObject.SetActive(true);
            isShieldActive = true;
            Invoke("DeactivateShield", 5f); // Shield lasts for 5 seconds
        }
    }

    void DeactivateShield()
    {
        shieldGameObject.SetActive(false);
        isShieldActive = false;
    }
}
