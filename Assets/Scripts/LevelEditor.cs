using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class LevelEditor : MonoBehaviour
{
    public GameObject selectedObj;
    public TMP_InputField inputScaleX, inputScaleY, inputRotation;
    public GameObject EditorPanel;

    public GameObject ground, bounce, noDraw;

    public Transform LevelEditorObjPOS;

    void Start()
    {

    }


    void Update()
    {
        if(selectedObj != null && !selectedObj.CompareTag("Main"))
        {
            EditorPanel.SetActive(true);
        }
        else
        {
            EditorPanel.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

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

        if (v < 0f)
        {
            return;
        }
        selectedObj.transform.rotation = Quaternion.Euler(0, 0, v);
    }

    public void SetInputFeild()
    {
        if (selectedObj == null) return;
        Transform t = selectedObj.transform;
        inputScaleX.text = t.localScale.x.ToString();
        inputScaleY.text = t.localScale.y.ToString();
        inputRotation.text = t.rotation.eulerAngles.z.ToString();
    }
    
    public void CreateBounce()
    {
        Instantiate(bounce, LevelEditorObjPOS);
    }
    public void CreateGround()
    {
        Instantiate(ground, LevelEditorObjPOS);
    }
    public void CreateNonDraw()
    {
        Instantiate(noDraw, LevelEditorObjPOS);
    }
    public void Delete()
    {
        if (selectedObj.CompareTag("ScoreBox")) return;
        Destroy(selectedObj);
    }
}
