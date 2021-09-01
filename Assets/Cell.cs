using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update
    public bool crossed = false;
    private GameObject cross;
    public MouseDownEvent mouseDown = new MouseDownEvent();
    public int i;
    public int j;
    public bool isSearched=false;
    void Start()
    {
        cross = this.gameObject.transform.GetChild(0).gameObject;
        Unmark();
    }
    void OnMouseDown(){
        mouseDown.Invoke(this);
    }    
    public void Mark()
    {
       cross.SetActive(true);
    }

    public void Unmark()
    {
        cross.SetActive(false);
    }

}
[System.Serializable]
public class MouseDownEvent : UnityEvent<Cell> { }