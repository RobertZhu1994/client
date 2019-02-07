using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

public class Book {

    public List<string> comments = new List<string>();
    //public int StuId;
    //public List<int> Step = new List<int>();
    public List<string> StuId = new List<string>();
    public List<string> Step = new List<string>();
    public List<int> Upvote = new List<int>();
    public int len;
    public int start;
    //******************Reply Part**************************..
    public List<List<String>> Reply = new List<List<String>>();
    //public List<List<int>> Upvote_Reply = new List<List<int>>();
    public List<int> len_reply=new List<int>();
    //public int start_reply;
    public Book() { start = 0; }
    public void BookAdd(string comment, string stuId, string step)
    {
        comments.Add(comment);
        StuId.Add(stuId);
        Step.Add(step);
        Upvote.Add(0);
        len = comments.Count;
        Reply.Add(new List<string>());
        //Upvote_Reply.Add(new List<int>());
        len_reply.Add(0);
    }

    public void BookUpvote(int index)
    {

        Upvote[index]++;
    }

    public void Pin(int index)
    {
        string tmp_stuid, tmp_step, tmp_cmt;
        List<string> tmp_reply;
        int tmp_upvote, temp_lenreply;
        if (index == start)
        {
            start++;
        }
        //****************Swap process**********************//
        tmp_stuid = this.StuId[index];
        tmp_step = this.Step[index];
        tmp_cmt = this.comments[index];
        tmp_upvote = this.Upvote[index];
        temp_lenreply = this.len_reply[index];
        tmp_reply = this.Reply[index];


        for (int i = index; i > start; i--)
        {
            this.StuId[i] = this.StuId[i - 1];
            this.Step[i] = this.Step[i - 1];
            this.comments[i] = this.comments[i - 1];
            this.Upvote[i] = this.Upvote[i - 1];
            this.len_reply[i] = this.len_reply[i - 1];
            this.Reply[i] = this.Reply[i - 1];
        }

        this.StuId[start] = tmp_stuid;
        this.Step[start] = tmp_step;
        this.comments[start] = tmp_cmt;
        this.Upvote[start] = tmp_upvote;
        this.len_reply[start] = temp_lenreply;
        this.Reply[start] = tmp_reply;
    }

    public void ReplyComment(string reply, int comment_index,int stuid)
    {
        reply = "Student " + stuid  + "replied";
        Reply[comment_index].Add(reply);
        len_reply[comment_index]++;
        //Upvote_Reply[index].Add(0);
    }
    public void print()
    {
        for (int i = 0; i < len; i++)
            Console.WriteLine("comment: {0} corresponding to step {1} sent from student {2}", comments[i], Step[i], StuId);
        for (int i = 0; i < len_reply[0]; i++)
            Console.WriteLine("Reply to comment {0} are {1}", comments[0], Reply[i]);
    }
   
}
