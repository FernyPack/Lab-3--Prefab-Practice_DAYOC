using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 10f;
    private int currentPoint = 0;

    void Update()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];
        if (target == null) return;

        Vector3 targetPos = target.position;
        Vector3 moveDir = (targetPos - transform.position);
        moveDir.y = 0;
        moveDir.Normalize();

        transform.position += moveDir * speed * Time.deltaTime;

        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion lookRot = Quaternion.LookRotation(moveDir);
            lookRot.x = 0;
            lookRot.z = 0;
            transform.rotation = lookRot;
        }

        if (Vector3.Distance(transform.position, targetPos) < 0.6f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}
