using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDataController {

    public List<CardData> deckData;

    public void InitCardData(){
        CreateDeck();

        // Shuffle the generated cards deck
        ShuffleDeck();
    }


    private void CreateDeck() {
        deckData = new List<CardData>();

        foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit))){

            foreach (CardRank rank in System.Enum.GetValues(typeof(CardRank))){

                deckData.Add(new CardData { Suit = suit, Rank = rank });
            }
        }
    }

    private void ShuffleDeck() {

        for (int i = 0; i < deckData.Count; i++){

            CardData temp = deckData[i];
            int randomIndex = Random.Range(i, deckData.Count);
            deckData[i] = deckData[randomIndex];
            deckData[randomIndex] = temp;
        }
    }

}
