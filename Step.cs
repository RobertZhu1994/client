using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour {

    public List<string> StuId = new List<string>();
    public List<string> StepNum = new List<string>();
    Dictionary<string, int> Upvote = new Dictionary<string, int>();
    public int len;
    public Step() { }
    public void StepAdd(string stuid, string stepnum)
    {
        if (Upvote.ContainsKey(stepnum))
            Upvote[stepnum]++;
        else
            Upvote.Add(stepnum, 1);

        StuId.Add(stuid);
        StepNum.Add(stepnum);
        len = StepNum.Count;
    }

}
