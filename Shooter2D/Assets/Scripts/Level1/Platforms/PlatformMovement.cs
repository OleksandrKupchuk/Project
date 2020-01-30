using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Vector3 startPosA;
    private Vector3 endPosB;
    private Vector3 nextPos;

    [SerializeField] float speed;

    [SerializeField] private Transform childTransform;

    [SerializeField] private Transform transformB;
    // Start is called before the first frame update
    void Start()
    {
        startPosA = childTransform.localPosition;
        endPosB = transformB.localPosition;
        nextPos = endPosB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
        
        if(Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != startPosA ? startPosA : endPosB;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.layer = 12;
            other.transform.SetParent(childTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
