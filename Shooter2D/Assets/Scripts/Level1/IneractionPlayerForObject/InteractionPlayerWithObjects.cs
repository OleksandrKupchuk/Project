using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPlayerWithObjects : MonoBehaviour
{
    public Text textCountGemsRed;
    [SerializeField]
    int countGemsRed;
    // Start is called before the first frame update
    void Start()
    {
        textCountGemsRed.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        textCountGemsRed.text = "" + countGemsRed;
    }

    public void AddGemsRed()
    {
        countGemsRed++;
    }
}
