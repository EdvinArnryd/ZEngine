using UnityEngine;

public class DrawGizmoOnPosition : MonoBehaviour
{
    [SerializeField] private Color _color = Color.blue;
    [SerializeField] private float _size = 1f;

    void Start()
    {
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _size);
    }
}
