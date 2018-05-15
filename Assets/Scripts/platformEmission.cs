using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformEmission : MonoBehaviour {

    public Material material;

    private void OnEnable()
    {
        material.EnableKeyword("_EMISSION");
    }


}
