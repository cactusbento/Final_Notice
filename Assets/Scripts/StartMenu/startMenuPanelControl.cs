using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMenuPanelControl : MonoBehaviour
{
    RectTransform rt;

    Vector2 anchor = new(0.5f, 0f);
    const float heightRatio = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float newHeight = Screen.height * heightRatio;
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.anchoredPosition = new Vector2(
            0.0f,
            newHeight
        );
        rt.sizeDelta = new Vector2(
            Screen.width,
            newHeight
        );
        
        RectTransform parent = GetComponent<RectTransform>();
        foreach (RectTransform child in parent) {
            child.localScale = new Vector3(1,1,1);
            child.sizeDelta = new Vector2(
                child.rect.width,
                newHeight
            );
        }
    }
}
