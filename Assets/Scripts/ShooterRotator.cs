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
    [SerializeField] private RotateState state = RotateState.Idle;

    [Header("Information")]
    [SerializeField] private float verticalRotateSpeed = 0f;
    [SerializeField] private float horizontalRotateSpeed = 0f;

    private void Start()
    {
        ClearRoatate();
    }

    private void ClearRoatate()
    {
        state = RotateState.Idle;

        // TODO : 외부에서 데이터 값을 불러오는 곳
        verticalRotateSpeed = 360f;
        horizontalRotateSpeed = 360f;
    }   // 회전값 초기화 함수 , 김민섭_220606

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
