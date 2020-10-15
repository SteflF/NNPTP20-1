using System;

namespace INPTPZ1
{
	namespace Mathematics
	{
		public class ComplexNumber
        {
            public double Real { get; set; }

			public double Imaginary { get; set; }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber Multiply(ComplexNumber b)
            {
                return new ComplexNumber()
                {
                    Real = Real * b.Real - Imaginary * b.Imaginary,
                    Imaginary = Real * b.Imaginary + Imaginary * b.Real
                };
            }

            public ComplexNumber Add(ComplexNumber b)
            {
                return new ComplexNumber()
                {
                    Real = Real + b.Real,
                    Imaginary = Imaginary + b.Imaginary
                };
            }

            public ComplexNumber Subtract(ComplexNumber b)
            {
                return new ComplexNumber()
                {
                    Real = Real - b.Real,
                    Imaginary = Imaginary - b.Imaginary
                };
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            internal ComplexNumber Divide(ComplexNumber b)
            {
                ComplexNumber numerator = Multiply(new ComplexNumber() { Real = b.Real, Imaginary = -b.Imaginary });
                double divisor = b.Real * b.Real + b.Imaginary * b.Imaginary;

                return new ComplexNumber()
                {
                    Real = numerator.Real / divisor,
                    Imaginary = (numerator.Imaginary / divisor)
                };
            }
		}
    }
}