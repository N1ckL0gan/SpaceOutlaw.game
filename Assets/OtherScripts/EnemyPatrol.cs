using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the enemy to patrol
    public float speed = 2f; // Speed of the enemy
    private int currentTargetIndex;

    void Start()
    {
        // Pick a random starting point
        currentTargetIndex = Random.Range(0, waypoints.Length);
    }

    void Update()
    {
        // Move toward the current target
        Transform target = waypoints[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If reached the target, pick a new random one (but not the same)
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        int newTarget;
        do
        {
            newTarget = Random.Range(0, waypoints.Length);
        }
        while (newTarget == currentTargetIndex); // Avoid picking the same point

        currentTargetIndex = newTarget;
    }
}
