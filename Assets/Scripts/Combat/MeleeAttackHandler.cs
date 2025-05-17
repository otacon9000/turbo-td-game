using System.Collections;
using UnityEngine;

public class MeleeAttackHandler : IAttackHandler
{
    private readonly Transform meleePoint;
    private readonly WeaponData data;
    private readonly MonoBehaviour context;

    private bool _canAttack = true;

    public bool CanAttack => _canAttack;

    public MeleeAttackHandler(Transform meleePoint, WeaponData data, MonoBehaviour context)
    {
        this.meleePoint = meleePoint;
        this.data = data;
        this.context = context;
    }

    public void Execute(Vector2 _)
    {
        if (!_canAttack || data.weaponType != WeaponType.Melee) return;

        GameObject hitboxGO = Object.Instantiate(data.meleeHitboxPrefab, meleePoint.position, meleePoint.rotation);
        MeleeHitbox melee = hitboxGO.GetComponent<MeleeHitbox>();
        melee.Initialize(data.damage, data.meleeDuration, data.targetTag);

        context.StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(data.cooldown);
        _canAttack = true;
    }
}