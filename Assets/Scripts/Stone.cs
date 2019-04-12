using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stone : MonoBehaviour
{
    [Header("Board Var")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;
    //public 
    private UpgradedMatchFinder findMatches;
    private Board board;
    private GameObject otherStone;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    void Start()
    {
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<UpgradedMatchFinder>();
        /*targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        if (board.SecondRow)
        {
            targetY++;
            row++;
            board.SecondRow = false;
        }
        if (targetY == 0)
        {
            board.SecondRow = true;
        }*/
        //previousRow = row;
        //previousColumn = column;
    }
    void Update()
    {
       // FindMatches();
        /*if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);
        }*/
        targetX = column;
        targetY = row;
        //int tempColumn = column;
        //int tempRow = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allStones[column,row]!=this.gameObject){
                board.allStones[column, row] = this.gameObject;
            }
            findMatches.FindAllMathches();
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allStones[column, row] != this.gameObject)
            {
                board.allStones[column, row] = this.gameObject;
            }
            findMatches.FindAllMathches();
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
           // board.allStones[column, row] = this.gameObject;
        }
    }
    private void OnMouseDown()
    {
        if (board.CurrentState == GameState.Move)
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseUp()
    {
        if (board.CurrentState == GameState.Move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
        else
            board.CurrentState = GameState.Move;
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > 1f || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > 1f)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            Debug.Log(swipeAngle);
            MovePieces();
            board.CurrentState = GameState.Wait;
        }
    }

    void MovePieces()
    {
        if (swipeAngle > -15 && swipeAngle <= 25 && column < board.width - 1)
        {
            //Right
            otherStone = board.allStones[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 75 && swipeAngle <= 105 && row < board.height - 1)
        {
            //Up
            otherStone = board.allStones[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().row -= 1;
            row += 1;
        }
        else if (swipeAngle > 25 && swipeAngle <= 75 && row < board.height - 1 && column < board.width-1)
        {
            //Right Up
            otherStone = board.allStones[column + 1, row + 1];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().row -= 1;
            otherStone.GetComponent<Stone>().column -= 1;
            row += 1;
            column += 1;

        }
        else if (swipeAngle < -15 && swipeAngle >= -75 && row != 0 && column < board.width - 1)
        {
            //Right Down
            otherStone = board.allStones[column + 1, row -1];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().row += 1;
            otherStone.GetComponent<Stone>().column -= 1;
            row -= 1;
            column += 1;

        }
        else if((swipeAngle > 165 || swipeAngle <= -165) && column > 0 )
        {
            //Left
            otherStone = board.allStones[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().column += 1;
            column -= 1;
        }else if (swipeAngle < -75 && swipeAngle >= -105 && row != 0)
        {
            //Down
            otherStone = board.allStones[column, row-1];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().row += 1;
            row -= 1;
        }
        else if (swipeAngle <= 165 && swipeAngle >= 105 && row < board.height - 1 && column > 0)
        {
            //Left up
            otherStone = board.allStones[column - 1, row + 1];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().row -= 1;
            otherStone.GetComponent<Stone>().column += 1;
            row += 1;
            column -= 1;
        }
        else if (swipeAngle < -105 && swipeAngle > -165 && row != 0 && column > 0)
        {
            //Left Down
            otherStone = board.allStones[column - 1, row - 1];
            previousRow = row;
            previousColumn = column;
            otherStone.GetComponent<Stone>().row += 1;
            otherStone.GetComponent<Stone>().column += 1;
            row -= 1;
            column -= 1;
        }
        StartCoroutine(checkMove());
        //board.DestroyMatches();
    }
    void FindMatches()
    {
        if (column > 0 && column < board.width-1)
        {
            GameObject leftStone1 = board.allStones[column - 1, row];
            GameObject rightStone1 = board.allStones[column + 1, row];
            if (leftStone1 != null && rightStone1 != null)
            {
                if (leftStone1.tag == this.gameObject.tag && rightStone1.tag == this.gameObject.tag)
                {
                    leftStone1.GetComponent<Stone>().isMatched = true;
                    rightStone1.GetComponent<Stone>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row> 0 && row < board.height - 1)
        {
            GameObject DownStone1 = board.allStones[column, row-1];
            GameObject UpStone1 = board.allStones[column, row+1];
            if (DownStone1 != null && UpStone1 != null)
            {
                if (DownStone1.tag == this.gameObject.tag && UpStone1.tag == this.gameObject.tag)
                {
                    DownStone1.GetComponent<Stone>().isMatched = true;
                    UpStone1.GetComponent<Stone>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
    public IEnumerator checkMove() {
        yield return new WaitForSeconds(.5f);
        if (otherStone != null)
        {
            if (!isMatched && !otherStone.GetComponent<Stone>().isMatched)
            {
                otherStone.GetComponent<Stone>().row = row;
                otherStone.GetComponent<Stone>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.CurrentState = GameState.Move;
            }
            else{
                board.DestroyMatches();
            }
            otherStone = null;
        }

    }
}
