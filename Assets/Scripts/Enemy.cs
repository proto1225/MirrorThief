using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform currentPoint;

    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float radius = 5f;
    [Range(1, 360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject player;
    public bool CanSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
        rb = GetComponent<Rigidbody2D>();
        if(pointB != null)
        {
            currentPoint = pointB.transform;
        }
    }

    IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if(rangeCheck.Length > 0 )
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if(Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer= false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!CanSeePlayer)
        {
            if(pointA != null && pointB != null)
            {
                Vector2 point = currentPoint.position - transform.position;
                Vector2 direction = new Vector2(currentPoint.transform.position.x - transform.position.x, currentPoint.transform.position.y - transform.position.y);
                transform.up = direction;
                transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
                {
                    currentPoint = pointA.transform;
                }

                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
                {
                    currentPoint = pointB.transform;
                }
            }
            else
            {
                rb.velocity = new Vector2(0,0);
            }
        }
        else
        {
            Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            transform.up = direction;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 2 * Time.deltaTime);
        }
        
    }
}
