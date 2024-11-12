#region Includes
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace TS.PageSlider
{
    /// <summary>
    /// The PageScroller class manages scrolling within a PageSlider component. 
    /// It handles user interaction for swiping between pages and snapping to the closest page on release.
    /// </summary>
    public class PageScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        #region Variables

        [Header("Configuration")]

        /// <summary>
        /// Minimum delta drag required to consider a page change (normalized value between 0 and 1).
        /// </summary>
        [Tooltip("Minimum delta drag required to consider a page change (normalized value between 0 and 1)")]
        [SerializeField] private float _minDeltaDrag = 0.1f;

        /// <summary>
        /// Duration (in seconds) for the page snapping animation.
        /// </summary>
        [Tooltip("Duration (in seconds) for the page snapping animation")]
        [SerializeField] private float _snapDuration = 0.3f;

        [Header("Events")]

        /// <summary>
        /// Event triggered when a page change starts. 
        /// The event arguments are the index of the current page and the index of the target page.
        /// </summary>
        [Tooltip("Event triggered when a page change starts: index current page, index of target page")]
        public UnityEvent<int, int> OnPageChangeStarted;

        /// <summary>
        /// Event triggered when a page change ends. 
        /// The event arguments are the index of the current page and the index of the new active page.
        /// </summary>
        [Tooltip("Event triggered when a page change ends: index of the current page, index of the new active page")]
        public UnityEvent<int, int> OnPageChangeEnded;

        /// <summary>
        /// Gets the rectangle of the ScrollRect component used for scrolling.
        /// </summary>
        public Rect Rect
        {
            get
            {
#if UNITY_EDITOR
                if (_scrollRect == null)
                {
                    _scrollRect = FindScrollRect();
                }
#endif
                return ((RectTransform)_scrollRect.transform).rect;
            }
        }

        /// <summary>
        /// Gets the RectTransform of the content being scrolled within the ScrollRect.
        /// </summary>
        public RectTransform Content
        {
            get
            {
#if UNITY_EDITOR
                if (_scrollRect == null)
                {
                    _scrollRect = FindScrollRect();
                }
#endif
                return _scrollRect.content;
            }
        }

        private ScrollRect _scrollRect;

        private int _currentPage; // Index of the currently active page.
        private int _targetPage; // Index of the target page during a page change animation.

        private float _startNormalizedPosition; // Normalized position of the scroll bar when drag begins.
        private float _targetNormalizedPosition; // Normalized position of the scroll bar for the target page.
        private float _moveSpeed; // Speed of the scroll bar animation (normalized units per second).

        #endregion

        private void Awake()
        {
            _scrollRect = FindScrollRect();
        }
        private void Update()
        {
            if (_moveSpeed == 0) { return; }

            var position = _scrollRect.horizontalNormalizedPosition;
            position += _moveSpeed * Time.deltaTime;

            var min = _moveSpeed > 0 ? position : _targetNormalizedPosition;
            var max = _moveSpeed > 0 ? _targetNormalizedPosition : position;
            position = Mathf.Clamp(position, min, max);

            _scrollRect.horizontalNormalizedPosition = position;

            if (Mathf.Abs(_targetNormalizedPosition - position) < Mathf.Epsilon)
            {
                _moveSpeed = 0;

                OnPageChangeEnded?.Invoke(_currentPage, _targetPage);

                _currentPage = _targetPage;
            }
        }

        public void SetPage(int index)
        {
            _scrollRect.horizontalNormalizedPosition = GetTargetPagePosition(index);

            _targetPage = index;
            _currentPage = index;
            OnPageChangeEnded?.Invoke(0, _currentPage);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startNormalizedPosition = _scrollRect.horizontalNormalizedPosition;

            if (_targetPage != _currentPage)
            {
                OnPageChangeEnded?.Invoke(_currentPage, _targetPage);

                _currentPage = _targetPage;
            }

            _moveSpeed = 0;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            var pageWidth = 1f / GetPageCount();

            var pagePosition = _currentPage * pageWidth;

            var currentPosition = _scrollRect.horizontalNormalizedPosition;

            var minPageDrag = pageWidth * _minDeltaDrag;

            var isForwardDrag = _scrollRect.horizontalNormalizedPosition > _startNormalizedPosition;

            var switchPageBreakpoint = pagePosition + (isForwardDrag ? minPageDrag : -minPageDrag);

            var page = _currentPage;
            if (isForwardDrag && currentPosition > switchPageBreakpoint)
            {
                page++;
            }
            else if (!isForwardDrag && currentPosition < switchPageBreakpoint)
            {
                page--;
            }

            ScrollToPage(page);
        }

        /// <summary>
        /// This function handles initiating a page change animation based on a target page index 
        /// during a scroll interaction. It calculates the target scroll position, determines if a page change 
        /// is required based on drag distance and direction, and triggers the animation if necessary.
        /// </summary>
        /// <param name="page">The index of the target page to scroll to.</param>
        private void ScrollToPage(int page)
        {
            _targetNormalizedPosition = GetTargetPagePosition(page);

            _moveSpeed = (_targetNormalizedPosition - _scrollRect.horizontalNormalizedPosition) / _snapDuration;
            
            _targetPage = page;

            if (_targetPage != _currentPage)
            {
                OnPageChangeStarted?.Invoke(_currentPage, _targetPage);
            }
        }

        /// <summary>
        /// Calculates the number of scrollable pages in the scroll view, considering the content and viewport width.
        /// </summary>
        /// <returns>The number of scrollable pages.</returns>
        private int GetPageCount()
        {
            var contentWidth = _scrollRect.content.rect.width;
            var rectWidth = ((RectTransform)_scrollRect.transform).rect.size.x;
            return Mathf.RoundToInt(contentWidth / rectWidth) - 1;
        }

        private float GetTargetPagePosition(int page)
        {
            return page * (1f / GetPageCount());
        }

        private ScrollRect FindScrollRect()
        {
            var scrollRect = GetComponentInChildren<ScrollRect>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (scrollRect == null)
            {
                Debug.LogError("Missing ScrollRect in Children");
            }
#endif
            return scrollRect;
        }
    }
}