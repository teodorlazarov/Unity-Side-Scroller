using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public GameObject platform;
    public float movementSpeed;
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelect;

    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelect];
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * movementSpeed);

        if (platform.transform.position == currentPoint.position)
        {
            pointSelect++;

            if (pointSelect == points.Length)
            {
                pointSelect = 0;
            }
            currentPoint = points[pointSelect];
        }
    }
}
