﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetSize {
    Mini,
    Small,
    Medium,
    Big,
    Huge,
    Gigantic
}
public abstract class Clickable : MonoBehaviour
{
    public int id;
    public new string name;
    public Collider2D col;
    public Rigidbody2D rb;
    public bool isSelected =false;
    public TargetSize size;
    public Target selectTarget;


    public virtual void Selected(Target target) {
        GameObject targetGO = Instantiate(target.gameObject, transform.position, Quaternion.identity, transform);
        Target currentTarget = targetGO.GetComponent<Target>();
        currentTarget.SetTarget(size);
        //rb.bodyType = RigidbodyType2D.Kinematic;
       
        isSelected = true;
        selectTarget = currentTarget;
    }
    public virtual void Deselected() {
        Destroy(selectTarget.gameObject);
       // rb.bodyType = RigidbodyType2D.Dynamic;
        isSelected = false;
    }
}
