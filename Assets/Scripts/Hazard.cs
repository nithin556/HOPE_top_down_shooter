using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private HazardData dataProfile;
    public HazardData Data => dataProfile;
}
