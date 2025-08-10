using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public UnityAction OnLeftMousePressed;
    public Vector3 MousePosition => Input.mousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMousePressed?.Invoke();
        }
    }
}
