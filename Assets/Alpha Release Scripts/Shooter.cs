using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
    }
}
