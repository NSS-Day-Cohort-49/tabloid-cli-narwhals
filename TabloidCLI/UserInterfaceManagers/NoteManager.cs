using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class NoteManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private int _postId;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString, postId);
            _postRepository = new PostRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Note");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListNotes();
                    return this;
                case "2":
                    AddNote();
                    return this;
                case "3":
                    RemoveNote();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void ListNotes()
        {
            throw new NotImplementedException();
        }

        private void AddNote()
        {
            Console.WriteLine("New Note");
            Note note = new Note();

            Console.Write("Note Title: ");
            note.Title = Console.ReadLine();

            Console.Write("Body: ");
            note.Content = Console.ReadLine();

            note.CreateDateTime = DateTime.Now;

            note.Post = _postRepository.Get(_postId);

            _noteRepository.Insert(note);
            Console.WriteLine("Note Added");
            Console.WriteLine("");
        }

        private void RemoveNote()
        {
            throw new NotImplementedException();
        }
    }
}
