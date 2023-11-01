using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    // Start is called before the first frame update
    void Start()
    {
         gameObject2.SetActive(false);
            //burak ünalan
            // Gelebek
            // laylaylom galiba sana göre sevmeler 
            //selamlar burak
    }

    // Update is called once per frame
    void Update()
    {
       
        method();
    }

    void method()
    {
 

       
        if(Input.GetKey(KeyCode.A))
        {
            gameObject1.SetActive(false);
            gameObject2.SetActive(true);
        }
        
    }
}
