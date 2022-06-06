using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private float maxDamage = 0f;
    [SerializeField] private float explosionForce = 0f;
    [SerializeField] private float lifeTime = 0f;
    [SerializeField] private float explosionRadius = 0f;

    private ParticleSystem explosionParticle;
    private AudioSource explosionAudio;

    private void Start()
    {
        ClearBall();
        DestroyBall(lifeTime);
    }

    private void ClearBall()
    {
        // TODO : �ܺο��� ������ �� �ҷ����� ��
        maxDamage = 100f;
        explosionForce = 1000f;
        lifeTime = 10f;
        explosionRadius = 20f;

        explosionParticle = GetComponentInChildren<ParticleSystem>();
        explosionAudio = GetComponentInChildren<AudioSource>();
    }   // �� �ʱ�ȭ �Լ� , ��μ�_220606

    private void OnTriggerEnter(Collider other)
    {
        PlayEffect();
        DestroyBall();
    }

    private void DestroyBall(float _time = 0f) => Destroy(gameObject, _time);   
    // �� �ı� �Լ� , ��μ�_220606

    private void PlayEffect()
    {
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
    }   // ����Ʈ ���� �Լ� , ��μ�_220606
}
