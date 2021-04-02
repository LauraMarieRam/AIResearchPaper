using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//===========================================================================================
// This script controls the sprites which change based on the bunny's most recent experience
//===========================================================================================
public class SpriteController : MonoBehaviour
{
    public Sprite veryHappy;
    public Sprite happy;
    public Sprite sad;
    public Sprite verySad;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Bunny.spriteInt)
        {
            case 1:
                spriteRenderer.sprite = veryHappy;
                break;

            case 2:
                spriteRenderer.sprite = happy;
                break;

            case 3:
                spriteRenderer.sprite = sad;
                break;

            case 4:
                spriteRenderer.sprite = verySad;
                break;
        }
    }
}
