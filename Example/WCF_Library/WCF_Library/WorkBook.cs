using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Library
{
    
    public class WorkBook : IWorkBook
    {
        ApplicationContext db = new ApplicationContext();

        public int global_id = 0;
        public List<Book> books = new List<Book>();

        public void AddBooks(Book kniga)
        {
            db.Books.Add(kniga);
            db.SaveChanges();
        }

        public void DelBook(int id)
        {
            books.Remove(books.Find(e => e.id.Equals(id)));
        }

        public List<Book> GetBooks()
        {
            return books;
        }

        public void AddNote(string note)
        {
            StreamWriter dataFile = new StreamWriter(@"C:\Users\user\Desktop\Solutions\data.txt", true);
            string date = DateTime.Now.ToString();
            dataFile.WriteLine(date + ": " + note);
            dataFile.Close();
        }

        public string VievNotes()
        {
            StreamReader dataFile = new StreamReader(@"C:\Users\user\Desktop\Solutions\data.txt");
            string text = dataFile.ReadToEnd();
            dataFile.Close();
            return text;
        }

        public void AddAuthor(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public void DelAuthor(string name)
        {
            db.Authors.Remove(db.Authors.Find(name));
            db.SaveChanges();
        }
        public List<Author> GetAuthors()
        {
            List<Author> authors = db.Authors.ToList<Author>();
            return authors;
        }
    }
}
