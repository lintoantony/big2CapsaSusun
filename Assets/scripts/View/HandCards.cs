using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandCards : MonoBehaviour {

    // Bottom Row of 5 cards
    [SerializeField]
    public List<Card> allHandCards;

    [SerializeField]
    public CardSlots cardSlots;

    // Bottom 5 cards row
    [HideInInspector]
    public List<Card> cardsRow1;

    // Middle 5 cards row
    [HideInInspector]
    public List<Card> cardsRow2;

    // Top 3 cards row
    [HideInInspector]
    public List<Card> cardsRow3;

    // Start is called before the first frame update
    void Start(){
        Debug.Log("Ivide Vanno");
    }

    private void OnEnable() {
        CardDropSlot.OnCardDrop += OnCardDrop;
    }

    private void OnDisable() {
        CardDropSlot.OnCardDrop -= OnCardDrop;
    }

    
    private void OnCardDrop(DropData dropData) {
        Debug.Log("HandCards : OnCardDrop : cardRow = " + dropData.cardRow + " : cardColumn = " + dropData.cardColumn + " : slotRow = " + dropData.slotRow + " : slotColumn = " + dropData.slotColumn);

        // Find the incoming card with row & column Slot data. Set the incoming card position with the card row & colum data

        swappingCard = GetSwappingCard(dropData);

        CardDropSlot cardPickSlot = cardSlots.GetCardPickSlot(dropData);
        swappingCard.transform.position = cardPickSlot.transform.position;

        // Updating the existing card row & column values and move to the incoming slot
        swappingCard.row = cardPickSlot.row;
        swappingCard.column = cardPickSlot.column;

        Invoke("UpdateCardRowsAfterDelay", 0.2f);
    }

    private void UpdateCardRowsAfterDelay() {
        // Update rows
        UpdateCardRows();
    }

    private Card swappingCard;
    public Card GetSwappingCard(DropData dropData) {

        return allHandCards.FirstOrDefault(i => (i.row == dropData.slotRow && i.column == dropData.slotColumn));
    }

    
    public void UpdateCardRows() {

        //allHandCards.FirstOrDefault(i => (i.row == dropData.slotRow && i.column == dropData.slotColumn));

        // Grouping the cards in Bottom Row & keeping in order as in the view
        cardsRow1 = allHandCards.Where(i => (i.row == 0)).ToList();
        cardsRow1 = cardsRow1.OrderBy(o => o.column).ToList();
        Debug.Log("UpdateCardRows : cardsRow1.Count = " + cardsRow1.Count);

        // Grouping the cards in Middle Row & keeping in order as in the view
        cardsRow2 = allHandCards.Where(i => (i.row == 1)).ToList();
        cardsRow2 = cardsRow2.OrderBy(o => o.column).ToList();
        Debug.Log("UpdateCardRows : cardsRow2.Count = " + cardsRow2.Count);

        // Grouping the cards in Top Row & keeping in order as in the view
        cardsRow3 = allHandCards.Where(i => (i.row == 2)).ToList();
        cardsRow3 = cardsRow3.OrderBy(o => o.column).ToList();
        Debug.Log("UpdateCardRows : cardsRow3.Count = " + cardsRow3.Count);


        // Bottom row result
        ResultData resultData1 = CardsSetValidator.ValidateRow(cardsRow1);
        Debug.Log("UpdateCardRows : CardsSetValidator : ValidateRow : BOTTOM Row = " + resultData1.setType);

        // Middle row result
        ResultData resultData2 = CardsSetValidator.ValidateRow(cardsRow2);
        Debug.Log("UpdateCardRows : CardsSetValidator : ValidateRow : MIDDLE Row = " + resultData2.setType);

        // Top row result
        ResultData resultData3 = CardsSetValidator.ValidateRow(cardsRow3);
        Debug.Log("UpdateCardRows : CardsSetValidator : ValidateRow : TOP Row = " + resultData3.setType);

    }

    public void InitCards(List<CardData> deckData) {

        // Render Cards

        // TODO:
        int len = allHandCards.Count;

        Card card;
        CardData cardData;

        for (int i = 0; i < len; i++){
            card = allHandCards[i];

            cardData = deckData[i];

            card.SetValue(cardData.Rank);
            card.SetSuite(cardData.Suit);

        }

    }

    public void UpdateRaycastTarget(bool enabled) {

        int len = allHandCards.Count;

        Card card;

        for (int i = 0; i < len; i++){
            card = allHandCards[i];

            DraggableItem di = card.GetComponent<DraggableItem>();
            di.UpdateRaycastTarget(enabled);
        }
    }

}
