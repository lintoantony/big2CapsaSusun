using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersContainer : MonoBehaviour {

    [SerializeField]
    List<PlayerView> playerViews;

    [SerializeField]
    public List<Sprite> avatarSprites;

    // Start is called before the first frame update
    void Start(){
        InitPlayerHandCards();
    }

    private void InitPlayerHandCards() {

        for (int i = 0; i < 4; i++) {
            playerViews[i].OpendHandCards(false);
        }
    }

    public void OpenPlayerHandCards(int playerId, bool isOpen){

        playerViews[playerId].OpendHandCards(isOpen);
    }

    public void SetPlayerData(int playerId, PlayerData playerData) {
        playerData.id = playerId;
        playerViews[playerId].SetData(playerData);
    }

    public void SetPlayerHandCards(int playerId, List<CardData> handCardsData){
        playerViews[playerId].SetHandCards(handCardsData);
    }

}
