using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public GameObject playerGameObject;
    public Transform target;    
    public Transform enemyGFX;
    public float speed = 300f;
    public float nextWayPointDistance = 1f;
    private Path path;
    private Vector3 standardScale;
    private Vector3 invertedScale;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Vector3 enemyMovementDirection;
    private Vector3 enemyMovementForce;

    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        if (playerGameObject != null)
        {
            target = playerGameObject.transform;
        }
        else
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) target = p.transform;
        }

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        standardScale = enemyGFX.localScale;
        invertedScale = new Vector3(-standardScale.x, standardScale.y, standardScale.z);

        InvokeRepeating("UpdatePath", 0f, .5f); //(function to repeat, instant of the first call, repeating rate)
    } 

    void FixedUpdate()
    {
        PahthHandler();
        AdjustSprite();
    }

    void PahthHandler()
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
        
        MoveEnemy();

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
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



    void AdjustSprite()
    {
        if(enemyMovementForce.x >= 0.01f)
        {
            enemyGFX.localScale = standardScale;
        }
        else if(enemyMovementForce.x <= -0.01f)
        {
            enemyGFX.localScale = invertedScale;
        }    
    }

    void MoveEnemy()
    {
        enemyMovementDirection = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        enemyMovementForce = enemyMovementDirection * speed * Time.deltaTime;
        rb.AddForce(enemyMovementForce);
    }
}
