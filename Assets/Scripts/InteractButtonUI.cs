using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonUI : MonoBehaviour
{
    public Canvas InteractCanvas;
    private Canvas SpawnedCanvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Canvas temp = Instantiate(InteractCanvas);
            RectTransform rectT = temp.GetComponent<RectTransform>();
            rectT.position = new Vector2(transform.position.x, transform.position.y + 2f);
            SpawnedCanvas = temp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Destroy(SpawnedCanvas.gameObject);
    }
}
