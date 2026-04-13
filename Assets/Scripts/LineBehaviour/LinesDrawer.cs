using TMPro;
using UnityEngine;

public class LinesDrawer : MonoBehaviour {

	public GameObject linePrefab;
	public LayerMask cantDrawOverLayer;
	int cantDrawOverLayerIndex;
	[Space ( 30f )]
	public Gradient lineColor;
	public float linePointsMinDistance;
	public float lineWidth;
	Line currentLine;
	Camera cam;
	public LayerMask ground;

	public int num_Shapes;
	public TMP_Text Score;
	void Start ( ) {
		cam = Camera.main;
		cantDrawOverLayerIndex = cantDrawOverLayer.value;
	}
	void Update ( ) {
		if ( Input.GetMouseButtonDown ( 0 ) )
			BeginDraw ( );
		if ( currentLine != null )
			Draw ( );
		if ( Input.GetMouseButtonUp ( 0 ) )
			EndDraw ( );

		Score.text = "Total Shapes: " + num_Shapes;
	}
	void BeginDraw ( ) {
		num_Shapes++;
		currentLine = Instantiate ( linePrefab, this.transform ).GetComponent <Line> ( );
		currentLine.UsePhysics ( false );
		currentLine.SetLineColor ( lineColor );
		currentLine.SetPointsMinDistance ( linePointsMinDistance );
		currentLine.SetLineWidth ( lineWidth );

	}
	void Draw ( ) {
		Vector2 mousePosition = cam.ScreenToWorldPoint ( Input.mousePosition );
		RaycastHit2D hit = Physics2D.CircleCast ( mousePosition, lineWidth / 5f, Vector2.zero, 1f, cantDrawOverLayer );

        bool hitGround = Physics2D.OverlapCircle(mousePosition, lineWidth / 5f,ground);
        
		if (hit)
			EndDraw();
		else if(!hitGround)
			currentLine.AddPoint(mousePosition);
	}
	void EndDraw ( ) {
		if ( currentLine != null ) {
			if ( currentLine.pointsCount < 2 ) {
				//if za line has one point we dont want it!
				Destroy ( currentLine.gameObject );
			} else {
				currentLine.gameObject.layer = cantDrawOverLayer;
				currentLine.UsePhysics ( true );

				currentLine = null;
			}
		}
	}
}
