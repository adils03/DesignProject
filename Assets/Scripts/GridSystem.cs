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
    [SerializeField] private GameObject hexPrefab;
    private GameObject hex;
    public List<Hex> hexes;//Oluşturulan hexagonları tutar



    private void Awake()
    {
        createGrid();
    }
    void Start()
    {
        AssignNeighbors();
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

                hex = Instantiate(hexPrefab, new Vector3(x, y * hexHeight, 0) - originPosition, quaternion.identity);
                hexes.Add(hex.GetComponent<Hex>());
                hex.GetComponent<Hex>().x = x;
                hex.GetComponent<Hex>().y = anotherY;
            }
            anotherY++;
            for (int x = 0; x < width; x++)
            {
                hex = Instantiate(hexPrefab, new Vector3(x + 0.5f, y * hexHeight + hexHeight / 2, 0) - originPosition, quaternion.identity);
                hexes.Add(hex.GetComponent<Hex>());
                hex.GetComponent<Hex>().x = x;
                hex.GetComponent<Hex>().y = anotherY;
            }
            anotherY++;
        }
    }

    private Hex findHex(int x, int y)//İstediğimiz hexagonu bulur
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


}
