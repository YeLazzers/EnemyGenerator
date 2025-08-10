using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Initialize(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        transform.LookAt(transform.position + direction);
    }

    private void Start()
    {
        StartCoroutine(MoveForward());
    }

    public IEnumerator MoveForward()
    {
        while(enabled)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

            yield return null;
        }
    }
}
