using UnityEngine;
using UnityEngine.UI;
public class EventTest2 : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public void OnCanvasTest()
    {
        _canvas.sortingOrder = 5;
    }
}
