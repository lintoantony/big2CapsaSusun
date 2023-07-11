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
        
    }

    public void InitPlayerHandCards() {

        for (int i = 0; i < 4; i++) {
            playerViews[i].OpendHandCards(false);
            playerViews[i].SetHandRanks(false, null);

            playerViews[i].StopEmojiAnim();
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

    public void SetHandRanks(bool isVisible, int playerId, List<ResultData> resultDataList) {

        playerViews[playerId].SetHandRanks(isVisible, resultDataList);
    }

    public void PlayEmojiAnim(int playerId, AnimType animType) {

        playerViews[playerId].PlayEmojiAnim(animType);
    }
}
