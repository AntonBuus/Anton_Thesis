using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGloveChange : MonoBehaviour
{

    public EquipGloves EquipGoves;
    public void callGloves()
    {
        EquipGoves.ChangeMaterialOnCall();
    }
}
