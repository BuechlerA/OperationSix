using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletholeBehaviour : FadeEffect
{
    [SerializeField]
    private Sprite[] sheet = new Sprite[8];
    
    public void SetSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sheet[Random.Range(1, 8)];
        StartCoroutine(Fade());
    }

}
