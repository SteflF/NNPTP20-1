using System.Collections.Generic;

namespace INPTPZ1
{
	class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; } = new List<ComplexNumber>();

        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();

            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynomial;
        }

        public ComplexNumber Evaluate(ComplexNumber inputComplexNumber)
        {
            ComplexNumber evaluation = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficitent = Coefficients[i];
                ComplexNumber copyOfTheInput = inputComplexNumber;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
					{
                        copyOfTheInput = copyOfTheInput.Multiply(inputComplexNumber);
                    }

                    coefficitent = coefficitent.Multiply(copyOfTheInput);
                }

                evaluation = evaluation.Add(coefficitent);
            }

            return evaluation;
        }

        public override string ToString()
        {
            string s = string.Empty;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        s += "x";
                    }
                }

                s += " + ";
            }

            return s.TrimEnd('+');
        }
    }
}