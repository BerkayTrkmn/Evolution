using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Clickable {

    public float speed;
    Coroutine moveCoroutine;
    public void MoveTowardsToPoint(GameObject objectToMove, Vector3 endPoint) {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveOverSpeed(objectToMove, endPoint, speed));
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
        float timer = 0;
        float roadTime = RoadTime(transform.position, end);
        Debug.Log("RoadTİme: " + roadTime);
        while (objectToMove.transform.position != end && timer < roadTime) {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            Debug.Log("Timer: " + timer);
            timer += Time.deltaTime;
        }
    }

    public float RoadDistance(Vector3 startPoint, Vector3 endPoint) {

        return (endPoint - startPoint).magnitude;
    }
    public float RoadTime(Vector3 startPoint, Vector3 endPoint) {

        return RoadDistance(startPoint, endPoint) / speed;
    }

    public override void Selected(Target target) {

        base.Selected(target);
    }

    public override void Deselected() {
        base.Deselected();
    }
}
