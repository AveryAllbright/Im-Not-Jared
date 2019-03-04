using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBasedMovement : MonoBehaviour {

    public int tileSize;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move("N", tileSize);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move("S", tileSize);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move("W", tileSize);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move("E", tileSize);
        }
    }

    private void Move(string dir, int dis) {

        switch (dir) {
            case "N":
                this.transform.position += Vector3.up * dis;
                break;
            case "S":
                this.transform.position += Vector3.down * dis;
                break;
            case "W":
                this.transform.position += Vector3.left * dis;
                break;
            case "E":
                this.transform.position += Vector3.right * dis;
                break;
            default:
                Debug.Log("error movement input");
                break;
        }
    }
}
