using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls the battle between player and monster in a monster
/// camp tile.
/// </summary>
public partial class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleEnded;
    public event Action OnPlayerDeath;

    [SerializeField] private GameObject _arena;
    [SerializeField] private SpawnLocationProvider[] _spawnPositions;
    [SerializeField] private DamagePopUp _damageCanvas;

    [SerializeField] private float _delayAfterBattle = 2;

    [SerializeField] private Player _player;
    public Player Player {
        get { return _player; }
    }

    private List<Entity> _enemies = new List<Entity>();

    void OnValidate()
    {
        if (_arena == null) {
            Debug.LogError("Arena not set.");
        }

        if (_spawnPositions == null) {
            Debug.LogError("Spawn positions not set.");
        }

        if (_damageCanvas == null) {
            Debug.LogError("Damage canvas not set.");
        }

        if (_player == null) {
            Debug.LogError("Player not set.");
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void OnEnable()
    {
        _player.OnAttack += PlayerAttack;
        _player.OnDeath += PlayerDeath;
    }

    void OnDisable()
    {
        _player.OnAttack -= PlayerAttack;
        _player.OnDeath -= PlayerDeath;
    }

    public void StartBattle(GameObject[] enemiesPrefab)
    {
        Debug.Log("Battle in motion!");
        _arena.SetActive(true);

        _player.StartAttacking();

        SpawnEnemies(enemiesPrefab);

        foreach (Entity enemy in _enemies) {
            enemy.StartAttacking();
        }
    }

    private void PlayerAttack(DamageInfo damageInfo)
    {
        if (_enemies.Count > 0) {
            _damageCanvas.ShowDamage(_enemies[0].transform.position, damageInfo.Damage);
            _enemies[0].TakeDamage(damageInfo.Damage);
            Debug.Log(string.Format("Player inflicts {0} damage on enemy!", damageInfo.Damage));
        }
    }

    private void EnemyAttack(DamageInfo damageInfo)
    {
        if (_player.CurrentHp > 0) {
            _damageCanvas.ShowDamage(_player.transform.position, damageInfo.Damage);
            _player.TakeDamage(damageInfo.Damage);
            Debug.Log(String.Format("Enemy inflicts {0} damage on player!", damageInfo.Damage));
        }
    }

    private void SpawnEnemies(GameObject[] enemiesPrefab)
    {
        if (enemiesPrefab.Length > _spawnPositions.Length) {
            Debug.LogError("Number of enemies not supported.");
            return;
        }

        // Enemies positions varies accord to the amount of enmies in battle
        List<Vector3> positions = _spawnPositions[enemiesPrefab.Length - 1].GetLocations();

        for (int i = 0; i < positions.Count; i++) {
            GameObject enemy = Instantiate(enemiesPrefab[i], positions[i], Quaternion.identity, transform);
            _enemies.Add(enemy.GetComponent<Entity>());
            _enemies[i].OnAttack += EnemyAttack;
            _enemies[i].OnDeath += EnemyDeath;
        }
    }

    private void EnemyDeath(Entity enemy)
    {
        Debug.Log(enemy.name + " killed!");
        enemy.Dispose();
        _enemies.RemoveAt(0);

        if (_enemies.Count == 0) {
            StartCoroutine(EndBattleWithDelay(_delayAfterBattle));
        }
    }

    private void PlayerDeath(Entity player)
    {
        player.Dispose();
        OnPlayerDeath?.Invoke();
        Debug.Log("GAME OVER");
    }

    private IEnumerator EndBattleWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        OnBattleEnded?.Invoke();
        Debug.Log("Battle ended!");
        _arena.SetActive(false);
    }
}
