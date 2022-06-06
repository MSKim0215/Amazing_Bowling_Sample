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

        explosionParticle = Resources.Load<ParticleSystem>("Prefabs/SmallExplosion");
    }   // ���� �ʱ�ȭ �Լ� , ��μ�_220606

    public void TakeDamage(float _value)
    {
        hp -= _value;

        if (hp <= 0)
        {
            ParticleSystem effect = Instantiate(explosionParticle, transform.position, transform.rotation);
            
            AudioSource sound = effect.GetComponent<AudioSource>();
            sound.Play();

            Destroy(effect.gameObject, effect.duration);
            gameObject.SetActive(false);
        }
    }   // ������ �Լ� , ��μ�_220606
}
