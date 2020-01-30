using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IUsable
{
    [SerializeField] private Collider2D platformCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseObject()
    {
        if (PlayerController.Instance.OnLadder)
        {
            //we need to stop climbing
            UseLadder(false, 2, 0, 1, "animatorAirDown");
        }

        else
        {
            //we need to start climbing
            UseLadder(true, 0, 1, 0, "animatorReset");
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<Collider2D>(), platformCollider, true);
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<CircleCollider2D>(), platformCollider, true);
        }
        //Debug.Log("Player on Ladder");
    }

    void UseLadder(bool onLadder, int gravity, int layerWeight, int animSpeed, string trigger)
    {
        PlayerController.Instance.OnLadder = onLadder;
        PlayerController.Instance.PlayerRigidbody2D.gravityScale = gravity;
        PlayerController.Instance.ObjectAnimator.SetLayerWeight(2, layerWeight);
        PlayerController.Instance.ObjectAnimator.speed = animSpeed;
        PlayerController.Instance.ObjectAnimator.SetTrigger(trigger);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UseLadder(false, 2, 0, 1, "animatorAirDown");
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<Collider2D>(), platformCollider, false);
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<CircleCollider2D>(), platformCollider, false);
        }
    }
}
