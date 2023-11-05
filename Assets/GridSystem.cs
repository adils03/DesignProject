using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem 
{

private int width;
private int height;
private Vector3 originPosition;
private float cellSize;
private int [,] gridArray;

public GridSystem(int width,int height,float cellSize,Vector3 originPosition){
    this.width=width;
    this.height=height;
    this.cellSize=cellSize;
    this.originPosition=originPosition;

    gridArray = new int[width,height];
    
    for(int y=0; y<gridArray.GetLength(1);y++){
        for(int x=0;x<gridArray.GetLength(0);x++){
           
        }


    }




    for(int x=0; x < gridArray.GetLength(0);x++){
        for(int y=0; y < gridArray.GetLength(1);y++){
            Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y+1),Color.white,100f);
            Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white,100f);
        }
    }
}

private Vector3 GetWorldPosition(int x,int y){
    return new Vector3(x,y)*cellSize+originPosition;
}

private void GetXY(Vector3 worldPosition,out int x,out int y){
    x = Mathf.FloorToInt((worldPosition-originPosition).x/cellSize);
    y = Mathf.FloorToInt((worldPosition-originPosition).y/cellSize);
}

public void SetValue(int x,int y,int value){
    if(x>=0 && y>=0 && x<width && y<height){
        gridArray[x,y]=value;
    }
    
}

public void SetValue(Vector3 worldPosition,int value){
    int x,y;
    GetXY(worldPosition,out x,out y);
    SetValue(x,y,value);
    Debug.Log(x + " " +y + " " +value);
}
public int GetValue(int x,int y){
    if(x>=0 && y>=0 && x<width && y<height){
        return gridArray[x,y];
    }else {
        return 0 ;
    }
}
public int GetValue(Vector3 worldPosition){
    int x,y;
    GetXY(worldPosition, out x ,out y);
    Debug.Log(gridArray[x,y]);
    return GetValue(x,y);
    
}

}