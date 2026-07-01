using System;
using UnityEngine;

public class ForcePushBack : MonoBehaviour
{
    public event EventHandler OnHitRotator;

    void OnTriggerEnter(Collider other)
    {
        OnHitRotator?.Invoke(this,EventArgs.Empty);
    }

}
