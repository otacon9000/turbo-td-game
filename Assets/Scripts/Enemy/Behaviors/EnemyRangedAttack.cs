using System.Collections;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttackBase
{
    private bool canAttack = true;
    private Transform player;

    [Header("Attack Config")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackCooldown = 2f;
    public int burstCount = 3;
    public float timeBetweenBurstShots = 0.2f;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public int bulletDamage = 1;
    public float spreadAngle = 15f;
    public string targetTag = "Player";

    public override bool CanAttack => canAttack;

    public override void SetData(EnemyData d)
    {
        // opzionale
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        // Ruota solo il firePoint verso il player
        Vector2 toPlayer = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public override void ExecuteAttack()
    {
        if (canAttack)
            StartCoroutine(FireBurst());
    }

    private IEnumerator FireBurst()
    {
        canAttack = false;

        for (int i = 0; i < burstCount; i++)
        {
            Vector2 baseDirection = (player.position - firePoint.position).normalized;
            float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Vector2 direction = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(direction, bulletSpeed, bulletLifetime, bulletDamage, targetTag);

            yield return new WaitForSeconds(timeBetweenBurstShots);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}