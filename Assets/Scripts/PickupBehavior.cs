using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static int pickupCount = 0;
    public static int scoreValue = 1;
    public AudioClip coinCollectedSFX;
    void Start()
    {
        pickupCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.gameOver)
        {
            pickupCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LevelManager.score += scoreValue;
            AudioSource.PlayClipAtPoint(coinCollectedSFX, transform.position);
            //gameObject.GetComponent<Animator>().SetTrigger("pickupCollected");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(!LevelManager.gameOver)
        {
            pickupCount--;
            if(pickupCount <= 0)
            {
                FindObjectOfType<LevelManager>().LevelBeat();
            }
        }
    }
}
