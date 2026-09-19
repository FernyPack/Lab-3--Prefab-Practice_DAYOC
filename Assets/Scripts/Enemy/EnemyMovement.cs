using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float gravity = -9.8f;

    [Header("References")]
    public Transform player;
    public EnemyAggro isAggro; 

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (isAggro == null)
            isAggro = GetComponent<EnemyAggro>();

        if (player == null && GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f; 

        velocity.y += gravity * Time.deltaTime;

        Vector3 horizontalMove = Vector3.zero;

        if (isAggro != null && isAggro.isAggro && player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; 
            horizontalMove = direction.normalized * moveSpeed;

            if (horizontalMove != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(horizontalMove), 5f * Time.deltaTime);
        }
        Vector3 finalMove = horizontalMove * Time.deltaTime;
        finalMove.y = velocity.y * Time.deltaTime;
        controller.Move(finalMove);
    }
}
