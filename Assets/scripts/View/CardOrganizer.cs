using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardOrganizer : MonoBehaviour {

    public delegate void OnSubmitDelegate(DataFromOrganizer dataFromOrganizer);
    public static event OnSubmitDelegate OnSubmitResult;

    [SerializeField]
    private GameObject cardsHolder;

    [SerializeField]
    private GameObject cardPrefabRef;

    [SerializeField]
    private CardSlots cardSlotsRef;

    //[SerializeField]
    private HandCards handCards;

    private GameController gameController;

    private List<CardData> handCardsData;

    [SerializeField] private TMP_Text firstRowTxt;
    [SerializeField] private TMP_Text secondRowTxt;
    [SerializeField] private TMP_Text thridRowTxt;


    // Start is called before the first frame update
    void Start() {
        handCards = cardsHolder.GetComponent<HandCards>();
        Debug.Log("handCards 1 = " + handCards);
    }

    private void OnEnable() {
        HandCards.OnCardArrange += OnCardArrange;
        CountdownTimer.OnCountDownTimerMessage += OnCountDownTimerMessage;
    }

    private void OnDisable() {
        HandCards.OnCardArrange -= OnCardArrange;
        CountdownTimer.OnCountDownTimerMessage -= OnCountDownTimerMessage;
    }

    private void OnCountDownTimerMessage(MsgType msgType) {
        switch (msgType) {
            case MsgType.ON_TIMER_START:


                break;
            case MsgType.ON_TIMER_END:

                // Submit and close the window.

                SubmitAndClose();

                break;
            case MsgType.ON_ABOUT_TO_COMPLETE:


                break;
        }
    }

    private void OnCardArrange(List<ResultData> rankResultsData) {
        Debug.Log("CardOrganizer : OnCardArrange");

        firstRowTxt.text = rankResultsData[0].setType.ToString();
        secondRowTxt.text = rankResultsData[1].setType.ToString();
        thridRowTxt.text = rankResultsData[2].setType.ToString();
    }

    public void InitDataAndRender(GameController gameControllerRef, List<CardData> cardsData) {
        gameController = gameControllerRef;

        //deckData = gameController.deckData;

        //RenderCards();

        //handCards.InitCards(deckData);

        handCardsData = cardsData;


        Invoke("InitCardsAfterDelay", 0.1f);

        //handCards.InitCards(handCardsData);
    }

    
    private void InitCardsAfterDelay() {
       
        handCards.InitCards(handCardsData);
    }
    

    /*
    private void RenderCards() {

        // Render Cards

        int len = handCardsData.Count;

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

            instantiatedCard.transform.localScale = new Vector3(0.5f, 0.5f, 1);


            card = instantiatedCard.GetComponent<Card>();

            cardData = handCardsData[i];

            card.SetValue(cardData.Rank);
            card.SetSuite(cardData.Suit);

            xPos += 62;

        }
    }
    */

    public void OnSubmit() {
        Debug.Log("OnSubmit");

        SubmitAndClose();
    }

    private void SubmitAndClose() {
        // Convert the View Data in to CardData list after user's interaction with the cards to organize in to groups
      
        OnSubmitResult(GetOrganizedCards());

        this.gameObject.SetActive(false);
    }

    private DataFromOrganizer GetOrganizedCards(){

        DataFromOrganizer dataFromOrganizer = new DataFromOrganizer();
        dataFromOrganizer.organizedCards = handCards.GetOrganizedCards();
        dataFromOrganizer.rankResultsData = handCards.rankResultsData;

        return dataFromOrganizer;
    }

}

public class DataFromOrganizer {
    public List<CardData> organizedCards;
    public List<ResultData> rankResultsData;
}
