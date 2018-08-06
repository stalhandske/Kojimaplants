using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float throwSpeed;
    public float throwRandomness;
    public float throwRotationRandom;
    public float throwRotationMin;
    public LayerMask groundLayer;
    public float coyoteTime;
    public float rumpRegainTime;

    [Header("References")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Assets")]
    public GameObject[] seedPrefabs;
    public GameObject pufPrefab;

    Rigidbody2D _rigidbody2D;
    bool _hasJump;
    float _timeOfGrounded;
    float _timeOfJump;

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
                seed.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-throwRotationRandom, throwRotationRandom);
                seed.GetComponent<Rigidbody2D>().angularVelocity +=
                    Mathf.Sign(seed.GetComponent<Rigidbody2D>().angularVelocity) * throwRotationMin;
            }
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, .24f, groundLayer);

        if (hit)
        {
            _timeOfGrounded = Time.time;
        }

        animator.SetBool("isInAir", !hit);

        if (Time.time - _timeOfGrounded < coyoteTime && Time.time - _timeOfJump > rumpRegainTime)
        {
            _hasJump = true;
        }
        else
        {
            _hasJump = false;
        }

        if (Input.GetButtonDown("Fire1") && _hasJump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jumpSpeed);
            _timeOfJump = Time.time;
            Instantiate(pufPrefab, transform.position, Quaternion.identity);
        }

        animator.SetFloat("AirBlend", _rigidbody2D.velocity.y);

        if (transform.position.y < -40)
            Destroy(gameObject);

        
    }

    void OnDisable()
    {
        if (FindObjectsOfType<Player>().Length == 1)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
