using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        // float step = speed * Time.deltaTime;
        // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.rigidbody);
    }
}
