using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesCity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckNoteCollected()
    {
        this.gameObject.SetActive(MainMenu.cityNoteCollected);
    }
}
