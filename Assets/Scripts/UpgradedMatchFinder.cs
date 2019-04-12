using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedMatchFinder : MonoBehaviour
{
    private Board board;
    public List<GameObject> CurrentMatch = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FindAllMathches()
    {
        StartCoroutine(FindAllMatchesCoroutine());
    }
    private IEnumerator FindAllMatchesCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject CurStone = board.allStones[i, j];
                if (CurStone != null){
                    if(i>0 && i < board.width-1){
                        GameObject leftStone = board.allStones[i - 1, j];
                        GameObject RightStone = board.allStones[i + 1, j];
                        if (RightStone != null && leftStone != null){
                            if (leftStone.tag == CurStone.tag && RightStone.tag == CurStone.tag)
                            {
                                if (!CurrentMatch.Contains(leftStone))
                                    CurrentMatch.Add(leftStone);
                                leftStone.GetComponent<Stone>().isMatched = true;
                                if (!CurrentMatch.Contains(RightStone))
                                    CurrentMatch.Add(RightStone);
                                RightStone.GetComponent<Stone>().isMatched = true;
                                if (!CurrentMatch.Contains(CurStone))
                                    CurrentMatch.Add(CurStone);
                                CurStone.GetComponent<Stone>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject DownStone = board.allStones[i, j-1];
                        GameObject UpStone = board.allStones[i, j+1];
                        if (UpStone != null && DownStone != null)
                        {
                            if (DownStone.tag == CurStone.tag && UpStone.tag == CurStone.tag)
                            {
                                if (!CurrentMatch.Contains(DownStone))
                                    CurrentMatch.Add(DownStone);
                                DownStone.GetComponent<Stone>().isMatched = true;
                                if (!CurrentMatch.Contains(UpStone))
                                    CurrentMatch.Add(UpStone);
                                UpStone.GetComponent<Stone>().isMatched = true;
                                if (!CurrentMatch.Contains(CurStone))
                                    CurrentMatch.Add(CurStone);
                                CurStone.GetComponent<Stone>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
