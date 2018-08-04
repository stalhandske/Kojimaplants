using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject seedPrefab;
    public float throwSpeed;
    public float throwRotation;

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
	void FixedUpdate ()
	{
        Vector2 axisInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

	    float xVelocity = speed * axisInput.x * Time.deltaTime;

        _rigidbody2D.velocity = new Vector2(xVelocity, _rigidbody2D.velocity.y);
	}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Seed seed = Instantiate(seedPrefab, transform.position + Vector3.up*1.5f, Quaternion.identity)
                .GetComponent<Seed>();
            seed.GetComponent<Rigidbody2D>().velocity = (Random.insideUnitCircle + Vector2.up*2)* throwSpeed;
            seed.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-throwRotation, throwRotation);
        }
    }
}
