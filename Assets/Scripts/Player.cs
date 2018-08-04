using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public GameObject[] seedPrefabs;
    public float throwSpeed;
    public float throwRandomness;
    public float throwRotation;
    public LayerMask groundLayer;

    [Header("References")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
	void FixedUpdate ()
	{
        Vector2 axisInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

	    bool isWalking = Mathf.Abs(axisInput.x) > .1f;
        animator.SetBool("isWalking", isWalking);

	    if (isWalking)
	    {
	        spriteRenderer.flipX = axisInput.x < 0;
	    }

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
                Seed seed = Instantiate(seedPrefabs[Random.Range(0,seedPrefabs.Length)], spawnPosition, Quaternion.identity)
                    .GetComponent<Seed>();
                int xDir = spriteRenderer.flipX ? -1 : 1;
                seed.GetComponent<Rigidbody2D>().velocity = (Random.insideUnitCircle* throwRandomness + Vector2.up * 2 + (Vector2)transform.right*xDir) * throwSpeed;
                seed.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-throwRotation, throwRotation);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jumpSpeed);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, .5f, groundLayer);
        animator.SetBool("isInAir", !hit);
        animator.SetFloat("AirBlend", _rigidbody2D.velocity.y);
    }
}
