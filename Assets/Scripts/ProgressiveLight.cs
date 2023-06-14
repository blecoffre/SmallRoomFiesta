using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProgressiveLight : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float _minIntensity = 0.0f;
    [SerializeField] [Range(0, 100)] private float _maxIntensity = 10.0f;
    [SerializeField] private float _progressSpeed = 1.0f;

    [Inject] private ProgressBar _bar = default;

    private Light _light = default;
    private float _startingIntensity = 0.0f;
    private float _newIntensityValue = 0.0f;
    private bool _isLerping = false;
    private float _progress = 0.0f;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _light.intensity = _minIntensity;
    }

    void Start()
    {
        _bar.onValueChanged.AddListener(UpdateLightIntensity);
    }

    public void UpdateLightIntensity(float value)
    {
        _newIntensityValue = (_maxIntensity - _minIntensity) * (value / _bar.maxValue) + _minIntensity;

        if (_newIntensityValue != _light.intensity)
        {
            EnableLerp();
        }
    }

    private void Update()
    {
        if (_isLerping)
        {
            Lerp();
        }

        if (_light.intensity >= _maxIntensity || _bar.currentPercent >= _bar.maxValue) //Return to minimal intensity when we reach the max
        {
            _newIntensityValue = _minIntensity;
            EnableLerp();
        }
    }

    private void Lerp()
    {
        _progress += Time.deltaTime * _progressSpeed;
        _light.intensity = Mathf.Lerp(_startingIntensity, _newIntensityValue, _progress);
        if ((_newIntensityValue > _startingIntensity && _light.intensity >= _newIntensityValue) ||
            (_newIntensityValue < _startingIntensity && _light.intensity <= _newIntensityValue))
        {
            _isLerping = false;
            _light.intensity = _newIntensityValue;
        }
    }

    private void EnableLerp()
    {
        _startingIntensity = _light.intensity;
        _progress = 0.0f;
        _isLerping = true;
    }
}
