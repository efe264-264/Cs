using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;

namespace assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IShape> figures = new List<IShape>
            {
                ShapeCreator.CreateFigure("Rectangle", 3, 4),
                ShapeCreator.CreateFigure("Square", 5),
                ShapeCreator.CreateFigure("Triangle", 4, 2)
            };

            foreach (var figure in figures)
            {
                Console.WriteLine($" {figure.GetType().Name}的面积是{figure.CalculateArea()}");
            }
        }
    }

    public interface IShape
    {
        double CalculateArea();
    }

    class RectangularFigure : IShape
    {
        protected int Length;
        protected int Breadth;

        public RectangularFigure(int length, int breadth)
        {
            this.Length = length;
            this.Breadth = breadth;
        }

        public virtual double CalculateArea()
        {
            return this.Length * this.Breadth;
        }
    }

    class RegularQuadrilateral : RectangularFigure
    {
        public RegularQuadrilateral(int sideLength)
            : base(sideLength, sideLength)
        {
        }
    }

    class TriangularFigure : IShape
    {
        private int BaseLength;
        private int Height;

        public TriangularFigure(int baseLength, int height)
        {
            this.BaseLength = baseLength;
            this.Height = height;
        }

        public double CalculateArea()
        {
            return this.BaseLength * this.Height / 2.0;
        }
    }

    class ShapeCreator
    {
        public static IShape CreateFigure(string figureType, params int[] dimensions)
        {
            switch (figureType)
            {
                case "Rectangle":
                    ValidateDimensions(dimensions, 2);
                    return new RectangularFigure(dimensions[0], dimensions[1]);
                case "Square":
                    ValidateDimensions(dimensions, 1);
                    return new RegularQuadrilateral(dimensions[0]);
                case "Triangle":
                    ValidateDimensions(dimensions, 2);
                    return new TriangularFigure(dimensions[0], dimensions[1]);
                default:
                    throw new ArgumentException("Unsupported geometric figure");
            }
        }

        private static void ValidateDimensions(int[] dimensions, int requiredCount)
        {
            if (dimensions.Length != requiredCount)
            {
                throw new ArgumentException(
                    $"Invalid parameter count. Requires {requiredCount} values");
            }
        }
    }
}