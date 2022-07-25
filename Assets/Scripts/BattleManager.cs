using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleEnded;

    public GameObject Arena;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void StartBattle(GameObject[] enemies)
    {
        Debug.Log("Battle in motion!");
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        Arena.SetActive(true);
        yield return new WaitForSeconds(2);
        OnBattleEnded?.Invoke();
        Debug.Log("Battle ended!");
        Arena.SetActive(false);
    }
}