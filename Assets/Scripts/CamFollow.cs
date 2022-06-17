using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        IDLE, READY, TRACKING
    }
    private State state
    {
        set
        {
            switch(value)
            {
                case State.IDLE: targetZoomSize = roundReadyZoomSize; break;
                case State.READY: targetZoomSize = readyShotZoomSize; break;
                case State.TRACKING: targetZoomSize = trackingZoomSize; break;
            }
        }
    }

    [SerializeField] private Transform target;

    private float smoothTime;       // 타겟을 쫓아가는 지연시간

    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize;

    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;

    private float lastZoomSpeed;

    private void Awake()
    {
        SetFollower();
    }

    private void SetFollower()
    {
        cam = GetComponentInChildren<Camera>();

        smoothTime = 0.2f;

        state = State.IDLE;

    }

    private void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref lastMovingVelocity, smoothTime);
        transform.position = smoothPosition;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.IDLE;
    }

    public void SetTarget(Transform _newTarget, State _newState)
    {
        target = _newTarget;
        state = _newState;
    }
}
