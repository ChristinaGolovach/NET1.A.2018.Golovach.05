using System;
using System.Text;

namespace PolynomialLogic
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Polynomial : IEquatable<Polynomial>
    {
        private static readonly double epsilon;
        private readonly double[] coefficients;
        private int degree;
        
        static Polynomial()
        {
            epsilon = 0.0001;
        }  
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
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
        /// 
        /// </summary>
        public int Degree
        {
            get
            {
                return degree;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="firstPolynom"></param>
        /// <param name="secondPolynom"></param>
        /// <returns></returns>
        public static Polynomial operator +(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            CheckInputPolynomials(firstPolynom, secondPolynom);

            return SumTwoPolynom(firstPolynom, secondPolynom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynom"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Polynomial operator +(Polynomial polynom, double number)
        {
            CheckInputPolynomial(polynom);

            return SumPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="polynom"></param>
        /// <returns></returns>
        public static Polynomial operator +(double number, Polynomial polynom)
        {
            CheckInputPolynomial(polynom);

            return SumPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstPolynom"></param>
        /// <param name="secondPolynom"></param>
        /// <returns></returns>
        public static Polynomial operator -(Polynomial firstPolynom, Polynomial secondPolynom)
        {            
            CheckInputPolynomials(firstPolynom, secondPolynom);

            return SubtractTwoPolynom(firstPolynom, secondPolynom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynom"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Polynomial operator -(Polynomial polynom, double number)
        {
            CheckInputPolynomial(polynom);

            return SubtractPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="polynom"></param>
        /// <returns></returns>
        public static Polynomial operator -(double number, Polynomial polynom)
        {
            CheckInputPolynomial(polynom);

            return SubtractPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstPolynom"></param>
        /// <param name="secondPolynom"></param>
        /// <returns></returns>
        public static Polynomial operator *(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            CheckInputPolynomials(firstPolynom, secondPolynom);

            return MultiplyTwoPolynom(firstPolynom, secondPolynom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynom"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Polynomial operator *(Polynomial polynom, double number)
        {
            CheckInputPolynomial(polynom);

            return MultiplyPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="polynom"></param>
        /// <returns></returns>
        public static Polynomial operator *(double number, Polynomial polynom)
        {
            CheckInputPolynomial(polynom);

            return MultiplyPolynomAndNumber(polynom, number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firtstPolynom"></param>
        /// <param name="secondPolynom"></param>
        /// <returns></returns>
        public static bool operator ==(Polynomial firtstPolynom, Polynomial secondPolynom)
        {
            if (ReferenceEquals(firtstPolynom, null) || ReferenceEquals(secondPolynom, null))
            {
                return false;
            }

            return firtstPolynom.Equals(secondPolynom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firtstPolynom"></param>
        /// <param name="secondPolynom"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="polynom"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Polynomial);          
        }        

        public override int GetHashCode()
        {
            int hashCode = 23;
            int middleArray = coefficients.Length / 2;

            for (int i = 0; i < middleArray; i++)
            {
                hashCode += this[i].GetHashCode();
            }

            hashCode = hashCode + coefficients.GetHashCode();

            return hashCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder polynomInStringFormat = new StringBuilder();
            for (int i = 0; i < coefficients.Length; i++)
            {
                polynomInStringFormat.Append($"{coefficients[i]}*x^{i}{coefficients[i].ToString().Substring(0,1)} ");
            }

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
            var comparisonOfPolynom = FindLesserAndBiggerPolynoms(firstPolynom, secondPolynom);
            double[] resultCoefficients = MakeArrayForResultPolynom(comparisonOfPolynom.biggerPolynom);
            int i = 0;

            while (i <= comparisonOfPolynom.lesserPolynom.degree)
            {
                resultCoefficients[i] = firstPolynom[i] - secondPolynom[i];
                i++;
            }

            if (secondPolynom.degree > firstPolynom.degree)
            {
                ChangeSignInRestCoefficients(resultCoefficients, firstPolynom.degree + 1);
            }

            return new Polynomial(resultCoefficients);
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

        private static void ChangeSignInRestCoefficients(double[] array, int position)
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
            if (ReferenceEquals(polynom,null))
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

            if (ReferenceEquals(secondPolynom,null))
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
