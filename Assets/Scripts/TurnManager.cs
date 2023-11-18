using System.Collections.Generic;
using UnityEngine;

//Oyuncuların sırasını yönetir.Players adında bir liste ve turnQueue adında bir kuyruk içerir.
public class TurnManager : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    private Queue<Player> turnQueue = new Queue<Player>();

    private Player currentPlayer;


    //Start metodu, oyun başladığında çağrılır ve tüm oyuncuları sırayla kuyruğa ekler.
    void Start()
    {
        foreach (Player player in players)
        {
            turnQueue.Enqueue(player);
        }

        StartTurn();

    }



    //StartTurn metodu, bir oyuncunun sırasını başlatır. Eğer tüm oyuncuların sırası biterse, kuyruğu yeniden doldurur.
    void StartTurn()
    {
        if (turnQueue.Count == 0)
        {
            foreach (Player player in players)
            {
                turnQueue.Enqueue(player);
            }
        }

        Player currentPlayer = turnQueue.Dequeue();

        currentPlayer.StartTurn();
    }

    //EndTurn metodu geçerli oyuncunun sırasını bitirir ve bir sonraki oyuncunun sırasını başlatır.
    public void EndTurn()
    {
        currentPlayer = null;
    }


    public void passTurn()
    {
        if (currentPlayer == null)
        {
            StartTurn();
        }
    }
}