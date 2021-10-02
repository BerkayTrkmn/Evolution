using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFox : Animal {


    public override void Move(GameObject objectToMove, Vector3 endPoint) {
        base.Move(objectToMove, endPoint);
        Debug.Log("WhiteFox");
       
    }
}
