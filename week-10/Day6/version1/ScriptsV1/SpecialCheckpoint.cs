using UnityEngine;

public class SpecialCheckpoint : MonoBehaviour
{
    public enum CheckpointType { Normal, Teleport, Final }
    public CheckpointType type = CheckpointType.Normal;
    public Transform teleportTarget;
}
