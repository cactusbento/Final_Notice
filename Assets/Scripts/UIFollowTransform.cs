using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackPlayer : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] Vector3 offset;
    RectTransform rt;
    Camera mainCamera;
    Vector3 targetPosition;

    void Start()
    {
        mainCamera = Camera.main;
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = mainCamera.WorldToScreenPoint(target.position) + offset;
        rt.position = targetPosition;
    }
}
