using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 0f;
    [SerializeField] private float _explosionForce = 0f;
    [SerializeField] private ParticleSystem _effect;

    public void Explode(Transform objectTransform)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(objectTransform))
        {
            explodableObject.AddExplosionForce(_explosionForce, objectTransform.position, _explosionRadius);
        }
        Instantiate(_effect, objectTransform.position, objectTransform.rotation, transform);
    }

    private List<Rigidbody> GetExplodableObjects(Transform objectTransform)
    {
        Collider[] hits = Physics.OverlapSphere(objectTransform.position, _explosionRadius);

        List<Rigidbody> explodableObjects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                explodableObjects.Add(hit.attachedRigidbody);

        return explodableObjects;
    }
}
