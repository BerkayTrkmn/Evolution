using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour {
    public Target target;

    public List<Clickable> selectedClickables;

    public Type currentClickedType;
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {


            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            Clickable clickable;
            if (hit.collider != null) {
                Debug.Log("Hit " + hit.transform.gameObject.name);

                if (hit.transform.TryGetComponent<Clickable>(out clickable)) {

                    if (clickable.isSelected) {
                        TargetDeselect(clickable);
                    } else {
                        ClickedObjectSelected(clickable);
                    }
                   
                    Debug.Log("Type:" + clickable.GetType());
                } else {
                    Debug.Log("nopz");
                }
            } else {
                if (selectedClickables.Count != 0) {
                    MoveAnimals(selectedClickables, worldPoint);

                }
                Debug.Log("No hit");
            }


        }
        if (Input.GetMouseButtonDown(1)) {
            AllTargetsCleared(selectedClickables);
           
        }
    }

   
      public void ClickedObjectSelected(Clickable clickable) {

       Type type = clickable.GetType().BaseType;
        if(currentClickedType == null) {
            currentClickedType = type;
        } else
        if (currentClickedType != type ) {
            AllTargetsCleared(selectedClickables);
            currentClickedType = type;
        }
        TargetSelect(clickable);
    }
    public void MoveAnimals(List<Clickable> clickables, Vector3 movePoint) {
        
        foreach (Clickable clickable in clickables) {

            ((Animal)clickable).MoveTowardsToPoint(clickable.gameObject,movePoint);
        }
    }
    public void TargetSelect(Clickable clickable) {

        clickable.Selected(target);
        selectedClickables.Add(clickable);
    }

    public void TargetDeselect(Clickable clickable) {

        clickable.Deselected();
        selectedClickables.Remove(clickable);
    }
    public void AllTargetsCleared(List<Clickable> selectedClickables) {

        foreach (Clickable clickable in selectedClickables) {

            clickable.Deselected();
        }
        ClearSelectedClickables();
    }
    public void ClearSelectedClickables() => selectedClickables.Clear();


}
