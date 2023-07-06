using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOrganizer : MonoBehaviour {

    [SerializeField]
    private GameObject cardsHolder;

    [SerializeField]
    private GameObject cardPrefabRef;

    [SerializeField]
    private CardSlots cardSlotsRef;

    //[SerializeField]
    private HandCards handCards;

    private GameController gameController;

    private List<CardData> deckData;


    // Start is called before the first frame update
    void Start() {
        handCards = cardsHolder.GetComponent<HandCards>();
        Debug.Log("handCards 1 = " + handCards);
    }

    public void InitDataAndRender(GameController gameControllerRef) {
        gameController = gameControllerRef;

        deckData = gameController.deckData;

        //RenderCards();

        //handCards.InitCards(deckData);

        Invoke("InitCardsAfterDelay", 1f);
    }

    private void InitCardsAfterDelay() {
        Debug.Log("handCards 2 = " + handCards);
        Debug.Log("deckData = " + deckData);

        handCards.InitCards(deckData);
    }

    private void RenderCards() {

        // Render Cards

        int len = deckData.Count;

        GameObject instantiatedCard;
        Card card;
        CardData cardData;

        int xPos = 60;
        int yPos = 100;

        for (int i = 0; i < len; i++)
        {

            if (i != 0 && (i % 10) == 0)
            {
                yPos += 82;
                xPos = 60;
            }

            instantiatedCard = Instantiate(cardPrefabRef, new Vector3(xPos, yPos), Quaternion.identity);
            instantiatedCard.transform.SetParent(cardsHolder.transform);

            // Add a BoxCollider component to enable mouse interaction
            /*
            BoxCollider boxCollider = instantiatedCard.AddComponent<BoxCollider>();
            boxCollider.size = Vector3.one; // Set the collider size to match the object's size
            boxCollider.isTrigger = true;
            */

            instantiatedCard.transform.localScale = new Vector3(0.5f, 0.5f, 1);


            card = instantiatedCard.GetComponent<Card>();

            cardData = deckData[i];

            card.SetValue(cardData.Rank);
            card.SetSuite(cardData.Suit);

            xPos += 62;

        }

    }


}
