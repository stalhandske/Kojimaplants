using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject plantPrefab;
    public float checkForPlantRadius;
    public LayerMask plantLayer;
    public LayerMask groundLayer;
    public float disappearTime;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    float _createTime;

    void Awake()
    {
        _createTime = Time.time;
        
    }

    void Update()
    {
        if (Time.time - _createTime > disappearTime)
        {
            Destroy(gameObject);
        }

        if (Time.time - _createTime > disappearTime-.5f)
        {
            spriteRenderer.color = Time.frameCount % 2 == 0 ? Color.white : Color.clear;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 seedHitPosition = col.contacts[0].point;
        seedHitPosition.y = Mathf.Round(seedHitPosition.y);
        seedHitPosition.x = Mathf.Round(seedHitPosition.x+.5f)-.5f;

        Collider2D hit = Physics2D.OverlapCircle(seedHitPosition, checkForPlantRadius, plantLayer);
        if (col.collider.CompareTag("Ground") && col.contacts[0].normal.y > .5f && !hit)
        {
            
            Instantiate(plantPrefab, seedHitPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
