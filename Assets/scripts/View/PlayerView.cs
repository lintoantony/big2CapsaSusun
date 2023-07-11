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

    [SerializeField]
    private EmojiController emojiController;

    // Start is called before the first frame update
    void Start() {
        emojiController.gameObject.SetActive(false);
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

    public void SetHandRanks(bool isVisible, List<ResultData> resultDataList) {

        playerHandCards.SetHandRanks(isVisible, resultDataList);
    }

    public void PlayEmojiAnim(AnimType animType) {

        emojiController.gameObject.SetActive(true);

        emojiController.PlayAnim(animType);
    }

    public void StopEmojiAnim() {

        emojiController.gameObject.SetActive(false);
    }
}
