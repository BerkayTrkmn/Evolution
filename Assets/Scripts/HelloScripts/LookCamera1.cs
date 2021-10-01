using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelloScripts
{
    public class LookCamera : MonoBehaviour
    {

        Transform lookObject;

        private void Start()
        {
            lookObject = Camera.main.transform;
        }

        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - lookObject.position);
        }
    }
}
