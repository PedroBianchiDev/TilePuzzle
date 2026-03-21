using TilePuzzle.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TilePuzzle.Gameplay
{
    public class Tile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public int correctPositionIndex;
        public int currentPositionIndex;

        private Image image;
        private PuzzleManager puzzleManager;
        private RectTransform rectTransform;

        [SerializeField] private Canvas canvas;

        private Transform originalParent;
        public Vector2 startPosition;
        private Vector2 offset;

        public void Init(int correctIndex, PuzzleManager manager, Sprite sprite)
        {
            image = GetComponent<Image>();
            image.sprite = sprite;

            correctPositionIndex = correctIndex;
            currentPositionIndex = correctIndex;
            puzzleManager = manager;

            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!puzzleManager.isGameActive) return;

            startPosition = rectTransform.anchoredPosition;
            originalParent = transform.parent;

            transform.SetParent(puzzleManager.gameplayObject);
            transform.SetAsLastSibling();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            offset = rectTransform.anchoredPosition - localPoint;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!puzzleManager.isGameActive) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            rectTransform.anchoredPosition = localPoint + offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!puzzleManager.isGameActive) return;

            transform.SetParent(originalParent);

            bool moveSuccessful = puzzleManager.TryMoveTile(this);

            if (!moveSuccessful)
            {
                rectTransform.anchoredPosition = startPosition;
            }
            else
            {
                //Core.Application.Instance.GetService<AudioService>().PlaySFX("MovTile");
            }
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            if (!puzzleManager.isGameActive) return;

            rectTransform.anchoredPosition = newPosition;
        }
    }
}