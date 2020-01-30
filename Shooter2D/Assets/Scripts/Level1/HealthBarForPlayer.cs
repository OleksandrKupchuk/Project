using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarForPlayer : MonoBehaviour
{
    public Image barHealthForPlayer;
    public float fillAmount;
    PlayerController userController;
    // Start is called before the first frame update
    void Start()
    {
        userController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //fillAmount = 1f;
        //barHealthForPlayer.fillAmount = (userController.playerHealth / 10);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerBarHealh();
    }

    public void PlayerBarHealh()
    {
        //barHealthForPlayer.fillAmount = (userController.playerHealth / 10);
    }

}
