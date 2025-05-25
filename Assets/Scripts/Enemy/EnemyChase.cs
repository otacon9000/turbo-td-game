using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform player;
    private float speed = 2.5f;

    public void SetSpeed(float s) => speed = s;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}