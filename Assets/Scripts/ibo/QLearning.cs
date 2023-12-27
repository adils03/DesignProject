using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class QLearning
{
static void Main()
    {
        // Durumlar ve eylemler
        string[] states = new string[] { "state1", "state2", "state3" };
        string[] actions = new string[] { "action1", "action2", "action3" };

        // Q-tablosu
        Dictionary<string, Dictionary<string, double>> QTable = new Dictionary<string, Dictionary<string, double>>();

        // Q-tablosunu başlat
        foreach (string state in states)
        {
            QTable[state] = new Dictionary<string, double>();
            foreach (string action in actions)
            {
                QTable[state][action] = 0;
            }
        }

        // Pekiştirmeli öğrenme döngüsü
        for (int episode = 0; episode < 1000; episode++)
        {
            // Başlangıç durumunu seç
            string state = "state1";
            while (true)
            {
                // Eylemi ε-greedy politikasına göre seç
                string action;
                if (UnityEngine.Random.Range(0.0f,1.0f) < 0.1)  // %10 olasılıkla rastgele bir eylem seç
                {
                    action = actions[UnityEngine.Random.Range(0,actions.Length)];
                }
                else  // %90 olasılıkla en yüksek Q-değerine sahip eylemi seç
                {
                    action = MaxQAction(QTable[state]);
                }

                // Eylemi gerçekleştir ve ödülü al
                string nextState = PerformAction(state, action);
                double reward = Reward(nextState);

                // Q-değerini güncelle
                double oldQ = QTable[state][action];
                double maxNextQ = MaxQValue(QTable[nextState]);
                double newQ = oldQ + 0.1 * (reward + 0.9 * maxNextQ - oldQ);
                QTable[state][action] = newQ;

                // Yeni duruma geç
                state = nextState;

                // Eğer oyun bitti ise döngüyü kır
                if (state == "state3")
                {
                    break;
                }
            }
        }
    }

    static string PerformAction(string state, string action)
    {
        // Bu fonksiyon, ajanın eylemini gerçekleştirir ve yeni durumu döndürür.
        // Bu örnekte, bu fonksiyonun tam implementasyonu verilmemiştir.
        return state;
    }

    static double Reward(string state)
    {
        // Bu fonksiyon, belirli bir durum için ödülü hesaplar.
        // Eğer ajan hedef duruma ulaştıysa büyük ödül
        if (state == "state3")
        {
            return 100;
        }
        // Diğer durumlar için küçük ceza (hamle sayısını minimize etmek için)
        else
        {
            return -0.1;
        }
    }

    static string MaxQAction(Dictionary<string, double> actionValues)
    {
        // Bu fonksiyon, belirli bir durum için en yüksek Q-değerine sahip eylemi döndürür.
        string maxQAction = null;
        double maxQValue = double.NegativeInfinity;
        foreach (KeyValuePair<string, double> entry in actionValues)
        {
            if (entry.Value > maxQValue)
            {
                maxQValue = entry.Value;
                maxQAction = entry.Key;
            }
        }
        return maxQAction;
    }

    static double MaxQValue(Dictionary<string, double> actionValues)
    {
        // Bu fonksiyon, belirli bir durum için en yüksek Q-değerini döndürür.
        double maxQValue = double.NegativeInfinity;
        foreach (KeyValuePair<string, double> entry in actionValues)
        {
            if (entry.Value > maxQValue)
            {
                maxQValue = entry.Value;
            }
        }
        return maxQValue;
    }
}
