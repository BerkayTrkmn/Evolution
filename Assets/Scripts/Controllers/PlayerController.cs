using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour {
    public Target target;

    public List<Clickable> selectedClickables;
    public Clickable singleSelectClickable;
    public Type currentClickedType;

    public bool isMultiSelect = false;
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {


            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (isMultiSelect) {
                MultiSelect(hit, worldPoint);
            } else {
                SingleSelect(hit,worldPoint);
            }



        }
        if (Input.GetMouseButtonDown(1)) {
            AllTargetsCleared(selectedClickables);
           
        }
    }
    public void SingleSelect(RaycastHit2D hit, Vector2 worldPoint) {
        Clickable clickable;
        if (hit.collider != null) {
            Debug.Log("Hit " + hit.transform.gameObject.name);
            if (hit.transform.TryGetComponent<Clickable>(out clickable)) {

                if (clickable.isSelected) {
                    SingleTargetDeselect(clickable);
                } else {
                    SingleTargetDeselect(singleSelectClickable);
                    SingleTargetSelect(clickable);
                }

                Debug.Log("Type:" + clickable.GetType());
            } else {
                Debug.Log("nopz");
            }
        } else {
            if (singleSelectClickable != null) {
                MoveSingleAnimal(singleSelectClickable, worldPoint);

            }
            Debug.Log("No hit");
        }
    }
   public void MultiSelect(RaycastHit2D hit, Vector2 worldPoint) {

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
            if (clickable is IMovable) continue;
            ((IMovable)clickable).Move(clickable.gameObject,movePoint);
        }
    }
    public void MoveSingleAnimal(Clickable clickable, Vector3 movePoint) {
        //Debug.Log((clickable is IMovable) +" " + clickable.GetType());
        if (clickable is IMovable)
        ((IMovable)clickable).Move(clickable.gameObject, movePoint);
        
    }
    public void TargetSelect(Clickable clickable) {

        clickable.Selected(target);
        selectedClickables.Add(clickable);
    }
    public void SingleTargetSelect(Clickable clickable) {

        clickable.Selected(target);
        singleSelectClickable=clickable;
    }
    public void TargetDeselect(Clickable clickable) {

        clickable.Deselected();
        selectedClickables.Remove(clickable);
    }
    public void SingleTargetDeselect(Clickable clickable) {
        if (clickable == null) return;
        clickable.Deselected();
        selectedClickables = null;
    }
    public void AllTargetsCleared(List<Clickable> selectedClickables) {

        foreach (Clickable clickable in selectedClickables) {

            clickable.Deselected();
        }
        ClearSelectedClickables();
    }
    public void ClearSelectedClickables() => selectedClickables.Clear();


}
