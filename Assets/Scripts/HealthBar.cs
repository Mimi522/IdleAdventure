using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    private int _maxHp;
    private Image _healthBar;

    void OnEnable()
    {
        BattleManager battleManager = BattleManager.Instance;
        _maxHp = battleManager.PlayerStats.Hp;
        battleManager.OnHealthChange += UpdateBar;
    }

    void Awake()
    {
        _healthBar = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateBar(int value)
    {
        _healthBar.fillAmount = value / (float)_maxHp;
    }
}
