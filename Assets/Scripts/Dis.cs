using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dis : MonoBehaviour
{
    Text disText;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        disText = gameObject.GetComponent<Text>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // current score display
        time += Time.deltaTime;
        disText.text = ((int)(time * 100)).ToString();
    }
}
