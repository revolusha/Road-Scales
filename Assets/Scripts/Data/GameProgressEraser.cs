using UnityEngine;

public class GameProgressEraser : MonoBehaviour
{
    public void Delete()
    {
        Saving.ClearSavedData();
    }
}