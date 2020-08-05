using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PrepareTrap : MonoBehaviour
{
    public float trapSize;
    public float colliderSize;
    public BoxCollider2D b2d;

    public GameObject trapPart;

#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying) return;
        b2d.size = new Vector2(trapSize * colliderSize, colliderSize);
        b2d.offset = new Vector2(-(trapSize * colliderSize / 2) - .75f, 0f);
    }
#endif
}
