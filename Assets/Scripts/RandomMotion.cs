// Script for random motion of objects within bounds of parent object (e.g. a room)

using UnityEngine;
using System.Collections;

public class RandomMotion : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxDistance = 1.0f;
    public float minDistance = 0.0f;
    public float maxRotation = 1.0f;
    public float minRotation = 0.0f;
    public float maxRotationChange = 0.1f;
    public float minRotationChange = 0.0f;
    public float maxDistanceChange = 1.0f;
    public float minDistanceChange = 0.0f;
    public float maxSpeed = 0.01f;
    public float minSpeed = 0.0f;
    public float maxSpeedChange = 0.001f;
    public float minSpeedChange = 0.0f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float targetSpeed;
    private float targetRotationChange;
    private float targetDistanceChange;
    private float targetSpeedChange;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float startSpeed;

    private float distance;
    private float rotation;
    private float speedChange;
    private float distanceChange;
    private float rotationChange;

    private float time;
    private float timeChange;

    private float timeScale = 0.2f;

    private float timeScaleChange = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startSpeed = speed;

        targetPosition = startPosition;
        targetRotation = startRotation;
        targetSpeed = startSpeed;

        time = 0.0f;
        timeChange = 0.0f;

        
        rotation = 0.0f;
        speedChange = 0.0f;
        distanceChange = 0.0f;
        rotationChange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * timeScale;
        timeChange += Time.deltaTime * timeScaleChange;

        if (time > timeChange)
        {
            time = 0.0f;
            timeChange = Random.Range(minDistanceChange, maxDistanceChange);

            targetPosition = startPosition + Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
            targetRotation = startRotation * Quaternion.Euler(Random.insideUnitSphere * Random.Range(minRotation, maxRotation));
            targetSpeed = Random.Range(minSpeed, maxSpeed);
            targetRotationChange = Random.Range(minRotationChange, maxRotationChange);
            targetDistanceChange = Random.Range(minDistanceChange, maxDistanceChange);
            targetSpeedChange = Random.Range(minSpeedChange, maxSpeedChange);
        }

        distance = Mathf.Lerp(distance, 1.0f, Time.deltaTime * targetSpeed);
        rotation = Mathf.Lerp(rotation, 1.0f, Time.deltaTime * targetSpeed);
        speedChange = Mathf.Lerp(speedChange, 0.1f, Time.deltaTime * targetSpeed);
        distanceChange = Mathf.Lerp(distanceChange, 1.0f, Time.deltaTime * targetSpeed);
        rotationChange = Mathf.Lerp(rotationChange, 1.0f, Time.deltaTime * targetSpeed);

        transform.position = Vector3.Lerp(transform.position, targetPosition, distance);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotation);
        speed = Mathf.Lerp(speed, targetSpeed, speedChange);
        minDistanceChange = Mathf.Lerp(minDistanceChange, targetDistanceChange, distanceChange);
        maxDistanceChange = Mathf.Lerp(maxDistanceChange, targetDistanceChange, distanceChange);
        minRotationChange = Mathf.Lerp(minRotationChange, targetRotationChange, rotationChange);
        maxRotationChange = Mathf.Lerp(maxRotationChange, targetRotationChange, rotationChange);
    }
}