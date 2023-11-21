using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int width;//Genişlik
    [SerializeField] private int height;//Uzunluk
    [SerializeField] private Vector3 originPosition;//Gridin başlama pozisyonu 
    [SerializeField] private GameObject hexPrefab;//heximiz
    private GameObject hex;
    public List<Hex> hexes;//Oluşturulan hexagonları tutar
    int[,] map;



    private void Awake()
    {
        offsetX = UnityEngine.Random.Range(0, 99999);
        offsetY = UnityEngine.Random.Range(0, 99999);
        GenerateMap();
        createGrid();
        AssignNeighbors();
    }
    void Start()
    {

    }


    void Update()
    {

    }
    void createGrid()//Hexagonları ikili sıralar şeklinde oluşturur
    {
        float hexHeight = 1.73f; // Hexagon yüksekliği
        int anotherY = 0;
        for (int y = 0; y < height / 2; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[x, y] != 0)
                {
                    hex = Instantiate(hexPrefab, new Vector3(x, y * hexHeight, 0) - originPosition, quaternion.identity);
                    hexes.Add(hex.GetComponent<Hex>());
                    hex.GetComponent<Hex>().x = x;
                    hex.GetComponent<Hex>().y = anotherY;
                }

            }
            anotherY++;
            for (int x = 0; x < width; x++)
            {
                if (map[x, y] != 0)
                {
                    hex = Instantiate(hexPrefab, new Vector3(x + 0.5f, y * hexHeight + hexHeight / 2, 0) - originPosition, quaternion.identity);
                    hexes.Add(hex.GetComponent<Hex>());
                    hex.GetComponent<Hex>().x = x;
                    hex.GetComponent<Hex>().y = anotherY;
                }
            }
            anotherY++;
        }

    }

    public Hex findHex(int x, int y)//İstediğimiz hexagonu bulur
    {
        foreach (Hex hex in hexes)
        {
            if (hex.x == x && hex.y == y)
            {
                return hex;
            }
        }
        return null;
    }

    void AssignNeighbors()//Hexagonların komşularını atar
    {
        foreach (Hex hex in hexes)
        {
            Hex neighbor;
            if (hex.y % 2 == 0)
            {
                // Sol üst komşu
                neighbor = findHex(hex.x - 1, hex.y + 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sol komşu
                neighbor = findHex(hex.x - 1, hex.y);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sol alt komşu
                neighbor = findHex(hex.x - 1, hex.y - 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sağ üst komşu
                neighbor = findHex(hex.x, hex.y + 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sağ komşu
                neighbor = findHex(hex.x + 1, hex.y);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sağ alt komşu
                neighbor = findHex(hex.x, hex.y - 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);
            }

            else
            {
                // Sol üst komşu
                neighbor = findHex(hex.x, hex.y + 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sol komşu
                neighbor = findHex(hex.x - 1, hex.y);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sol alt komşu
                neighbor = findHex(hex.x, hex.y - 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sağ üst komşu
                neighbor = findHex(hex.x + 1, hex.y + 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sağ komşu
                neighbor = findHex(hex.x + 1, hex.y);
                if (neighbor != null) hex.neighbors.Add(neighbor);

                // Sağ alt komşu
                neighbor = findHex(hex.x + 1, hex.y - 1);
                if (neighbor != null) hex.neighbors.Add(neighbor);
            }

        }
    }

    public List<Hex> findStartingHexes(Hex _hex)
    { //Başlangıç için arazileri bulur
        List<Hex> _hexes = new List<Hex>();
        _hexes.Add(_hex.neighbors[3]);
        _hexes.Add(_hex.neighbors[4]);
        _hexes.Add(_hex.neighbors[5]);
        return _hexes;
    }

    public float noiseScale = 1f;
    public float threshold = 0.5f;

    public float offsetX, offsetY;


    void GenerateMap()
    {
        map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinNoise = Mathf.PerlinNoise(x * noiseScale + offsetX, y * noiseScale + offsetY);
                map[x, y] = perlinNoise > threshold ? 1 : 0;
            }
        }
    }
}
