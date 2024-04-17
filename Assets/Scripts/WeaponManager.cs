using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    // References for Shooter and Projectile components/scripts
    //private Shooter shooter;
    public GameObject projectilePrefab;
    public GameObject ampedProjectilePrefab;
    public GameObject rayGun; // The ray gun game object
    public GameObject reloadingRayGun;
    public int rayGunMagSize = 6;
    public static int rayGunCurrMagSize;
    public float projectileSpeed = 100;
    public Image reticleImage;
    public AudioClip gunFireSFX;
    public AudioClip emptyMagSFX;
    public AudioClip reloadSFX;
    //public GameObject grenadeLauncher; // The grenade launcher game object
    public GameObject grenadePrefab; // The grenade prefab to be instantiated when shooting
    public GameObject liveGrenadePrefab;
    public float grenadeSpeed = 10;
    public GameObject currProjectile;
    private GameObject activeWeapon;
    private GameObject inactiveWeapon;
    private bool hasGrenade = true; // Tracks if the player has picked up a grenade
    public static bool isReloading = false;
    private float reloadTime = 2.5f;
    public static float reloadTimer;

    void Start()
    {
        // Initially, the ray gun is active and the grenade launcher is not
        currProjectile = projectilePrefab;
        activeWeapon = rayGun;
        rayGunCurrMagSize = rayGunMagSize;
        reloadTimer = reloadTime;
        inactiveWeapon = grenadePrefab;
        activeWeapon.SetActive(true);
        inactiveWeapon.SetActive(false);
    }

    void Update()
    {
        if(!PauseMenuBehavior.isGamePaused)
        {
            reticleImage.enabled = true;
            if(!isReloading)
            {
                // Toggle weapon on 'Q' key press
                if (Input.GetKeyDown(KeyCode.Q) && InactiveWeaponReady())
                {
                    ToggleWeapon();
                }

                if (Input.GetKeyDown(KeyCode.R) && rayGunCurrMagSize < rayGunMagSize
                    && activeWeapon == rayGun)
                {
                    isReloading = true;
                    rayGun.SetActive(false);
                    reloadingRayGun.SetActive(true);
                    AudioSource.PlayClipAtPoint(reloadSFX, transform.position);
                }

                // Use the appropriate weapon
                if (Input.GetButtonDown("Fire1")) // Left mouse click
                {
                    if (activeWeapon == rayGun)
                    {
                        if (rayGunCurrMagSize > 0)
                        {
                            AudioSource.PlayClipAtPoint(gunFireSFX, transform.position);
                            GameObject projectile = Instantiate(currProjectile, transform.position + transform.forward, 
                                        transform.rotation) as GameObject; //explicit typecasting to GameObject
                            Rigidbody rb = projectile.GetComponent<Rigidbody>();
                            
                            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
                            
                            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
                            rayGunCurrMagSize--;
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(emptyMagSFX, transform.position);
                        }
                    }
                    else if (hasGrenade)
                    {
                        GameObject projectile = Instantiate(liveGrenadePrefab, transform.position + transform.forward, 
                                    transform.rotation) as GameObject; //explicit typecasting to GameObject
                        Rigidbody rb = projectile.GetComponent<Rigidbody>();
                        rb.isKinematic = false;
                        rb.AddForce(transform.forward * grenadeSpeed, ForceMode.VelocityChange);
                        
                        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
                        hasGrenade = false; // Consume the grenade
                        ToggleWeapon();
                    }
                }
            }
            else
            {
                reloadTimer -= Time.deltaTime;
                if(reloadTimer <= 0)
                {
                    isReloading = false;
                    reloadTimer = reloadTime;
                    rayGunCurrMagSize = rayGunMagSize;
                    rayGun.SetActive(true);
                    reloadingRayGun.SetActive(false);
                }
            }
        }
        else
        {
            reticleImage.enabled = false;
        }
    }

    void ToggleWeapon()
    {
        // Switch weapons and update the Shooter component reference
        if (activeWeapon == rayGun)
        {
            rayGun.SetActive(false);
            grenadePrefab.SetActive(true);
            activeWeapon = grenadePrefab;
            inactiveWeapon = rayGun;
            //shooter = grenadeLauncher.GetComponent<Shooter>();
        }
        else
        {
            rayGun.SetActive(true);
            grenadePrefab.SetActive(false);
            activeWeapon = rayGun;
            inactiveWeapon = grenadePrefab;
            //shooter = rayGun.GetComponent<Shooter>();
        }
    }

    private bool InactiveWeaponReady()
    {
        return inactiveWeapon == rayGun || inactiveWeapon == grenadePrefab && hasGrenade;
    }

    // Call this method to pick up a grenade
    public void PickupGrenade()
    {
        hasGrenade = true;
    }

    public void setAmpedProjectile()
    {
        currProjectile = ampedProjectilePrefab;
    }
}
