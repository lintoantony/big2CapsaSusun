using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerData : DataToPass {
    public int id;
    public string name;
    public string avatarId;
}

public class PlayerDummyData {
    public static List<PlayerData> playersList;

    public static List<PlayerData> GetPlayerList() {

        playersList = new List<PlayerData>();

        playersList.Add(new PlayerData { name = "Lin", avatarId = "1" });
        playersList.Add(new PlayerData { name = "Katherine", avatarId = "2" });
        playersList.Add(new PlayerData { name = "Irene", avatarId = "3" });
        playersList.Add(new PlayerData { name = "Manggala", avatarId = "4" });
        playersList.Add(new PlayerData { name = "Kaz", avatarId = "5" });
        playersList.Add(new PlayerData { name = "Rose", avatarId = "6" });
        playersList.Add(new PlayerData { name = "Evelyn", avatarId = "7" });
        playersList.Add(new PlayerData { name = "Ammu", avatarId = "8" });
        playersList.Add(new PlayerData { name = "Daisu", avatarId = "9" });
        playersList.Add(new PlayerData { name = "Antony", avatarId = "10" });


        Shuffle(playersList);

        return playersList;
    }

    public static void Shuffle<T>(List<T> list) {
        Random random = new Random();
        int n = list.Count;

        while (n > 1){
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
