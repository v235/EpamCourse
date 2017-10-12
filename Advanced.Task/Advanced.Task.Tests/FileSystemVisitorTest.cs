using System;
using Advanced.Task.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Task.Tests
{
    [TestClass]
    public class FileSystemVisitorTest
    {
        [TestInitialize]
        public void Init()
        {
            FileSystemVisitor fsVisitor = new FileSystemVisitor("");
        }
        [TestMethod]
        public void GetFilesTest()
        {

        }
    }
}
