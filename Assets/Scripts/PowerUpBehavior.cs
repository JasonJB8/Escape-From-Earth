using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{
    public AudioClip powerupSFX;
    public int scoreValue = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            LevelManager.score += scoreValue;
            AudioSource.PlayClipAtPoint(powerupSFX, transform.position);
            LevelManager.ActivatePowerUp();
            //gameObject.GetComponent<Animator>().SetTrigger("pickupCollected");
            Destroy(gameObject, 1f);
        }
    }
}
