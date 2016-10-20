using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Group : MonoBehaviour {

    public static int gameDifficulty = 1;
    // Time since last gravity tick
    float lastFall = 0;
    int fallSpeed = 1; 

    //find each child block's position in the group
    //and find out whether the next moving position is aviliale.
    bool isValidGridPos()
    {
        foreach(Transform child in transform)
        {
            Vector3 v = Grid.roundVec3(child.position);

            //if not inside the border
            if (!Grid.insideBorder(v))
                return false;

            //block in grid cell (and not part of the same group)
            if (Grid.grid[(int)v.x, (int)v.y, (int)v.z] != null && Grid.grid[(int)v.x, (int)v.y, (int)v.z].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        //Remove old children from Grid
        for(int y = 0; y < Grid.h; ++y)
        {
            for(int x = 0; x < Grid.w; ++x)
            {
                for(int z = 0; z < Grid.l; ++z)
                {
                    if (Grid.grid[x, y, z] != null) 
                        if (Grid.grid[x, y, z].parent == transform)
                            Grid.grid[x, y, z] = null;
                }
            }
        }

        //add new children to grid
        foreach (Transform child in transform)
        {
            Vector3 v = Grid.roundVec3(child.position);
            Grid.grid[(int)v.x, (int)v.y, (int)v.z] = child;
        }
    }

    // Update is called once per frame
    void Update () {

        //Accelerometer input
        Vector3 dir = Vector3.zero;
        dir.y = -Input.acceleration.y;
        dir.z = Input.acceleration.z;

        //Move left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(-1, 0, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(1, 0, 0);
        }
        //Move right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(1, 0, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(-1, 0, 0);
        }
        //Move forward
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(0, 0, 1);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(0, 0, -1);
        }
        //Move backward
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(0, 0, -1);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(0, 0, 1);
        }
        //Rotate
        //Z Axis
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.Rotate(0, 0, -90);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.Rotate(0, 0, 90); ;
        }
        //X Axis
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.Rotate(-90, 0, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.Rotate(90, 0, 0); ;
        }
        //Y Axis
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.Rotate(0, -90, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.Rotate(0, 90, 0); ;
        }
        //Fall && move downwards
        else if (Input.GetKeyDown(KeyCode.Space) || Time.time - lastFall >= 1)
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(0, -1, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                //Update gameScore.
                Grid.gameScore += 50;

                //If not valid, reverse the process.
                transform.position += new Vector3(0, 1, 0);

                //Clear filled horizontal lines
                Grid.deleteFullPlane();

                //Spawn next Group
                FindObjectOfType<Spawner>().NextSpawner();

                //Disable script
                enabled = false;
            }
            lastFall = Time.time;
        }
        //Accelerate drop with accelerometer
        else if (dir.sqrMagnitude > 2) 
        {
            if (GamePlayButton.isPaused == true) return;
            // Modify position
            transform.position += new Vector3(0, -fallSpeed, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                //Update gameScore.
                Grid.gameScore += 50;

                // It's not valid. revert.
                transform.position += new Vector3(0, fallSpeed, 0);

                // Clear filled horizontal lines
                Grid.deleteFullPlane();

                // Spawn next Group
                FindObjectOfType<Spawner>().NextSpawner();

                // Disable script
                enabled = false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = gameDifficulty;
        //If default position is not valid, Game over.
        if (!isValidGridPos())
        {
            Grid.lastGameScore = Grid.gameScore;
            Grid.gameScore = 0;
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }        
    }

    public Texture up;
    public Texture down;
    public Texture right;
    public Texture left;
    public Texture xRot;
    public Texture yRot;
    public Texture zRot;

    void OnGUI()
    {
        if (!up || !down || !right || !left || !xRot || !yRot || !zRot)
        {
            Debug.LogError("Please assign a texture on the inspector");
            return;
        }

        //Transform Part
        //Up
        if (GUI.Button(new Rect((Screen.width * 0.85f), (Screen.height * 0.65f), 100, 100), up))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(0, 0, 1);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(0, 0, -1);
        }
        //Down
        if (GUI.Button(new Rect((Screen.width * 0.85f), (Screen.height * 0.85f), 100, 100), down)) 
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(0, 0, -1);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(0, 0, 1);
        }
        //Left
        if (GUI.Button(new Rect((Screen.width * 0.77f), (Screen.height * 0.75f), 100, 100), left))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(-1, 0, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(1, 0, 0);
        }
        //Right
        if (GUI.Button(new Rect((Screen.width * 0.93f), (Screen.height * 0.75f), 100, 100), right)) 
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.position += new Vector3(1, 0, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.position += new Vector3(-1, 0, 0);
        }

        //Now comes to the rotation part
        //x-axis rotation
        if (GUI.Button(new Rect((Screen.width * 0.02f), (Screen.height * 0.75f), 100, 100), xRot)) 
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.Rotate(-90, 0, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.Rotate(90, 0, 0); ;
        }
        //y-axis rotation
        if (GUI.Button(new Rect((Screen.width * 0.10f), (Screen.height * 0.75f), 100, 100), yRot)) 
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.Rotate(0, -90, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.Rotate(0, 90, 0); 
        }
        //z-axis rotation
        if (GUI.Button(new Rect((Screen.width * 0.18f), (Screen.height * 0.75f), 100, 100), zRot))
        {
            if (GamePlayButton.isPaused == true) return;
            //Modify position
            transform.Rotate(0, 0, -90);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
                //If not valid, reverse the process.
                transform.Rotate(0, 0, 90); ;
        }
    }
}
