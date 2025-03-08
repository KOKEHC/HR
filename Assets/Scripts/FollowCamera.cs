using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ������ �� ������� ������ (������ ������ ������)
    public RectTransform hpBar; // ������ �� RectTransform HP bar

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (target != null && hpBar != null)
        {
            Vector3 targetPosition = mainCamera.WorldToScreenPoint(target.position);
            hpBar.position = targetPosition;
        }
    }
}
