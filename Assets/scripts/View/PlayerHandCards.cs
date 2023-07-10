using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandCards : MonoBehaviour {

    [SerializeField]
    private List<Card> handCards;

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

}
