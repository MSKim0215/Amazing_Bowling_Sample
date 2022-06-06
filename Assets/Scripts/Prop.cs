using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private int score = 0;
    [SerializeField] private float hp = 0f;

    private ParticleSystem explosionParticle;

    private void Start()
    {
        ClearProp();
    }

    private void ClearProp()
    {
        // TODO : 외부에서 데이터 값 불러오는 곳
        score = 5;
        hp = 10f;

        explosionParticle = GetComponentInChildren<ParticleSystem>();
    }   // 프롭 초기화 함수 , 김민섭_220606
}
