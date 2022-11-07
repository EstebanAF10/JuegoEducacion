using System;

[Serializable]
public class InputEntry {
    public string playerName;
    public int maxLevel;

    public InputEntry (string name, int maxLevel) {
        playerName = name;
        this.maxLevel = maxLevel;
    }
}
