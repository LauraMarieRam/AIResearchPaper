using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarWontStayDown : MonoBehaviour
{
    public Scrollbar scroll;
    // Start is called before the first frame update
    void Start()
    {
        scroll.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
