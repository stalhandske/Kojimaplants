﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public GameObject seedPrefab;
    public float throwSpeed;
    public float throwRotation;
    public LayerMask groundLayer;

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
	void FixedUpdate ()
	{
        Vector2 axisInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

	    float xVelocity = speed * axisInput.x;

        _rigidbody2D.velocity = new Vector2(xVelocity, _rigidbody2D.velocity.y);
	}

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Vector2 spawnPosition = transform.position + Vector3.up * 1.5f;
            if (!Physics2D.OverlapPoint(spawnPosition, groundLayer))
            {
                Seed seed = Instantiate(seedPrefab, spawnPosition, Quaternion.identity)
                    .GetComponent<Seed>();
                seed.GetComponent<Rigidbody2D>().velocity = (Random.insideUnitCircle + Vector2.up * 2) * throwSpeed;
                seed.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-throwRotation, throwRotation);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jumpSpeed);
        }
    }
}
