using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICategoryServiceTest
    {
        private static IKernel kernel;
        private static ICategoryService categoryService;
        private TransactionScope transactionScope;
        public TestContext TestContext { get; set; }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            categoryService = kernel.Get<ICategoryService>();

        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestMethod]
        public void TestCreateCategoryAndFindThem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                categoryService.RegisterCategory("Prueba"); 
                categoryService.RegisterCategory("Prueba2"); 
                List<Category> categoryList = categoryService.FindAllCategories(0, 9); 
                Assert.AreEqual(categoryList.Count(), 2);
                Assert.AreEqual(categoryList[0].name, "Prueba");
                Assert.AreEqual(categoryList[1].name, "Prueba2");
            }
        }

        public void TestSameCategoryNameDoesNotCreate()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                categoryService.RegisterCategory("Prueba");
                categoryService.RegisterCategory("Prueba");

                List<Category> categoryList = categoryService.FindAllCategories(0, 9);
                Assert.AreEqual(categoryList.Count(), 1);
            }
        }
    }
}
