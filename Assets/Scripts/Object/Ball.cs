using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private LayerMask targetLayer = default;
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
        targetLayer = LayerMask.GetMask("Prop");
        maxDamage = 100f;
        explosionForce = 1000f;
        lifeTime = 10f;
        explosionRadius = 20f;

        explosionParticle = GetComponentInChildren<ParticleSystem>();
        explosionAudio = GetComponentInChildren<AudioSource>();
    }   // �� �ʱ�ȭ �Լ� , ��μ�_220606

    private void OnTriggerEnter(Collider other)
    {
        CheckArea();
        PlayEffect();
        DestroyBall();
    }

    private void CheckArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, targetLayer);
        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigid = colliders[i].GetComponent<Rigidbody>();
            targetRigid.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop target = colliders[i].GetComponent<Prop>();
            float damage = CalculateDamage(colliders[i].transform.position);
            target.TakeDamage(damage);
        }
    }   // ���� üũ �Լ� , ��μ�_220606

    private float CalculateDamage(Vector3 _targetPos)
    {
        // TODO : ���� ���� �Ÿ��� ����Ͽ� ������
        Vector3 explosionToTarget = _targetPos - transform.position;
        float distance = explosionToTarget.magnitude;
        float edgeToCenterDistance = explosionRadius - distance;
        float percentage = edgeToCenterDistance / explosionRadius;
        float damage = maxDamage * percentage;
        return Mathf.Max(0, damage);
    }   // ������ ��� �Լ� , ��μ�_220606

    private void DestroyBall(float _time = 0f) => Destroy(gameObject, _time);
    // �� �ı� �Լ� , ��μ�_220606

    private void OnDestroy()
    {
        GameManager.instance.OnBallDestroy();
    }

    private void PlayEffect()
    {
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        GameManager.instance.OnBallDestroy();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
    }   // ����Ʈ ���� �Լ� , ��μ�_220606
}
