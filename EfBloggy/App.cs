using System;
using System.Collections.Generic;
using System.Text;

namespace EfBloggy
{
    public class App
    {
        static BlogContext context = new BlogContext();

        public void Run()
        {
            //Functions.ClearDatabase();                //Körs en gång sedan kommenteras ut
            //Functions.AddSomeTitles();                //Körs en gång sedan kommenteras ut
            MainMenu();
        }
        public void MainMenu()
        {
            Header("Huvudmeny");

            ShowAllBlogPostsBrief();

            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("a) Gå till huvudmeny");
            Console.WriteLine("b) Uppdatera en blogpost");
            Console.WriteLine("c) Skapa en ny blogpost");
            Console.WriteLine("D) Ta bort en blogpost");
            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.A)
                MainMenu();

            if (command == ConsoleKey.B)
                PageUpdatePost();

            if (command == ConsoleKey.C)
                PageCreatePost();

            if (command == ConsoleKey.D)
                PageDeletePost();
        }

        private void PageUpdatePost()
        {
            Header("Uppdatera");

            ShowAllBlogPostsBrief();

            Write("\nVilken bloggpost vill du uppdatera? ");

            int blogPostId = int.Parse(Console.ReadLine());

            var blogPost = context.BlogPosts.Find(blogPostId);

            WriteLine("Den nuvarande titeln är: " + blogPost.Title);

            Write("Skriv in ny titel: ");

            string newTitle = Console.ReadLine();

            blogPost.Title = newTitle;

            context.BlogPosts.Update(blogPost);
            context.SaveChanges();

            Write("Bloggposten uppdaterad.");
            Console.ReadKey();
            MainMenu();
        }

        private void PageCreatePost() 
        {
            Header("Skapa");
            Write("Skriv en ny titel:");
            var blogPost = new BlogPost();
            blogPost.Title = Console.ReadLine();
            Write("Skriv författare namn:");
            blogPost.Author = Console.ReadLine();
            context.BlogPosts.Add(blogPost);
            context.SaveChanges();
            Write("Bloggposten skapat.");
            Console.ReadKey();
            MainMenu();
        }

        private void PageDeletePost() 
        {
            Header("Ta bort");
            ShowAllBlogPostsBrief();
            Write("\nVilken bloggpost vill du ta bort? ");
            int blogPostId = int.Parse(Console.ReadLine());
            var blogPost = context.BlogPosts.Find(blogPostId);
            context.BlogPosts.Remove(blogPost);
            context.SaveChanges();
            Write("Bloggposten tog bort.");
            Console.ReadKey();
            MainMenu();
        }
        private void ShowAllBlogPostsBrief()
        {
            foreach (var x in context.BlogPosts)
            {
                WriteLine(x.Id.ToString().PadRight(5) + x.Title.PadRight(30) + x.Author.PadRight(20));
            }
        }

        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
        }
        private void WriteLine(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }

}
