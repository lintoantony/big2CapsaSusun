
// REF : https://www.youtube.com/watch?v=kWRyZ3hb1Vc&t=753s

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    [HideInInspector]
    public Transform parentAfterDrag;

    Vector3 initialPosition;

    [SerializeField]
    Image image;

    private HandCards handCards;

    public void OnBeginDrag(PointerEventData eventData) {

        Debug.Log("OnBeginDrag");

        handCards = this.GetComponentInParent<HandCards>();
        handCards.UpdateRaycastTarget(false);

        initialPosition = transform.position;

        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);

        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {

        //Debug.Log("OnDrag");


        transform.position = Input.mousePosition;
 
    }

    public void OnEndDrag(PointerEventData eventData){

        Debug.Log("OnEndDrag");

        transform.SetParent(parentAfterDrag);

        //transform.position = initialPosition;

        image.raycastTarget = true;

        handCards = this.GetComponentInParent<HandCards>();
        handCards.UpdateRaycastTarget(true);
    }

    public void UpdateRaycastTarget(bool enabled) {
        image.raycastTarget = enabled;
    }
}
