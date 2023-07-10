using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    public delegate void GameEngineMessageDelegate(MessageType msgType, int playerId, DataToPass data);
    public static event GameEngineMessageDelegate OnGameEngineMessage;

    private CardsDataController cardsDataController;

    public List<CardData> deckData;

    // Start is called before the first frame update
    void Start() {
        // Create cards deck
        cardsDataController = new CardsDataController();
        cardsDataController.InitCardData();

        // Deck Data
        deckData = cardsDataController.deckData;

        // Dealing ( Splitting in to 13 each ) to each players
        SplitCardsToDeal(deckData);


        // Joining the players
        PlayersJoining();


        HandCardsData handCardsData = handCardsDataList[0];

        //string msgDataStr = JsonUtility.ToJson(cards);

        OnGameEngineMessage(MessageType.DEAL_HAND_CARDS, 0, handCardsData);
    }

    private void PlayersJoining() {

        // Player 0 | Main Player
        PlayerData mainPlayer = new PlayerData();
        mainPlayer.name = "Linto";
        mainPlayer.avatarId = "0";

        // Other Players
        List<PlayerData> opponentsList = PlayerDummyData.GetPlayerList();
        
        // Final 4 players
        List<PlayerData> playerList = new List<PlayerData>();
        playerList.Add(mainPlayer);
        playerList.Add(opponentsList[0]);
        playerList.Add(opponentsList[1]);
        playerList.Add(opponentsList[2]);


        OnGameEngineMessage(MessageType.PLAYERS_JOIN, 0, playerList[0]);
        OnGameEngineMessage(MessageType.PLAYERS_JOIN, 1, playerList[1]);
        OnGameEngineMessage(MessageType.PLAYERS_JOIN, 2, playerList[2]);
        OnGameEngineMessage(MessageType.PLAYERS_JOIN, 3, playerList[3]);
    }

    private HandCardsData handCardsDataObj;
    private List<HandCardsData> handCardsDataList;

    private void SplitCardsToDeal(List<CardData> deckData) {

        handCardsDataList = new List<HandCardsData>();


        int len = deckData.Count;

        List<CardData> testList = new List<CardData>();

        int k;

        // For four players
        for (int i=0; i<4; i++){

            handCardsDataObj = new HandCardsData();
            handCardsDataObj.playerId = i;

            k = 0;

            handCardsDataObj.cards = new List<CardData>();

            for (int j=0; j<len; j++){

                handCardsDataObj.cards.Add(deckData[j]);

                k++;

                if (k%13 == 0) {
                    break;
                }
            }

            handCardsDataList.Add(handCardsDataObj);
        }   
    }

}

public enum MessageType {
    PLAYERS_JOIN, // n players (4 Players ) join the table
    DEAL_HAND_CARDS, // HandCards detail to each player
    GAME_TIMER, // Game Start and End timer
    GAME_RESULT, // Winner Looser data ( Card Data etc. of all players so that we can display all those in the screen)
    CLEAR_TABLE,
    NEW_GAME
}

[System.Serializable]
public class HandCardsData : DataToPass {

    public int playerId;

    public List<CardData> cards;

}

public interface IData{
    
}

public class BaseData {

}

public class DataToPass : BaseData , IData{

}



