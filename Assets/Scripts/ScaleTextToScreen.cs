using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is for the Start Menu to scale 
/// the title text to the Start menu.
/// 
/// This is to facilitate the use of fonts to stylize
/// the text of the game.
/// </summary>
public class ScaleTitle : MonoBehaviour
{
    RectTransform rt;
    public float maxHeightRatio = 0.3f;
    public float maxWidthRatio = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rt_p = this.transform.parent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2 (
            rt_p.rect.width * maxWidthRatio,
            rt_p.rect.height * maxHeightRatio
        );
    }
}
