using System.Collections;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttackBase
{
    private EnemyData data;
    private bool canAttack = true;
    private Transform player;
    public Transform firePoint;

    public override void SetData(EnemyData d)
    {
        data = d;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (!canAttack || player == null) return;

        Vector2 toPlayer = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle - 90);

        ExecuteAttack();
    }

    public override bool CanAttack => canAttack;

    public override void ExecuteAttack()
    {
        StartCoroutine(FireRoutine());
    }
    


    private IEnumerator FireRoutine()
    {
        canAttack = false;

        GameObject bullet = Instantiate(data.attackHitboxPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Initialize(
            (player.position - firePoint.position).normalized,
            7f, 3f, data.damage, data.targetTag
        );

        yield return new WaitForSeconds(data.attackCooldown);
        canAttack = true;
    }
}
