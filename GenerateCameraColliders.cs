/*
 https://github.com/owik100/Unity

 Just attach script to camera
 Tested in Unity 5.5.2f1 (64-bit) and Unity 2017.1.1f1 (64-bit)
*/

using UnityEngine;

public class GenerateCameraColliders : MonoBehaviour {

    [Tooltip("Colliders Size should not be less than 0.01f")]
    [SerializeField]
    private float collidersSize = 0.01f;

    private Camera cam;
    private float height;
    private float width;
    private bool ok;

    private BoxCollider2D boxRight;
    private BoxCollider2D boxLeft;
    private BoxCollider2D boxUp;
    private BoxCollider2D boxDown;

    void Start () {

        cam = GetComponent<Camera>();
        GenerateColliders();
        ok = everythingOk();

        if(!ok)
        {
            Debug.LogError("Make sure that the script is connected to the camera and camera is in orthographic mode");
        }
    }
	
	void Update () {

        if(ok)
        {
            UpdateColliders();
        }
	}

    private void GenerateColliders()
    {
        boxRight = gameObject.AddComponent<BoxCollider2D>();
        boxLeft = gameObject.AddComponent<BoxCollider2D>();
        boxUp = gameObject.AddComponent<BoxCollider2D>();
        boxDown = gameObject.AddComponent<BoxCollider2D>();
    }

    private bool everythingOk()
    {
        if (cam != null && cam.orthographic)
            return true;
        else return false;
    }

    private void UpdateColliders()
    {
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        boxRight.size = new Vector2(collidersSize, height);
        boxRight.offset = new Vector2(width / 2, boxRight.offset.y);

        boxLeft.size = new Vector2(collidersSize, height);
        boxLeft.offset = new Vector2(-width / 2, boxRight.offset.y);

        boxUp.size = new Vector2(width, collidersSize);
        boxUp.offset = new Vector2(0f, cam.orthographicSize);
      
        boxDown.size = new Vector2(width, collidersSize);
        boxDown.offset = new Vector2(0f, -cam.orthographicSize);
    }
}
