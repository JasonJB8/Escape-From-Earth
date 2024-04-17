using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGrenadeBehavior : MonoBehaviour
{
    public float grenadeFuseTime = 1;
    public float grenadeExplosionRadius = 4;
    public int damageAmount = 50;
    public ParticleSystem grenadeExplosionEffect;
    public AudioClip grenadeExplosionSFX;
    void Start()
    {
        Invoke("Explode", grenadeFuseTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        grenadeExplosionEffect.Play();
        AudioSource.PlayClipAtPoint(grenadeExplosionSFX, transform.position);
        Collider[] explosionColliders = Physics.OverlapSphere(transform.position, grenadeExplosionRadius);
        List<Transform> collidingObjects = new List<Transform>();
        foreach(Collider collider in explosionColliders)
        {
            Transform rootTransform = collider.transform.root;
            if(!collidingObjects.Contains(rootTransform))
            {
                collidingObjects.Add(rootTransform);
            }
        }
        foreach(Transform enemy in collidingObjects)
        {
            if(enemy.gameObject.CompareTag("Enemy"))
            {
                var enemyHealth = enemy.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);
            }
        }
        Destroy(gameObject, 2f);
    }
}
