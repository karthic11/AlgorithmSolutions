using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puzzles.DataStructures.Common;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //test this
        }


        [TestMethod]
        public void TestValidTriangles()
        {
            //Assuming FindTriangleTypeBySides() method is in Triangle class
            Triangle triangle = new Triangle();
            //I can either has the result as enum name or convert it into a integer
            var result = triangle.FindTriangleTypeBySides(10, 10, 10); //Equilateral
            Assert.AreEqual(3, result);

            result = triangle.FindTriangleTypeBySides(10, 12, 14); //Scalene
            Assert.AreEqual(1, result);

            result = triangle.FindTriangleTypeBySides(10, 10, 5); //Isosceles
            Assert.AreEqual(2, result);

            result = triangle.FindTriangleTypeBySides(0, 10, 5); //Error
            Assert.AreEqual(4, result);

            result = triangle.FindTriangleTypeBySides(3, 10, 5); //Error(sum should be greater)
            Assert.AreEqual(4, result);

            result = triangle.FindTriangleTypeBySides(-5, 10, 7); //Error
            Assert.AreEqual(4, result);

            result = triangle.FindTriangleTypeBySides(-5, -5, -5); //Error
            Assert.AreEqual(4, result);
       

        }



        //      10,10,10 Equilateral
        //10,5,4 scalene
        //10,10,5 Isosceles
        //0,10,5 invalid
        //0,0,0 invalid
        //-7,10,10 invalid
        //10,2,3 invalid(sum should be greater)
    }
}
