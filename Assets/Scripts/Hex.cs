using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public hexType _hexType;
    public int q, r, s;
    public List<Hex> neighbors = new List<Hex>();
    public List<Hex> continent = new List<Hex>();
    public List<Hex> areaForStep = new List<Hex>();
    public bool hasVisited = false;
    public enum hexType
    {
        grass,
        water
    }
    private void OnMouseDown()
    {
        travelContinent(this);
        travelContinentByStep(this, 2);
    }
    void travelContinent(Hex startHex)//Hex'in bulunduğu kıtayı continent listesine eşitler
    {
        Stack<Hex> stack = new Stack<Hex>();

        stack.Push(startHex);

        hexType __hexType = startHex._hexType;

        while (stack.Count > 0)
        {
            Hex currentHex = stack.Pop();

            if (!currentHex.hasVisited && currentHex._hexType == __hexType)
            {
                continent.Add(currentHex);
                currentHex.hasVisited = true;

                foreach (Hex neighbor in currentHex.neighbors)
                {
                    if (!neighbor.hasVisited)
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }
        foreach (Hex hex in continent)
        {
            hex.hasVisited = false;
        }
    }

    void travelContinentByStep(Hex startHex, int step) //Hex'in bulunduğu konumdan istenilen adım büyüklüğü kadar alanı areaForStep'e eşitler
    {
        step++;

        Queue<Hex> queue = new Queue<Hex>();

        queue.Enqueue(startHex);

        hexType __hexType = startHex._hexType;

        while (queue.Count > 0 && step > 0)
        {
            int size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                Hex currentHex = queue.Dequeue();

                if (!currentHex.hasVisited && currentHex._hexType == __hexType)
                {
                    areaForStep.Add(currentHex);
                    currentHex.hasVisited = true;
                    currentHex.transform.localScale = new Vector3(1, 1, 1);

                    foreach (Hex neighbor in currentHex.neighbors)
                    {
                        if (!neighbor.hasVisited)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            step--;
        }
        foreach (Hex hex in areaForStep)
        {
            hex.hasVisited = false;
        }
    }






}

