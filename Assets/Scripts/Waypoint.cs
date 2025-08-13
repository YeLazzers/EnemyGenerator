using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 size = GetComponent<BoxCollider>().size;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
