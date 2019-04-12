using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState{Move,Wait}

public class Board : MonoBehaviour
{
    private UpgradedMatchFinder FindMatches;
    public int width;
    public int height;
    public int offSetRefill;
    public GameObject tilePrefab;
    public GameObject[] Stones;
    private BackgroundTile[,] alltiles;
    public GameObject[,] allStones;
    public bool SecondRow = false;
    public GameState CurrentState = GameState.Move;
    // Start is called before the first frame update
    void Start()
    {
        FindMatches = FindObjectOfType<UpgradedMatchFinder>();
        alltiles = new BackgroundTile[width, height];
        allStones = new GameObject[width, height];
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                //GameObject Background = Instantiate(tilePrefab, tempPosition/4, Quaternion.identity);
                //Background.transform.parent = this.transform;
                //Background.name= "( " + i + ", " + j + " )";
                int StonesToUse = Random.Range(0, Stones.Length);
                int MaxIt = 0;
                while (MatchesAt(i, j, Stones[StonesToUse]) && MaxIt < 100) {
                    StonesToUse = Random.Range(0, Stones.Length);
                }
                MaxIt = 0;
                GameObject Stone = Instantiate(Stones[StonesToUse], tempPosition, Quaternion.identity);
                Stone.GetComponent<Stone>().row = j;
                Stone.GetComponent<Stone>().column = i;
                Stone.transform.parent = this.transform;
                Stone.name = "( " + i + ", " + j + " )";
                allStones[i, j] = Stone;
            }
        }
    }
    private bool MatchesAt(int column, int row, GameObject stone)
    {
        if (column > 1 && row > 1)
        {
            if (allStones[column - 1, row].tag == stone.tag && allStones[column - 2, row].tag == stone.tag)
                return true;
            if (allStones[column, row - 1].tag == stone.tag && allStones[column, row - 2].tag == stone.tag)
                return true;
        } else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allStones[column, row - 1].tag == stone.tag && allStones[column, row - 2].tag == stone.tag)
                    return true;
            }
            if (column > 1)
                if (allStones[column - 1, row].tag == stone.tag && allStones[column - 2, row].tag == stone.tag)
                    return true;
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (allStones[column, row].GetComponent<Stone>().isMatched)
        {
            FindMatches.CurrentMatch.Remove(allStones[column, row]);
            Destroy(allStones[column, row]);
            allStones[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allStones[i, j] != null)
                    DestroyMatchesAt(i, j);
            }
        }
        StartCoroutine(DecreaseRowCol());
    }
    private IEnumerator DecreaseRowCol()
    {
        int DestroyedCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allStones[i, j] == null) {
                    DestroyedCount++;
                }
                else if (DestroyedCount > 0) {
                    allStones[i, j].GetComponent<Stone>().row -= DestroyedCount;
                    allStones[i, j] = null;
                }
            }
            DestroyedCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCoroutine());
    }

    private void Refill()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allStones[i, j] == null)
                {
                    Vector2 NullStonePos = new Vector2(i, j+offSetRefill);
                    int StoneToUse = Random.Range(0, Stones.Length);
                    GameObject newStone = Instantiate(Stones[StoneToUse], NullStonePos, Quaternion.identity);
                    allStones[i, j] = newStone;
                    newStone.GetComponent<Stone>().row = j;
                    newStone.GetComponent<Stone>().column = i;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private bool Matches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allStones[i,j] != null)
                    if (allStones[i, j].GetComponent<Stone>().isMatched)
                         return true;
            }
        }
    return false;
    }

    private IEnumerator FillBoardCoroutine()
    {
        Refill();
        yield return new WaitForSeconds(.5f);
        while (Matches())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        yield return new WaitForSeconds(.5f);
        CurrentState = GameState.Move;
    }
}
