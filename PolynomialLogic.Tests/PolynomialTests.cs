using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace PolynomialLogic.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [TestCase(new double[] { -0.123456, 1.5, 7.333 }, new double[] { -0.123456, 1.5, 7.333 })]
        [TestCase(new double[] { -0.0, -1.9, 0 }, new double[] { -0.0, -1.9, 0 })]
        public void OperatorEquality_PassTwoEqualsCoefficients_ComparisonIsTrue(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsTrue(p1 == p2);
        }

        [TestCase(new double[] { -0.11119, 1.55550, 7.33339999 }, new double[] { -0.111100000, 1.55556666, 7.333377777 })]
        [TestCase(new double[] { -0.000099999, -1.989833333, 0.111187, }, new double[] { -0.0000777777, -1.9898444444, 0.11115 })]
        public void OperatorEquality_PassTwoEqualsCoefficientsWithAccuracyMore_0dot0001_ComparisonIsTrue(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsTrue(p1 == p2);
        }

        [TestCase(new double[] { -0.123456, 1.5556 }, new double[] { -0.123456, 1.5556, 7.333 })]
        [TestCase(new double[] { -0.0, -1.9, 0 }, new double[] { -0.0, -1.9, 0, 45.4 })]
        public void OperatorEquality_PassTwoEqualsCoefficientsButDifferentLengthArray_ComparisonIsFalse(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsFalse(p1 == p2);
        }

        [TestCase(new double[] { -0.123456, 1.5556 })]
        public void OperatorEquality_ComparingWithNull_ComparisonIsFalse(double[] first)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = null;

            // Act - Assert
            Assert.IsFalse(p1 == p2);
        }

        [TestCase(new double[] { 1, 1.5, 7.333 }, new double[] { 2, 0.5, 0.3 }, new double[] { 3, 2, 7.633 })]
        [TestCase(new double[] { -2.4, 0.5, -7.33 }, new double[] { 2, 0.5, -7.33 }, new double[] { -0.4, 1, -14.66 })]
        [TestCase(new double[] { -2.4 }, new double[] { 2, 0.5, -7.33, -7.45, 9.44444444444444444 }, new double[] { -0.4, 0.5, -7.33, -7.45, 9.44444444444444444 })]
        public void OperatorPlus_PassToArrayOfCoefficientAndResultArrayOfSum_ResultPolynomEqualExpectedResult(double[] first, double[] second, double[] result)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act
            Polynomial p = p1 + p2;
            Polynomial expected = new Polynomial(result);

            // Assert
            Assert.IsTrue(p == expected);
        }               

        [TestCase(new double[] { -2.4, 1.1, 0 }, 5.0, new double[] { 2.6, 1.1, 0 })]
        public void OperatorPlus_CheckPlusWithNumber_ResultPolynomEqualExpectedResult(double[] coefficients, double number, double[] expectedResult)
        {
            // Arange
            Polynomial p = new Polynomial(coefficients);
            Polynomial result = new Polynomial(expectedResult);

            // Act - Assert
            Assert.IsTrue(result == p + number);
        }

        [TestCase(new double[] { -2.4 })]
        public void OperatorPlus_CheckPlusWithNull_ThrownArgumentNullException(double[] coefficients)
        {
            // Arange
            Polynomial p1 = new Polynomial(coefficients);
            Polynomial p2 = null;
            Polynomial result;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => result = p1 + p2);           
        }

        [TestCase(new double[] { 1 }, new double[] { 2, 0.5, 0.3 }, new double[] { -1, -0.5, -0.3 })]
        [TestCase(new double[] { 2, 0.5, 0.3 }, new double[] { 1 }, new double[] { 1, 0.5, 0.3 })]
        [TestCase(new double[] { 2, 0.5, 0.3 }, new double[] { 2, 0.5, 0.3, 78.000001 }, new double[] { 0, 0, 0, -78.000001 })]
        public void OperatorMinus_PassToArrayOfCoefficientAndResultArrayOfSubtraction_ResultPolynomEqualExpectedResult(double[] first, double[] second, double[] result)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act
            Polynomial p = p1 - p2;
            Polynomial expected = new Polynomial(result);

            // Assert
            Assert.IsTrue(p == expected);
        }
        
        [TestCase(new double[] { -2.4 })]
        public void OperatorMinus_CheckPlusWithNull_ThrownArgumentNullException(double[] coefficients)
        {
            // Arange
            Polynomial p1 = new Polynomial(coefficients);
            Polynomial p2 = null;
            Polynomial result;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => result = p1 - p2);
        }

        [TestCase(new double[] { -2.4, 1.1, 0 }, 5.0, new double[] { -7.4, 1.1, 0 })]
        public void OperatorMinus_CheckPlusWithNumber_ResultPolynomEqualExpectedResult(double[] coefficients, double number, double[] expectedResult)
        {
            // Arange
            Polynomial p = new Polynomial(coefficients);
            Polynomial result = new Polynomial(expectedResult);

            // Act - Assert
            Assert.IsTrue(result == p - number);
        }

        [TestCase(new double[] { 1, 5, 7 }, new double[] { 2, 4, 8, 1 }, new double[] { 2, 14, 42, 69, 61, 7 })]
        public void OperatorMultiply_CheckMultiplyOfTwoPolynoms_ResultPolynomEqualExpectedResult(double[] first, double[] second, double[] result)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act
            Polynomial p = p1 * p2;
            Polynomial expected = new Polynomial(result);

            // Assert
            Assert.IsTrue(p == expected);
        }

        [TestCase(new double[] { -2.4 })]
        public void OperatorMultiply_CheckMultiplyWithNull_ThrownArgumentNullException(double[] coefficients)
        {
            // Arange
            Polynomial p1 = new Polynomial(coefficients);
            Polynomial p2 = null;
            Polynomial result;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => result = p1 * p2);
        }

        [TestCase(new double[] { -0.123456, 1.5, 7.333 }, new double[] { -0.123456, 1.5, 7.333 })]
        public void Equals_CopmparisonTwoEqualPolynom_ReturnTrue(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsTrue(p1.Equals(p2));
            Assert.IsTrue(p2.Equals(p1));
        }

        [TestCase(new double[] { -0.123456, 1.5, 7.333 }, new double[] { -0.123456, 1.5, 7.333, 4 })]
        public void Equals_CopmparisonTwoNotEqualPolynom_ReturnFalse(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsFalse(p1.Equals(p2));
            Assert.IsFalse(p2.Equals(p1));
        }

        [TestCase(new double[] { -0.123456, 1.5, 7.333 })]
        public void Equals_CopmparisonPolynomWithNull_ReturnFalse(double[] coefficients)
        {
            // Arange
            Polynomial p = new Polynomial(coefficients);

            // Act - Assert
            Assert.IsFalse(p.Equals(null));
        }

        [TestCase(new double[] { -0.123456, 1.5, 7.333 }, new double[] { -0.123456, 1.5, 7.333 })]
        [TestCase(new double[] { -0.5454545454, 1.5, 0.333 }, new double[] { -0.5454545454, 1.5, 0.333 })]
        public void GetHashCode_CopmparisonHashCodesOfEqualPolynoms_ReturnTrue(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsTrue(p1.GetHashCode() == p2.GetHashCode());
        }
                
        [TestCase(new double[] { 1, 2 }, new double[] { 2, 1 })]     
        [TestCase(new double[] { -0.123456, 1.5, 7.333 }, new double[] { -0.123456, 7.333, 1.5 })]
        public void GetHashCode_CopmparisonHashCodesOfUnEqualPolynoms_ReturnFalse(double[] first, double[] second)
        {
            // Arange
            Polynomial p1 = new Polynomial(first);
            Polynomial p2 = new Polynomial(second);

            // Act - Assert
            Assert.IsFalse(p1.GetHashCode() == p2.GetHashCode());
        }

        [TestCase(new double[] { 1, 2 })]
        public void Constructor_CheckCreatePolynom_PolynomCreated(double[] coefficients)
        {
            // Arange
            Polynomial p = new Polynomial(coefficients);

            // Act - Assert
            Assert.IsTrue(p == new Polynomial(coefficients));
        }

        [Test]
        public void Constructor_ChekCreatePolynomWithNullValueInConstructor_TrownArgumentNullException()
        {
            Polynomial p;

            Assert.Throws<ArgumentNullException>(() => p = new Polynomial(null));
        }

        [TestCase(new double[] { 1, -2, -7.69 })]
        public void Index_TakeValueByIndex_ReturnValue(double[] coefficients)
        {
            // Arange
            Polynomial p = new Polynomial(coefficients);

            // Act - Assert
            Assert.AreEqual(-7.69, coefficients[2], 0.0001);
        }

        [TestCase(new double[] { 1, -2, -7.69 })]
        public void Index_TakeValueByOutOfRangeIndex_ReturnValue(double[] coefficients)
        {
            // Arange
            Polynomial p = new Polynomial(coefficients);
            double value = 0.0;

            // Act - Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => value = p[4]);
        }
    }
}
