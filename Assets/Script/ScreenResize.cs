using UnityEngine;
using System.Collections;

public class ScreenResize : MonoBehaviour {
    
    void Start()
    {
        Resize();
    }
    void Resize()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        Vector3 xWidth = transform.localScale;
        xWidth.x = - Screen.width / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = - Screen.height / height;
        transform.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }
}
