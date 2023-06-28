using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private Material materialSwitch;

    private Material materialDefault;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        materialDefault = rend.materials[1];

        ShowMaterial();
    }

    public void HideMaterial()
    {
        List<Material> temp = new List<Material>(rend.materials);

        temp[1] = materialSwitch;
        rend.materials = temp.ToArray();
    }

    public void ShowMaterial()
    {
        List<Material> temp = new List<Material>(rend.materials);

        temp[1] = materialDefault;
        rend.materials = temp.ToArray();
    }

}
