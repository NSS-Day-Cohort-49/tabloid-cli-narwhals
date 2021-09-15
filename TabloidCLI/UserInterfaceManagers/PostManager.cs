using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }



        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title:");
            post.Title = Console.ReadLine();

            Console.Write("URL:");
            post.Url = Console.ReadLine();

            Console.Write("Publication Date:(DD/MM/YYYY)");
            DateTime publishDate = DateTime.Parse(Console.ReadLine());
            post.PublishDateTime = publishDate;

            Console.Write("Author:");
            ListAuthors();

            int selectedAuth = int.Parse(Console.ReadLine());
            post.Author = _authorRepository.Get(selectedAuth);

            Console.Write("Blog:");
            ListBlogs();

            int selectedBlog = int.Parse(Console.ReadLine());
            post.Blog = _blogRepository.Get(selectedBlog);

            _postRepository.Insert(post);

        }
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Id}) {post.Title} {post.Url}");
            }
        }

        private Post Choose(string prompt = null)
        {
            if ( prompt == null)
            {
                 prompt = "Please choose an Post:";
            }
            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title} {post.Url}");
            }
            Console.WriteLine("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void ListAuthors()
        {
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}) {author.ToString()}");
            }
        }

        private void ListBlogs()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id}) {blog.Title}");
            }
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New Url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New Publication Date (DD/MM/YYYY) (blank to leave unchanged): ");
            var publishDateTime = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(publishDateTime))
            {
                    postToEdit.PublishDateTime = DateTime.Parse(publishDateTime);
                
            }
            Console.WriteLine("Authors List to Update");
            List<Author> authors = _authorRepository.GetAll();
            for (int i = 0; i < authors.Count; i++)
            {
                Author updateAuthor = authors[i];
                Console.WriteLine($" {i + 1}) {updateAuthor.FullName}"); // could also use ToString() method
            }
            Console.Write("> ");
            var authorIndex = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorIndex))
            {
                postToEdit.Author = authors[int.Parse(authorIndex) - 1];
            }
            Console.WriteLine("Blogs List to Update");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog updateBlog = blogs[i];
                Console.WriteLine($" {i + 1}) {updateBlog.ToString()}"); 
            }
            Console.Write("> ");
            var blogIndex = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blogIndex))
            {
                postToEdit.Blog = blogs[int.Parse(blogIndex) - 1];
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}
