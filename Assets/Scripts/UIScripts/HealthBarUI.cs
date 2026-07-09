using System;
using UnityEngine;
using UnityEngine.UIElements; // Required for UI Toolkit

public class HealthBarController : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private ProgressBar healthBar;
    private Label x;

    [SerializeField] private PlayerHealth playerHealth;

    void Awake()
    {
        panelRenderer = GetComponent<PanelRenderer>();
    }

    void OnEnable()
    {
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
        playerHealth.HealthChange += OnHealthChangeUI;
    }

    void OnDisable()
    {
        // Always clean up your callbacks when disabled
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
        playerHealth.HealthChange -= OnHealthChangeUI;
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        healthBar = root.Q<ProgressBar>("HealthBar");
        x = root.Q<Label>("Time");
    }
    void Update()
    {
        x.text = Time.time.ToString();
    }
    void OnHealthChangeUI(object sender, EventArgs eventArgs)
    {
        healthBar.value = playerHealth.health;
    }
}