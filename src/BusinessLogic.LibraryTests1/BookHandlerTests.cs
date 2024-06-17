using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.DAO;
using BusinessLogic.Library.Authentication;
using Model.Library;

namespace BusinessLogic.Library.Tests
{
    [TestClass()]
    public class BookHandlerTests
    {
        [TestMethod()]
        public void SearchManyTest()
        {
            IBookHandler bh = new BookHandler(Session.GetInstance(), new BookRepository(new BookDAO(new DatabaseContext())), new ReservationHandler(Session.GetInstance(), new ReservationRepository(new ReservationDAO(new DatabaseContext()))));

            var r = bh.GetAll();
            Assert.IsNotNull(r);
        }
    }
}