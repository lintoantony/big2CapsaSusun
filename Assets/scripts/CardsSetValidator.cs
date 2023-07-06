using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardsSetValidator {

    // Validate a set of Big Two cards
    public static bool ValidateBigTwoSet(List<Card> cardSet) {

        Debug.Log("cardSet.Count = " + cardSet.Count);

        // Check for empty set
        if (cardSet.Count == 0) {
            Debug.Log("Card set is empty.");
            return false;
        }

        CardRank referenceRank = cardSet[0].cardRank;

        // Check if all cards have the same rank
        /*
        foreach (Card card in cardSet) {
            if (card.cardRank != referenceRank) {
                Debug.Log("Invalid set. Cards must have the same rank.");
                return false;
            }
        }
        */

        // Check for valid set combinations (e.g., single, pair, triple, etc.)
        int setCount = cardSet.Count;
        if (setCount == 1){

            // Single card is valid
            return true;

        } else if (setCount == 2) {

            // Check for valid pair
            if (cardSet[0].cardRank == cardSet[1].cardRank) {
                // Normal pair is valid
                return true;
            }
        } else if (setCount >= 3) {

            // Check for valid triple or higher set
            int referenceCount = 1;
            for (int i = 1; i < setCount; i++) {

                Debug.Log("Ivide cardSet[i].cardRank = " + cardSet[i].cardRank + " : referenceRank = " + referenceRank);
                if (cardSet[i].cardRank == referenceRank) {

                    referenceCount++;

                    Debug.Log("Ivide referenceCount = " + referenceCount);

                    if (referenceCount >=3 ){
                        Debug.Log("Set of " + referenceCount  + " cards of same rank");
                        return true;
                    }

                } else {

                    Debug.Log("Resetting since rank was not same");

                    referenceRank = cardSet[i].cardRank;
                    referenceCount = 1;
                }
            }

            if (referenceCount < 3) {
                Debug.Log("Invalid set. Triple or higher set required.");
                return false;
            }

            return true;
        }

        Debug.Log("Invalid set.");

        return false;
    }

}
