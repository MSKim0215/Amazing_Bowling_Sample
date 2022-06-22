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
        if(tag == "SmallProp")
        {
            score = 5;
            hp = 10f;

            explosionParticle = Resources.Load<ParticleSystem>("Prefabs/Effect/SmallExplosion");
        }
        else if(tag == "BigProp")
        {
            score = 30;
            hp = 60f;

            explosionParticle = Resources.Load<ParticleSystem>("Prefabs/Effect/BigExplosion");
        }
    }   // 프롭 초기화 함수 , 김민섭_220616

    public void TakeDamage(float _value)
    {
        hp -= _value;

        if (hp <= 0)
        {
            ParticleSystem effect = Instantiate(explosionParticle, transform.position, transform.rotation);
            
            AudioSource sound = effect.GetComponent<AudioSource>();
            sound.Play();

            GameManager.instance.AddScore(score);

            Destroy(effect.gameObject, effect.duration);
            gameObject.SetActive(false);
        }
    }   // 데미지 함수 , 김민섭_220606
}
