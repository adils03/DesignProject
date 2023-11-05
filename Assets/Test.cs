using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GridSystem gridSystem;
    void Start()
    {
        gridSystem = new GridSystem(8,8,2f,new Vector3(-11f,-5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            gridSystem.SetValue(GetMousePosition(),56);
        }
        if(Input.GetMouseButtonDown(1)){
            gridSystem.GetValue(GetMousePosition());
        }
    }

    public Vector3 getWorldPosition(){
        Vector3 vec = GetMousePosition(Input.mousePosition,Camera.main);
        vec.z=0f;
        return vec;
    }
    public Vector3 GetMousePosition(){
        return GetMousePosition(Input.mousePosition,Camera.main);
    }
    public Vector3 GetMousePosition(Camera worldCamera){
        return GetMousePosition(Input.mousePosition,worldCamera);
    }
    public Vector3 GetMousePosition(Vector3 screenPosition,Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
