using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public delegate void OnStickInputValueUpdated(Vector2 inputValue);
        public event OnStickInputValueUpdated OnStickValueUpdated;
        
        [SerializeField] private RectTransform centerTransform;
        [SerializeField] private RectTransform backgroundTransform;
        [SerializeField] private RectTransform thumbStickTransform;
        
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 touchPosition = eventData.position;
            Vector2 centerPosition = backgroundTransform.position;

            Vector2 localOffset =
                Vector2.ClampMagnitude(touchPosition - centerPosition, backgroundTransform.sizeDelta.x / 2);
            Vector2 inputValue = localOffset / backgroundTransform.sizeDelta.x / 2;
            
            thumbStickTransform.position = centerPosition + localOffset;
            
            OnStickValueUpdated?.Invoke(inputValue);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            backgroundTransform.position = eventData.position;
            thumbStickTransform.position = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            backgroundTransform.position = centerTransform.position;
            thumbStickTransform.position = backgroundTransform.position;
            
            OnStickValueUpdated?.Invoke(Vector2.zero);
        }
    }    
}