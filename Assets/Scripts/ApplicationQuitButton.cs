using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationQuitButton : MonoBehaviour
{
    private Button _button = default;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
#if UNITY_EDITOR
        _button.onClick.AddListener(() => EditorApplication.ExitPlaymode());
#else
        _button.onClick.AddListener(() => Application.Quit());
#endif
    }
}
