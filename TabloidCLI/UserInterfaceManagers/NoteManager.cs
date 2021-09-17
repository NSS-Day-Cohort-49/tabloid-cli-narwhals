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

        private Note Choose(string promt = null)
        {
            if ( promt == null)
            {
                promt = "Please choose a Note:";
            }
            Console.WriteLine(promt);

            List<Note> notes = _noteRepository.GetAll();

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($"{i + 1}) {note.Title}");
            }
            Console.WriteLine("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return notes[choice - 1]; 
            }
            catch (Exception ex)
            {
                Console.Write("Invalid Selection");
                return null;
            }
        }

        private void ListNotes()
        {
            List<Note> notes = _noteRepository.GetAll();
            Console.WriteLine();
            Console.WriteLine("Notes List:");
            foreach (Note note in notes)
            {
                if (note.Post.Id == _postId)
                {
                    Console.WriteLine($"{note.Id}) {note.Title} - Content: {note.Content} - Publication Date: {note.CreateDateTime}");
                }
            }
            Console.WriteLine();
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
            Note noteToDelete = Choose("Which note would you like to remove?");
            if (noteToDelete != null)
            {
                _noteRepository.Delete(noteToDelete.Id);
            }
        }
    }
}
