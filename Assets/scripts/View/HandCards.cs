using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandCards : MonoBehaviour {

    public delegate void OnArrangeCardsDelegate(List<ResultData> rankResultsData);
    public static event OnArrangeCardsDelegate OnCardArrange;

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

        Invoke("UpdateCardRowsAfterDelay", 0.1f);
    }

    private void UpdateCardRowsAfterDelay() {
        // Update rows
        UpdateCardRows();
    }

    private Card swappingCard;
    public Card GetSwappingCard(DropData dropData) {

        return allHandCards.FirstOrDefault(i => (i.row == dropData.slotRow && i.column == dropData.slotColumn));
    }


    private List<Card> cardsRow1View;
    private List<Card> cardsRow2View;
    private List<Card> cardsRow3View;

    public void UpdateCardRows() {

        //allHandCards.FirstOrDefault(i => (i.row == dropData.slotRow && i.column == dropData.slotColumn));

        // Grouping the cards in Bottom Row & keeping in order as in the view
        List<Card> row1 = allHandCards.Where(i => (i.row == 0)).ToList();
        row1 = row1.OrderBy(o => o.column).ToList();
        cardsRow1View = new List<Card>();
        cardsRow1View = row1;
        Debug.Log("UpdateCardRows : row1.Count = " + row1.Count);

        // Grouping the cards in Middle Row & keeping in order as in the view
        List<Card> row2 = allHandCards.Where(i => (i.row == 1)).ToList();
        row2 = row2.OrderBy(o => o.column).ToList();
        cardsRow2View = new List<Card>();
        cardsRow2View = row2;
        Debug.Log("UpdateCardRows : row2.Count = " + row2.Count);

        // Grouping the cards in Top Row & keeping in order as in the view
        List<Card> row3 = allHandCards.Where(i => (i.row == 2)).ToList();
        row3 = row3.OrderBy(o => o.column).ToList();
        cardsRow3View = new List<Card>();
        cardsRow3View = row3;
        Debug.Log("UpdateCardRows : row3.Count = " + row3.Count);

        ValidateRows(row1, row2, row3);
    }


    public List<ResultData> rankResultsData;

    private void ValidateRows(List<Card> row1, List<Card> row2, List<Card> row3) {

        rankResultsData = new List<ResultData>();

        // Bottom row result
        ResultData resultData1 = CardsSetValidator.ValidateRow(row1);
        Debug.Log("ValidateRows : CardsSetValidator : ValidateRow : BOTTOM Row = " + resultData1.setType);

        // Middle row result
        ResultData resultData2 = CardsSetValidator.ValidateRow(row2);
        Debug.Log("ValidateRows : CardsSetValidator : ValidateRow : MIDDLE Row = " + resultData2.setType);

        // Top row result
        ResultData resultData3 = CardsSetValidator.ValidateRow(row3);
        Debug.Log("ValidateRows : CardsSetValidator : ValidateRow : TOP Row = " + resultData3.setType);

        rankResultsData.Add(resultData1);
        rankResultsData.Add(resultData2);
        rankResultsData.Add(resultData3);

        OnCardArrange(rankResultsData);
    }

    public void InitCards(List<CardData> handCardsData) {

        // Render Cards

        // TODO:
        int len = allHandCards.Count;

        Card card;
        CardData cardData;

        for (int i = 0; i < len; i++){
            card = allHandCards[i];

            cardData = handCardsData[i];

            card.SetValue(cardData.Rank);
            card.SetSuite(cardData.Suit);

            card.OpenCard(true);
        }

        Invoke("UpdateCardRowsAfterDelay", 0.1f);
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

    public List<CardData> GetOrganizedCards() {

        if (cardsRow1View == null && cardsRow2View == null && cardsRow3View == null) {
            return null;
        }

        List<CardData> organizedCards = new List<CardData>(13);

        CardData cardData;

        int k=0;

        // Pushing Row1 data
        for (int i=0; i<cardsRow1View.Count; i++) {

            cardData = new CardData();

            cardData.Rank = cardsRow1View[i].cardRank;
            cardData.Suit = cardsRow1View[i].cardSuit;

            //organizedCards.Add(cardData);
            organizedCards.Insert(k, cardData);
            k++;
        }

        // Pushing Row2 data
        for (int i = 0; i < cardsRow2View.Count; i++) {

            cardData = new CardData();

            cardData.Rank = cardsRow2View[i].cardRank;
            cardData.Suit = cardsRow2View[i].cardSuit;

            //organizedCards.Add(cardData);
            organizedCards.Insert(k, cardData);
            k++;
        }

        // Pushing Row3 data
        for (int i = 0; i < cardsRow3View.Count; i++) {

            cardData = new CardData();

            cardData.Rank = cardsRow3View[i].cardRank;
            cardData.Suit = cardsRow3View[i].cardSuit;

            //organizedCards.Add(cardData);
            organizedCards.Insert(k, cardData);
            k++;
        }


        return organizedCards;
    }

}
