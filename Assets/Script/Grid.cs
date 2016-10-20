using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    //Store game score
    public static double gameScore = 0;
    public static double lastGameScore = 0;

    //initialize the width, length and height of the game area
    public static int w = 10;
    public static int l = 10;
    public static int h = 30;
    public static Transform[,,] grid = new Transform[w,h,l];

    //Help to round a vector
    public static Vector3 roundVec3 (Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }

    //The insideBouder function, find out if a certain coordinate is in between
    //the bouders.
    public static bool insideBorder(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w
            && (int)pos.z >= 0 && (int)pos.z < l && (int)pos.y >= 0 && (int)pos.y < (h - 2));
    }

    //deletePlane is the function of deleting a complete plane, when player managed to fill every
    //entry in a plane. Parameter y means the No.y plane is going to be deleted. 
    public static void deletePlane(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            for(int z = 0; z< l; ++z)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    //decreasePlane helps to drop the above plane towards the bottom, when a deletion happens.
    //Parameter y decribes the level y plane. 
    public static void decreasePlane(int y)
    {
        for (int x = 0; x < w; ++x) 
        {
            for(int z = 0; z < l; ++z)
            {
                if (grid[x, y, z] != null)
                {
                    //move one towards the bottom
                    grid[x, y - 1, z] = grid[x, y, z];
                    grid[x, y, z] = null;

                    //update the blocks positon
                    grid[x, y - 1, z].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    //decreasePlaneAbove is used to drop all planes for on unit above the deleted plane.
    //Parameter y is the plane's level which is going to be deleted.
    public static void decreasePlaneAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreasePlane(i);
    }

    //isPlaneFull function detects whether a plane is full. 
    //Find each cube's tag on plane y to find out 
    public static bool isPlaneFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            for (int z = 0; z < l; ++z)
            {
                if (grid[x, y, z] == null)
                    return false;
            }
        }
        return true;
    }

    //deleteFullRow function is the function that delete the full plane and drop the planes,
    //which are above the deleted plane.
    public static void deleteFullPlane()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isPlaneFull(y))
            {
                gameScore += 5000;
                deletePlane(y);
                decreasePlaneAbove(y + 1);
                --y;
            }
        }
    }
}
