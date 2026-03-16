using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int correctPositionIndex;
    public int currentPositionIndex;

    private PuzzleManager puzzleManager;
    private RectTransform rectTransform;
    private Canvas canvas;

    public Vector2 startPosition;

    public void Init(int correctIndex, PuzzleManager manager)
    {
        correctPositionIndex = correctIndex;
        currentPositionIndex = correctIndex;
        puzzleManager = manager;

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool moveSuccessful = puzzleManager.TryMoveTile(this);

        if (!moveSuccessful)
        {
            rectTransform.anchoredPosition = startPosition;
        }
    }
    public void MoveToPosition(Vector2 newPosition)
    {
        rectTransform.anchoredPosition = newPosition;
    }
}