using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Vector3 _dragOrigin;
    [SerializeField] private float dragSpeed;
    [SerializeField] private float xBoundLeft;
    [SerializeField] private float xBoundRight;
    [SerializeField] private float yBoundUp;
    [SerializeField] private float yBoundDown;

    void Update()
    {
        Drag();
        keepBounds();
    }

    void Drag()
    { //Kamerayı mouse ile kaydırır
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(_dragOrigin - Input.mousePosition);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
    }

    void keepBounds()
    { //Kameranın sınırları aşmasını önler
        if (transform.position.x < xBoundLeft)
        {
            transform.position = new Vector3(xBoundLeft, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBoundRight)
        {
            transform.position = new Vector3(xBoundRight, transform.position.y, transform.position.z);
        }
        if (transform.position.y < yBoundDown)
        {
            transform.position = new Vector3(transform.position.x, yBoundDown, transform.position.z);
        }
        if (transform.position.y > yBoundUp)
        {
            transform.position = new Vector3(transform.position.x, yBoundUp, transform.position.z);
        }
    }
}
