using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonePile : Clickable {
   
    Clickable livedClickable;
    SpriteRenderer pileSprite;
    
    public float updateInterval =1;
    private void Start() {
        pileSprite = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdateClickable", 1, updateInterval);
    }

    public override void UpdateClickable() {
        Color32 tmp = pileSprite.color;
        tmp.a -= 10;
        pileSprite.color = tmp;
        if (pileSprite.color.a <= 0.04f)
            Destroy(gameObject);
        if (isSelected) UIController.Instance.UpdateCLickableUI(name, 0, 0);
    }
}
