using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.UnitTest;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.IO;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IImageServiceTest
    {
        private TransactionScope transanctionScope;
        private static IKernel kernel;
        private static IUserService userProfileService;
        private static IImageService imageService;
        private static ICategoryService categoryService;
        private const string loginName = "loginNameTest";
        private const string enPassword = "password";
        private const string language = "es";
        private const string language1 = "en";
        private const string firstName = "firstName";
        private const string firstName1 = "firstName1";
        private const string lastName = "lastName";
        private const string lastName1 = "lastName1";
        private const string email = "a@a";
        private const string email1 = "a@1";
        private const string country = "es";
        private const string country1 = "en";
        
        public TestContext TestContext { get; set; }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            transanctionScope = new TransactionScope();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            transanctionScope.Dispose();
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userProfileService = kernel.Get<IUserService>();
            imageService = kernel.Get<IImageService>();
            categoryService = kernel.Get<ICategoryService>();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestMethod]
        public void TestUploadImage()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat1");

                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad", "asd",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                List<String> tags = new List<string>();

                UploadImageDto uploadImage = new UploadImageDto(userId, "title", "description","cat1", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);

                long imageId = imageService.uploadImage(uploadImage);
                List<Image> imgUser1 = imageService.GetImages(userId, 0, 9);
                Assert.AreEqual(imgUser1[0].title, uploadImage.title);

            }
        }

        [TestMethod]
        public void testfindByFilter()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat2");
                long categoryId2 = categoryService.RegisterCategory("cat3");

                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad1", "asd1",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));

                List<String> tags = new List<string>();

                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat2", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                UploadImageDto uploadImage2 = new UploadImageDto(userId, "arbol2", "bosque2", "cat2", 1, 1, 1, FileStream.Null, "filename", "arbol2.jpg", tags);
                UploadImageDto uploadImage3 = new UploadImageDto(userId, "arbol3", "bosque3", "cat2", 1, 1, 1, FileStream.Null, "filename", "arbol3.jpg", tags);

                long imageId = imageService.uploadImage(uploadImage);
                long imageId2 = imageService.uploadImage(uploadImage2);
                long imageId3 = imageService.uploadImage(uploadImage3);
                 
                List<Image> imgUser1 = imageService.findByFilter(null, null, 0, 0, 9);
                Assert.AreEqual(imgUser1[0].title, uploadImage.title);
                Assert.AreEqual(imgUser1[1].title, uploadImage2.title);
                Assert.AreEqual(imgUser1[2].title, uploadImage3.title);

                List<Image> imgUser2 = imageService.findByFilter("arbol", null, categoryId, 0, 9);
                Assert.AreEqual(imgUser1[0].title, uploadImage.title);
                Assert.AreEqual(imgUser1[1].title, uploadImage2.title);

                List<Image> imgUser3 = imageService.findByFilter(null, "bosque", categoryId, 0, 9);
                Assert.AreEqual(imgUser3[0].title, uploadImage.title);
                Assert.AreEqual(imgUser3.Count, 3);

                List<Image> imgUser4 = imageService.findByFilter("arbol", "bosque", categoryId, 0, 9);
                Assert.AreEqual(imgUser4[0].title, uploadImage.title);
                Assert.AreEqual(imgUser4.Count, 3);

            }
        }

        [TestMethod]
        public void testDoLike()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat4");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad2", "asd2",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                List<String> tags = new List<string>();

                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat4", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                long imageId = imageService.uploadImage(uploadImage);

                imageService.doLike(userId, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 1);
                imageService.doLike(userId, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 1);
                imageService.doLike(userId2, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 2);

            }
        }
        [TestMethod]
        public void testDeleteLike()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                List<String> tags = new List<string>();
                long categoryId = categoryService.RegisterCategory("cat5");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad3", "asd3",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat5", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                long imageId = imageService.uploadImage(uploadImage);

                imageService.doLike(userId, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 1);
                imageService.doLike(userId2, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 2);
                imageService.deleteLike(userId, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 1);
                imageService.deleteLike(userId2, imageId);
                Assert.AreEqual(imageService.getNumberOfLikes(imageId), 0);

            }
        }
        [TestMethod]
        public void testAddCommentCountComment()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                List<String> tags = new List<string>();
                String prueba = "muy buen arbol";
                long categoryId = categoryService.RegisterCategory("cat6");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad4", "asd4",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat6", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                long imageId = imageService.uploadImage(uploadImage);

                long commentId = imageService.uploadComment(userId, prueba, imageId);
                List<Comment> comments = imageService.GetComments(imageId, 0, 9);
                Assert.AreEqual(comments[0].content, prueba);
                Assert.AreEqual(comments.Count, 1);


                long commentId2 = imageService.uploadComment(userId, "asda", imageId);
                //List<Comment> comments2 = imageService.GetComments(imageId);
                Assert.AreEqual(imageService.getNumberOfComments(imageId), 2);
            }
        }

        [TestMethod]
        public void testDeleteComment()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                String prueba = "muy buen arbol";
                long categoryId = categoryService.RegisterCategory("cat7");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad5", "asd5",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                List<String> tags = new List<string>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat7", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);

                long imageId = imageService.uploadImage(uploadImage);

                long commentId = imageService.uploadComment(userId, prueba, imageId);
                List<Comment> comments = imageService.GetComments(imageId, 0, 9);
                Assert.AreEqual(comments[0].content, prueba);
                Assert.AreEqual(comments.Count, 1);

                imageService.deleteComment(imageId, commentId);
                List<Comment> comments2 = imageService.GetComments(imageId, 0, 9);
                Assert.AreEqual(comments2.Count, 0);

            }
        }

        [TestMethod]
        public void testUpdateComment()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                String prueba = "muy buen arbol";
                long categoryId = categoryService.RegisterCategory("cat8");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad6", "asd6",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                List<String> tags = new List<string>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat8", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);

                long imageId = imageService.uploadImage(uploadImage);

                long commentId = imageService.uploadComment(userId, prueba, imageId);
                List<Comment> comments = imageService.GetComments(imageId, 0, 9);
                Assert.AreEqual(comments[0].content, prueba);
                Assert.AreEqual(comments.Count, 1);

                imageService.updateComment(userId, comments[0].commentId, "hola");
                List<Comment> comments2 = imageService.GetComments(imageId, 0, 9);
                Assert.AreEqual(comments2[0].content, "hola");

                imageService.updateComment(userId2, comments[0].commentId, "gol");
                List<Comment> comments3 = imageService.GetComments(imageId, 0, 9);
                Assert.AreEqual(comments3[0].content, "hola");
            }
        }

        [TestMethod]
        public void testAddTag()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat9");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad7", "asd7",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                List<String> tags = new List<String>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol", "bosque1", "cat9", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);

                long imageId = imageService.uploadImage(uploadImage);
                tags.Add("tag1");
                imageService.addTags(tags, imageId, userId);

                List<Image> images = imageService.GetImagesByTag("tag1", 0, 9);
                Assert.AreEqual(images[0].title, "arbol");

            }
        }

        [TestMethod]
        public void testDeleteTag()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat10");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad8", "asd8",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));
                List<String> tags = new List<String>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol", "bosque1", "cat10", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                long imageId = imageService.uploadImage(uploadImage);

                tags.Add("tag1");
                imageService.addTags(tags, imageId, userId);

                List<Image> images = imageService.GetImagesByTag("tag1", 0, 9);
                Assert.AreEqual(images[0].title, "arbol");
                List<Tag> tags1 = imageService.GetTags(0, 9);

                imageService.deleteTags(tags1[0].tagId, imageId, userId);
                List<Image> images2 = imageService.GetImagesByTag("tag1", 0, 9);
                Assert.AreEqual(images2.Count, 0);
            }
        }


        [TestMethod]
        public void testGetTagsAndImagesOnTags()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat11");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad9", "asd9",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));

                List<String> tags = new List<String>();
                List<String> tags2 = new List<String>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat11", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                UploadImageDto uploadImage2 = new UploadImageDto(userId, "arbol2", "bosque2", "cat11", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags2);

                long imageId = imageService.uploadImage(uploadImage);
                long imageId2 = imageService.uploadImage(uploadImage2);

                tags.Add("Tag1");
                imageService.addTags(tags, imageId, userId);
                tags.Add("tag2");
                imageService.addTags(tags, imageId2, userId);

                List<Tag> tagL = imageService.GetTags(0, 9);
                Assert.AreEqual(tagL[0].title, "tag1");
                Assert.AreEqual(tagL[1].title, "tag2");

                Assert.AreEqual(imageService.getNumberOfImagesTag("tag1"), 2);
            }
        }
        [TestMethod]
        public void testUpdateTag()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long categoryId = categoryService.RegisterCategory("cat12");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad10", "asd10",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));

                List<String> tags = new List<String>();
                List<String> tags2 = new List<String>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat12", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                UploadImageDto uploadImage2 = new UploadImageDto(userId, "arbol2", "bosque2", "cat12", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags2);

                long imageId = imageService.uploadImage(uploadImage);
                long imageId2 = imageService.uploadImage(uploadImage2);

                tags.Add("tag1");
                imageService.addTags(tags, imageId, userId);
                imageService.addTags(tags, imageId2, userId);

                List<Tag> tagL = imageService.GetTags(0, 9);
                tagL[0].title = "Hola";
                imageService.updateTag(tagL[0]);

                List<Tag> tagL2 = imageService.GetTags(0, 9);
                Assert.AreEqual("hola", tagL2[0].title);
            }

        }
        [TestMethod]
        public void testGetTagsPerUsage()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                String prueba = "muy buen arbol";
                long categoryId = categoryService.RegisterCategory("cat13");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad11", "asd11",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));

                List<String> tags = new List<String>();
                List<String> tags2 = new List<String>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat13", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                UploadImageDto uploadImage2 = new UploadImageDto(userId, "arbol2", "bosque2", "cat13", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags2);

                long imageId = imageService.uploadImage(uploadImage);
                long imageId2 = imageService.uploadImage(uploadImage2);

                long commentId = imageService.uploadComment(userId, prueba, imageId);
                long commentId2 = imageService.uploadComment(userId2, prueba, imageId);

                tags.Add("tag1");
                imageService.addTags(tags, imageId, userId);
                tags.Add("tag2");
                imageService.addTags(tags, imageId2, userId);

                List<TagDto> tagDtos = imageService.getTagsOrderByUsage(0, 9);
                Assert.AreEqual(tagDtos[0].title, "tag1");
                Assert.AreEqual(tagDtos[0].usage, 2);

                Assert.AreEqual(tagDtos[1].title, "tag2");
                Assert.AreEqual(tagDtos[1].usage, 0);




            }
        }




        [TestMethod]
        public void TestGetCreatorsImages()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                String prueba = "muy buen arbol";
                long categoryId = categoryService.RegisterCategory("cat14");
                long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));
                long userId2 = userProfileService.RegisterUser("adasdad11", "asd11",
                          new UserDetails(firstName1, lastName1, email1, language1, country1));

                List<String> tags = new List<String>();
                List<String> tags2 = new List<String>();
                UploadImageDto uploadImage = new UploadImageDto(userId, "arbol1", "bosque1", "cat14", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags);
                UploadImageDto uploadImage2 = new UploadImageDto(userId, "arbol2", "bosque2", "cat14", 1, 1, 1, FileStream.Null, "filename", "arbol.jpg", tags2);

                long imageId = imageService.uploadImage(uploadImage);
                long imageId2 = imageService.uploadImage(uploadImage2);

                userProfileService.Follow(userId2, userId);

                List<Image> images = imageService.GetFollowedImages(userId2, 0, 9);
                Assert.AreEqual(images.Count, 2);
                Assert.AreEqual(images[0].title, "arbol2");
                Assert.AreEqual(images[1].title, "arbol1");

            }
        }
    }
}
