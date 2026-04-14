using UnityEngine;

public class LevelEditor : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider.gameObject.GetComponent<EditObject>())
            {
                Debug.Log("Hit object: " + hit.collider.name);
                hit.collider.gameObject.GetComponent<EditObject>().MoveToMouse();
            }
        }
    }

    
}
