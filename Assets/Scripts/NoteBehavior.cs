using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteBehavior : MonoBehaviour
{
    public AudioClip noteCollectedSFX;
    private bool noteCollected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name == "AlphaScene")
        {
            MainMenu.officeNoteCollected = true;
        }
        else if(SceneManager.GetActiveScene().name == "BetaScene")
        {
            MainMenu.parkNoteCollected = true;
        }
        else if(SceneManager.GetActiveScene().name == "Level3")
        {
            MainMenu.cityNoteCollected = true;
        }
        if(other.CompareTag("Player"))
        {
            if(!noteCollected)
            {
                AudioSource.PlayClipAtPoint(noteCollectedSFX, transform.position);
                noteCollected = true;
            }
            gameObject.GetComponent<Animator>().SetTrigger("noteCollected");
            Destroy(gameObject, 1);
        }
    }
}
