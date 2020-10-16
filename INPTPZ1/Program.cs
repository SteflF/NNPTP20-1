using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
	public static class Program
	{
		private const int newtonIterations = 30;
		private const double epsilon = 0.0001;
		private const double precision = 0.5;
		private const double tolerance = 0.01;

		private static readonly Color[] colors = new Color[]
		{
			Color.Red,
			Color.Blue, 
			Color.Green, 
			Color.Yellow, 
			Color.Orange, 
			Color.Fuchsia, 
			Color.Gold, 
			Color.Cyan, 
			Color.Magenta
		};

		private static Bitmap image;
		private static string fileName;
		private static double xMin;
		private static double xMax;
		private static double yMin;
		private static double yMax;
		private static double xStep;
		private static double yStep;

		private static int width;
		private static int height;

		private static Polynomial polynomial;
		private static Polynomial derivation;
		private static List<ComplexNumber> roots;

		public static void Main(string[] args)
		{
			Init(args);

			Console.WriteLine(polynomial);
			Console.WriteLine(derivation);

			CreateImage(width, height);

			SaveImage(fileName);
		}

		private static void Init(string[] args)
		{
			if (args.Length != 7)
			{
				throw new ArgumentException("Vyžadováno 7 vstupních parametrů!");
			}

			int[] imageSize = new int[2];
			for (int i = 0; i < imageSize.Length; i++)
			{
				imageSize[i] = int.Parse(args[i]);
			}

			width = imageSize[0];
			height = imageSize[1];

			double[] inputParameters = new double[4];
			for (int i = 0; i < inputParameters.Length; i++)
			{
				inputParameters[i] = double.Parse(args[i + 2]);
			}

			fileName = args[6];

			image = new Bitmap(width, height);
			xMin = inputParameters[0];
			xMax = inputParameters[1];
			yMin = inputParameters[2];
			yMax = inputParameters[3];

			xStep = (xMax - xMin) / width;
			yStep = (yMax - yMin) / height;

			roots = new List<ComplexNumber>();

			polynomial = new Polynomial();
			polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
			polynomial.Coefficients.Add(ComplexNumber.Zero);
			polynomial.Coefficients.Add(ComplexNumber.Zero);
			polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
			derivation = polynomial.Derive();
		}

		private static void CreateImage(int width, int height)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					ComplexNumber coordinates = FindWorldCoordinatesOfPixel(i, j);

					int iterations = CalculateSolutionOfEquation(ref coordinates);
					int rootsCount = FindNumberOfRoots(coordinates);

					Color color = PickColorForPixel(iterations, rootsCount);

					image.SetPixel(j, i, color);
				}
			}
		}

		private static ComplexNumber FindWorldCoordinatesOfPixel(int i, int j)
		{
			double x = xMin + j * xStep;
			double y = yMin + i * yStep;

			return new ComplexNumber()
			{
				Real = x == 0 ? epsilon : x,
				Imaginary = y == 0 ? epsilon : y
			};
		}

		private static int CalculateSolutionOfEquation(ref ComplexNumber worldCoordinate)
		{
			int iterations = 0;
			for (int i = 0; i < newtonIterations; i++)
			{
				ComplexNumber quotient = polynomial.Evaluate(worldCoordinate).Divide(derivation.Evaluate(worldCoordinate));
				worldCoordinate = worldCoordinate.Subtract(quotient);

				if (Math.Pow(quotient.Real, 2) + Math.Pow(quotient.Imaginary, 2) >= precision)
				{
					i--;
				}

				iterations++;
			}

			return iterations;
		}

		private static int FindNumberOfRoots(ComplexNumber worldCoordinate)
		{
			bool known = false;
			int rootsCount = 0;
			for (int i = 0; i < roots.Count; i++)
			{
				if (Math.Pow(worldCoordinate.Real - roots[i].Real, 2) + Math.Pow(worldCoordinate.Imaginary - roots[i].Imaginary, 2) <= tolerance)
				{
					known = true;
					rootsCount = i;
				}
			}

			if (!known)
			{
				roots.Add(worldCoordinate);
				rootsCount = roots.Count;
			}

			return rootsCount;
		}

		private static Color PickColorForPixel(int iterations, int rootsCount)
		{
			Color color = colors[rootsCount % colors.Length];
			
			color = Color.FromArgb(
				Math.Min(Math.Max(0, color.R - iterations * 2), 255),
				Math.Min(Math.Max(0, color.G - iterations * 2), 255),
				Math.Min(Math.Max(0, color.B - iterations * 2), 255)
				);
			
			return color;
		}

		private static void SaveImage(string fileName)
		{
			image.Save(fileName ?? "../../../image.png");
		}
	}
}