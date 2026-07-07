using UnityEngine;

[CreateAssetMenu(fileName = "NewHazardData", menuName = "Gameplay/Hazard Data")]
public class HazardData : ScriptableObject
{
    //public float KnockBackDuration = 0.5f;
    public float rotationSpeed = 180.0f;
    public int damageAmount = 10;
}
