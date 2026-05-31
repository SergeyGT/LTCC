using UnityEngine;

public class BothFeetIK : MonoBehaviour
{
    [Header("Цели для ног (Target из Two Bone IK)")]
    public Transform leftFootTarget;
    public Transform rightFootTarget;

    [Header("Кости (для расчёта позиции)")]
    public Transform leftUpperLeg;   // бедро левой
    public Transform rightUpperLeg;  // бедро правой
    public Transform leftFoot;       // стопа левой (кость скелета)
    public Transform rightFoot;      // стопа правой (кость скелета)

    [Header("Настройки рейкаста")]
    [Tooltip("Слой, который считается полом")]
    public LayerMask groundLayer = ~0;
    [Tooltip("Смещение стопы над точкой попадания луча")]
    public float footOffsetY = 0.02f;
    [Tooltip("Длина луча от позиции стопы вверх и вниз")]
    public float rayLength = 1.5f;

    [Header("Сглаживание (плавность)")]
    [Range(0f, 1f)]
    public float positionWeight = 1f;  
    [Range(0f, 30f)]
    public float smoothSpeed = 20f;

    private void LateUpdate()
    {
        PlaceFoot(leftFoot, leftFootTarget, leftUpperLeg);
        PlaceFoot(rightFoot, rightFootTarget, rightUpperLeg);
    }

    private void PlaceFoot(Transform footBone, Transform target, Transform upperLeg)
    {
        Vector3 origin = footBone.position + Vector3.up * rayLength;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, rayLength * 2f, groundLayer))
        {
            Vector3 targetPosition = hit.point + Vector3.up * footOffsetY;

            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * footBone.rotation;

            if (positionWeight > 0f)
            {
                target.position = Vector3.Lerp(target.position, targetPosition, smoothSpeed * Time.deltaTime);
                target.rotation = Quaternion.Slerp(target.rotation, targetRotation, smoothSpeed * Time.deltaTime);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (leftFoot == null || rightFoot == null) return;

        Gizmos.color = Color.green;
        DrawFootRay(leftFoot);
        DrawFootRay(rightFoot);
    }

    private void DrawFootRay(Transform foot)
    {
        Vector3 origin = foot.position + Vector3.up * rayLength;
        Gizmos.DrawLine(origin, origin + Vector3.down * rayLength * 2f);
    }
}