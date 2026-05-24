using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Foots : MonoBehaviour
{
    [Header("Звук шагов")]
    public AudioClip AudioClip;
    public AudioSource AudioSource;

    [Header("Декали (следы)")]
    public GameObject decalProjectorPrefab; // Перетащи сюда префаб Decal Projector
    public LayerMask groundLayer;           // Слой земли/пола
    public float decalLifetime = 5f;        // Через сколько секунд след исчезнет
    public float footHeight = 0.2f;         // Высота, с которой бьём луч вниз
    public float heightOffset = 0.02f;      // Приподнять след, чтобы не мерцал (z-fighting)

    private Animator animator;

    void Start()
    {
        // Ищем Animator на этом же объекте или у родителя
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }

    // Этот метод вызывается из Animation Event
    public void OnFootStep()
    {
        // === ЗВУК ===
        if (AudioSource != null && AudioClip != null)
        {
            AudioSource.pitch = Random.Range(0.95f, 1.05f);
            AudioSource.PlayOneShot(AudioClip);
        }

        // === ДЕКАЛЬ (СЛЕД) ===
        if (decalProjectorPrefab != null && animator != null)
        {
            PlaceFootstepDecal();
        }
    }

    private void PlaceFootstepDecal()
    {
        // Берём кость правой стопы (она чаще используется для шагов)
        // Если нужно различать ноги, читай дальше в ответе
        Transform footBone = animator.GetBoneTransform(HumanBodyBones.RightFoot);

        if (footBone == null)
        {
            Debug.LogWarning("Не удалось найти кость стопы!");
            return;
        }

        // Бьём лучом вниз от стопы
        Vector3 rayOrigin = footBone.position + Vector3.up * footHeight;
        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, footHeight + 0.3f, groundLayer))
        {
            // Создаём проектор в точке попадания
            GameObject decal = Instantiate(decalProjectorPrefab, hit.point, Quaternion.identity);

            // Поворачиваем проектор по нормали поверхности
            decal.transform.rotation = Quaternion.LookRotation(-hit.normal);

            // Чуть приподнимаем, чтобы избежать мерцания
            decal.transform.position += hit.normal * heightOffset;

            // Уничтожаем след через время
            Destroy(decal, decalLifetime);
        }
    }
}