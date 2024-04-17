using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBehavior : MonoBehaviour
{
    public AudioSource spaceshipEngine;

    private void Start()
    {
        if (spaceshipEngine == null)
        {
            spaceshipEngine = GetComponent<AudioSource>();
        }
        spaceshipEngine.playOnAwake = false;
        spaceshipEngine.loop = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupBehavior.pickupCount <= 0)
        {
            if (!spaceshipEngine.isPlaying)
            {
                spaceshipEngine.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && PickupBehavior.pickupCount <= 0 && !LevelManager.gameOver)
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spaceshipEngine.Stop();
        }
    }
}
