using System;
using System.Configuration;
using System.Text;

namespace PolynomialLogic
{
    /// <summary>
    /// Represent an immutable class Polynomial for working with polynomials of degree from one variable of real type.
    /// </summary>
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private static readonly double epsilon;
        private readonly double[] coefficients = { };
        private int degree;
        
        static Polynomial()
        {
            try
            {
                epsilon = double.Parse(ConfigurationManager.AppSettings["epsilon"]);
            }
            catch (Exception)
            {
                epsilon = 0.0001;
            }
        }  
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="numbers">
        /// Coefficients of the polynomial.
        /// </param>
        public Polynomial(params double[] numbers)
        {
            if (IsValidInputArray(numbers))
            {
                coefficients = new double[numbers.Length];
                numbers.CopyTo(coefficients, 0);
                degree = numbers.Length - 1;
            }
        }

        /// <summary>
        /// Property for getting degree of the polynomial.
        /// </summary>
        public int Degree
        {
            get
            {
                return degree;
            }
        }

        /// <summary>
        /// Property for access to the elementsof the polynomial by index.
        /// </summary>
        /// <param name="index">
        /// Integer value for access to particular coefficient in polynomial. 
        /// </param>
        /// <returns>
        /// The particular coefficient from polynomial.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when index less than zero or more than degree of the polynomial.
        /// </exception>
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= coefficients.Length)
                {
                    throw new ArgumentOutOfRangeException($"The {nameof(index)} can not be less than zero oe more then {nameof(Degree)} of Polynomial");
                }

                return coefficients[index];
            }

            private set
            {
                if (index < 0 || index >= coefficients.Length)
                {
                    throw new ArgumentOutOfRangeException($"The {nameof(index)} can not be less than zero oe more then {nameof(Degree)} of Polynomial");
                }

                coefficients[index] = value;
            }
        }

        /// <summary>
        /// Performs a addition of coefficients of two polynomials and return new instance.
        /// </summary>
        /// <param name="firstPolynom">
        /// First polynomial for summ.
        /// </param>
        /// <param name="secondPolynom">
        /// Second polynomial for summ.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to the sum of the coefficients of the polynomials.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when firstPolynom or secondPolynom is null.
        /// </exception>
        public static Polynomial operator +(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            CheckInputPolynomials(firstPolynom, secondPolynom);

            return SumTwoPolynom(firstPolynom, secondPolynom);
        }

        /// <summary>
        /// Performs a addition of polynomial and number.
        /// </summary>
        /// <param name="polynom">
        /// The polynomial for summ.
        /// </param>
        /// <param name="number">
        /// The double value for sum.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to the sum of the coefficients of the polynomial and input number.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when input polynomial is null.
        /// </exception>
        public static Polynomial operator +(Polynomial polynom, double number)
        {
            CheckInputPolynomial(polynom);

            return SumPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// Performs a addition of polynomial and number.
        /// </summary>
        /// <param name="polynom">
        /// The polynomial for summ.
        /// </param>
        /// <param name="number">
        /// The double value for sum.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to the sum of the coefficients of the polynomial and input number.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when input polynomial is null.
        /// </exception>
        public static Polynomial operator +(double number, Polynomial polynom)
        {
            CheckInputPolynomial(polynom);

            return SumPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// Performs a subtraction of coefficients of two polynomials and return new instance.
        /// </summary>
        /// <param name="firstPolynom">
        /// First polynomial for subtraction.
        /// </param>
        /// <param name="secondPolynom">
        /// Second polynomial for subtraction.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to the subtraction of the coefficients of the polynomials.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when firstPolynom or secondPolynom is null.
        /// </exception>
        public static Polynomial operator -(Polynomial firstPolynom, Polynomial secondPolynom)
        {            
            CheckInputPolynomials(firstPolynom, secondPolynom);

            return SubtractTwoPolynom(firstPolynom, secondPolynom);
        }

        /// <summary>
        /// Performs a subtraction of polynomial and number.
        /// </summary>
        /// <param name="polynom">
        /// The polynomial for subtraction.
        /// </param>
        /// <param name="number">
        /// The double value for subtraction.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to the subtraction of the coefficients of the polynomial and input number.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when input polynomial is null.
        /// </exception>
        public static Polynomial operator -(Polynomial polynom, double number)
        {
            CheckInputPolynomial(polynom);

            return SubtractPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// Performs a subtraction of polynomial and number.
        /// </summary>
        /// <param name="polynom">
        /// The polynomial for subtraction.
        /// </param>
        /// <param name="number">
        /// The double value for subtraction.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to the subtraction of the coefficients of the polynomial and input number.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when input polynomial is null.
        /// </exception>
        public static Polynomial operator -(double number, Polynomial polynom)
        {
            CheckInputPolynomial(polynom);

            return SubtractPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// Performs a multiplication of the coefficients of two polynomials and returns a new instance.
        /// </summary>
        /// <param name="firstPolynom">
        /// First polynomial for multiplication.
        /// </param>
        /// <param name="secondPolynom">
        /// Second polynomial for multiplication.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to a multiplication of the coefficients of the polynomials.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when firstPolynom or secondPolynom is null.
        /// </exception>
        public static Polynomial operator *(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            CheckInputPolynomials(firstPolynom, secondPolynom);

            return MultiplyTwoPolynom(firstPolynom, secondPolynom);
        }

        /// <summary>
        /// Performs a multiplication of polynomial and number.
        /// </summary>
        /// <param name="polynom">
        /// The polynomial for multiplication.
        /// </param>
        /// <param name="number">
        /// The double value for multiplication.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to a multiplication of the coefficients of the polynomial and input number.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when input polynomial is null.
        /// </exception>
        public static Polynomial operator *(Polynomial polynom, double number)
        {
            CheckInputPolynomial(polynom);

            return MultiplyPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// Performs a multiplication of polynomial and number.
        /// </summary>
        /// <param name="polynom">
        /// The polynomial for multiplication.
        /// </param>
        /// <param name="number">
        /// The double value for multiplication.
        /// </param>
        /// <returns>
        /// A new instance of Polynomial whose coefficients are equal to a multiplication of the coefficients of the polynomial and input number.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when input polynomial is null.
        /// </exception>
        public static Polynomial operator *(double number, Polynomial polynom)
        {
            CheckInputPolynomial(polynom);

            return MultiplyPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// Checks two polynomials for equality of coefficients' value.
        /// </summary>
        /// <param name="firtstPolynom">
        /// First polynomial for comparison.
        /// </param>
        /// <param name="secondPolynom">
        /// Second polynomial for comparison.
        /// </param>
        /// <returns>
        /// True - when all coefficients' value of polynomials are equal.
        /// False - in the opposite case.
        /// </returns>
        public static bool operator ==(Polynomial firtstPolynom, Polynomial secondPolynom)
        {
            if (ReferenceEquals(firtstPolynom, secondPolynom))
            {
                return true;
            }

            if (ReferenceEquals(firtstPolynom, null) || ReferenceEquals(secondPolynom, null))
            {
                return false;
            }

            return firtstPolynom.Equals(secondPolynom);
        }
        
        /// <summary>
        /// Checks two polynomials for inequality of coefficients' value.
        /// </summary>
        /// <param name="firtstPolynom">
        /// First polynomial for comparison.
        /// </param>
        /// <param name="secondPolynom">
        /// Second polynomial for comparison.
        /// </param>
        /// <returns>
        /// True - when all coefficients' value of polynomials are not equal.
        /// False - in the opposite case.
        /// </returns>
        public static bool operator !=(Polynomial firtstPolynom, Polynomial secondPolynom) => !(firtstPolynom == secondPolynom);
       
        #region CLR - Comliant Methods

        public static Polynomial Add(Polynomial firstPolynom, Polynomial secondPolynom) => firstPolynom + secondPolynom;

        public static Polynomial Add(Polynomial polynom, double number) => polynom + number;

        public static Polynomial Add(double number, Polynomial polynom) => number + polynom;

        public static Polynomial Substruct(Polynomial firstPolynom, Polynomial secondPolynom) => firstPolynom - secondPolynom;

        public static Polynomial Substruct(Polynomial polynom, double number) => polynom - number;

        public static Polynomial Substruct(double number, Polynomial polynom) => number - polynom;

        public static Polynomial Multiply(Polynomial firstPolynom, Polynomial secondPolynom) => firstPolynom * secondPolynom;

        #endregion CLR - Comliant Methods

        /// <summary>
        /// Performs cloning curent instance of polynomial.
        /// </summary>
        /// <returns>
        /// A clone of current polynomial.
        /// </returns>
        public Polynomial Clone()
        {
            double[] cloneCoefficients = new double[coefficients.Length];
            coefficients.CopyTo(cloneCoefficients, 0);

            return new Polynomial(cloneCoefficients);
        }

        object ICloneable.Clone()
        {
             return this.Clone();
        }

        /// <summary>
        /// Implementation of Equals from IEquatable<T> interface.
        /// Сhecks whether the coefficients of the input polynomial are equal to the coefficients of the current inctance. 
        /// </summary>
        /// <param name="polynom">
        /// Input polynomial for comparison. 
        /// </param>
        /// <returns>
        /// True - when all coefficients' value of polynomials are equal.
        /// False - in the opposite case.
        /// </returns>
        public bool Equals(Polynomial polynom)
        {
            if (ReferenceEquals(polynom, null))
            {
                return false;
            }

            if (ReferenceEquals(this, polynom))
            {
                return true;
            }

            if (this.GetType() != polynom.GetType())
            {
                return false;
            }

            if (this.coefficients.Length != polynom.coefficients.Length)
            {
                return false;
            }

            return IsEqualCoefficientsInSameLengthPolynoms(this, polynom);
        }

        #region Object methods overloading
        /// <summary>
        /// Overloading the virtual method Equals from Object class.
        /// Сhecks whether the coefficients of the input polynomial are equal to the coefficients of the current inctance. 
        /// </summary>
        /// <param name="polynom">
        /// Input polynomial for comparison. 
        /// </param>
        /// <returns>
        /// True - when all coefficients' value of polynomials are equal.
        /// False - in the opposite case.
        /// </returns
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Polynomial);          
        }

        /// <summary>
        /// Overloading the virtual method GetHashCode from Object class.
        /// </summary>
        /// <returns>
        /// The HasCode of the current instance.
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = 23;         

            for (int i = 0; i < coefficients.Length; i++)
            {
                hashCode = hashCode * 145 + this[i].GetHashCode();               
            }

            return hashCode;
        }

        /// <summary>
        /// Overloading the virtual method ToString from Object class.
        /// </summary>
        /// <returns>
        /// A string representation of polynomial.
        /// </returns>
        public override string ToString()
        {
            StringBuilder polynomInStringFormat = new StringBuilder();
            for (int i = 0; i < coefficients.Length; i++)
            {
                polynomInStringFormat.Append($"{coefficients[i]}*x^{i} ");
            }

            polynomInStringFormat.Remove(polynomInStringFormat.Length - 1, 1);

            return polynomInStringFormat.ToString();
        }

        #endregion Object methods overloading

        #region Core Logic for operations

        private static Polynomial SumTwoPolynom(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            var comparisonOfPolynom = FindLesserAndBiggerPolynoms(firstPolynom, secondPolynom);
            double[] resultCoefficients = MakeArrayForResultPolynom(comparisonOfPolynom.biggerPolynom);
            int i = 0;

            while (i <= comparisonOfPolynom.lesserPolynom.degree)
            {
                resultCoefficients[i] = firstPolynom[i] + secondPolynom[i];             
                i++;
            }

            return new Polynomial(resultCoefficients);
        }

        private static Polynomial SumPolynomAndNumber(Polynomial polynom, double number)
        {
            double[] resultCoefficients = MakeArrayForResultPolynom(polynom);

            resultCoefficients[0] += number;

            return new Polynomial(resultCoefficients);
        }

        private static Polynomial SubtractTwoPolynom(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            int length = secondPolynom.Degree + 1;
            double[] oppositeInSignCoefficientsOfSecondPolynom = new double[length];

            Array.Copy(secondPolynom.coefficients, oppositeInSignCoefficientsOfSecondPolynom, length);

            ChangeSignInCoefficients(oppositeInSignCoefficientsOfSecondPolynom, 0);

            Polynomial oppositeInSignOfSecondPolynom = new Polynomial(oppositeInSignCoefficientsOfSecondPolynom);

            Polynomial result = SumTwoPolynom(firstPolynom, oppositeInSignOfSecondPolynom);

            if (firstPolynom.degree > secondPolynom.degree)
            {
                ChangeSignInCoefficients(result.coefficients, firstPolynom.degree + 1);
            }

            return result;
        }

        private static Polynomial SubtractPolynomAndNumber(Polynomial polynom, double number)
        {
            double[] resultCoefficients = MakeArrayForResultPolynom(polynom);

            resultCoefficients[0] -= number;

            return new Polynomial(resultCoefficients);
        }

        private static Polynomial MultiplyTwoPolynom(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            int firstPolynomLength = firstPolynom.coefficients.Length;
            int secondPolynomLength = secondPolynom.coefficients.Length;
            double[] resultCoefficients = new double[firstPolynomLength + secondPolynomLength - 1];

            for (int i = 0; i < firstPolynomLength; i++)
            {
                for (int j = 0; j < secondPolynomLength; j++)
                {
                    resultCoefficients[i + j] += firstPolynom[i] * secondPolynom[j];
                }
            }

            return new Polynomial(resultCoefficients);
        }

        private static Polynomial MultiplyPolynomAndNumber(Polynomial polynom, double number)
        {
            double[] resultCoefficients = MakeArrayForResultPolynom(polynom);

            for (int i = 0; i < polynom.coefficients.Length; i++)
            {
                resultCoefficients[i] *= number;
            }

            return new Polynomial(resultCoefficients);
        }

        #endregion Core Logic for oerations

        #region Utility Logic

        private static (Polynomial lesserPolynom, Polynomial biggerPolynom) FindLesserAndBiggerPolynoms(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            if (firstPolynom.degree < secondPolynom.degree)
            {
                return (lesserPolynom: firstPolynom, biggerPolynom: secondPolynom);
            }
            else
            {
                return (lesserPolynom: secondPolynom, biggerPolynom: firstPolynom);
            }
        }

        private static double[] MakeArrayForResultPolynom(Polynomial sourceBiggerPolynom)
        {
            int length = sourceBiggerPolynom.coefficients.Length;
            double[] result = new double[length];
            sourceBiggerPolynom.coefficients.CopyTo(result, 0);

            return result;
        }

        private static void ChangeSignInCoefficients(double[] array, int position)
        {
            for (int i = position; i < array.Length; i++)
            {
                array[i] = -array[i];
            }
        }

        private static bool IsEqualCoefficientsInSameLengthPolynoms(Polynomial firsPolynom, Polynomial secondPolynom)
        {
            for (int i = 0; i < firsPolynom.coefficients.Length; i++)
            {
                if (Math.Abs(firsPolynom[i] - secondPolynom[i]) > epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion Utility Logic

        #region Validation        

        private static void CheckInputPolynomial(Polynomial polynom)
        {
            if (ReferenceEquals(polynom, null))
            {
                throw new ArgumentNullException($"The {nameof(polynom)} can not be null.");
            }
        }
    
        private static void CheckInputPolynomials(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            if (ReferenceEquals(firstPolynom, null))
            {
                throw new ArgumentNullException($"The {nameof(firstPolynom)} can not be null.");
            }

            if (ReferenceEquals(secondPolynom, null))
            {
                throw new ArgumentNullException($"The {nameof(secondPolynom)} can not be null.");
            }
        }

        private bool IsValidInputArray(double[] numbers)
        {
            if (ReferenceEquals(numbers, null))
            {
                throw new ArgumentNullException($"The {nameof(numbers)} can not be null.");
            }

            if (numbers.Length < 1)
            {
                throw new ArgumentException($"The {nameof(numbers)} can not be empty");
            }

            return true;
        }

        private bool IsValidInputDegree(int degree)
        {
            if (degree < 0)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(degree)} can less than zero.");
            }

            return true;
        }

        #endregion Validation
    }
}
