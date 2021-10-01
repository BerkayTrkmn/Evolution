using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
using System;

public class CameraMovement : MonoBehaviour {
    public float speed = 0.01f;

    public SpriteMapGenerator spriteMapGenerator;
    float width;
    float height;
    [Range(0.5f, 2f)]
    public float cameraFieldOfVision;

    [SerializeField] float maxMapWidth;
    [SerializeField] float minMapWidth;
    [SerializeField] float maxMapHeight;
    [SerializeField] float minMapHeight;

    void Start() {
        GetCameraSize();
        GetMapSize();
        TouchManager.Instance.onTouchBegan += OnTouchBegan;
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
        TouchManager.Instance.onTouchEnded += OnTouchEnded;
    }
    private void OnValidate() {
        GetComponent<Camera>().orthographicSize = cameraFieldOfVision;
        GetCameraSize();
    }
    public void GetCameraSize() {
        height = GetComponent<Camera>().orthographicSize * 2.0f;
        width = height * Screen.width / Screen.height;
    }
    private void OnTouchBegan(TouchInput touch) {
    }

    private void OnTouchMoved(TouchInput touch) {

        transform.position -= new Vector3(touch.DeltaScreenPosition.x * speed, touch.DeltaScreenPosition.y * speed, 0f);
        CamRestriction();
    }

    private void OnTouchEnded(TouchInput touch) {
    }

    public void GetMapSize() {
        float cameraHalfWidth = width / 2;
        float cameraHalfHeight = height / 2;
        float halfSpriteWidth = spriteMapGenerator.spriteWidth / 2;
        float halfSpriteHeight = spriteMapGenerator.spriteHeight / 2;
        maxMapWidth = spriteMapGenerator.MapWidth() / 2 - cameraHalfWidth-halfSpriteWidth;
        maxMapHeight = spriteMapGenerator.MapHeight() / 2 - cameraHalfHeight- halfSpriteHeight;
        minMapWidth = -spriteMapGenerator.MapWidth() / 2 + cameraHalfWidth- halfSpriteWidth;
        minMapHeight = -spriteMapGenerator.MapHeight() / 2 + cameraHalfHeight-halfSpriteHeight;
    }
    public void CamRestriction() {
        transform.position = new Vector3(
           Mathf.Clamp(transform.position.x, minMapWidth, maxMapWidth),
        Mathf.Clamp(transform.position.y, minMapHeight, maxMapHeight),
        transform.position.z);
    }

}
