using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Canvas mainCanvas;

    [SerializeField]
    private GameObject cardPrefabRef;

    [SerializeField]
    private CardOrganizer cardOrganizer;

    private CardsDataController cardsDataController;

    public List<CardData> deckData;

    // Start is called before the first frame update
    void Start(){
        
        // Create cards deck
        cardsDataController = new CardsDataController();
        cardsDataController.InitCardData();

        // Deck Data
        deckData = cardsDataController.deckData;

        // Initialize Card Organizer
        cardOrganizer.InitDataAndRender(this);
    }

}
