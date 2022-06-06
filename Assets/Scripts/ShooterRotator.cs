using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum RotateState
    {
        Idle, Vertical, Horizontal, Ready
    }
    [Header("Statement")]
    [SerializeField] private RotateState state;

    [Header("Information")]
    [SerializeField] private float verticalRotateSpeed = 360f;
    [SerializeField] private float horizontalRotateSpeed = 360f;

    private void Update()
    {
        switch (state)
        {
            case RotateState.Idle:
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        state = RotateState.Horizontal;
                    }
                }
                break;

            case RotateState.Horizontal:
                {
                    if (Input.GetButton("Fire1"))
                    {
                        transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                    }
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        state = RotateState.Vertical;
                    }
                }
                break;

            case RotateState.Vertical:
                {
                    if (Input.GetButton("Fire1"))
                    {
                        transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                    }
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        state = RotateState.Ready;
                    }
                }
                break;
        }
    }
}
