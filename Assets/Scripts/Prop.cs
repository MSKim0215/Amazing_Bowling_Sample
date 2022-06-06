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
        // TODO : �ܺο��� ������ �� �ҷ����� ��
        score = 5;
        hp = 10f;

        explosionParticle = GetComponentInChildren<ParticleSystem>();
    }   // ���� �ʱ�ȭ �Լ� , ��μ�_220606
}
