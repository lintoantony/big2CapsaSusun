using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardsSetValidator {

    // Three­-of­--a­kind : Three equally ranked cards, three twos are highest, then aces,kings, etc.down to three threes, which is the lowest triple.
    // 1 and set of 2 are not considered in this type of game where only 3 rows are validated only at the end. 
    private static bool ValidateBigTwoSet(List<Card> cardList) {

        List<Card> cardSet = new List<Card>(cardList);

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

    /*
    Straight/run: Any 5 cards in a sequence (but not all of the same suit). Rank is
    determined by the value of the biggest card, with the suit used only as a
    tie­breaker. Therefore 3­4­5­6­7 < 2­3­4­5­6, since 2 is considered the largest
    card in the 2­3­4­5­6 straight. The largest straight is J­Q­K­A­2, while the smallest
    straight is 3­4­5­6­7.
    */
    private static bool ValidateStraight(List<Card> cardList) {

        List<Card> cardSet = new List<Card>(cardList);

        // Check for empty set
        if (cardSet.Count == 0) {
            Debug.Log("Card set is empty.");
            return false;
        }

        // Sort the cards in ascending order of rank
        cardSet.Sort((x, y) => x.cardValue.CompareTo(y.cardValue));

        // Check if all cards have consecutive ranks
        for (int i = 1; i < cardSet.Count; i++) {
            if ( (cardSet[i].cardValue != cardSet[i - 1].cardValue + 1 ) && (cardSet[i].cardValue + cardSet[i - 1].cardValue != 18)) {
                Debug.Log("Invalid set. Cards do not form a straight/run.");
                return false;
            }
        }

        // Valid straight/run
        return true;
    }

    // Flush: Any 5 cards of the same suit (but not in a sequence). Rank is determined by highest suit and then by highest rank.
    private static bool ValidateFlush(List<Card> cardList) {

        List<Card> cardSet = new List<Card>(cardList);

        // Check for empty set
        if (cardSet.Count == 0 || cardSet.Count < 5) {
            Debug.Log("Card set is empty or Count is less than 5 : Count = " + cardSet.Count);
            return false;
        }

        // Check if all cards have the same suit
        CardSuit referenceSuit = cardSet[0].cardSuit;
        foreach (Card card in cardSet) {

            if (card.cardSuit != referenceSuit) {

                Debug.Log("Invalid set. Cards do not form a flush.");

                return false;
            }
        }

        // Valid flush
        return true;
    }

    /*
    Full House: a composite of a three­of­a­kind combination and a pair. Rank is
    determined by the value of the triple, regardless of the value of the pair.
    */
    private static bool ValidateFullHouse(List<Card> cardList) {

        List<Card> cardSet = new List<Card>(cardList);

        // Check for empty set
        if (cardSet.Count == 0) {
            Debug.Log("Card set is empty.");
            return false;
        }

        // Check if the set has exactly 5 cards
        if (cardSet.Count != 5) {
            Debug.Log("Invalid set. Full house requires exactly 5 cards.");
            return false;
        }

        // Sort the cards in ascending order of rank
        cardSet.Sort((x, y) => x.cardValue.CompareTo(y.cardValue));

        // Check for valid full house combinations
        if (cardSet[0].cardValue == cardSet[1].cardValue && cardSet[1].cardValue == cardSet[2].cardValue &&
            cardSet[3].cardValue == cardSet[4].cardValue) {

            // Three of a kind and a pair
            return true;

        } else if (cardSet[0].cardValue == cardSet[1].cardValue && cardSet[2].cardValue == cardSet[3].cardValue &&
                 cardSet[3].cardValue == cardSet[4].cardValue) {

            // Pair and three of a kind
            return true;
        }

        // Invalid full house
        Debug.Log("Invalid set. Cards do not form a full house.");
        return false;
    }

    /*
     Four of a kind + One card: Any set of 4 cards of the same rank, plus any 5th
    card. A 4 of a kind cannot be played unless it is played as a 5­card hand and rank
    is determined by the value of the 4 card set, regardless of the value of the 5th
    card.
    */
    private static bool ValidateFourOfAKind(List<Card> cardList) {

        List<Card> cardSet = new List<Card>(cardList);

        // Check for empty set
        if (cardSet.Count == 0){
            Debug.Log("Card set is empty.");
            return false;
        }

        // Check if the set has exactly 5 cards
        if (cardSet.Count != 5){
            Debug.Log("Invalid set. Four of a Kind requires exactly 5 cards.");
            return false;
        }

        // Sort the cards in ascending order of rank
        cardSet.Sort((x, y) => x.cardValue.CompareTo(y.cardValue));

        // Check for valid "Four of a Kind + One card" combination
        if (cardSet[0].cardValue == cardSet[1].cardValue && cardSet[1].cardValue == cardSet[2].cardValue &&
            cardSet[2].cardValue == cardSet[3].cardValue) {

            // Four cards of the same rank
            return true;

        } else if (cardSet[1].cardValue == cardSet[2].cardValue && cardSet[2].cardValue == cardSet[3].cardValue &&
                 cardSet[3].cardValue == cardSet[4].cardValue) {

            // Four cards of the same rank
            return true;
        }

        // Invalid "Four of a Kind + One card" combination
        Debug.Log("Invalid set. Cards do not form a Four of a Kind combination.");

        return false;
    }


    /*
    Straight Flush: Five cards in sequence in the same suit. Ranked the same as
    straights, suit being a tie­breaker. 
    */

    private static bool ValidateStraightFlush(List<Card> cardList) {

        List<Card> cardSet = new List<Card>(cardList);

        // Check for empty set
        if (cardSet.Count == 0) {

            Debug.Log("Card set is empty.");
            return false;
        }

        // Check if the set has exactly 5 cards
        if (cardSet.Count != 5) {

            Debug.Log("Invalid set. Straight Flush requires exactly 5 cards.");
            return false;
        }

        // Sort the cards in ascending order of rank
        cardSet.Sort((x, y) => x.cardValue.CompareTo(y.cardValue));

        // Check if all cards have the same suit
        CardSuit referenceSuit = cardSet[0].cardSuit;
        foreach (Card card in cardSet){

            if (card.cardSuit != referenceSuit){

                Debug.Log("Invalid set. Cards do not have the same suit.");
                return false;
            }
        }

        // Check if the ranks form a straight
        for (int i = 1; i < cardSet.Count; i++) {
            if ( (cardSet[i].cardValue != cardSet[i - 1].cardValue + 1 ) && (cardSet[i].cardValue + cardSet[i - 1].cardValue != 18)) {

                Debug.Log("Invalid set. Cards do not form a straight.");
                return false;
            }
        }

        // Valid Straight Flush
        return true;
    }

    private static ResultData resultData;
    public static ResultData ValidateRow(List<Card> cardSet) {
        resultData = new ResultData();

        resultData.setType = SetType.HIGH_CARD;
        resultData.rowRank = 0;

        bool isStraightFlush = ValidateStraightFlush(cardSet);

        if (isStraightFlush) {

            resultData.setType = SetType.STRAIGHT_FLUSH;
            resultData.rowRank = 0;

            return resultData;

        } else {

            bool isFourOfAKind = ValidateFourOfAKind(cardSet);

            if (isFourOfAKind) {

                resultData.setType = SetType.FOUR_OF_A_KIND;
                resultData.rowRank = 0;

                return resultData;

            } else {

                bool isFullHouse = ValidateFullHouse(cardSet);

                if (isFullHouse) {

                    resultData.setType = SetType.FULL_HOUSE;
                    resultData.rowRank = 0;

                    return resultData;

                } else {

                    bool isFlush = ValidateFlush(cardSet);

                    if (isFlush) {

                        resultData.setType = SetType.FLUSH;
                        resultData.rowRank = 0;

                        return resultData;

                    } else {

                        bool isStraight = ValidateStraight(cardSet);

                        if (isStraight) {

                            resultData.setType = SetType.STRAIGHT;
                            resultData.rowRank = 0;

                            return resultData;

                        } else {

                            bool isThreeOfAKind = ValidateBigTwoSet(cardSet);

                            if (isThreeOfAKind) {

                                resultData.setType = SetType.THREE_OF_A_KIND;
                                resultData.rowRank = 0;

                                return resultData;

                            } else {

                                resultData.setType = SetType.HIGH_CARD;
                                resultData.rowRank = 0;

                            }

                        }

                    }

                }

            }

        }

        return resultData;
    }

}

public enum SetType {
    HIGH_CARD,
    ONE_PAIR,
    TWO_PAIR,
    THREE_OF_A_KIND,
    STRAIGHT,
    FLUSH,
    FULL_HOUSE,
    FOUR_OF_A_KIND,
    STRAIGHT_FLUSH
}

public class ResultData {
    public int rowRank;
    public SetType setType;
} 
