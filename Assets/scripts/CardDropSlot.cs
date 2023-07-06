
// REF : https://www.youtube.com/watch?v=kWRyZ3hb1Vc&t=753s

using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropSlot : MonoBehaviour, IDropHandler {

    public int row;
    public int column;

    public delegate void OnDropDelegate(DropData dropData);
    public static event OnDropDelegate OnCardDrop;

    private DropData dropData;

    public void OnDrop(PointerEventData eventData) {

        //Debug.Log("OnDrop-------");


        GameObject droppedObj = eventData.pointerDrag;
        droppedObj.transform.position = transform.position;

        Card card = droppedObj.GetComponent<Card>();

        /*
        Debug.Log("OnDrop : card : " + card.row + " : " + card.column);
        Debug.Log("OnDrop : slot : " + this.row + " : " + this.column);
        */

        dropData = new DropData();
        dropData.cardRow = card.row;
        dropData.cardColumn = card.column;
        dropData.slotRow = this.row;
        dropData.slotColumn = this.column;
        dropData.targetPosition = this.transform.position;

        // Update the incoming card to current slot row & colum values
        card.row = this.row;
        card.column = this.column;

        // Notify obserevers
        OnCardDrop(dropData);
    }
}

public class DropData {
    public int slotRow;
    public int slotColumn;
    public int cardRow;
    public int cardColumn;
    public Vector3 targetPosition;
}