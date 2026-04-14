using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelEditor : MonoBehaviour
{
    public GameObject selectedObj;

    public TMP_InputField inputScaleX, inputScaleY, inputRotation;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider && hit.collider.gameObject.GetComponent<EditObject>())
            {
                selectedObj = hit.collider.gameObject;
                SetInputFeild();
                Debug.Log("Hit object: " + hit.collider.name);
                selectedObj.GetComponent<EditObject>().StartMoveToMouse();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObj != null)
                selectedObj.GetComponent<EditObject>().StopMoveToMouse();
        }
    }

    public void ChangeScaleX()
    {
        if (selectedObj == null) return;
        float v;
        if (inputScaleX.text != "")
        {

            v = float.Parse(inputScaleX.text);
        }
        else
        {
            v = 0f;
        }
        if (v < 1f)
        {
            return;
        }
        
        selectedObj.transform.localScale = new Vector2(v, selectedObj.transform.localScale.y);
    }
    public void ChangeScaleY()
    {
        if (selectedObj == null) return;
        float v;
        if (inputScaleY.text != "")
        {

            v = float.Parse(inputScaleY.text);
        }
        else
        {
            v = 0f;
        }

        if (v < 1f)
        {
            return;
        }

        selectedObj.transform.localScale = new Vector2(selectedObj.transform.localScale.x, v);
    }
    public void ChangeRotation()
    {
        if (selectedObj == null) return;
        float v;
        if (inputRotation.text != "")
        {

            v = float.Parse(inputRotation.text);
        }
        else
        {
            v = 0f;
        }
        selectedObj.transform.rotation = Quaternion.Euler(0, 0, v);
    }

    public void SetInputFeild()
    {
        if (selectedObj == null) return;
        Transform t = selectedObj.transform;
        inputScaleX.text = selectedObj.transform.localScale.x.ToString();
        inputScaleY.text = selectedObj.transform.localScale.y.ToString();
        inputRotation.text = selectedObj.transform.rotation.z.ToString();
    }
    

}
