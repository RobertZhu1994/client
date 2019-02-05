/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInstance : MonoBehaviour {

    public class Book
    {
        public List<string> comments = new List<string>();
        //public int StuId;
        //public List<int> Step = new List<int>();
        public List<string> StuId = new List<string>();
        public List<string> Step = new List<string>();
        public List<int> Upvote = new List<int>();
        public int len;
        public int start;
        public Book() { start = 0; }
        public void BookAdd(string comment, string stuId, string step)
        {
            comments.Add(comment);
            StuId.Add(stuId);
            Step.Add(step);
            Upvote.Add(0);
            len = comments.Count;
        }
        public void BookUpvote(int index)
        {

            Upvote[index]++;
        }
        public void Pin(int index)
        {
            string tmp_stuid, tmp_step, tmp_cmt;
            int tmp_upvote;
            if (index == start)
            {
                start++;
            }
            //****************Swap process**********************//*
            tmp_stuid = this.StuId[index];
            tmp_step = this.Step[index];
            tmp_cmt = this.comments[index];
            tmp_upvote = this.Upvote[index];

            for (int i = index; i > start; i--)
            {
                this.StuId[i] = this.StuId[i - 1];
                this.Step[i] = this.Step[i - 1];
                this.comments[i] = this.comments[i - 1];
                this.Upvote[i] = this.Upvote[i - 1];
            }

            this.StuId[start] = tmp_stuid;
            this.Step[start] = tmp_step;
            this.comments[start] = tmp_cmt;
            this.Upvote[start] = tmp_upvote;
        }
        public void print()
        {
            for (int i = 0; i < len; i++)
                Console.WriteLine("comment: {0} corresponding to step {1} sent from student {2}", comments[i], Step[i], StuId);
        }
    }
}
*/