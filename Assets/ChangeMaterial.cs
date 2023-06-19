using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] Renderer rend;

    Material mat;

    [SerializeField] Material test2;

    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        mat = rend.materials[1];

        ShowMaterial();


    }

    public void HideMaterial()
    {
        List<Material> temp = new List<Material>(rend.materials);

        temp[1] = test2;
        rend.materials = temp.ToArray();

        Debug.Log(rend.materials[1]);
    }

    public void ShowMaterial()
    {
        List<Material> temp = new List<Material>(rend.materials);

        temp[1] = mat;
        rend.materials = temp.ToArray();
    }





}
