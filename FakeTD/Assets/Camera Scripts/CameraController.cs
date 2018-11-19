using UnityEngine;
using System.Collections;

// Camera Controller
// Revision 2
// Allows the camera to move left, right, up and down along a fixed axis.
// Attach to a camera GameObject (e.g MainCamera) for functionality.

public class CameraController : MonoBehaviour
{

    //float Xmax, Ymax;
    int UP = 1;
    int DOWN = 2;
    int LEFT = 3;
    int RIGHT = 4;
    int dir = 0;
    int rot = 0;
    int RLEFT = 1;
    int RRIGHT = 2;

    public int TerrainBoundX;
    public int TerrainBoundY;
    int offset = 15;

    Vector3 tsize= new Vector3(50,50,50);
    Transform camt;

    // Use this for initialization
    void Start()
    {

        // camt = transform;
        camt = GameObject.FindGameObjectWithTag("Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = UP;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = DOWN;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = LEFT;

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = RIGHT;
        }
        else if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D))
        {
            dir = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rot = RLEFT;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rot = RRIGHT;
        }
        else if (Input.GetKeyUp(KeyCode.Q) | Input.GetKeyUp(KeyCode.E))
        {
            rot = 0;
        }

        //mouse scroll
        if (Input.mousePosition.x >= Screen.width - 10 /*&& camt.position.x<tsize.x-10*/)
        {
            //scroll right
            camt.Translate(10 * Time.deltaTime, 0, 0);
        }
        else if (Input.mousePosition.x <= 10 /*&& camt.position.x>10*/)
        {
            camt.Translate(-10 * Time.deltaTime, 0, 0);
        }

        if (Input.mousePosition.y >= Screen.height - 10 /*&& camt.position.z>10*/)
        {
            camt.Translate(0, 0, 10 * Time.deltaTime);
        }
        else if (Input.mousePosition.y <= 10 /*&& camt.position.z<tsize.z-10*/)
        {
            camt.Translate(0, 0, -10 * Time.deltaTime);
        }

        //camera scroll
        if (dir == UP)
        {
        //    if (camt.position.z < tsize.z - 10) //is inside terrain border?\
               if(camt.position.z < TerrainBoundY -offset)
            {
                camt.transform.eulerAngles = camt.transform.eulerAngles - new Vector3(0, 0, 50);
                camt.Translate(0, 0, 10 * Time.deltaTime + 10);
                camt.transform.eulerAngles = camt.transform.eulerAngles + new Vector3(0, 0, 50);
            }
            
          //      camt.Translate(0, , 0 );
        }
        else if (dir == DOWN)
        {

            //if (camt.position.z > 10)   //is inside terrain border?
            camt.Translate(0, 8 * Time.deltaTime, -10 * Time.deltaTime);
        }
        else if (dir == LEFT)
        {
            if (camt.position.x < 0 - offset )
                return;
           // if (camt.position.x > 10)   //is inside terrain border?
                camt.Translate(-10 * Time.deltaTime, 0, 0);
        }
        else if (dir == RIGHT)
        {
           
            //if (camt.position.x < tsize.x - 10) //is inside terrain border?
            camt.Translate(10 * Time.deltaTime, 0, 0);
        }
        //camera rotation with middle mouse
        if (Input.GetMouseButton(2))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                //moving left
                camt.Rotate(0, 45 * Time.deltaTime, 0);
                camt.LookAt(transform.position);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                camt.Rotate(0, -45 * Time.deltaTime, 0);
                camt.LookAt(transform.position);
            }
        }   //if 
        if (rot == RLEFT)
        {
            camt.Rotate(0, 25 * Time.deltaTime, 0);
            camt.LookAt(transform.position);
        }
        else if (rot == RRIGHT)
        {
            camt.Rotate(0, -25 * Time.deltaTime, 0);
            camt.LookAt(transform.position);
        }
        // Mouse wheel zoom 
        //if (Input.GetAxis("Mouse ScrollWheel") < 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //{
        //    if (Camera.main.fieldOfView <= 125)
        //        Camera.main.fieldOfView += 2;
        //    if (Camera.main.orthographicSize <= 7)
        //        Camera.main.orthographicSize += 0.2f;
        //}

        //if (Input.GetAxis("Mouse ScrollWheel") > 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //{
        //    if (Camera.main.fieldOfView > 2)
        //        Camera.main.fieldOfView -= 2;
        //    if (Camera.main.orthographicSize >= 3)
        //        Camera.main.orthographicSize -= 0.2f;
        //}



    }
}