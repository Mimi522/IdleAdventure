using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public partial class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleEnded;

    public GameObject Arena;

    public SpawnLocationProvider[] SpawnPositions;

    private Entity _player;

    private List<Entity> _enemies;
    private List<Coroutine> _enemiesActions;

    private Coroutine _playerAction;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _enemies = new List<Entity>();
        _enemiesActions = new List<Coroutine>();
        _player = Arena.transform.Find("Player").GetComponent<Entity>();
    }

    public void StartBattle(GameObject[] enemiesPrefab)
    {
        Debug.Log("Battle in motion!");
        Arena.SetActive(true);
        SpawnEnemies(enemiesPrefab);
        _playerAction = StartCoroutine(PlayerAttack());

        for (int i = 0; i < _enemies.Count; i++) {
            _enemiesActions.Add(StartCoroutine(EnemyAttack(_enemies[i].EntityStats)));
        }
    }

    private IEnumerator PlayerAttack()
    {
        while (_enemies.Count > 0) {
            yield return new WaitForSeconds(_player.EntityStats.AttackCooldown);
            _enemies[0].TakeDamage(_player.EntityStats.DamageDealt);
            Debug.Log(string.Format("Player inflicts {0} damage on enemy!", _player.EntityStats.DamageDealt));
            CheckForEnemyKill();
        }

        OnBattleEnded?.Invoke();
        Debug.Log("Battle ended!");
        Arena.SetActive(false);
    }

    private IEnumerator EnemyAttack(EntityStats enemyStats)
    {
        while (_player.CurrentHp > 0) {
            yield return new WaitForSeconds(enemyStats.AttackCooldown);
            _player.TakeDamage(enemyStats.DamageDealt);
            Debug.Log(String.Format("Enemy inflicts {0} damage on player!", enemyStats.DamageDealt));
        }

        StopCoroutine(_playerAction);
        Destroy(Arena.transform.Find("Player").gameObject);
        Debug.Log("GAME OVER");
    }

    private void SpawnEnemies(GameObject[] enemiesPrefab)
    {
        List<Vector3> positions = SpawnPositions[enemiesPrefab.Length - 1].GetLocations();

        for (int i = 0; i < positions.Count; i++) {
            var go = Instantiate(enemiesPrefab[i], positions[i], Quaternion.identity);
            _enemies.Add(go.GetComponent<Entity>());
        }
    }

    private void CheckForEnemyKill()
    {
        if (_enemies[0].CurrentHp <= 0) {
            StopCoroutine(_enemiesActions[0]);
            _enemiesActions.RemoveAt(0);
            Debug.Log(_enemies[0].name + " killed!");
            _enemies[0].Dispose();
            _enemies.RemoveAt(0);
        }
    }
}