using System;

namespace INPTPZ1
{
	public class ComplexNumber
	{
		public readonly static ComplexNumber Zero = new ComplexNumber()
		{
			Real = 0,
			Imaginary = 0
		};

		public double Real { get; set; }

		public double Imaginary { get; set; }

		public ComplexNumber Multiply(ComplexNumber number)
		{
			return new ComplexNumber()
			{
				Real = (Real * number.Real) - (Imaginary * number.Imaginary),
				Imaginary = (Real * number.Imaginary) + (Imaginary * number.Real)
			};
		}

		public ComplexNumber Add(ComplexNumber number)
		{
			return new ComplexNumber()
			{
				Real = Real + number.Real,
				Imaginary = Imaginary + number.Imaginary
			};
		}

		public ComplexNumber Subtract(ComplexNumber number)
		{
			return new ComplexNumber()
			{
				Real = Real - number.Real,
				Imaginary = Imaginary - number.Imaginary
			};
		}

		public ComplexNumber Divide(ComplexNumber number)
		{
			ComplexNumber numerator = Multiply(new ComplexNumber() { Real = number.Real, Imaginary = -number.Imaginary });
			double divisor = (number.Real * number.Real) + (number.Imaginary * number.Imaginary);

			return new ComplexNumber()
			{
				Real = numerator.Real / divisor,
				Imaginary = numerator.Imaginary / divisor
			};
		}

		public override string ToString()
		{
			return $"({Real} + {Imaginary}i)";
		}
	}
}