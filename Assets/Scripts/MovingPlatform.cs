using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform finishPoint;

    [SerializeField] float moveSpeed;
    Vector2 target;

    private void Start()
    {
        transform.position = startPoint.position;
        target = finishPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, startPoint.position) < 0.1f)
        {
            target = finishPoint.position;
        }
        if (Vector2.Distance(transform.position, finishPoint.position) < 0.1f)
        {
            target = startPoint.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint.position , finishPoint.position );
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startPoint.position, 0.1f);
        Gizmos.DrawWireSphere(finishPoint.position, 0.1f);
    }

}
