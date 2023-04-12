using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressEraser : MonoBehaviour
{
    public void Delete()
    {
        Saving.ClearSavedData();
    }
}
