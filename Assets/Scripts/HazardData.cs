using UnityEngine;

[CreateAssetMenu(fileName = "NewHazardData", menuName = "Gameplay/Hazard Data")]
public class HazardData : ScriptableObject
{
    public float damageAmount = 10.0f;
    public float invincibility_Time = 0.0f;
    [Range(0f, 1f)]
    public float slow_Movement_Multiplier = 0.3f;
    public float slow_Movement_Duration = 2.0f;
}
