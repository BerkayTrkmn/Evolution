using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace HelloScripts
{
    [ExecuteAlways]
    public class CameraFollow : MonoBehaviour
    {
        public GameObject objectToFollow;

        public Vector3 offset;
        public Vector3 startingOffset;
        private bool isChanging = false;
        public float changingTime = 1f;

        public bool isSmoothCameraMovement = false;
        private Vector3 current_vel;
        public float moveSpeed = 2;
        

        void Start()
        {
            startingOffset = offset;

        }
        

        void LateUpdate()
        {
           
            if (!isSmoothCameraMovement)
            {
                if (!isChanging)
                    if (objectToFollow != null)
                        transform.position = objectToFollow.transform.position + offset;
            }
            else
            {
                if (!isChanging)
                    if (objectToFollow != null)
                    {
                        Vector3 target_pos = objectToFollow.transform.position + offset;
                        transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref current_vel, 1f / moveSpeed);
                    }
            }
            
        }


        public void ChangeFollowingObject(GameObject go)
        {
            isChanging = true;
            objectToFollow = go;
            gameObject.transform.DOMove(objectToFollow.transform.position + offset, changingTime).OnComplete(() => { isChanging = false; });

        }
        public void MoveCameraWithTime(Vector3 newOffset, Vector3 newRotation, float moveTime, Action endFunction)
        {
            offset = newOffset;
            isChanging = true;
            gameObject.transform.DORotate(newRotation, moveTime);
            gameObject.transform.DOMove(objectToFollow.transform.position + newOffset, moveTime).OnComplete(() => { endFunction.Invoke(); isChanging = false; });


        }
        public void ResetCamera(float resetTime)
        {
            offset = startingOffset;
            isChanging = true;
            gameObject.transform.DORotate(Vector3.right * 25, resetTime);
            gameObject.transform.DOMove(objectToFollow.transform.position + offset, resetTime).OnComplete(() => { isChanging = false; });
        }


    }
}