using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {
        [TestMethod()]
        public void AddTest()
        {
             // Arrange
            ComplexNumber numberA = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };

            ComplexNumber numberB = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            // Act
            ComplexNumber actual = numberA.Add(numberB);

            // Assert
            Assert.AreEqual(11, actual.Real);
            Assert.AreEqual(22, actual.Imaginary);
        }
    }
}