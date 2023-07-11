using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHandCards : MonoBehaviour {

    [SerializeField]
    private List<Card> handCards;

    [SerializeField]
    private GameObject rankDisplay;

    [SerializeField]
    private List<TMP_Text> rankDisplayList;

    // Start is called before the first frame update
    void Start(){
        //OpenAllCards(false);
    }

    public void OpenAllCards(bool isOpen) {

        for (int i=0; i<13; i++){
            handCards[i].OpenCard(isOpen);
        }
    }

    public void SetCards(List<CardData> handCardsData) {
        for (int i = 0; i < 13; i++){
            handCards[i].SetSuite(handCardsData[i].Suit);
            handCards[i].SetValue(handCardsData[i].Rank);
        }
    }

    public void SetHandRanks(bool isVisible, List<ResultData> resultDataList){

        for (int i = 0; i < rankDisplayList.Count; i++) {

            if (resultDataList != null){

                rankDisplayList[i].text = resultDataList[i].setType.ToString();

            } else {
                // Default ( Without validation )
                rankDisplayList[i].text = SetType.HIGH_CARD.ToString();
            }
        }

        rankDisplay.SetActive(isVisible);
    }

}
