using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public int x;
    public int y;
    public List<Hex> neighbors;//Komşularını tutar
    private void OnMouseDown()
    {
        Debug.Log(x + ":" + y + "tiklandi");
    }

}
