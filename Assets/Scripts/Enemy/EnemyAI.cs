using UnityEngine;

[RequireComponent(typeof(SimplePatrol))]
[RequireComponent(typeof(EnemyAggro))]
public class EnemyAI : MonoBehaviour
{
    public float chaseSpeed = 4f;
    private SimplePatrol patrol;
    private EnemyAggro aggro;
    private Transform player;

    void Start()
    {
        patrol = GetComponent<SimplePatrol>();
        aggro = GetComponent<EnemyAggro>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (patrol == null)
            Debug.LogError("EnemyAI: Missing SimplePatrol component!");
        if (aggro == null)
            Debug.LogError("EnemyAI: Missing EnemyAggro component!");
        if (player == null)
            Debug.LogError("EnemyAI: No Player found!");
    }

    void Update()
    {
        if (aggro == null || patrol == null) return;

        if (aggro.isAggro && player != null)
        {
            patrol.enabled = false;
            ChasePlayer();
        }
        else
        {
            patrol.enabled = true;
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * chaseSpeed * Time.deltaTime;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            lookRot.x = 0;
            lookRot.z = 0;
            transform.rotation = lookRot;
        }
    }
}
