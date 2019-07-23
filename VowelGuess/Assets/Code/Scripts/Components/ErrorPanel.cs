using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;

    public string PanelText
    {
        get => errorText.text;
        set => errorText.text = value;
    }

    private void Start()
    {
        if (errorText == null)
        {
            errorText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
