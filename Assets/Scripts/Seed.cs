using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject plantPrefab;
    public float checkForPlantRadius;
    public LayerMask plantLayer;

    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D hit = Physics2D.OverlapCircle(col.contacts[0].point, checkForPlantRadius, plantLayer);
        if (col.collider.CompareTag("Ground") && col.contacts[0].normal.y > .5f && !hit)
        {
            Vector2 seedHitPosition = col.contacts[0].point;
            seedHitPosition.y = Mathf.Round(seedHitPosition.y);
            Instantiate(plantPrefab, seedHitPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
