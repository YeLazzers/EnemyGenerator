using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private List<Spawn> _spawns;

    private ObjectPool<Enemy> _enemyPool;
    private BoxCollider _spawnArea;

    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();
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

        if (_spawns == null || _spawns.Count == 0)
        {
            Debug.LogError("Нет точек спавна");
            return; 
        }

        StartCoroutine(SpawnEnemies());
    }

    protected Enemy CreateFunc()
    {
        Enemy enemy = Instantiate(_prefab, transform);

        
        
        enemy.Initialize(GetRandomSpawnPosition(), GetRandomDirection(), _enemyPool);

        Debug.Log($"CreateFunc {enemy.GetInstanceID()}");
        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        Debug.Log($"ActionOnGet {enemy.GetInstanceID()}");
        enemy.Initialize(GetRandomSpawnPosition(), GetRandomDirection(), _enemyPool);
        enemy.gameObject.SetActive(true);
    }
    private void ActionOnRelease(Enemy enemy)
    {

        Debug.Log($"ActionOnRelease {enemy.GetInstanceID()}");
        enemy.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(Enemy enemy)
    {

        Debug.Log($"ActionOnDestroy {enemy.GetInstanceID()}");
        Destroy(enemy.gameObject);
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_spawnDelay);
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
