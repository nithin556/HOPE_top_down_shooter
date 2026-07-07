using UnityEngine;
using UnityEngine.UIElements; // Required for UI Toolkit

public class HealthBarController : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private ProgressBar healthBar;

    [SerializeField] private GameManager gameManager;

    void Awake()
    {
        // 1. Get the new PanelRenderer component
        panelRenderer = GetComponent<PanelRenderer>();
    }

    void OnEnable()
    {
        // 2. Register to the brand new 6.5 reload callback
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
        gameManager.HealthChangeUI += OnHealthChangeUI;
    }

    void OnDisable()
    {
        // Always clean up your callbacks when disabled
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
        gameManager.HealthChangeUI -= OnHealthChangeUI;
    }

    // 3. Unity automatically passes the live 'root' element into this function
    private void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        // 4. Safely query your elements here!
        // NOTE: Make sure "ProgressBar" matches the exact Name property in UI Builder
        healthBar = root.Q<ProgressBar>("HealthBar");
    }
    void OnHealthChangeUI(object sender, HealthEventDataPass healthEventDataPass)
    {
        healthBar.value = healthEventDataPass.playerHealthGM;
    }
}