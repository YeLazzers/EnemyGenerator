using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputReader))]
public class RayCaster : MonoBehaviour
{
    private InputReader _inputReader;

    public UnityAction<Enemy> OnCubeHitted;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.OnLeftMousePressed += RaycastToMousePoint;
    }

    private void OnDisable()
    {
        _inputReader.OnLeftMousePressed -= RaycastToMousePoint;
    }

    private void RaycastToMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(_inputReader.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out Enemy cube))
            {
                Debug.Log("Clicked: " + raycastHit.collider.name);
                OnCubeHitted?.Invoke(cube);
            }
        }
    }
}
