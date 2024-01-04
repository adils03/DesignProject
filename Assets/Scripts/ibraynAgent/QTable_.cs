using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTable_
{
    private Dictionary<(GameStateH, Action), double> table;

    public QTable_()
    {
        table = new Dictionary<(GameStateH, Action), double>();
    }

    public double GetQValue(GameStateH state, Action action)
    {
        if (table.TryGetValue((state, action), out double qValue))
        {
            return qValue;
        }

        // Varsa d�nd�r, yoksa varsay�lan bir de�er (�rne�in, 0.0) d�nd�r�lebilir.
        return 0.0;
    }
    public double GetMaxQValue(GameStateH state, Action action)
    {
        // Belirli bir durum ve aksiyon i�in en y�ksek Q de�erini bulma
        double maxQValue = double.MinValue;

        foreach (var key in table.Keys)
        {
            if (key.Item1.Equals(state) && key.Item2.Equals(action))
            {
                double qValue = table[key];
                if (qValue > maxQValue)
                {
                    maxQValue = qValue;
                }
            }
        }

        return maxQValue;
    }

    public void UpdateQValue(GameStateH state, Action action, double newValue)
    {

        var key = (state, action);

        if (table.ContainsKey(key))
        {
            // E�er varsa, de�eri g�ncelle
            table[key] = newValue;
        }
        else
        {
            // Yoksa, yeni bir giri� ekle
            table.Add(key, newValue);
        }
    }


}
