using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class MageMovement : MonoBehaviour
{
    private int attackCoolDown = 3;
    [SerializeField] GameObject mageProjectile;
    [SerializeField] GameObject player;
    [SerializeField] int movementSpeed = 2;
    private PlayerStats playerStats;
    private Vector3 startPos;
    private float speed = 50.0f;

    private void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MageSpellRoutine());
        StartCoroutine(MageMovementRoutine());
    }
    IEnumerator MageSpellRoutine()
    {
        while (true)
        {
            // Null kontrolleri
            if (player == null || mageProjectile == null)
            {
                Debug.LogWarning("Mage: Player veya MageProjectile atanmadı.");
                yield break; // Coroutine'i güvenli şekilde durdur
            }

            // Güncel mage pozisyonu ve yön
            Vector3 magePosition = transform.position;
            Vector3 directionToPlayer =  player.transform.position - magePosition;

            // Eğer yön vektörü sıfırsa (aynı konumdalar), hata çıkarabilir — bunu da engelle
            if (directionToPlayer == Vector3.zero)
            {
                directionToPlayer = transform.forward; // fallback yön
            }

            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            Instantiate(mageProjectile, magePosition, lookRotation);

            yield return new WaitForSeconds(attackCoolDown);
        }
    }

    IEnumerator MageMovementRoutine()
    {
        while (true)
        {
            yield return MoveForDuration(Vector3.forward, 2f);
            yield return MoveForDuration(Vector3.back, 4f);
            yield return MoveForDuration(Vector3.forward, 2f);
            yield return MoveForDuration(Vector3.right, 2f);
            yield return MoveForDuration(Vector3.left, 4f);
        }
    }

    IEnumerator MoveForDuration(Vector3 direction, float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            transform.position += direction * movementSpeed * Time.deltaTime;
            yield return null;
        }
    }


}
