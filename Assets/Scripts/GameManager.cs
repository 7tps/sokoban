using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public GameObject[] crates;
    public Vector2Int[] boundariesInt;
    private Boundary[] boundaries;
    public Transform[] crateTargets;
    public Objective objective;
    public Spikes spikes;

    public bool allCratesPlaced = false;
    
    private float previousHorizontal = 0f;
    private float previousVertical = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (boundariesInt != null && boundariesInt.Length > 0)
        {
            boundaries = new Boundary[boundariesInt.Length / 2];
            for (int i = 0; i < boundariesInt.Length; i += 2)
            {
                boundaries[i / 2] = new Boundary(boundariesInt[i].x, boundariesInt[i].y, boundariesInt[i+1].x, boundariesInt[i+1].y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandleInput(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleInput(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HandleInput(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandleInput(Vector2.right);
        }
        */
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (Mathf.Abs(horizontal) > 0.5f || Mathf.Abs(vertical) > 0.5f)
        {
            bool isNewHorizontalPress = Mathf.Abs(horizontal) > 0.5f && Mathf.Abs(previousHorizontal) < 0.5f;
            bool isNewVerticalPress = Mathf.Abs(vertical) > 0.5f && Mathf.Abs(previousVertical) < 0.5f;
            
            if (isNewHorizontalPress || isNewVerticalPress)
            {
                if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                {
                    if (horizontal > 0.5f)
                    {
                        HandleInput(Vector2.right);
                    }
                    else if (horizontal < -0.5f)
                    {
                        HandleInput(Vector2.left);
                    }
                }
                else
                {
                    if (vertical > 0.5f)
                    {
                        HandleInput(Vector2.up);
                    }
                    else if (vertical < -0.5f)
                    {
                        HandleInput(Vector2.down);
                    }
                }
            }
        }
        
        previousHorizontal = horizontal;
        previousVertical = vertical;


        if (!allCratesPlaced)
        {
            bool allCratesPlacedCheck = true;
            for (int i = 0; i < crates.Length; i++)
            {
                if (crates[i] != null)
                {
                    if (!(crates[i].transform == crateTargets[i].transform))
                    {
                        allCratesPlacedCheck = false;
                        break;
                    }
                }
            }
            allCratesPlaced = allCratesPlacedCheck;
        }
        else
        {
            spikes.LowerSpikes();
        }
    }
    
    void HandleInput(Vector2 direction)
    {
        float gridSize = 1f;
        
        foreach (GameObject crate in crates)
        {
            if (crate == null) continue;
            
            Vector2 currentPos = crate.transform.position;
            Vector2 targetPos = currentPos + direction * gridSize;
            
            crate.transform.position = targetPos;
            Physics2D.SyncTransforms();
            
            bool isColliding = IsCollidingWithBoundaries(crate);
            
            if (isColliding)
            {
                crate.transform.position = currentPos;
                Physics2D.SyncTransforms();
            }
        }
    }

    bool IsCollidingWithBoundaries(GameObject crate)
    {
        if (boundaries == null) return false;
        
        foreach (Boundary b in boundaries)
        {
            if (b.contains(crate.transform))
            {
                Debug.Log($"Collision detected: {crate.name} colliding with boundary at position {crate.transform.position}");
                return true;
            }
        }
        
        foreach (GameObject otherCrate in crates)
        {
            if (otherCrate != null && otherCrate != crate)
            {
                float distance = Vector2.Distance(crate.transform.position, otherCrate.transform.position);
                if (distance < 0.1f)
                {
                    Debug.Log($"Collision detected: {crate.name} colliding with {otherCrate.name} at position {crate.transform.position}");
                    return true;
                }
            }
        }

        return false;
    }
}

[System.Serializable]
public class Boundary
{
    int x1, x2, y1, y2;

    public Boundary(int x1, int y1, int x2, int y2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;
    }
    
    public bool contains(Transform t)
    {
        return x1 < t.position.x && t.position.x < x2 && y1 < t.position.y && t.position.y < y2;
    }
}
