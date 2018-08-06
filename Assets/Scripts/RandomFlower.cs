using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlower : Flower
{

    public Sprite[] possibleSprites;

    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
    }
}
