using UnityEngine;
using NaughtyAttributes;

public enum WeaponType
{
    Ranged,
    Melee
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;

    [Header("Shared")]
    public float cooldown = 0.5f;
    public int damage = 1;
    public string targetTag = "Enemy";

    [Header("Ranged")]
    [ShowIf("weaponType", WeaponType.Ranged)]
    public GameObject projectilePrefab;
    [ShowIf("weaponType", WeaponType.Ranged)]
    public float projectileSpawnOffset = 0.5f;
    [ShowIf("weaponType", WeaponType.Ranged)]
    public float projectileSpeed = 10f;
    [ShowIf("weaponType", WeaponType.Ranged)]
    public float projectileLifetime = 2f;

    [Header("Melee")]
    [ShowIf("weaponType", WeaponType.Melee)]
    public GameObject meleeHitboxPrefab;
    [ShowIf("weaponType", WeaponType.Melee)]
    public float meleeDuration = 0.1f;
}