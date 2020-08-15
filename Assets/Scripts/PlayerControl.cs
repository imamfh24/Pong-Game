﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;

    // Tombol untuk menggerakkan ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10f;

    // Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
    public float yBoundary = 9f;

    //Rigidbody2D raket
    private Rigidbody2D rigidBody2D;

    // Skor Pemain
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        YBoundary();
    }

    private void Movement()
    {
        Vector2 velocity = rigidBody2D.velocity; // Dapatkan kecepatan roket sekarang

        // Jika pemain menekan tombol ke atas, beri kecepatan posirig ke komponen y (ke atas)
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        // Jika pemain menekam tombol kebawah, beri kecepatan negatif ke komponen y (ke bawah)

        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        // Jika pemain tidak menekan tombol apa apa, kecepatannya nol

        else
        {
            velocity.y = 0f;
        }

        // Masukkan kembali kecepatannya ke rigidbody2D
        rigidBody2D.velocity = velocity;
    }

    private void YBoundary()
    {
        // Dapatkan posisi raket sekarang
        Vector2 position = transform.position;

        // Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.

        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        // Masukkan kembali posisinya ke transform.
        transform.position = position;
    }

    public void IncrementScore(){
        score++;
    }

    public void ResetScore(){
        score = 0;
    }

    public int Score{
        get{
            return score;
            }
    }
}