using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EditObject : MonoBehaviour
{
    bool moveToMouse;
    public string id;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToMouse)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseWorldPos;
        }
    }
    public void StartMoveToMouse()
    {
        moveToMouse = true;

    }
    public void StopMoveToMouse()
    {
        moveToMouse= false;
    }

}
[Serializable]
public class ObjectData
{
    public string id;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
}

[Serializable]
public class LevelData
{
    public List<ObjectData> objects = new List<ObjectData>();
}
