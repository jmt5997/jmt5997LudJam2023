using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public int ammo;
    // Start is called before the first frame update
    void Start()
    {
        ammo = 50;
        projectileSpeed = 45;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        // Rigidbody rb = clone.AddComponent<Rigidbody>();
        // rb.mass = 0.05f;
        var clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.Impulse);
        // rb.velocity = transform.TransformDirection(Vector3.forward * 45);
    }
}
