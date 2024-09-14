using UnityEngine;

public class UIScalingModule : MonoBehaviour
{
    private RectTransform canvasTransform;
    private Camera mainCam;
    private static readonly Vector2 refRes = new Vector2(1280, 720);

    private void Awake()
    {
        canvasTransform = GetComponent<RectTransform>();
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        //var height = 2f * mainCam.orthographicSize;
        var logWidth = Mathf.Log(mainCam.pixelWidth / 1280, 2f);
        canvasTransform.sizeDelta = new Vector2(mainCam.pixelWidth, mainCam.pixelHeight) * Mathf.Pow(2, logWidth);
    }
}
