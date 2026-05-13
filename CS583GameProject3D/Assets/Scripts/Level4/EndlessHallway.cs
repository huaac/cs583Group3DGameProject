using Unity.VisualScripting;
using UnityEngine;

public class EndlessHallway : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject repeatedSectionPrefab;
    public Transform endPiece;
    public Transform killZone;
    public GameObject errorMessage;

    [Header("Settings")]
    private float sectionLength = 15;
    public float endDistanceAhead = 60f;
    public int startingSections = 3;

    private float nextSpawnZ;
    private int spawnedSections;
    private bool endReached = false;

    //sets variables
    void Start()
    {
        spawnedSections = startingSections;
        nextSpawnZ = sectionLength * startingSections;
        //nextSpawnZ += nextSpawnZ;
        MoveEndPiece();
        Debug.Log(nextSpawnZ);
    }

    
    void Update()
    {
        if (PlayerInfo.dialogTriggered.ContainsKey("Level4NeverendingTunnel4"))
        {
            if (PlayerInfo.dialogTriggered["Level4NeverendingTunnel4"] == false)
            {
                MoveEndPiece();
            }
            else
            {
                endOfHallReached();
            }
        }
        else
        {
            MoveEndPiece();
        }
    }

    //spawns the 2nd to last section of the hallway at the position in front of the 2nd section
    public void SpawnNextSection()
    {
        Vector3 spawnPos = new Vector3(
            repeatedSectionPrefab.transform.position.x,
            repeatedSectionPrefab.transform.position.y,
            nextSpawnZ
        );

        Instantiate(repeatedSectionPrefab, spawnPos, repeatedSectionPrefab.transform.rotation);

        nextSpawnZ += sectionLength;
        spawnedSections++;
    }

    //moves the end piece at a certain distance from the player
    void MoveEndPiece()
    {
        Vector3 endPos = endPiece.position;
        endPos.z = player.position.z + endDistanceAhead;
        endPiece.position = endPos;
        endPos.y = -6.4f;
        killZone.position = endPos;
    }

    void endOfHallReached()
    {
        if (endReached == false)
        {
            endReached = true;
            errorMessage.SetActive(true);
        }
    }
}