using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Suit : MonoBehaviour {

    [SerializeField]
    public List<Sprite> suitSprites;

    private Image imageComp;

    private void Start(){
        imageComp = GetComponent<Image>();
    }

    public void SetType(CardSuit type) {

        Sprite sprite = null;

        switch (type){
            case CardSuit.Clubs:

                sprite = suitSprites[0];

                break;

            case CardSuit.Diamonds:

                sprite = suitSprites[1];

                break;

            case CardSuit.Hearts:

                sprite = suitSprites[2];

                break;

            case CardSuit.Spades:

                sprite = suitSprites[3];

                break;
        }

        if (imageComp == null) {
            imageComp = GetComponent<Image>();
        }

        imageComp.sprite = sprite;
    }
}
