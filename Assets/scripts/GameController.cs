using System;
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

    [SerializeField]
    private PlayersContainer playersContainer;

    // Start is called before the first frame update
    void Start(){
        
        
    }

    private void OnEnable() {
        GameEngine.OnGameEngineMessage += OnGameEngineDataReceive;
        CardOrganizer.OnSubmitResult += OnSubmitResultReceive;
    }

    private void OnDisable() {
        GameEngine.OnGameEngineMessage -= OnGameEngineDataReceive;
        CardOrganizer.OnSubmitResult -= OnSubmitResultReceive;
    }

    private List<CardData> handCardsMainPlayer;

    private void OnGameEngineDataReceive(MessageType msgType, int playerId, DataToPass msg){

        switch (msgType) {

            case MessageType.PLAYERS_JOIN:

                PlayerData playerData = msg as PlayerData;

                playersContainer.SetPlayerData(playerId, playerData);

                break;

            case MessageType.DEAL_HAND_CARDS:

                // Main Player ID
                if (playerId == 0){

                    //List<CardData> handCardsData = JsonUtility.FromJson<List<CardData>>(msg);

                    HandCardsData handCardsData = msg as HandCardsData;

                    // Cachiing the data
                    handCardsMainPlayer = handCardsData.cards;

                    // Pass the data to render player hand-cards
                    playersContainer.SetPlayerHandCards(playerId, handCardsMainPlayer);

                    // Open the cards
                    playersContainer.OpenPlayerHandCards(playerId, true);
                }

                break;
            
        }
    }

    // For updating the Main Player HandCards after Organizing the cards in the organizing window.
    private void OnSubmitResultReceive(List<CardData> organizedCards) {
        //handCardsMainPlayer = organizedCards;

        // Pass the data to render player hand-cards
        playersContainer.SetPlayerHandCards(0, organizedCards);
    }

    public void OnArrangeCardsButClick() {
        cardOrganizer.gameObject.SetActive(true);

        // Initialize Card Organizer
        cardOrganizer.InitDataAndRender(this, handCardsMainPlayer);
    }




}
