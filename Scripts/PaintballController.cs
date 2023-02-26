using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballController : MonoBehaviour
{
    public GameObject paintball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        Destroy(paintball, 3);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Debug.Log(collision.gameObject.tag);
        Destroy(paintball);
    }
}
