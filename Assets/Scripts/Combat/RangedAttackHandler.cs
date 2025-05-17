using System.Collections;
using UnityEngine;

public class RangedAttackHandler : IAttackHandler
{
    private readonly Transform origin;
    private readonly WeaponData data;
    private readonly MonoBehaviour context;

    private bool _canAttack = true;

    public bool CanAttack => _canAttack;

    public RangedAttackHandler(Transform origin, WeaponData data, MonoBehaviour context)
    {
        this.origin = origin;
        this.data = data;
        this.context = context;
    }

    public void Execute(Vector2 targetPos)
    {
        if (!_canAttack || data.weaponType != WeaponType.Ranged) return;

        Vector2 direction = (targetPos - (Vector2)origin.position).normalized;
        Vector2 spawnPos = (Vector2)origin.position + direction * data.projectileSpawnOffset;

        GameObject bulletGO = Object.Instantiate(data.projectilePrefab, spawnPos, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Initialize(direction, data.projectileSpeed, data.projectileLifetime, data.damage, data.targetTag);

        context.StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(data.cooldown);
        _canAttack = true;
    }
}