﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AIProjectile : MonoBehaviour
{
    [SerializeField]
    [Range(5000f, 25000f)]
    float _launchForce = 10000f;
    [SerializeField] [Range(10, 1000)] int _damage = 100;
    [SerializeField] [Range(2f, 10f)] float _range = 5f;
    [SerializeField] AudioClip _impactSound;
    //[SerializeField] private Detonator _hitEffect;
    AudioSource _audioSource;

    bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    Rigidbody _rigidBody;
    float _duration;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());
    }

    void OnEnable()
    {
        _rigidBody.AddForce(_launchForce * transform.forward);
        _duration = _range;
    }
    private void OnDisable()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }
    void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }
    public void Init(int launchForce, int damage, float range, Vector3 velocity, Vector3 angularVelocity)
    {
        //Debug.Log($"Projectile({launchForce}, {damage}, {range}");
        _launchForce = launchForce;
        _damage = damage;
        _range = range;
        _rigidBody.velocity = velocity;
        _rigidBody.angularVelocity = angularVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_impactSound) _audioSource.PlayOneShot(_impactSound);
        IDamageable damageable = collision.collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Vector3 hitPosition = collision.GetContact(0).point;
            damageable.TakeDamage(_damage, hitPosition);
        }

       /* if (_hitEffect != null)
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);*/
    }
}