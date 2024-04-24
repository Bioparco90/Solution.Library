using DataAccessLayer.Library;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class TestBook
    {
        [TestMethod()]
        public void CreateBook()
        {
            Book book = new Book()
            {
                Title = "Harry Potter",
                Name = "Pippo",
                Surname = "Franco",
                PublishingHouse = "Salani"
            };
            DataTableAccess<Book> da = new();
            DataTable dt = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.Add(book));
            handler.Save();
        }
    }
}
