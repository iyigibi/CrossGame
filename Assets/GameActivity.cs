using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActivity : MonoBehaviour
{
    public int n;
    public GameObject cell;
    public bool[,] GameState;
    private List<Cell> cells = new List<Cell>();
    

    // Start is called before the first frame update
    void Start()
    {
        GameState = new bool[n,n];
        CreateGrid();
       
       
       float screenRatio = (float)Screen.width / (float)Screen.height;
        float nf=(float)n;
         if (screenRatio >= 1)
         {
             Camera.main.orthographicSize = nf/2;
         }
         else
         {
             float differenceInSize = 1 / screenRatio;
             Camera.main.orthographicSize = nf/2;
         }
 
         Camera.main.transform.position = new Vector3(nf/2, nf/2, -1f);


    }

    void CreateGrid()
    {
        for(int i=0;i<n;i++){
            for(int j=0;j<n;j++){
                GameState[i,j]=false;
                GameObject go = GameObject.Instantiate(cell);
                
                go.transform.Translate(new Vector3(i+0.5f,j+0.5f,0));
                go.transform.localScale=new Vector3(0.9f,0.9f,0);
                Cell goCell=go.GetComponent<Cell>();
                cells.Add(goCell);
                goCell.mouseDown.AddListener(MouseEventHandler);
                goCell.isSearched=false;
                goCell.i=i;
                goCell.j=j;
            }
        }
    }

    private void MouseEventHandler(Cell targetCell) {
        int x=targetCell.i;
        int y=targetCell.j;
        if(!GameState[x,y]){
            GameState[x,y]=true;
            targetCell.Mark();
            //Debug.Log(cells[x*n+y].i+"r"+cells[x*n+y].j);
            int x_=0;
            int y_=0;

            List<Cell> neighborCells = new List<Cell>();
            neighborCells.Add(targetCell);
            targetCell.isSearched=true;
            bool AOK=true;
            int tryNum=0;

            while(AOK)
            {
                cells[(x+x_)*n+y+y_].isSearched=true;
                if(x+x_<n-1 && GameState[x+x_+1,y+y_] ){
                    Cell rigth=cells[(x+x_+1)*n+y+y_];
                    if(!neighborCells.Contains(rigth)){
                        neighborCells.Add(rigth);
                    }
                }
                if(x+x_>0 && GameState[x+x_-1,y+y_]){
                    Cell left=cells[(x+x_-1)*n+y+y_];
                    if(!neighborCells.Contains(left)){
                        neighborCells.Add(left);
                    }
                }
                if(y+y_<n-1 && GameState[x+x_,y+y_+1]){
                    Cell up=cells[(x+x_)*n+(y+y_+1)];
                    if(!neighborCells.Contains(up)){
                        neighborCells.Add(up);
                    }
                }
                if(y+y_>0 && GameState[x+x_,y+y_-1]){
                    Cell down=cells[(x+x_)*n+(y+y_-1)];
                if(!neighborCells.Contains(down)){
                        neighborCells.Add(down);
                    }
                }
                AOK=false;
                foreach(Cell item in neighborCells)
                {
                    if(!item.isSearched){
                        
                        AOK=true;
                        x_=item.i-x;
                        y_=item.j-y;
                    }
                }
                
                
                Debug.Log(neighborCells.Count);
                tryNum++;
                
                
            }
        
        if(neighborCells.Count>2){
            Debug.Log("Scored x"+(neighborCells.Count-2));
            foreach(Cell item in neighborCells)
            {
                item.Unmark();
                GameState[item.i,item.j]=false;
            }
        }

        foreach(Cell item in neighborCells)
        {
            item.isSearched=false;
        }
        neighborCells.Clear();

        }
        
        
    }

   
}
