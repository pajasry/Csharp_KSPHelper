using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections;
using KSPHelperLibrary;
using System.Collections.Generic;

namespace PathValidationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PathParsingTest()
        {
            KSPHelper pathCheck;
            try
            {
                //.in extension
                pathCheck = new KSPHelper(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest", "test.in");
                pathCheck = new KSPHelper(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest\test.in");
                //.txt extension
                pathCheck = new KSPHelper(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest02", "test.txt");
                pathCheck = new KSPHelper(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest02\test.txt");
            }
            catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NotFoundTest() {
            KSPHelper helper;
            try
            {
                helper = new KSPHelper(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\definetlyExists", "test.in");
                helper = new KSPHelper(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\definetlyExists\test.in");
                Assert.Fail("Chyba v kontrole");
            }
            catch { 
                //errors occurs, everything is okay
            }
           
        }
        [TestMethod]
        public void InputTest() {
            Assert.IsTrue(validateInput(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest\test.in"), "Vstupy se neshoduji");
            Assert.IsTrue(validateInput(@"C: \Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest02\test.txt"), "Vstupy se neshoduji");
        }

        [TestMethod]
        public void OutputTest()
        {
            Assert.IsTrue(validateOutput(@"C:\Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest\test.in"),"Vystupy se neshoduji");
            Assert.IsTrue(validateOutput(@"C: \Users\pavel\source\repos\KSPHelper\PathValidationTest\testData\pathTest02\test.txt"), "Vystupy se neshoduji");
        }

        private bool validateInput(string path) {
            List<string[]> correctData = new List<string[]>();
            foreach (var line in File.ReadAllLines(path))
                correctData.Add(line.Split(' '));
            string[] firstLine = correctData[0];
            correctData.RemoveAt(0);
            KSPHelper helper = new KSPHelper(path);
            return correctData.Equals(helper.Collection) && firstLine.Equals(helper.firstLine);
        }
        private bool validateOutput(string path) { 
            string[] desired = { "ANO","NE","ANO","MOZNA"};
            KSPHelper helper = new KSPHelper(path);
            foreach (var ot in desired)
                helper.AddOutput(ot);
            helper.FlushOutput();

            List<string> correctData = new List<string>();
            foreach (var line in File.ReadAllLines(helper.outputFilePath))
                correctData.Add(line);

            return desired.Equals(correctData.ToArray());

        }
    }
}
