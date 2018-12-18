using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour {

    public GameObject prefab;
    public float gap;
    public float sizePerBox;

    [SerializeField]
    private int boxesInWidth = 10;
    [SerializeField]
    private int boxesInHeight = 10;
    [SerializeField]
    private int boxesInDeepth = 10;

    private void Start ()
    {
        float stripe = sizePerBox + gap;

	    for(int i = 0; i < boxesInWidth; i++)
        {
            for(int j = 0; j < boxesInHeight; j++)
            {
                for(int k = 0; k < boxesInDeepth; k++)
                {
                    GameObject go = Instantiate(prefab, new Vector3(i * stripe, j * stripe, k * stripe), Quaternion.identity);
                }
            }
        }
	}

}
