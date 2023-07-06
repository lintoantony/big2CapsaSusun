using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlots : MonoBehaviour {

    // All the slots
    [SerializeField]
    public List<CardDropSlot> allDropSlots;


    public CardDropSlot GetCardPickSlot(DropData dropData) {
        // To get the slot from where the card is picked...

        return allDropSlots.FirstOrDefault(i => (i.row == dropData.cardRow && i.column == dropData.cardColumn));
    }
}
