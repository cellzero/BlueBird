using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour {

    public GameObject[] elements;
    public GameObject card;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        ElementGenerate(Random.Range(0,150));
	}

    void ElementGenerate(int i) {
        if(i < elements.Length)
            Instantiate(elements[i], card.transform.position, card.transform.rotation);
    }

}
