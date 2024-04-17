using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveNotificationManager : MonoBehaviour
{
    public Text objectiveText; // Assign in the inspector
    public AudioSource notificationSound; // Assign in the inspector
    public float displayDuration = 5.0f;

    //objective here is used to explain a new popup
    public void ShowObjective(string objective)
    {
        objectiveText.text = objective;
        objectiveText.gameObject.SetActive(true);
        notificationSound.Play();

        StartCoroutine(HideTextAfterDelay(displayDuration));
    }

    // make the text go away after a certain delay
    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectiveText.gameObject.SetActive(false);
    }
}
