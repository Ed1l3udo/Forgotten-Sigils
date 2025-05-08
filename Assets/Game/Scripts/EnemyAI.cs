using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;    
    public Transform enemyGFX;
    public float speed = 300f;
    public float nextWayPointDistance = 1f;
    private Path path;
    private Vector3 standardScale;
    private Vector3 invertedScale;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        standardScale = enemyGFX.localScale;
        invertedScale = new Vector3(-standardScale.x, standardScale.y, standardScale.z);

        InvokeRepeating("UpdatePath", 0f, .5f); //(function to repeat, instant of the first call, repeating rate)
    } 

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            this.path = path;
            currentWayPoint = 0;
        }
    }


    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }



        if(force.x >= 0.01f)
        {
            enemyGFX.localScale = standardScale;
        }
        else if(force.x <= -0.01f)
        {
            enemyGFX.localScale = invertedScale;
        }

    }
}
