using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> targetSprites;


    public void SetTarget(TargetSize size) {

        switch (size) {
            case TargetSize.Mini:
                spriteRenderer.sprite = targetSprites[0];
                break;
            case TargetSize.Small:
                spriteRenderer.sprite = targetSprites[1];
                break;
            case TargetSize.Medium:
                spriteRenderer.sprite = targetSprites[2];
                break;
            case TargetSize.Big:
                spriteRenderer.sprite = targetSprites[3];
                break;
            case TargetSize.Huge:
                spriteRenderer.sprite = targetSprites[4];
                break;
            case TargetSize.Gigantic:
                spriteRenderer.sprite = targetSprites[5];
                break;
            default:
                break;
        }


    }
}
