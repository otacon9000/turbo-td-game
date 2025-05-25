using UnityEngine;

[RequireComponent(typeof(EnemyPatrol), typeof(EnemyChase), typeof(EnemyAttack))]
public class EnemyController : MonoBehaviour
{
    public EnemyData data;
    private Transform player;
    private EnemyPatrol patrol;
    private EnemyChase chase;
    private EnemyAttack attack;

    private enum EnemyState { Patrolling, Chasing, Attacking }
    private EnemyState currentState;

    private void Awake()
    {
        patrol = GetComponent<EnemyPatrol>();
        chase = GetComponent<EnemyChase>();
        attack = GetComponent<EnemyAttack>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Inizializza con dati dal SO
        patrol.SetSpeed(data.patrolSpeed);
        chase.SetSpeed(data.chaseSpeed);
        attack.SetData(data);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= data.attackDistance)
            SetState(EnemyState.Attacking);
        else if (distance <= data.chaseDistance)
            SetState(EnemyState.Chasing);
        else
            SetState(EnemyState.Patrolling);

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