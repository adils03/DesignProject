using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public string playerName;

    public void StartTurn()
    {
        Debug.Log(playerName + " oyuncunun sirasi basladi.");
    }
}
