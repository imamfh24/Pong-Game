using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            BallControl ball = collision.gameObject.GetComponent<BallControl>();
            ball.AnimationFireBall(true);
            Destroy(gameObject);
        }
    }
}
