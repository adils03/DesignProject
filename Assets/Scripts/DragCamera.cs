using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    public float zoomSpeed = 4.0f;
    public float targetOrtho;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;
    void Start()
    {
        ResetCamera = Camera.main.transform.position;
        targetOrtho = Camera.main.orthographicSize;
    }
    void LateUpdate()
    {
        Dragging();
        
    }
    private void Update() {
        Zoom();   
    }

    void Dragging(){
        if (Input.GetMouseButton(1))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - Diference;
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.position = ResetCamera;
        }
    }
    void Zoom(){
        
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0.0f)
            targetOrtho -= scrollData * zoomSpeed;

        targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);

        Camera.main.orthographicSize = targetOrtho; // Kamerayı hemen hedef yakınlaştırma seviyesine ayarlıyoruz.
    }
}
