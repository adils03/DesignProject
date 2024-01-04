using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QReinforcmentAgent
{
    private QTable_ qTable;
    private double epsilon; // Keþif-exploitation dengesi için epsilon deðeri
    private double learningRate; // Öðrenme oraný
    private double discountFactor; // Ýndirim faktörü

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
            // Keþif: Rastgele bir aksiyon seç
            return GetRandomAction();
        }
        else
        {
            // Exploitation: En iyi Q deðerine sahip aksiyonu seç
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
        // Belirli bir sayýda bölüm (episode) boyunca ajaný eðitme
        for (int episode = 0; episode < numEpisodes; episode++)
        {
            GameStateH currentState = /* Baþlangýç durumu oluþtur */;
            double totalReward = 0;

            // Bir bölüm boyunca adýmlarý gerçekleþtir
            while (!IsEpisodeFinished(currentState))
            {
                ActionH selectedAction = SelectAction(currentState);
                GameStateH newGameState = /* Seçilen aksiyonu gerçekleþtirerek yeni durumu al */;
                double reward = /* Yapýlan aksiyonun ödülünü al */;

                UpdateQValue(currentState, selectedAction, newGameState, reward);

                totalReward += reward;
                currentState = newGameState;
            }

            // Her bölüm sonunda istatistikleri güncelleme veya loglama iþlemleri yapabilirsiniz
            LogEpisodeResults(episode, totalReward);
        }
    }

    private bool IsEpisodeFinished(GameStateH currentState)
    {
        // Bölümün bitip bitmediðini kontrol etme lojikleri
        // Örneðin: Belirli bir duruma ulaþýldýðýnda veya belirli bir sayýda adým gerçekleþtiðinde bölümü bitir
        return /* Bölümün bitip bitmediðini kontrol et */;
    }

    private void LogEpisodeResults(int episode, double totalReward)
    {
        // Bölüm sonuçlarýný loglama veya istatistikleri güncelleme
        // Örneðin: Console.WriteLine($"Episode {episode}: Total Reward = {totalReward}");
    }




}
