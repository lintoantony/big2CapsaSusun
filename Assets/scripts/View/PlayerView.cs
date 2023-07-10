using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerView : MonoBehaviour {

    [SerializeField]
    private TMP_Text name;

    [SerializeField]
    private Image avatar;

    [SerializeField]
    private PlayerHandCards playerHandCards;

    [SerializeField]
    private PlayersContainer playersContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpendHandCards(bool isOpen){
        playerHandCards.OpenAllCards(isOpen);
    }

    public PlayerData playerData;
    public void SetData(PlayerData playerData){
        this.playerData = playerData;

        // Set the name
        name.text = playerData.name;

        // Set Avatar
        avatar.sprite = GetSprite(playerData.avatarId);

        this.gameObject.SetActive(true);
    }

    private Sprite GetSprite(string avatarId) {
        int avatarIdInt = int.Parse(avatarId);
        return playersContainer.avatarSprites[avatarIdInt];
    }

    public void SetHandCards(List<CardData> handCardsData){
        playerHandCards.SetCards(handCardsData);
    }

}
