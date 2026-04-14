using UnityEngine;
using UnityEngine.UIElements;

public class EditObject : MonoBehaviour
{
    bool moveToMouse;
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
