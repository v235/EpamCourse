using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Advanced.Task.BL;
using Advanced.Task.Data;
using NUnit.Framework;

namespace Advanced.Task.Tests
{
    [TestFixture]
    public class FileSystemVisitorTest
    {
        private readonly string testFolderPath = @"D:\";
        private IFileSystemVisitor fsVisitor;

        static private string MakePath(
            params string[] tokens)
        {
            string fullpath = "";
            foreach (string token in tokens)
            {
                fullpath = Path.Combine(fullpath, token);
            }
            return fullpath;
        }

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(testFolderPath);

            string[] testDirs =
            {
                MakePath(testFolderPath, "Test"),
                MakePath(testFolderPath, "Test", "dir1"),
                MakePath(testFolderPath, "Test", "dir1",
                    "dir2"),
                MakePath(testFolderPath, "Test", "dir1",
                    "dir2", "dir3")
            };

            foreach (string dir in testDirs)
            {
                Directory.CreateDirectory(dir);
            }

            string[] testFiles =
            {
                MakePath(testFolderPath, "Test", "dir1",
                    "file1.txt"),
                MakePath(testFolderPath, "Test", "dir1",
                    "file2.txt"),
                MakePath(testFolderPath, "Test", "dir1",
                    "dir2", "file3.txt"),
                MakePath(testFolderPath, "Test", "dir1",
                    "dir2", "file4.txt")
            };
            foreach (string file in testFiles)
            {
                FileStream str = File.Create(file);
                str.Close();
            }
            //
            IRepository moqRepository = new RepositoryMock();
            fsVisitor = new FileSystemVisitor(moqRepository, Path.Combine(
                testFolderPath, "Test"));

        }

        [Test]
        public void FileSystemVisitorGetAllFilesWithoutFilter()
        {
            //Arrange
            var expected = new List<FileInfo>()
            {
                new FileInfo("file1.txt"),
                new FileInfo("file2.txt"),
                new FileInfo("file3.txt"),
                new FileInfo("file4.txt")
            };
            //Act
            var result = fsVisitor.GetFiles(null).ToList();
            //Assert
            Assert.AreEqual(expected.Count, result.Count);
        }

        [Test]
        public void FileSystemVisitorGetAllFilesWithFilter()
        {
            //Arrange
            var expected = new List<FileInfo>() {new FileInfo("file1.txt")};
            //Act
            var result = fsVisitor.GetFiles("file1.txt").ToList();
            //Assert
            Assert.AreEqual(expected.Count, result.Count);

        }

        [Test]
        public void FileSystemVisitorGetAllDirectoryWithoutFilter()
        {
            //Arrange
            var expected = new List<DirectoryInfo>() { 
                new DirectoryInfo("dir1"),
                new DirectoryInfo("dir2"),
                new DirectoryInfo("dir3")};
            //Act
            var result = fsVisitor.GetDirs(null).ToList();
            //Assert
            Assert.AreEqual(expected.Count, result.Count);


        }

        [Test]
        public void FileSystemVisitorGetAllDirectoryWithFilter()
        {
            //Arrange
            var expected = new List<DirectoryInfo>() {
                new DirectoryInfo("dir2"),
                new DirectoryInfo("dir3")};
            //Act
            var result = fsVisitor.GetDirs("dir2").ToList();
            //Assert
            Assert.AreEqual(expected.Count, result.Count);


        }
        [TearDown]
        public void TearDown()
        {
            Directory.Delete(
                testFolderPath + "Test", true);
        }

    }
}
