using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public AudioClip gameOverSFX;
    public AudioClip gameWinSFX;
    public Text timerText;
    public Text gameText;
    public Text scoreText;
    public TMP_Text ammoText;
    public static bool gameOver = false;
    public static int score = 0;
    public static float levelDuration = 100.0f;
    public static float countdown;
    public static int playerBaseGunDamage = 10;
    public static int playerGunDamageAmount = playerBaseGunDamage;
    public static int powerupMultiplier = 2;
    public string nextLevel;
    public static bool isPoweredUp;
    void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 1) {
            levelDuration = 100.0f;
        }
        if(currentScene == 2) {
            levelDuration = 200.0f;
        }
        if(currentScene == 3) {
            levelDuration = 300.0f;
        }
        playerGunDamageAmount = playerBaseGunDamage;
        gameOver = false;
        countdown = levelDuration;
        score = 0;
        isPoweredUp = false;
        SetTimeText();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if(countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                countdown = 0.0f;
                LevelLost();
            }
            SetTimeText();
            SetScoreText();
            SetAmmoText();
        }
    }

    void SetTimeText()
    {
        timerText.text = countdown.ToString("f2"); 
    }

    void SetScoreText()
    {
        scoreText.text = score.ToString() 
        + "/" + PickupBehavior.pickupCount.ToString();
    }

    void SetAmmoText()
    {
        if(WeaponManager.isReloading)
        {
            ammoText.text = WeaponManager.reloadTimer.ToString("f2");
        }
        else
        {
            ammoText.text = WeaponManager.rayGunCurrMagSize.ToString();
        }
    }

    public void LevelLost()
    {
        gameOver = true;
        gameText.text = "MISSION FAILED!";
        gameText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        gameOver = true;
        gameText.text = "MISSION SUCCESSFUL!";
        gameText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(gameWinSFX, Camera.main.transform.position);
        if(!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public static void ActivatePowerUp()
    {
        isPoweredUp = true;
        playerGunDamageAmount *= powerupMultiplier;
    }
}
