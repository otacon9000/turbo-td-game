using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject attackHitboxPrefab;
    public Transform attackPoint;
    public float attackCooldown = 1.5f;

    private bool canAttack = true;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (!canAttack || player == null) return;

        Vector2 toPlayer = (player.position - attackPoint.position).normalized;
        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        attackPoint.rotation = Quaternion.Euler(0, 0, angle - 90);

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;

        GameObject hitbox = Instantiate(attackHitboxPrefab, attackPoint.position, attackPoint.rotation);
        MeleeHitbox melee = hitbox.GetComponent<MeleeHitbox>();
        melee.Initialize(
            damage: 1,                // oppure passa valore se vuoi più flessibilità
            duration: 0.2f,
            targetTag: "Player"
        );

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}