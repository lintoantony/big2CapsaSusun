using UnityEngine;
using TMPro;

public class Card : MonoBehaviour {

    public int row;
    public int column;

    public CardSuit cardSuit;

    public CardRank cardRank;

    public int cardValue;

    [SerializeField]
    private Suit suit;

    [SerializeField]
    public TMP_Text txt;

    private void Start() {
    }

    public void SetSuite(CardSuit type) {
        this.cardSuit = type;
        suit.SetType(type);
    }

    public void SetValue(CardRank val){

        this.cardRank = val;

        string valStr = "";

        switch (val){
            case CardRank.Three:

                valStr = "3";
                cardValue = 3;

                break;
            case CardRank.Four:

                valStr = "4";
                cardValue = 4;

                break;
            case CardRank.Five:

                valStr = "5";
                cardValue = 5;

                break;
            case CardRank.Six:

                valStr = "6";
                cardValue = 6;

                break;
            case CardRank.Seven:

                valStr = "7";
                cardValue = 7;

                break;
            case CardRank.Eight:

                valStr = "8";
                cardValue = 8;

                break;
            case CardRank.Nine:

                valStr = "9";
                cardValue = 9;

                break;
            case CardRank.Ten:

                valStr = "10";
                cardValue = 10;

                break;
            case CardRank.Jack:

                valStr = "J";
                cardValue = 11;

                break;
            case CardRank.Queen:

                valStr = "Q";
                cardValue = 12;

                break;
            case CardRank.King:

                valStr = "K";
                cardValue = 13;

                break;
            case CardRank.Ace:

                valStr = "A";
                cardValue = 14;

                break;
            case CardRank.Two:

                valStr = "2";
                cardValue = 15;

                break;
        }

        txt.text = valStr;
    }
}
