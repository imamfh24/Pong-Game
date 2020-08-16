using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float speedForce;
    /*public float xInitialForce;
    public float yInitialForce;*/

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    const string PLAYER_1 = "Player 1";
    const string PLAYER_2 = "Player 2";

    GameObject lastTouchPlayer;

    public GameObject LastTouchPlayer
    {
        get { return lastTouchPlayer; }
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        rigidBody2D = GetComponent<Rigidbody2D>();

        //Mulai game
        RestartGame();
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        /*float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);*/
        float randomInitialForce = Random.Range(-speedForce, speedForce);

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilai di bawah 1, bola bergerak ke kiri
        // Jika tidak, bola bergerak ke kanan
        if(randomDirection < 1f)
        {
            Vector2 arah = new Vector2(-speedForce, randomInitialForce).normalized;
            // Gunakan gaya untuk menggerakkan bola ini
            rigidBody2D.AddForce(arah * speedForce);
        } else
        {
            Vector2 arah = new Vector2(speedForce, randomInitialForce).normalized;
            rigidBody2D.AddForce(arah * speedForce);
        }
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik berikan gaya ke bola
        Invoke("PushBall", 2f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerCollision(coll);
    }

    private void PlayerCollision(Collision2D coll)
    {
        if (coll.gameObject.name == PLAYER_1)
        {
            BounceFromRacket(coll);
            lastTouchPlayer = GameObject.Find(PLAYER_1);
        }
        else if (coll.gameObject.name == PLAYER_2)
        {
            BounceFromRacket(coll);
            lastTouchPlayer = GameObject.Find(PLAYER_2);
        }
    }

    private void BounceFromRacket(Collision2D coll)
    {
        float sudut = (transform.position.y - coll.transform.position.y) * 5f;
        Vector2 arah = new Vector2(rigidBody2D.velocity.x, sudut).normalized;
        rigidBody2D.velocity = new Vector2(0, 0);
        rigidBody2D.AddForce(arah * speedForce);
    }
}
