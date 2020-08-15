using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Pemain 1
    public PlayerControl player1;
    private Rigidbody2D player1RigidBody2D;

    // Pemain 2
    public PlayerControl player2;
    private Rigidbody2D player2RigidBody2D;

    // Bola
    public BallControl ball;
    private Rigidbody2D ballRigidBody2D;
    private CircleCollider2D ballCollider2D;

    // Skor Maksimal
    public int maxScore;

    // Apakah debug windows ditampilkan?
    private bool isDebugWindowShow = false;

    // Start is called before the first frame update
    void Start()
    {
        player1RigidBody2D = player1.GetComponent<Rigidbody2D>();
        player2RigidBody2D = player2.GetComponent<Rigidbody2D>();
        ballRigidBody2D = ball.GetComponent<Rigidbody2D>();
        ballCollider2D = ball.GetComponent<CircleCollider2D>();
    }

    private void OnGUI()
    {
        PlayerScoreGUI();
        RestartButtonGUI();
        PlayerWinGUI();
        DebugWindowGUI();
    }

    private void DebugWindowGUI()
    {
        DebugWindowGUIToggle();
        // Jika isDebugWindowShow == true, tampilkan text area untuk debug window
        if (isDebugWindowShow)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            //Simpan variabel-variabel fisika yang akan ditampilkan
            float ballMass = ballRigidBody2D.mass;
            Vector2 ballVelocity = ballRigidBody2D.velocity;

            float ballSpeed = ballRigidBody2D.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;

            float ballFriction = ballCollider2D.friction;

            float impulsePlayer1X = player1.LastContactPoint2D.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint2D.tangentImpulse;

            float impulsePlayer2X = player2.LastContactPoint2D.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint2D.tangentImpulse;

            // Tentukan debug text-nya
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            // Tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 1010), debugText, guiStyle);

            // Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;
        }

        
    }

    private void DebugWindowGUIToggle()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShow = !isDebugWindowShow;
        }
    }

    private void PlayerWinGUI()
    {
        // Jika pemain 1 menang (mencapai skor maksimal)
        if (player1.Score == maxScore)
        {
            // Tampilkan teks PLAYER ONE WINS di bagian kiri layar
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            // Dan kembalikan boile ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            // Tampilkan teks PLAYER TWO WINS di bagian kanan layar
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            // Dan kembalikan bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }

    private void RestartButtonGUI()
    {
        //Tombol restart untuk memulai game dari awal
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            // Ketika tombol restart ditekan, reset skor kedua pemain
            player1.ResetScore();
            player2.ResetScore();

            // Dan Restart Game
            ball.SendMessage("RestartGame", 1f, SendMessageOptions.RequireReceiver);
        }
    }

    private void PlayerScoreGUI()
    {
        // Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);
    }
}
