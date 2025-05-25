using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyData data;
    private bool canAttack = true;
    private Transform player;
    public Transform attackPoint;

    public void SetData(EnemyData d) => data = d;

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

        GameObject hitbox = Instantiate(data.attackHitboxPrefab, attackPoint.position, attackPoint.rotation);
        var melee = hitbox.GetComponent<MeleeHitbox>();
        melee.Initialize(data.damage, data.hitboxDuration, data.targetTag);

        yield return new WaitForSeconds(data.attackCooldown);
        canAttack = true;
    }
}