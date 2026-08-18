using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Lines")]
    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    [Header("Crosshair Settings")]
    public float normalSpread = 10f;
    public float shootSpread = 20f;
    public float returnSpeed = 10f;

    private float currentSpread;

    void Start()
    {
        currentSpread = normalSpread;
        UpdateCrosshair();
    }

    void Update()
    {
        // Slowly return to normal size
        currentSpread = Mathf.Lerp(
            currentSpread,
            normalSpread,
            Time.deltaTime * returnSpeed
        );

        UpdateCrosshair();
    }

    public void OnShoot()
    {
        // Expand crosshair when shooting
        currentSpread = shootSpread;
    }

    void UpdateCrosshair()
    {
        top.anchoredPosition = new Vector2(0, currentSpread);
        bottom.anchoredPosition = new Vector2(0, -currentSpread);
        left.anchoredPosition = new Vector2(-currentSpread, 0);
        right.anchoredPosition = new Vector2(currentSpread, 0);
    }
}