using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _spawnDelay = 2f;

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

        StartCoroutine(SpawnEnemies());
    }

    protected Enemy CreateFunc()
    {
        Enemy enemy = Instantiate(_prefab, transform);

        
        
        enemy.Initialize(GetRandomPosition(), GetRandomDirection(), _enemyPool);

        Debug.Log($"CreateFunc {enemy.GetInstanceID()}");
        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        Debug.Log($"ActionOnGet {enemy.GetInstanceID()}");
        enemy.Initialize(GetRandomPosition(), GetRandomDirection(), _enemyPool);
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

    private Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(_spawnArea.size.x / 2 * -1, _spawnArea.size.x / 2);
        float zPosition = Random.Range(_spawnArea.size.z / 2 * -1, _spawnArea.size.z / 2);
        return new Vector3(transform.position.x + xPosition, transform.position.y, transform.position.z + zPosition);
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
