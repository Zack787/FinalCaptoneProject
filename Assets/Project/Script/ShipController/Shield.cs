using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] DamageHandler _damageHandlerShield;
    [SerializeField] Color _flashColor = Color.white;
    [SerializeField] [Range(0.25f, 1f)] private float _fadeOutTime = 0.5f;
    [SerializeField] float _minIntensity = -10f, _maxIntensity = 0f;
    

    Renderer _renderer;
    Color _baseColor;
    static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
   

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
      
        _baseColor = _renderer.material.color;

    }

    public void Init(int shieldStrength )
    {
        _damageHandlerShield.Init(shieldStrength);
        
    }

    void OnEnable()
    {
        _damageHandlerShield.HealthChanged.AddListener(OnHealthChanged);
        _damageHandlerShield.ObjectDestroyed.AddListener(DestroyShield);
    }

    void OnDisable()
    {
        _damageHandlerShield.HealthChanged.RemoveListener(OnHealthChanged);
        _damageHandlerShield.ObjectDestroyed.RemoveListener(DestroyShield);
    }

    void OnHealthChanged()
    {
        StartCoroutine(FlashAndFadeShields());
    }

    private void DestroyShield()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator FlashAndFadeShields()
    {
        Color shieldColor;
        Color emissionColor = _renderer.material.GetColor(EmissionColor);
        float currentTime = 0f;
        float intensity = _maxIntensity;
        while (currentTime < _fadeOutTime)
        {
            shieldColor = Color.Lerp(_flashColor, _baseColor, currentTime / _fadeOutTime);
            intensity = Mathf.Lerp(_maxIntensity, _minIntensity, currentTime / _fadeOutTime);
            ChangeShieldColor(shieldColor, emissionColor * Mathf.Pow(2, intensity));
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    private void ChangeShieldColor(Color shieldColor, Color emissionColor)
    {
        _renderer.material.color = shieldColor;
        _renderer.material.SetColor(EmissionColor, emissionColor);
    }
}
