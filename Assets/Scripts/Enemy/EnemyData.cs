using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Behavior")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 2.5f;
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f;

    [Header("Attack")]
    public int damage = 1;
    public float attackCooldown = 1.5f;
    public float hitboxDuration = 0.2f;
    public GameObject attackHitboxPrefab;

    [Header("General")]
    public int maxHealth = 3;
    public string targetTag = "Player";
}