using UnityEngine;

[RequireComponent(typeof(EnemyPatrol), typeof(EnemyChase), typeof(EnemyAttack))]
public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Attacking }

    [Header("Settings")]
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f;

    private Transform player;
    private EnemyState currentState;

    private EnemyPatrol patrol;
    private EnemyChase chase;
    private EnemyAttack attack;

    private void Awake()
    {
        patrol = GetComponent<EnemyPatrol>();
        chase = GetComponent<EnemyChase>();
        attack = GetComponent<EnemyAttack>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Stato
        if (distance <= attackDistance)
            SetState(EnemyState.Attacking);
        else if (distance <= chaseDistance)
            SetState(EnemyState.Chasing);
        else
            SetState(EnemyState.Patrolling);

        // Azioni
        patrol.enabled = currentState == EnemyState.Patrolling;
        chase.enabled = currentState == EnemyState.Chasing;
        attack.enabled = currentState == EnemyState.Attacking;
    }

    private void SetState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }
}
