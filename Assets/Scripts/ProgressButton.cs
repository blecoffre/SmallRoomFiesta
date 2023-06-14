using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ProgressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private float _baseValueToAdd = 1.0f;
    [SerializeField] private float _maxValueCanBeAdded = 5.0f;
    [SerializeField] private float _increment = 1.0f;

    [Inject] private ProgressBar _bar = default;

    private Button _attachedButton = default;
    private bool _isDown = false;
    private float _currentValueToAdd = 0.0f;

    private void Start()
    {
        _bar.onMaxValueReached.AddListener(_ => _isDown = false);

        if(TryGetComponent(out Button button))
        {
            _attachedButton = button;
        }
        else
        {
            Debug.LogError($"No button attached to script { gameObject.GetType() } on object { gameObject.name }.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_attachedButton.enabled is true)
        {
            _currentValueToAdd = _baseValueToAdd;
            _isDown = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isDown = false;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDown = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (_isDown)
        {
            _currentValueToAdd += _increment * Time.deltaTime;

            if(_currentValueToAdd > _maxValueCanBeAdded)
            {
                _currentValueToAdd = _maxValueCanBeAdded;
            }

            _bar.AddToValue(_currentValueToAdd);
        }
    }
}
