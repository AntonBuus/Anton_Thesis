using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveChange1 : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer left;
    [SerializeField] SkinnedMeshRenderer right;

    [SerializeField] System.Collections.Generic.List<UnityEngine.Material> mat;
     public void changeGloves()
    {
        left.SetMaterials(mat) ;
        right.SetMaterials(mat);
    }
}
