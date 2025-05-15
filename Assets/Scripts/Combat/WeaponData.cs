using UnityEngine;

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

    [Header("Ranged")]
    public GameObject projectilePrefab;
    public float projectileSpawnOffset = 0.5f;

    [Header("Melee")]
    public GameObject meleeHitboxPrefab;
    public Transform relativeHitboxOrigin; // opzionale se vuoi prefab dinamico con offset
}
