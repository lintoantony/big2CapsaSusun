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


        // Setting the cards for the Main-Player to display 
        OnGameEngineMessage(MessageType.DEAL_HAND_CARDS, 0, handCardsDataList[0]);

        // TEST
        /*
        OnGameEngineMessage(MessageType.DISPLAY_OPPONENT_CARDS, 1, handCardsDataList[1]);
        OnGameEngineMessage(MessageType.DISPLAY_OPPONENT_CARDS, 2, handCardsDataList[2]);
        OnGameEngineMessage(MessageType.DISPLAY_OPPONENT_CARDS, 3, handCardsDataList[3]);
        */

        // Game Timer Starts and end at the mentioned time
        OnGameEngineMessage(MessageType.GAME_TIMER, -1, null);


        // During Game, Other players ( Bots in this case ) will be ready to submit by the timer ends



        // Message to Display the cards ( The sequence of displaying row-by-row ) and Result announcement sequence.



        // Clear the Table and ready for the next Deal
    }

    private void DisplayOpponentCardsAtTheEnd(){
        OnGameEngineMessage(MessageType.DISPLAY_OPPONENT_CARDS, 1, handCardsDataList[1]);
        OnGameEngineMessage(MessageType.DISPLAY_OPPONENT_CARDS, 2, handCardsDataList[2]);
        OnGameEngineMessage(MessageType.DISPLAY_OPPONENT_CARDS, 3, handCardsDataList[3]);
    }

    private void DisplayHandCardRanks() {

        // TODO: To pass appropriate data ( ie. Rank info of 3 hands each of all 4 players)

        OnGameEngineMessage(MessageType.GAME_RESULT, 0, null);
        OnGameEngineMessage(MessageType.GAME_RESULT, 1, null);
        OnGameEngineMessage(MessageType.GAME_RESULT, 2, null);
        OnGameEngineMessage(MessageType.GAME_RESULT, 3, null);
    }

    private void OnEnable() {
        CountdownTimer.OnCountDownTimerMessage += OnCountDownTimerMessage;
    }

    private void OnDisable() {
        CountdownTimer.OnCountDownTimerMessage -= OnCountDownTimerMessage;
    }

    private void OnCountDownTimerMessage(MsgType msgType) {
        switch (msgType) {
            case MsgType.ON_TIMER_START:


                break;
            case MsgType.ON_TIMER_END:

                
                PlayResultDisplaySequence();


                break;
            case MsgType.ON_ABOUT_TO_COMPLETE:


                break;
        }
    }

    private float totalTime = 10f;
    private float currentTime;

    private void PlayResultDisplaySequence() {
        currentTime = totalTime;

        StartCoroutine(SequenceCoroutine());

    }

    private IEnumerator SequenceCoroutine() {

        while (currentTime > 0f) {

            yield return new WaitForSeconds(1f);

            currentTime--;

            switch (currentTime) {
                case 8:

                    // Show Everyone's open cards with latest reorganized data to evaluate the results
                    DisplayOpponentCardsAtTheEnd();

                    break;

                case 6:

                    DisplayHandCardRanks();

                    break;

                case 4:


                    break;
                case 2:


                    break;
                case 0:


                    break;
            }
        }

        Debug.Log("SequenceCoroutine Complete!");
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

        int k = 0;

        // For four players
        for (int i=0; i<4; i++){

            handCardsDataObj = new HandCardsData();
            handCardsDataObj.playerId = i;

            handCardsDataObj.cards = new List<CardData>();

            for (int j=k; j<len; j++){

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
    DISPLAY_OPPONENT_CARDS, // To show opponent hand-cards
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



