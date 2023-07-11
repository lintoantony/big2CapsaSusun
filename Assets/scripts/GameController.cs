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

    [SerializeField]
    private CountdownTimer countdownTimer;

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

                playersContainer.InitPlayerHandCards();

                break;

            case MessageType.DEAL_HAND_CARDS:

                // Main Player ID
                if (playerId == 0){

                    HandCardsData handCardsData0 = msg as HandCardsData;

                    // Cachiing the data
                    handCardsMainPlayer = handCardsData0.cards;

                    // Pass the data to render player hand-cards
                    playersContainer.SetPlayerHandCards(playerId, handCardsMainPlayer);

                    // Open the cards
                    playersContainer.OpenPlayerHandCards(playerId, true);
                }

                break;
            case MessageType.GAME_TIMER:

                countdownTimer.InitAndStart();

                break;
            case MessageType.DISPLAY_OPPONENT_CARDS:

                HandCardsData handCardsData = msg as HandCardsData;

                // Pass the data to render player hand-cards
                playersContainer.SetPlayerHandCards(playerId, handCardsData.cards);

                // Open the cards
                playersContainer.OpenPlayerHandCards(playerId, true);
                
                break;

            case MessageType.GAME_RESULT:

                // Set the Rank of all the players.
                // TODO: TO pass the required data here

                List<ResultData> rankResultsData = null;
                if (playerId == 0){

                    if (dataFromOrganizer != null) { 
                        rankResultsData = dataFromOrganizer.rankResultsData;
                    }
                } else {
                    // To get data of Opponent players
                    
                }

                playersContainer.SetHandRanks(true, playerId, rankResultsData);

                // TESTING
                playersContainer.PlayEmojiAnim(0, AnimType.LAUGH);

                playersContainer.PlayEmojiAnim(1, AnimType.CRY);
                playersContainer.PlayEmojiAnim(2, AnimType.CRY);
                playersContainer.PlayEmojiAnim(3, AnimType.CRY);


                break;

        }
    }

    // Caching this data from Organizer to use
    public DataFromOrganizer dataFromOrganizer = null;

    // For updating the Main Player HandCards after Organizing the cards in the organizing window.
    private void OnSubmitResultReceive(DataFromOrganizer dataFromOrganizer) {
        this.dataFromOrganizer = dataFromOrganizer;

        // Pass the data to render player hand-cards
        playersContainer.SetPlayerHandCards(0, dataFromOrganizer.organizedCards);
    }

    public void OnArrangeCardsButClick() {
        cardOrganizer.gameObject.SetActive(true);

        // Initialize Card Organizer
        cardOrganizer.InitDataAndRender(this, handCardsMainPlayer);
    }




}
