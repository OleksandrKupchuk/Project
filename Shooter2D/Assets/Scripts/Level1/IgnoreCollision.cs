using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] Collider2D other;
    [SerializeField] Collider2D other2;

    // Start is called before the first frame update
    void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other2, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
