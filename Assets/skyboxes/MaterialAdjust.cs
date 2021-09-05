using UnityEngine;
using UnityEditor;
using System.Collections;

public class MaterialAdjust : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDrawGizmos()
    {
        Renderer rend = this.gameObject.GetComponent<Renderer>();

        rend.sharedMaterial.SetTextureScale("_MainTex", new Vector2(this.gameObject.transform.lossyScale.x, this.gameObject.transform.lossyScale.y));
    }

}


