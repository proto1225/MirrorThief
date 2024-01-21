using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    const int Infinity = 999;

    int maxReflections = 100;
    int currentReflections = 0;
    public GameObject player;
    private Vector2 endPos;

    [SerializeField]
    List<Vector3> Points;
    int defaultRayDistance = 100;
    LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            lr.enabled = true;
        }
        else
        {
            lr.enabled = false;
        }

        if(lr.enabled && Input.GetKeyDown(KeyCode.E)) 
        {
            player.transform.position = endPos;
        }

        lr.SetPosition(0, transform.position);

        var hitData = Physics2D.Raycast(transform.position, transform.up, defaultRayDistance);

        currentReflections = 0;
        Points.Clear();
        Points.Add(transform.position);

        if (hitData.collider.tag == "Mirror")
        {
            ReflectFurther(transform.position, hitData);
        }
        else
        {
            Points.Add(hitData.point);
            endPos = player.transform.position;
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);
        if (newHitData.collider.tag == "Mirror")
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(newHitData.point);
            endPos = newHitData.point;
        }
    }
}