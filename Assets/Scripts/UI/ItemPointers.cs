using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemPointers : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Camera uiCamera;

    Transform nearestPackage;

    [SerializeField]
    Text packageText;

    [SerializeField]
    Text baseText;

    [SerializeField]
    Text bothBaseText;

    [SerializeField]
    Text bothPackageText;

    GameObject bothPointer;

    GameObject packagePointer;

    GameObject basePointer;

    RectTransform packageRectTransform;

    RectTransform baseRectTransform;

    RectTransform bothRectTransform;

    [SerializeField]
    RectTransform bothContainer;

    Image baseImage;

    [SerializeField]
    float borderSize = 2;

    [SerializeField]
    float borderTopSize = 4;

    [SerializeField]
    float borderBottomSize = 10;

    Image packageImage;

    [SerializeField]
    Sprite arrow;

    [SerializeField]
    Transform homeBase;

    [SerializeField]
    RectTransform canvas;

    void Start()
    {
        DetermineSize();
        bothPointer = transform.Find("Both Pointer").gameObject;
        packagePointer = transform.Find("Package Pointer").gameObject;
        packageRectTransform = packagePointer.GetComponent<RectTransform>();
        packageImage = packagePointer.GetComponent<Image>();
        basePointer = transform.Find("Base Pointer").gameObject;
        baseRectTransform = basePointer.GetComponent<RectTransform>();
        baseImage = transform.Find("Base Pointer").GetComponent<Image>();
        bothRectTransform = transform.Find("Both Pointer").GetComponent<RectTransform>();
    }

    public void DetermineSize()
    {
        while (CountCornersVisibleFrom(canvas, uiCamera) < 4)
        {
            uiCamera.orthographicSize++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PackageFinder();
        BaseFinder();

        float dist = Vector2.Distance(packageRectTransform.position, baseRectTransform.position);
        if (dist < 100)
        {
            BothFinder();
        }
        else
        {
            bothPointer.SetActive(false);
            packagePointer.SetActive(true);
            basePointer.SetActive(true);
        }
    }

    void BaseFinder()
    {
        Vector3 targetPositionScreenPointer = Camera.main.WorldToScreenPoint(homeBase.position);
        bool isOffscreen = targetPositionScreenPointer.x <= 0 || targetPositionScreenPointer.x >= Screen.width || targetPositionScreenPointer.y <= 0 || targetPositionScreenPointer.y >= Screen.height;

        if (isOffscreen)
        {
            Rotate(baseRectTransform, homeBase.position);
            baseImage.enabled = true;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPointer;

            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderBottomSize) cappedTargetScreenPosition.y = borderBottomSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderTopSize) cappedTargetScreenPosition.y = Screen.height - borderTopSize;       

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            baseRectTransform.position = pointerWorldPosition;
            baseRectTransform.localPosition = new Vector3(baseRectTransform.localPosition.x, baseRectTransform.localPosition.y, 0f);
            baseText.text = ((int) Vector2.Distance(player.position, homeBase.position)).ToString() + "m";
            baseText.transform.eulerAngles = Vector3.zero;
        }
        else
        {
            baseImage.enabled = false;
            baseText.text = "";
        }
    }

    void PackageFinder()
    {
        Vector3 targetPositionScreenPointer = Camera.main.WorldToScreenPoint(nearestPackage.position);
        bool isOffscreen = targetPositionScreenPointer.x <= 0 || targetPositionScreenPointer.x >= Screen.width || targetPositionScreenPointer.y <= 0 || targetPositionScreenPointer.y >= Screen.height;

        if (isOffscreen)
        {
            Rotate(packageRectTransform, nearestPackage.position);
            packageImage.enabled = true;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPointer;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderBottomSize) cappedTargetScreenPosition.y = borderBottomSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderTopSize) cappedTargetScreenPosition.y = Screen.height - borderTopSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            packageRectTransform.position = pointerWorldPosition;
            packageRectTransform.localPosition = new Vector3(packageRectTransform.localPosition.x, packageRectTransform.localPosition.y, 0f);
            packageText.text = ((int)Vector2.Distance(player.position, nearestPackage.position)).ToString() + "m";
            packageText.transform.eulerAngles = Vector3.zero;
        }
        else
        {
            packageImage.enabled = false;
            packageText.text = "";
        }
    }

    void BothFinder()
    {
        bothPointer.SetActive(true);
        packagePointer.SetActive(false);
        basePointer.SetActive(false);
        bothRectTransform.position = baseRectTransform.position;
        bothRectTransform.localPosition = baseRectTransform.localPosition;
        Rotate(bothRectTransform, homeBase.position);
        bothPackageText.text = packageText.text;
        bothBaseText.text = baseText.text;
        bothContainer.transform.eulerAngles = Vector3.zero;
    }

    public void UpdatePackage(Transform newPackage)
    {
        nearestPackage = newPackage;
    }

    void Rotate(RectTransform rect, Vector3 toPosition)
    {
        toPosition.z = 0f;
        Vector3 dir = (toPosition - player.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        rect.localEulerAngles = new Vector3(0, 0, angle);
    }

    int CountCornersVisibleFrom(RectTransform rectTransform, Camera camera)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objectCorners = new Vector3[4];
        rectTransform.GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        Vector3 tempScreenSpaceCorner; // Cached
        for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
        {
            tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
            if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
            {
                visibleCorners++;
            }
        }
        return visibleCorners;
    }
}
