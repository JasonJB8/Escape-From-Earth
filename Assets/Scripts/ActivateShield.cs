using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    GameObject shield;
    bool shieldActive;
    public int shieldUptime = 3;
    public int shieldCooldown = 10;
    float shieldStartTime;
    float cooldownStartTime;
    void Start()
    {
        shield = transform.GetChild(1).gameObject;
        shieldActive = false;
        shieldStartTime = 0.0f;
        cooldownStartTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldActive)
        {
            shieldStartTime += Time.deltaTime;
            if(shieldStartTime >= shieldUptime)
            {
                shield.SetActive(false);
                shieldActive = false;
                shieldStartTime = 0.0f;
                cooldownStartTime = shieldCooldown;
            }
        }
        else if (cooldownStartTime > 0.0f)
        {
            cooldownStartTime -= Time.deltaTime;
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(cooldownStartTime <= 0.0f)
            {
                if(!shieldActive)
                {
                    shield.SetActive(true);
                    shieldActive = true;
                }
                else
                {
                    shield.SetActive(false);
                    shieldActive = false;
                }
            }
        }
    }
}
