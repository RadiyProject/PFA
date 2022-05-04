using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF_Library
{
    [ServiceContract]
    public interface IWorkBook
    {
        [OperationContract]
        void AddBooks(Book kniga);
        [OperationContract]
        void DelBook(int id);
        [OperationContract]
        List<Book> GetBooks();
        [OperationContract]
        string VievNotes();
        [OperationContract]
        void AddNote(string note);
        [OperationContract]
        void AddAuthor(Author author);
        [OperationContract]
        List<Author> GetAuthors();
        [OperationContract]
        void DelAuthor(string name);

    }
}
