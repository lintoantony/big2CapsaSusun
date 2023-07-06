using System.Collections;
using System.Collections.Generic;

public enum CardSuit {
    Diamonds,
    Clubs,
    Hearts,
    Spades
}

public enum CardRank {
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
    Two
}

public class CardData{
    public CardSuit Suit { get; set; }
    public CardRank Rank { get; set; }
}
