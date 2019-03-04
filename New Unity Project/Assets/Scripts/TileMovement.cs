using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {

    public Transform playerObj;
    public int tileSize;
    bool up;
    bool down;
    bool left;
    bool right;

    bool isLerping;

    bool yCurve = false;

    private float targetX;
    private float targetY;

    bool initLerp;
    Vector3 targetP;
    Vector3 startP;
    Vector3 previousP;
    float speedActual;
    public float baseSpeed = 5.0f;
    float t;
    public float curveScale = 0.7f;
    public AnimationCurve moveCurve;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isLerping) {
            LerpToPos();
            
            // LerpBack();
            // playerObj.position = previousP;
        }

        GetInput();

        Vector3 targetPosition = Vector3.zero;

        if (up) {
            previousP = playerObj.position;
            yCurve = false;
            targetPosition.y = tileSize;
            Pathfind(targetPosition);
            return;
        }
        if (down)
        {
            previousP = playerObj.position;
            yCurve = false;
            targetPosition.y = -tileSize;
            Pathfind(targetPosition);
            return;
        }
        if (left)
        {
            previousP = playerObj.position;
            yCurve = true;
            targetPosition.x = -tileSize;
            Pathfind(targetPosition);
            return;
        }
        if (right)
        {
            previousP = playerObj.position;
            yCurve = true;
            targetPosition.x = tileSize;
            Pathfind(targetPosition);
            return;
        }
    }

    void GetInput() {

        up = Input.GetKeyDown(KeyCode.W);
        down = Input.GetKeyDown(KeyCode.S);
        right = Input.GetKeyDown(KeyCode.D);
        left = Input.GetKeyDown(KeyCode.A);

    }

    void Pathfind(Vector3 tp) {
        int x = Mathf.RoundToInt(tp.x);
        int y = Mathf.RoundToInt(tp.y);

        // targetObj.position = new Vector3(playerObj.position.x + x, playerObj.position.y, playerObj.position.z);
        targetX = playerObj.position.x + x;
        targetY = playerObj.position.y + y;
        isLerping = true;
    }

    void LerpToPos() {

        if (!initLerp) {
            t = 0;
            startP = playerObj.position;
            targetP = new Vector3(targetX, targetY);

            speedActual = baseSpeed;
            Vector3 scale = Vector3.one;

            playerObj.localScale = scale;
            initLerp = true;
        }

        t += Time.deltaTime * speedActual;
        if (t > 1)
        {
            t = 1;
            isLerping = false;
            initLerp = false;
        }
        float y = moveCurve.Evaluate(t);
        y *= curveScale;

        Vector3 tp = Vector3.Lerp(startP, targetP, t);
        if (yCurve)
        {
            tp.y += y;
        }
        playerObj.position = tp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall") {
            Debug.Log("Detect Walls");
            // playerObj.position = previousP;
            isLerping = true;
            // startP = playerObj.position;
            // targetP = previousP;
            initLerp = false;
            LerpBack();
        }
    }

    void LerpBack() {

        if (!initLerp)
        {
            t = 0;
            startP = playerObj.position;
            targetP = previousP;

            speedActual = baseSpeed;
            Vector3 scale = Vector3.one;

            playerObj.localScale = scale;
            initLerp = true;
        }

        t += Time.deltaTime * speedActual;
        if (t > 1)
        {
            t = 1;
            isLerping = false;
            initLerp = false;
        }
        float y = moveCurve.Evaluate(t);
        y *= curveScale;

        Vector3 tp = Vector3.Lerp(startP, targetP, t);

        if (yCurve) {
            tp.y += y;
        } 
        playerObj.position = tp;
    }
}
