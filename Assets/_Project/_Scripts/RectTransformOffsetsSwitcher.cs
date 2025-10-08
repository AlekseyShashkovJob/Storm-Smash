using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class RectTransformOffsetsSwitcher : MonoBehaviour
{
    [Header("Portrait: Left, Right, Top, Bottom")]
    public Vector4 portraitOffsets = new Vector4(174.0243f, 174.0243f, 367.3848f, 367.3848f);

    [Header("Landscape: Left, Right, Top, Bottom")]
    public Vector4 landscapeOffsets = new Vector4(367.3848f, 367.3848f, 174.0243f, 174.0243f);

    private RectTransform _rectTransform;
    private Vector2 _lastScreenSize;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        ApplyOffsets();
    }

    private void Start()
    {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        ApplyOffsets();
    }

    private void Update()
    {
        Vector2 currentSize = new Vector2(Screen.width, Screen.height);
        if (currentSize != _lastScreenSize)
        {
            _lastScreenSize = currentSize;
            ApplyOffsets();
        }
    }

    private void ApplyOffsets()
    {
        bool isPortrait = Screen.height >= Screen.width;
        Vector4 offsets = isPortrait ? portraitOffsets : landscapeOffsets;
        _rectTransform.offsetMin = new Vector2(offsets.x, offsets.w);      // Left, Bottom
        _rectTransform.offsetMax = new Vector2(-offsets.y, -offsets.z);    // -Right, -Top
    }
}