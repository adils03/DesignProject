using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] public int size;
    [SerializeField] private GameObject hexPrefabGrass;
    [SerializeField] private GameObject hexPrefabWater;
    private GameObject _hex;
    public List<Hex> continent = new List<Hex>();
    public List<Hex> hexes = new List<Hex>();
    public float noiseScale = 1f;
    public float threshold = 0.5f;
    public float offsetX, offsetY;

    private void Awake()
    {
        CreateGrid(size);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            for (int i = 0; i < hexes.Count; i++)
            {
                Destroy(hexes[i].gameObject);
            }
            CreateGrid(size);
        }

    }

    void CreateGrid(int size) //Haritamızı oluşturur
    {
        hexes.Clear();

        float hexSpacinValue = 1.73f;
        this.size = size;
        int leftBound = -size;
        int rightBound = 0;
        bool increase = true;
        float originPositionY = 0;
        float originPositionX = 0;
        float xSpacin = 0;
        offsetX = UnityEngine.Random.Range(0, 99999);
        offsetY = UnityEngine.Random.Range(0, 99999);
        for (int i = size; i >= -size; i--)
        {
            for (int j = leftBound; j <= rightBound; j++)
            {
                float perlinNoise = Mathf.PerlinNoise(j * noiseScale + offsetX, i * noiseScale + offsetY);
                if (perlinNoise > threshold)
                {
                    _hex = Instantiate(hexPrefabGrass, new Vector3(originPositionX + xSpacin - (hexSpacinValue / 2) * size, -(i * 1.5f)), quaternion.identity);
                    hexes.Add(_hex.GetComponent<Hex>());
                    _hex.GetComponent<Hex>()._hexType = Hex.hexType.grass;
                    _hex.GetComponent<Hex>().q = j;
                    _hex.GetComponent<Hex>().r = i;
                    _hex.GetComponent<Hex>().s = -i - j;
                }
                else
                {
                    _hex = Instantiate(hexPrefabWater, new Vector3(originPositionX + xSpacin - (hexSpacinValue / 2) * size, -(i * 1.5f)), quaternion.identity);
                    hexes.Add(_hex.GetComponent<Hex>());
                    _hex.GetComponent<Hex>()._hexType = Hex.hexType.water;
                    _hex.GetComponent<Hex>().q = j;
                    _hex.GetComponent<Hex>().r = i;
                    _hex.GetComponent<Hex>().s = -i - j;
                }
                xSpacin += hexSpacinValue;
            }
            xSpacin = 0;
            if (increase)
            {
                rightBound++;
                originPositionX -= hexSpacinValue / 2;
            }
            else
            {
                leftBound++;
                originPositionX += hexSpacinValue / 2;
            }
            if (rightBound == size && increase)
            {
                increase = false;
            }
            originPositionY += 1.5f;
        }
        InitializeNeighbors();
    }

    public Hex FindHex(int q, int r) //İstediğimiz hexi bulur
    {
        foreach (Hex hex in hexes)
        {
            if (hex.q == q && hex.r == r)
            {
                return hex;
            }
        }
        return null;
    }

    public Hex GetRandomHex()
    {
        if (hexes.Count == 0)
        {
            return null; 
        }

        int randomIndex = UnityEngine.Random.Range(0, hexes.Count);
        return hexes[randomIndex];
    }
    void InitializeNeighbors() //Komşuları atar
    {
        foreach (Hex hex in hexes)
        {
            AddNeighborIfNotNull(hex, hex.q, hex.r - 1);
            AddNeighborIfNotNull(hex, hex.q + 1, hex.r - 1);
            AddNeighborIfNotNull(hex, hex.q + 1, hex.r);
            AddNeighborIfNotNull(hex, hex.q, hex.r + 1);
            AddNeighborIfNotNull(hex, hex.q - 1, hex.r + 1);
            AddNeighborIfNotNull(hex, hex.q - 1, hex.r);
        }
    }

    void AddNeighborIfNotNull(Hex hex, int q, int r)
    {
        Hex neighbor = FindHex(q, r);
        if (neighbor != null)
        {
            hex.neighbors.Add(neighbor);
        }
    }

    public static int FindDistanceBetweenHexes(Hex a, Hex b) //İki hex arası uzaklığı bulur
    {
        return (Mathf.Abs(a.q - b.q) + Mathf.Abs(a.r - b.r) + Mathf.Abs(a.s - b.s)) / 2;
    }

    public List<Hex> travelContinent(Hex startHex)//Hex'in bulunduğu kıtayı continent listesine eşitler
    {
        Stack<Hex> stack = new Stack<Hex>();

        stack.Push(startHex);

        Hex.hexType __hexType = startHex._hexType;

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
        return continent;
    }

    public List<Hex> AStar(Hex start, Hex goal,List<Hex> validHexes)
    {
        List<Hex> openList = new List<Hex>();
        List<Hex> closedList = new List<Hex>();
        openList.Add(start);

        while (openList.Count > 0)
        {
            Hex current = openList.OrderBy(hex => hex.estimatedCost).First();
            if (current == goal)
            {
                List<Hex> path = new List<Hex>();
                while (current != null)
                {
                    path.Add(current);
                    current = current.parent;
                }
                path.Reverse();
                return path;
            }
            openList.Remove(current);
            closedList.Add(current);

            List<Hex> adjacentNodes = current.neighbors;
            foreach (Hex neighbor in adjacentNodes)
            {
                if (closedList.Contains(neighbor) || !validHexes.Contains(neighbor))
                {
                    continue;
                }
                if (!openList.Contains(neighbor))
                {
                    neighbor.parent = current;
                    neighbor.cost = current.cost + 1;
                    neighbor.estimatedCost = neighbor.cost + FindDistanceBetweenHexes(neighbor, goal);
                    openList.Add(neighbor);
                }
                else
                {
                    int newCost = current.cost + 1;

                    if (newCost < neighbor.cost)
                    {
                        neighbor.parent = current;
                        neighbor.cost = newCost;
                        neighbor.estimatedCost = neighbor.cost + FindDistanceBetweenHexes(neighbor, goal);
                    }



                }
            }

        }
        return null;
    }
}