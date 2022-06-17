using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    private Transform firePos;      // 발사위치

    private float minForce = 15f;
    private float maxForce = 30f;
    private float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool isFire;
    
    // Physic
    private Rigidbody ball;

    // UI
    private Slider powerSlider;

    // Audio
    private AudioSource shootingAudio;
    private AudioClip fireClip;
    private AudioClip chargingClip;

    private void Awake()
    {
        SetShooter();
    }

    private void SetShooter()
    {
        firePos = transform.Find("FirePos");
        ball = Resources.Load<Rigidbody>("Prefabs/Ball");
        powerSlider = transform.Find("Canvas/Power Slider").GetComponent<Slider>();
        shootingAudio = GetComponent<AudioSource>();
        fireClip = Resources.Load<AudioClip>("Sounds/Sfx/Shoot_00");
        chargingClip = Resources.Load<AudioClip>("Sounds/Sfx/Jingle_Achievement_01");
    }

    private void OnEnable()
    {
        ClearShooter();
    }

    private void ClearShooter()
    {
        // TODO : 외부 데이터 받아와서 능력치 세팅
        minForce = 15f;
        maxForce = 30f;
        chargingTime = 0.75f;
        chargeSpeed = (maxForce - minForce) / chargingTime;

        powerSlider.minValue = minForce;
        powerSlider.maxValue = maxForce;

        currentForce = minForce;
        powerSlider.value = minForce;
        isFire = false;
    }

    private void Update()
    {
        powerSlider.value = minForce;

        if(!isFire && currentForce >= maxForce)
        {
            currentForce = maxForce;
            Fire();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            currentForce = minForce;

            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if(!isFire && Input.GetButton("Fire1"))
        {
            currentForce = currentForce + chargeSpeed * Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if(!isFire && Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        isFire = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);
        ballInstance.velocity = currentForce * firePos.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;
    }   // 공 발사 함수 , 김민섭_220616
}
