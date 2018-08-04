using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    [Header("Assets")]
    public GameObject plantPrefab;
    public GameObject flowerPrefab;

    [Header("Parameters")]
    public float growTime;
    public float growTimeRandomness;
    public LayerMask noGrowLayer;
    public float bloomTime;
    public float bloomTimeRandomness;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    float _lastGrowTime = 0;
    float _createTime = 0;
    float _actualGrowTime;
    float _actualBloomTime;

    void Awake()
    {
        _lastGrowTime = Time.time;
        _actualGrowTime = growTime + Random.Range(-growTimeRandomness, growTimeRandomness);

        _createTime = Time.time;
        _actualBloomTime = bloomTime + Random.Range(-bloomTimeRandomness, bloomTimeRandomness);
    }

    void Update()
    {
        if (Time.time - _lastGrowTime > _actualGrowTime)
        {
            Grow();
        }

        if (Time.time - _createTime > _actualBloomTime)
        {
            GameObject flower = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Grow()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position + Vector3.up, noGrowLayer);
        if (hit)
        {
            _lastGrowTime = Time.time;
            _actualGrowTime = growTime + Random.Range(-growTimeRandomness, growTimeRandomness);
            return;
        }

        transform.position += Vector3.up;
        GameObject plant = Instantiate(plantPrefab, transform.position + Vector3.down, Quaternion.identity);
        _lastGrowTime = Time.time;
        _actualGrowTime = growTime + Random.Range(-growTimeRandomness, growTimeRandomness);
    }
}
