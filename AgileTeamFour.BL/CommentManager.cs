﻿
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace AgileTeamFour.BL
{
    public static class CommentManager
    {
        /*
        //Adding a value to any foreign key requires the foreign key value first exist as a a primary key value of the referenced table
        //Comments.AuthorID uses Players.PlayerID as a foreign key
        //Comments.EventID uses Event.EventID as a foreign key
        public static int Insert(Comment comment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblComment entity = new tblComment();
                    entity.CommentID = dc.tblComments.Any() ? dc.tblComments.Max(s => s.CommentID) + 1 : 1;           
                    entity.TimePosted = comment.TimePosted;              
                    entity.AuthorName = comment.AuthorName;
                    entity.Text = comment.Text;

                    //Must Check that EventID and AuthorID are valid values in the Events Table and Players Table
                    //*********************

                    int id = EventManager.LoadById(comment.EventID).EventID;
                    if (id == -99) //If -99, Must not be a primary key with the EventID (foreign key value) in Event Table
                        throw new Exception();
                    else
                        entity.EventID = comment.EventID;

                    id = PlayerManager.LoadById(comment.AuthorID).PlayerID;
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in Player Table
                        throw new Exception();
                    else
                        entity.AuthorID = comment.AuthorID;

                    //*********************



                    // IMPORTANT - BACK FILL THE ID
                    comment.CommentID = entity.CommentID;

                    dc.tblComments.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Comment comment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblComment entity = dc.tblComments.Where(e => e.CommentID == comment.CommentID).FirstOrDefault();
                    entity.TimePosted = comment.TimePosted;
                    entity.AuthorName = comment.AuthorName;
                    entity.Text = comment.Text;

                    //Must Check that EventID and AuthorID are valid values in the Events Table and Players Table
                    //*********************

                    int id = EventManager.LoadById(comment.EventID).EventID;
                    if (id == -99) //If -99, Must not be a primary key with the EventID (foreign key value) in Event Table
                        throw new Exception();
                    else
                        entity.EventID = comment.EventID;

                    id = PlayerManager.LoadById(comment.AuthorID).PlayerID;
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in Player Table
                        throw new Exception();
                    else
                        entity.AuthorID = comment.AuthorID;

                    //*********************

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblComment entity = dc.tblComments.Where(e => e.CommentID == id).FirstOrDefault();

                    dc.tblComments.Remove(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Comment LoadById(int id)
        {
            try
            {
                Comment comment = new Comment();
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblComment entity = dc.tblComments.Where(e => e.CommentID == id).FirstOrDefault();
                    comment.CommentID = id;
                    comment.TimePosted = entity.TimePosted;
                    comment.AuthorName = entity.AuthorName;
                    comment.Text = entity.Text;

                    comment.AuthorID = entity.AuthorID;
                    comment.EventID = entity.EventID;


                    return comment;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<Comment> Load()
        {
            try
            {
                List<Comment> list = new List<Comment>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from c in dc.tblComments
                     select new
                     {
                         c.CommentID,
                         c.TimePosted,
                         c.AuthorName,
                         c.Text,
                         c.AuthorID,
                         c.EventID
                     })
                     .ToList()
                     .ForEach(comment => list.Add(new Comment
                     {
                         CommentID = comment.CommentID,
                         TimePosted = comment.TimePosted,
                         AuthorName = comment.AuthorName,
                         Text = comment.Text,
                         AuthorID = comment.AuthorID,
                         EventID = comment.EventID
                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        */
    }
}

