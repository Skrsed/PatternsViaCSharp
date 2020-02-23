using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            var drawer = new Drawer();

            Creator[] shapes = { new TriangleCreator(), new RectangleCreator() };

            drawer.Draw(new List<int[,]> { shapes[0].Create(20,5).Matrix, shapes[1].Create(2,3).Matrix });
            Console.ReadKey();
        }
    }
    abstract class Creator
    {
        // factory method
        public abstract IShape Create(int x, int y);
    }
    class TriangleCreator : Creator
    {
        public override IShape Create(int x, int y)
        {
            var tri = new Triangle();
            tri.Position = new Point(x, y);
            return tri;
        }
    }
    class RectangleCreator : Creator
    {
        public override IShape Create(int x,int y)
        {
            var req = new Rectangle();
            req.Position = new Point(x,y);
            return req;
        }
    }
    interface IShape
    {
        int[,] Matrix { get;}
        void SetMatrix();
        void Move();
        Point Position { get; set; }

    }
    class Rectangle : IShape
    {
        public int[,] Matrix { get; private set; }
        public Point Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                SetMatrix();
                Move();
            }
        }

        private Point _position;

        public Rectangle()
        {
            _position = new Point();
            _position.X = 0;
            _position.Y = 0;
            SetMatrix();
        }
        public void Move()
        {
            var newArr = new int[Position.Y + Matrix.GetLength(0), Position.X + Matrix.GetLength(1)];
            for (var i = 0; i < Matrix.GetLength(0); i++)
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    newArr[Position.Y + i, Position.X + j] = Matrix[i, j];
                }
            Matrix = newArr;
        }

        public void SetMatrix()
        {
            Matrix = new int[5, 7]{
                    { 1,0,1,0,1,0,1},
                    { 1,0,0,0,0,0,1},
                    { 1,0,0,0,0,0,1},
                    { 1,0,0,0,0,0,1},
                    { 1,0,1,0,1,0,1},
            };
        }
    }
    class Triangle : IShape
    {
        private const int width = 13;
        public int[,] Matrix { get; private set; }
        private Point _position;
        public Triangle()
        {
            _position = new Point();
            _position.X = 0;
            _position.Y = 0;
            SetMatrix();
        }
        public Point Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                SetMatrix();
                Move();
            }
        }

        public void SetMatrix()
        {
            var center = width / 2;
            Matrix = new int[center, width];
            for (var i = 0; i < center; i++)
            {
                Matrix[i, center + i] = 1;
                Matrix[i, center - i] = 1;
            }
            
            for (int j = 0; j < width -1; j++)
            {
                Matrix[Matrix.GetLength(0) - 1, j] = j % 2;
            }
        }
        public void Move()
        {
            var newArr = new int[Position.Y + Matrix.GetLength(0), Position.X + Matrix.GetLength(1)];
            for (var i = 0; i < Matrix.GetLength(0); i++)
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    newArr[Position.Y + i, Position.X + j] = Matrix[i, j];
                }
            Matrix = newArr;
        }
    }
}
