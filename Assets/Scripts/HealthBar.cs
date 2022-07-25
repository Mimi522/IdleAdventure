using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    private Image _healthBar;

    void Awake()
    {
        _healthBar = GetComponent<Image>();
    }

    public void UpdateBar(float value)
    {
        _healthBar.fillAmount = value;
    }
}
