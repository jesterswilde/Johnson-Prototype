using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour {

    public GameObject prefab;
    public float gap;
    //public float sizePerBox;

    [SerializeField]
    private int boxesInWidth;
    [SerializeField]
    private int boxesInHeight;
    [SerializeField]
    private int boxesInDeepth;

    private void Start ()
    {
        float stripe = gap + prefab.transform.localScale.x;
        Debug.Log(stripe);
	    for(int i = 0; i < boxesInWidth; i++)
        {
            for(int j = 0; j < boxesInHeight; j++)
            {
                for(int k = 0; k < boxesInDeepth; k++)
                {
                    GameObject go = Instantiate(prefab, new Vector3(i * stripe, j * stripe, k * stripe), Quaternion.identity);
                    go.transform.parent = gameObject.transform;
                }
            }
        }
	}

}
