using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyAttackType
    {
        Melee,
        Ranged
    }

    [Header("Attack Type")] public EnemyAttackType attackType = EnemyAttackType.Melee;
    
    [Header("Behavior")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 2.5f;
    public float chaseDistance = 5f;
    public float patrolDistance = 7f; // distanza in cui passa da patrol a chasing
    public float attackDistance = 4f; // distanza per iniziare attacco
    public float minAttackDistance = 2f; // distanza minima prima di tornare a chasing
    
    [Header("Attack")]
    public int damage = 1;
    public float attackCooldown = 1.5f;
    public float hitboxDuration = 0.2f;
    public GameObject attackHitboxPrefab;

    [Header("General")]
    public int maxHealth = 3;
    public string targetTag = "Player";
    

}