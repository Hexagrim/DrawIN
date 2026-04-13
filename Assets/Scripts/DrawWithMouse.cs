using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class DrawWithMouse : MonoBehaviour
{
    private LineRenderer Line;
    private Vector3 previousPos;

    public float minDistance;

    public EdgeCollider2D coll;
    List<Vector2> points = new List<Vector2>();

    public GameObject lineObj;

    bool startedDrawing;
    void Start()
    {
        Line = GetComponent<LineRenderer>();
        Line.positionCount = 1;
        previousPos = transform.position;
        Line.startWidth = Line.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && CanDraw())
        {
            startedDrawing = true;
        }

        if (Input.GetMouseButton(0) && startedDrawing && CanDraw())
        {

            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0f;
            points.Add(currentPos);

            if(Vector3.Distance(currentPos, previousPos) > minDistance)
            {
                if(previousPos == transform.position)
                {
                    Line.SetPosition(0,currentPos);
                }
                else
                {
                    Line.positionCount++;
                    Line.SetPosition(Line.positionCount - 1, currentPos);

                    coll.SetPoints(points);
                    coll.transform.position -= transform.position;
                }

                previousPos = currentPos;
            }
            coll.points = points.ToArray();

        }
        if (Input.GetMouseButtonUp(0) && startedDrawing)
        {
            GameObject obj = Instantiate(lineObj , transform.position, Quaternion.identity);
            obj.GetComponent<LineRenderer>().SetPositions(new Vector3[0]);
            obj.GetComponent<EdgeCollider2D>().points = new Vector2[0];
            Destroy(this);

        }


    }
    bool CanDraw()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LayerMask ignoreDrawMask = LayerMask.GetMask("IgnoreDraw");

        Collider2D hit = Physics2D.OverlapPoint(mousePos, ignoreDrawMask);

        return hit == null;
    }
}
