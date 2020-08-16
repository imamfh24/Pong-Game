using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRacket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            GameObject player = collision.gameObject.GetComponent<BallControl>().LastTouchPlayer;
            if (player)
            {
                player.GetComponent<PlayerControl>().ChangeScaleRacket();
                Destroy(gameObject);
            }
        }
    }
}
