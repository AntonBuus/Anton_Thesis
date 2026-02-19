using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveTest : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer Lhand;
    [SerializeField] SkinnedMeshRenderer Rhand;

    [SerializeField] System.Collections.Generic.List<UnityEngine.Material> Mat;

    [SerializeField] string tag;

    public void changeGlove()
    {
        Lhand.SetMaterials(Mat);
        Rhand.SetMaterials(Mat);
    }
}
