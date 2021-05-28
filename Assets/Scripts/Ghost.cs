using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ISpawn
{
    public float moveSpeed = 6f;
    public float frightenedModeMoveSpeed = 3.5f;

    public int pinkyReleaseTimer = 5;
    public int inkyReleaseTimer = 14;
    public int clydeReleaseTimer = 21;
    public float ghostReleaseTimer = 0;

    public int frightenedModeDuration = 10;
    public int startBlinkingAt = 7;

    public bool isInGhostHouse = false;

    private Node startingPosition;

    public int scatterModeTimer1 = 7;
    public int chaseModeTimer1 = 20;
    public int scatterModeTimer2 = 7;
    public int chaseModeTimer2 = 20;
    public int scatterModeTimer3 = 5;
    public int chaseModeTimer3 = 20;
    public int scatterModeTimer4 = 5;

    public RuntimeAnimatorController ghost;
    public RuntimeAnimatorController ghostWhite;
    public RuntimeAnimatorController ghostBlue;

    private int modeChangeIteration = 1;
    private float modeChangeTimer = 0;

    private float frightenedModeTimer = 0;
    private float blinkTimer = 0;

    private bool frightenedModeIsWhite = false;

    private float previousMoveSpeed;

    //enum Mode
    public enum Mode
    {
        Chase,
        Scatter,
        Frightened 
    }

    public Mode currentMode = Mode.Scatter;
    Mode previousMode;

    //enum Type Ghost
    public enum GhostType
    {
        Red,
        Pink,
        Blue,
        Orange,
    }

    public GhostType ghostType = GhostType.Red;

    private GameObject pacMan;

    private Node currentNode, targetNode, previousNode;
    private Vector2 direction;

    void Start()
    {
        pacMan = FindObjectOfType<Pacman>().gameObject;
        startingPosition = SetStartingPosition();

        Node node = startingPosition;
        SpawnPosition(startingPosition.transform.position);

        if (node != null)
        {
            currentNode = node;
        }

        if (isInGhostHouse)
        {
            targetNode = currentNode.aroundNodes[0];
            direction = SetDirection();
        }
        else
        {
            direction = Vector2.left;
            targetNode = ChooseNextNode();
        }

        previousNode = currentNode;
        UpdateAnimatorController();
    }

    //fungsi yang dipanggil pada Game Manager
    public void Execute()
    {
        ModeUpdate();
        Move();
        ReleaseGhosts();
    }

    //untuk update kontrol animasi 
    void UpdateAnimatorController()
    {
        if (currentMode != Mode.Frightened)
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = ghost;
        }
        else
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = ghostBlue;
        }
    }

    //mengatur movement ghost
    void Move()
    {
        if (targetNode != currentNode && targetNode != null && !isInGhostHouse)
        {
            if (OverShotTarget ())
            {
                currentNode = targetNode;

                transform.localPosition = currentNode.transform.position;

                targetNode = ChooseNextNode();
                previousNode = currentNode;
                currentNode = null;
                UpdateAnimatorController();
            }

            else
            {
                transform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
            }

        }
    }

    //untuk cek mode yang sedang aktif
    void ModeUpdate()
    {
        if (currentMode != Mode.Frightened)
        {
            modeChangeTimer += Time.deltaTime;
            
            if (modeChangeIteration == 1)
            {
                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer1)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }

                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer1)
                {
                    modeChangeIteration = 2;
                    ChangeMode(Mode.Scatter);
                    modeChangeTimer = 0;
                }
            }

            else if (modeChangeIteration == 2 ) 
            {
                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer2)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }

                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer2)
                {
                    modeChangeIteration = 3;
                    ChangeMode(Mode.Scatter);
                    modeChangeTimer = 0;
                }
            }
            
            else if (modeChangeIteration == 3)
            {
                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer3)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }

                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer3)
                {
                    modeChangeIteration = 4;
                    ChangeMode(Mode.Scatter);
                    modeChangeTimer = 0;
                }
            }

            else if (modeChangeIteration == 4)
            {
                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer4)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }
            }
        }

        else if (currentMode == Mode.Frightened) 
        {
            frightenedModeTimer += Time.deltaTime;

            if (frightenedModeTimer >= frightenedModeDuration)
            {
                frightenedModeTimer = 0;
                ChangeMode(previousMode);
            }

            if (frightenedModeTimer >= startBlinkingAt)
            {
                blinkTimer += Time.deltaTime;

                if (blinkTimer >= 0.1f)
                {
                    blinkTimer = 0f;

                    if (frightenedModeIsWhite)
                    {
                        transform.GetComponent<Animator>().runtimeAnimatorController = ghostBlue;
                        frightenedModeIsWhite = false;
                    }

                    else
                    {
                        transform.GetComponent<Animator>().runtimeAnimatorController = ghostWhite;
                        frightenedModeIsWhite = true;
                    }
                }
            }
        }
    }

    //untuk mengubah mode ghost sesuai kondisi tertentu
    void ChangeMode (Mode m)
    {
        if (currentMode == Mode.Frightened)
        {
            moveSpeed = previousMoveSpeed;
        }

        if (m == Mode.Frightened)
        {
            previousMoveSpeed = moveSpeed;
            moveSpeed = frightenedModeMoveSpeed;
        }

        previousMode = currentMode;
        currentMode = m;

        if(previousMode == Mode.Frightened)
        {
            previousMode = Mode.Chase;
        }

        UpdateAnimatorController();
    }

    //setting direction berdasarkan type ghost
    Vector2 SetDirection()
    {
        if (ghostType == GhostType.Orange)
        {
            direction = Vector2.left;
        }
        else if (ghostType == GhostType.Pink)
        {
            direction = Vector2.right;
        }
        else if (ghostType == GhostType.Blue)
        {
            direction = Vector2.up;
        }
        else if (ghostType == GhostType.Red)
        {
            direction = Vector2.left;
        }

        return direction;
    }

    //setting starting position node berdasarkan type ghost
    Node SetStartingPosition()
    {
        if (ghostType == GhostType.Orange)
        {
            startingPosition = GameObject.Find("OrangePos").GetComponent<Node>();
        }
        else if (ghostType == GhostType.Pink)
        {
            startingPosition = GameObject.Find("PinkPos").GetComponent<Node>();
        }
        else if (ghostType == GhostType.Blue)
        {
            startingPosition = GameObject.Find("BluePos").GetComponent<Node>();
        }
        else if (ghostType == GhostType.Red)
        {
            startingPosition = GameObject.Find("RedPos").GetComponent<Node>();
        }
        return startingPosition;
    }

    public void StartFrightenedMode()
    {
        ChangeMode(Mode.Frightened);
    }

    #region Target Node Position
    Vector2 GetRedGhostTargetTile()
    {
        //mengejar pacman
        Vector2 pacManPosition = pacMan.transform.localPosition;
        Vector2 targetTile = new Vector2(Mathf.RoundToInt(pacManPosition.x), Mathf.RoundToInt(pacManPosition.y));

        return targetTile;
    }

    Vector2 GetPinkGhostTargetTile()
    {
        //berjarak 4 tile di depan pacman
        // Four tiles ahead Pacman
        // Taking account Position and Orientation

        Vector2 pacManPosition = pacMan.transform.localPosition;
        Vector2 pacManOrientation = pacMan.GetComponent<Pacman>().GetMovementValue();

        int pacManPositionX = Mathf.RoundToInt(pacManPosition.x);
        int pacManPositionY = Mathf.RoundToInt(pacManPosition.y);

        Vector2 pacManTile = new Vector2(pacManPositionX, pacManPositionY);
        Vector2 targetTile = pacManTile + (4 * pacManOrientation);

        return targetTile;
    }

    Vector2 GetBlueGhostTargetTile()
    {
        // Select the position two tiles in front of Pacman
        // Draw vector from blinky to that position
        // Duble the length of the vector

        Vector2 pacManPosition = pacMan.transform.localPosition;
        Vector2 pacManOrientation = pacMan.GetComponent<Pacman>().GetMovementValue();

        int pacManPositionX = Mathf.RoundToInt(pacManPosition.x);
        int pacManPositionY = Mathf.RoundToInt(pacManPosition.y);

        Vector2 pacManTile = new Vector2(pacManPositionX, pacManPositionY);
        Vector2 targetTile = pacManTile + (2 * pacManOrientation);

        // Temporary Blinky Position

        Vector2 blinkyPosition = GameObject.Find("GhostBlue(Clone)").transform.localPosition;

        int tempBlinkyPositionX = Mathf.RoundToInt(blinkyPosition.x);
        int tempBlinkyPositionY = Mathf.RoundToInt(blinkyPosition.y);

        blinkyPosition = new Vector2(tempBlinkyPositionX, tempBlinkyPositionY);

        float distance = GetDistance(blinkyPosition, targetTile);
        distance *= 2;

        targetTile = new Vector2(blinkyPosition.x + distance, blinkyPosition.y + distance);

        return targetTile;
    }

    Vector2 GetOrangeGhostTargetTile()
    {
        // Calculate the distance from Pacman
        // If the distance is greather than eight tiles targeting is the same as Blinky
        // If the distance is less than eight tiles, then target is his home node, so same as scatter mode.

        Vector2 pacManPosition = pacMan.transform.localPosition;

        float distance = GetDistance(transform.localPosition, pacManPosition);
        Vector2 targetTile = Vector2.zero;

        if (distance > 8)
        {
            targetTile = new Vector2(Mathf.RoundToInt(pacManPosition.x), Mathf.RoundToInt(pacManPosition.y));
        }
        else if (distance < 8)
        {
            targetTile = startingPosition.transform.position;
        }

        return targetTile;
    }

    //untuk mendapatkan posisi target node selanjutnya
    Vector2 GetTargetTile()
    {
        Vector2 targetTile = Vector2.zero;
        
        if (ghostType == GhostType.Red)
        {
            targetTile = GetRedGhostTargetTile();
        }
        else if (ghostType == GhostType.Pink) 
        {
            targetTile = GetPinkGhostTargetTile();
        }
        else if (ghostType == GhostType.Blue)
        {
            targetTile = GetBlueGhostTargetTile();
        }
        else if (ghostType == GhostType.Orange)
        {
            targetTile = GetOrangeGhostTargetTile();
        }
        return targetTile;
    }
    #endregion

    #region Release Ghost
    void ReleasePinkGhost()
    {
        if (ghostType == GhostType.Pink && isInGhostHouse)
        {
            isInGhostHouse = false;
        }
    }

    void ReleaseBlueGhost()
    {
        if (ghostType == GhostType.Blue && isInGhostHouse)
        {
            isInGhostHouse = false;
        }
    }

    void ReleaseOrangeGhost()
    {
        if (ghostType == GhostType.Orange && isInGhostHouse)
        {
            isInGhostHouse = false;
        }
    }

    void ReleaseGhosts()
    {
        ghostReleaseTimer += Time.deltaTime;

        if (ghostReleaseTimer > pinkyReleaseTimer)
            ReleasePinkGhost();

        if (ghostReleaseTimer > inkyReleaseTimer)
            ReleaseBlueGhost();

        if (ghostReleaseTimer > clydeReleaseTimer)
            ReleaseOrangeGhost();
    }
    #endregion

    //untuk menentukan node yang akan dituju selanjutnya
    Node ChooseNextNode()
    {
        Vector2 targetTile = Vector2.zero;

        if (currentMode == Mode.Chase)
        {
            targetTile = GetTargetTile();
        }
        else if (currentMode == Mode.Scatter)
        {
            targetTile = startingPosition.transform.position;
        }

        Node moveToNode = null;

        Node[] foundNodes = new Node[4];
        Vector2[] foundNodesDirection = new Vector2[4];

        int nodeCounter = 0;

        for (int i = 0; i < currentNode.aroundNodes.Length; i++)
        {
            if (currentNode.direction [i] !=  direction * -1)
            {
                foundNodes [nodeCounter] = currentNode.aroundNodes[i];
                foundNodesDirection[nodeCounter] = currentNode.direction[i];
                nodeCounter++;
            }
        }

        if (foundNodes.Length == 1)
        {
            moveToNode = foundNodes [0];
            direction = foundNodesDirection [0]; 
        }

        if (foundNodes.Length > 1)
        {
            float leastDistance = 100000f;

            for (int i = 0; i < foundNodes.Length; i++)
            {
                if (foundNodesDirection [i] != Vector2.zero)
                {
                    float distance = GetDistance(foundNodes[i].transform.position, targetTile);
                    if (distance < leastDistance)
                    {
                        leastDistance = distance;
                        moveToNode = foundNodes[i];
                        direction = foundNodesDirection[i];
                    }
                }
            }
        }

        return moveToNode;
    }

    //untuk reset posisi ghost setelah termakan oleh pacman pada mode frightened
    public void ResetPosition()
    {
        transform.position = startingPosition.transform.position;
        currentNode = startingPosition;
        targetNode = ChooseNextNode();
        direction = SetDirection();
    }

    //untuk menghitung jarak node tertentu dari suatu posisi
    float LengthFromNode (Vector2 targetPosition)
    {
        Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
        return vec.sqrMagnitude;
    }

    //untuk mendeteksi apakah ghost sudah mencapai target node atau belum
    bool OverShotTarget ()
    {
        float nodeToTarget = LengthFromNode (targetNode.transform.position);
        float nodeToSelf = LengthFromNode (transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    //untuk menghitung jarak antara dua posisi
    float GetDistance (Vector2 posA, Vector2 posB)
    {
        float dx = posA.x - posB.x;
        float dy = posA.y - posB.y;

        float distance = Mathf.Sqrt (dx * dx + dy * dy);

        return distance;
    }

    //implementasi fungsi dari interface ISpawn.
    //untuk mengatur posisi ghost saat spawn
    public void SpawnPosition(Vector2 pos)
    {
        transform.position = pos;
    }
}
