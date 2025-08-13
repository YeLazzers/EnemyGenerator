using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpawnSpot))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 1f;

    private ObjectPool<Enemy> _enemyPool;
    private WaitForSeconds _waitForSecondsDelay;
    private SpawnSpot _spawnSpot;
    
    private void Awake()
    {
        _spawnSpot = GetComponent<SpawnSpot>();
    }

    private void Start()
    {
        _enemyPool = new ObjectPool<Enemy>(
            CreateFunc,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy,
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 100
        );

        _waitForSecondsDelay = new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnEnemies());
    }

    protected Enemy CreateFunc()
    {
        Enemy enemy = _spawnSpot.SpawnEnemy();
        enemy.TargetReached += ActionOnRelease;
        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    private void ActionOnRelease(Enemy enemy)
    {
        enemy.transform.position = _spawnSpot.transform.position;
        enemy.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            yield return _waitForSecondsDelay;
            _enemyPool.Get();
        }
    }
}
