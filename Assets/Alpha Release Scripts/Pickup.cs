using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(1); // Assuming you have a ScoreManager script
            Destroy(gameObject); // Destroy the pickup
        }
    }
}
