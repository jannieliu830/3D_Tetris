using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour {

    // Time since last gravity tick
    float lastFall = 0;

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
        //Move left
        //Need to be update!!!!!!!!!!!!!!!!!
        //Need to be update!!!!!!!!!!!!!!!!!
        //Need to be update!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
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
        //Fall && move downwards
        else if (Input.GetKeyDown(KeyCode.Backspace) ||
            Time.time - lastFall >= 1) 
        {
            //Modify position
            transform.position += new Vector3(0, -1, 0);
            //Check the validity
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
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
    }

    // Use this for initialization
    void Start()
    {
        //If default position is not valid, Game over.
        if (!isValidGridPos())
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }
}
