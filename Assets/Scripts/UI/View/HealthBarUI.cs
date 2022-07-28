using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for updating the UI of an entity's health bar.
/// </summary>
[RequireComponent(typeof(Image))]
public class HealthBarUI : MonoBehaviour
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
