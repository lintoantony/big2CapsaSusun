using UnityEngine;
using TMPro;

public class Card : MonoBehaviour {

    public int row;
    public int column;

    public CardSuit cardSuit;

    public CardRank cardRank;

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

                break;
            case CardRank.Four:

                valStr = "4";

                break;
            case CardRank.Five:

                valStr = "5";

                break;
            case CardRank.Six:

                valStr = "6";

                break;
            case CardRank.Seven:

                valStr = "7";

                break;
            case CardRank.Eight:

                valStr = "8";

                break;
            case CardRank.Nine:

                valStr = "9";

                break;
            case CardRank.Ten:

                valStr = "10";

                break;
            case CardRank.Jack:

                valStr = "J";

                break;
            case CardRank.Queen:

                valStr = "Q";

                break;
            case CardRank.King:

                valStr = "K";

                break;
            case CardRank.Ace:

                valStr = "A";

                break;
            case CardRank.Two:

                valStr = "2";

                break;
        }

        txt.text = valStr;
    }
}
