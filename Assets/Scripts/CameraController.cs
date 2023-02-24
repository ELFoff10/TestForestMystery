using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;

    [SerializeField] private Transform m_Target; 

    [SerializeField] private float m_CameraOffsetZ; 

    private void FixedUpdate()
    {     
        m_Camera.transform.position = new Vector3(m_Target.position.x, m_Target.position.y, m_CameraOffsetZ);
    }
}
