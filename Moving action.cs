using UnityEngine;

public class SimpleCharacter2D : MonoBehaviour
{
    // 1. ARRAY: Waypoints defining the trajectory (X = forward, Y = height/jump)
    [SerializeField]
    private Vector2[] pathPoints = new Vector2[]
    {
        new Vector2(-8.25f, 1.8f),   // Start
        new Vector2(-11.3f, 5.46f)   // FINISH LINE
    };

    [SerializeField] private float speed = 4f;
    [SerializeField] private GameObject restartButton;
    private int currentTargetIndex = 0;

    void Start()
    {
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    void Update()
    {
        // Stop updating once the finish line is reached
        if (currentTargetIndex >= pathPoints.Length)
        {
            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }

            return;
        }

        // Target point from the array
        Vector2 targetPosition = pathPoints[currentTargetIndex];

        if (targetPosition.x < transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (targetPosition.x > transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        // 2. VECTOR: Smoothly move character's transform position toward target
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Check if character reached current target point
        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            // Move to next point in the array
            currentTargetIndex++;
        }
    }

    public void Restart()
    {
        if (pathPoints.Length == 0) return;

        transform.position = pathPoints[0];
        currentTargetIndex = 0;

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }
}