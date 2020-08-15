using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalWall : MonoBehaviour
{
    // Pemain yang akan bertambah skornya jika bola menyentuh dinding ini.
    public PlayerControl player;
    [SerializeField] private GameManager gameManager;

    void OnTriggerExit2D(Collider2D collider) // Akan dipanggil ketika objek lain ber-collider (bola) bersentuhan dengan dinding.
    {
        if(collider.name == "Ball")
        {
            player.IncrementScore(); // Tambahkan skor ke pemain

            // Jika skor pemain belum mencapai skor maksimal
            if(player.Score < gameManager.maxScore)
            {
                //...restart game setelah bola mengenai dinding.
                collider.gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
