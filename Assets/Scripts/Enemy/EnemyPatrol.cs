using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float arrivalThreshold = 0.1f;

    private int currentIndex = 0;

    private void Update()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentIndex];
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) <= arrivalThreshold)
        {
            currentIndex = (currentIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (patrolPoints == null || patrolPoints.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(patrolPoints[i].position, 0.2f);
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[(i + 1) % patrolPoints.Length].position);
        }
    }
}