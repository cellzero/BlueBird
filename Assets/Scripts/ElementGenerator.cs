using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElementGenerator : MonoBehaviour {

    public GameObject[] elements;
    public GameObject card;
    private CardFly cardController;
    

	// Use this for initialization
	void Start () {
        cardController = card.GetComponent<CardFly>();
        StartCoroutine(ElementGenerate());
    }
	
	// Update is called once per frame
	void Update () {
    
	}

    IEnumerator ElementGenerate() {
        
        while (true)
        {
            if (cardController.isStop()) yield return new WaitForSeconds(1);
            int i = Random.Range(0, 100);
            if (i < elements.Length)
            {
                Instantiate(elements[i], card.transform.position, card.transform.rotation);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

}
