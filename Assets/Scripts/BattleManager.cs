using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleEnded;
    public event Action<int> OnHealthChange;

    public GameObject Arena;

    public GameObject[] SpawnPositions;

    private List<Enemy> _enemies;
    private List<Coroutine> _enemiesActions;

    private EntityStats _playerStats;
    public EntityStats PlayerStats {
        get { return _playerStats; }
    }

    private Coroutine _playerAction;

    private int _playerCurrentHp;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _enemies = new List<Enemy>();
        _enemiesActions = new List<Coroutine>();
        _playerStats = Arena.transform.Find("Player").GetComponent<EntityStats>();
        _playerCurrentHp = _playerStats.Hp;
    }

    public void StartBattle(GameObject[] enemiesPrefab)
    {
        Debug.Log("Battle in motion!");
        Arena.SetActive(true);
        SpawnEnemies(enemiesPrefab);
        _playerAction = StartCoroutine(PlayerAttack());

        for (int i = 0; i < _enemies.Count; i++) {
            _enemiesActions.Add(StartCoroutine(EnemyAttack(_enemies[i].Stats)));
        }
    }

    private IEnumerator PlayerAttack()
    {
        while (_enemies.Count > 0) {
            yield return new WaitForSeconds(_playerStats.AttackCooldown);
            _enemies[0].CurrentHp -= _playerStats.DamageDealt;
            //OnHealthChange?.Invoke(_enemies[0].GameObject, _enemies[0].CurrentHp);
            Debug.Log(string.Format("Player inflicts {0} damage on enemy!", _playerStats.DamageDealt));
            CheckForEnemyKill();
        }

        OnBattleEnded?.Invoke();
        Debug.Log("Battle ended!");
        Arena.SetActive(false);
    }

    private IEnumerator EnemyAttack(EntityStats enemyStats)
    {
        while (_playerCurrentHp > 0) {
            yield return new WaitForSeconds(enemyStats.AttackCooldown);
            _playerCurrentHp -= enemyStats.DamageDealt;
            OnHealthChange?.Invoke(_playerCurrentHp);
            Debug.Log(String.Format("Enemy inflicts {0} damage on player!", enemyStats.DamageDealt));
        }

        StopCoroutine(_playerAction);
        Destroy(Arena.transform.Find("Player").gameObject);
        Debug.Log("GAME OVER");
    }

    private void SpawnEnemies(GameObject[] enemiesPrefab)
    {
        // Change to transform
        List<Transform> positions = SpawnPositions[enemiesPrefab.Length - 1].transform.Cast<Transform>().ToList();

        for (int i = 0; i < positions.Count; i++) {
            _enemies.Add(new Enemy(Instantiate(enemiesPrefab[i], positions[i].position, Quaternion.identity)));
        }
    }

    private void CheckForEnemyKill()
    {
        if (_enemies[0].CurrentHp <= 0) {
            StopCoroutine(_enemiesActions[0]);
            _enemiesActions.RemoveAt(0);
            Debug.Log(_enemies[0].GameObject.name + " killed!");
            _enemies[0].Dispose();
            _enemies.RemoveAt(0);
        }
    }

    private class Enemy
    {
        public GameObject GameObject;
        public EntityStats Stats;
        public int CurrentHp;

        public Enemy(GameObject enemyPrefab)
        {
            GameObject = enemyPrefab;
            Stats = GameObject.GetComponent<EntityStats>();
            CurrentHp = Stats.Hp;
        }

        public void Dispose()
        {
            Destroy(GameObject);
        }
    }
}