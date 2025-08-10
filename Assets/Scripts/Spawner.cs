using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private List<SpawnSpot> _spawns;

    private ObjectPool<Enemy> _enemyPool;
    private WaitForSeconds _wfsDelay;

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

        _wfsDelay = new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnEnemies());
    }

    protected Enemy CreateFunc()
    {
        Enemy enemy = Instantiate(_prefab, transform);

        enemy.Initialize(GetRandomSpawnPosition(), GetRandomDirection());

        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.Initialize(GetRandomSpawnPosition(), GetRandomDirection());
        enemy.gameObject.SetActive(true);
    }
    private void ActionOnRelease(Enemy enemy)
    {
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
            yield return _wfsDelay;
            _enemyPool.Get();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int spawnNumber = Random.Range(0, _spawns.Count);

        return _spawns[spawnNumber].transform.position;
    }

    private Vector3 GetRandomDirection()
    {
        float xDirection = Random.Range(-1f, 1f);
        float zDirection = Random.Range(-1f, 1f);
        return new Vector3(xDirection, 0, zDirection);
    }

    private void OnDrawGizmos()
    {
        Vector3 size = GetComponent<BoxCollider>().size;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
