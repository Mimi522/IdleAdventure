using UnityEngine;
using UnityEngine.UI;

/*
-- Class responsible for updating the UI of an entity.
*/
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
