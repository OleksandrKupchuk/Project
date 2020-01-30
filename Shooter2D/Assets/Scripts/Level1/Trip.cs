using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trip : MonoBehaviour
{
    public Rigidbody2D rbTrip;
    
    [SerializeField] float radiusTrip;

    public bool TripIsGround { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("tripIsGround" + TripIsGround);
        TripIsGround = IsGround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rbTrip.isKinematic = false;
            rbTrip.mass = 10;
            rbTrip.gravityScale = 3;
        }

        else if (collision.gameObject.CompareTag("Gems"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.GetComponent<BoxCollider2D>(), true);
        }
    }

    bool IsGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.localPosition, radiusTrip);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }

            else if (colliders[i].CompareTag("Gems"))
            {
                return false;
            }
        }

        return false;
    }
}
