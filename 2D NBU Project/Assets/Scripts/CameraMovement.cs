﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform cameraTarget;
    public float cameraSpeed;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private void FixedUpdate()
    {
        if(cameraTarget != null)
        {
            var newPos = Vector2.Lerp(transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);
            var vec3 = new Vector3(newPos.x, newPos.y, -10f);
            var clampX = Mathf.Clamp(vec3.x, minX, maxX);
            var clampY = Mathf.Clamp(vec3.y, minY, maxY);

            transform.position = new Vector3(clampX, clampY, -10f);

        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
