using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QReinforcmentAgent
{
    private QTable_ qTable;
    private double epsilon; // Ke�if-exploitation dengesi i�in epsilon de�eri
    private double learningRate; // ��renme oran�
    private double discountFactor; // �ndirim fakt�r�

    public QReinforcmentAgent(QTable_ qTable, double epsilon, double learningRate, double discountFactor)
    {
        this.qTable = qTable;
        this.epsilon = epsilon;
        this.learningRate = learningRate;
        this.discountFactor = discountFactor;
    }

    public Action SelectAction(GameStateH currentState)
    {
        System.Random random = new System.Random();

        if (random.NextDouble() < epsilon)
        {
            // Ke�if: Rastgele bir aksiyon se�
            return GetRandomAction();
        }
        else
        {
            // Exploitation: En iyi Q de�erine sahip aksiyonu se�
            return GetBestAction(currentState);
        }
    }

    private Action GetRandomAction()
    {
        return qTable.GetRandomAction();
    }

    private Action GetBestAction(GameStateH currentState)
    {
        return qTable.GetBestAction(currentState);
    }

    public void UpdateQValue(GameStateH currentState, ActionH action, GameStateH newGameState, double reward)
    {
        double currentQValue = qTable.GetQValue(currentState, action);
        double maxQValue = qTable.GetMaxQValue(newGameState);

        double newQValue = currentQValue + learningRate * (reward + discountFactor * maxQValue - currentQValue);
        qTable.SetQValue(currentState, action, newQValue);
    }

    public void Train(int numEpisodes)
    {
        // Belirli bir say�da b�l�m (episode) boyunca ajan� e�itme
        for (int episode = 0; episode < numEpisodes; episode++)
        {
            GameStateH currentState = /* Ba�lang�� durumu olu�tur */;
            double totalReward = 0;

            // Bir b�l�m boyunca ad�mlar� ger�ekle�tir
            while (!IsEpisodeFinished(currentState))
            {
                ActionH selectedAction = SelectAction(currentState);
                GameStateH newGameState = /* Se�ilen aksiyonu ger�ekle�tirerek yeni durumu al */;
                double reward = /* Yap�lan aksiyonun �d�l�n� al */;

                UpdateQValue(currentState, selectedAction, newGameState, reward);

                totalReward += reward;
                currentState = newGameState;
            }

            // Her b�l�m sonunda istatistikleri g�ncelleme veya loglama i�lemleri yapabilirsiniz
            LogEpisodeResults(episode, totalReward);
        }
    }

    private bool IsEpisodeFinished(GameStateH currentState)
    {
        // B�l�m�n bitip bitmedi�ini kontrol etme lojikleri
        // �rne�in: Belirli bir duruma ula��ld���nda veya belirli bir say�da ad�m ger�ekle�ti�inde b�l�m� bitir
        return /* B�l�m�n bitip bitmedi�ini kontrol et */;
    }

    private void LogEpisodeResults(int episode, double totalReward)
    {
        // B�l�m sonu�lar�n� loglama veya istatistikleri g�ncelleme
        // �rne�in: Console.WriteLine($"Episode {episode}: Total Reward = {totalReward}");
    }




}
