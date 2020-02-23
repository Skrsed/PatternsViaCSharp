using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    // Строитель + компановщик + декоратор
    class Program
    {
        static void Main(string[] args)
        {
            var drawer = new Drawer();
            List<int[,]> matrices = new List<int[,]>();

            var req = new Rectangle();
            req.Position = new Point(5, 17);
            matrices.Add(req.Matrix);

            var req2 = new Rectangle();
            var decorator = new Decorator(req2);
            decorator.Position = new Point(20, 20);
            matrices.Add(decorator.Matrix);

            var director = new Director();

            var builder = new TriforceBuilder();
            director.Builder = builder;
            director.BuildNiceTriforce();
            var triforce = builder.Triforce;
            // !!!
            triforce.Displace(20, 5);
            // /!!!
            matrices.AddRange(triforce.GetMatrices());



            builder = new TriforceBuilder();
            director.Builder = builder;
            director.BuildRTriforce();
            var r_triforce = builder.Triforce;
            matrices.AddRange(r_triforce.GetMatrices());

            drawer.Draw(matrices);
            Console.ReadKey();
        }
        class Decorator: Rectangle
        {
            protected Rectangle _component;

            public Decorator(Rectangle component)
            {
                this._component = component;
            }

            public void SetComponent(Rectangle component)
            {
                this._component = component;
            }
            public override Point Position
            {
                get
                {
                    return base.Position;
                }

                set
                {
                    base.Position = value;
                    SetMatrix();
                    SetNewMatrix();
                    Move();
                }
            }

            private void SetNewMatrix()
            {
                for (var i = 0; i < base.Matrix.GetLength(0);i++)
                {
                    for (var j = 0; j < base.Matrix.GetLength(1); j++)
                    {
                        base.Matrix[i, j] = base.Matrix[i, j] != 0 ? 3 : 0;
                    }
                }
            }
        }

        public interface IBuilder
        {
            void BuildPartA();

            void BuildPartB();

            void BuildPartC();

            void BuildPartR();
        }
        class TriforceBuilder : IBuilder
        {
            public Composite Triforce { get; private set; }

            public TriforceBuilder()
            {
                this.Reset();
            }

            public void Reset()
            {
                this.Triforce = new Composite();
            }

            public void BuildPartA()
            {
                var tri = new Triangle();
                tri.Position = new Point(6, 0);
            
                Triforce.Add(tri);

            }

            public void BuildPartB()
            {
                var tri = new Triangle();
                tri.Position = new Point(0, 6);

                Triforce.Add(tri);
            }

            public void BuildPartC()
            {
                var tri = new Triangle();
                tri.Position = new Point(12, 6);

                Triforce.Add(tri);
            }

            public void BuildPartR()
            {
                var tri = new Triangle();
                tri.Position = new Point(0, 0);

                Triforce.Add(tri);
            }
        }
        class Director
        {
            public IBuilder Builder { get; set; }

            public void BuildNiceTriforce()
            {
                this.Builder.BuildPartA();
                this.Builder.BuildPartB();
                this.Builder.BuildPartC();
            }

            public void BuildRTriforce()
            {
                this.Builder.BuildPartR();
                this.Builder.BuildPartB();
                this.Builder.BuildPartC();
            }
        }

        class Composite : Shape
        {
            public Composite(List<Shape> shapes) => _children = shapes;
            public Composite() { }

            protected List<Shape> _children = new List<Shape>();
            
            public override void Add(Shape shape)
            {
                this._children.Add(shape);
            }

            public override void Remove(Shape shape)
            {
                this._children.Remove(shape);
            }

            public override void SetMatrix()
            {
                foreach (var child in _children)
                    child.SetMatrix();
            }

            public override void Move()
            {
                foreach (var child in _children)
                    child.Move();
            }
            public override void SetPoints(List<Point> points)
            {
                if (points.Count() != _children.Count())
                    throw new Exception("Количество объектов и точек должны совпадать");
                for(var i=0; i< _children.Count();i++)
                    _children[i].Position = points[i];
                SetMatrix();
                Move();
            }
            public override List<int[,]> GetMatrices()
            {
                var matrices = new List<int[,]>();
                foreach (var child in _children)
                    matrices.Add(child.Matrix);
                return matrices;
            }
            public override void Displace(int x, int y)
            {
                foreach (var child in _children)
                    child.Displace(x, y);
            }
        }





        abstract class Shape
        {
            virtual public int[,] Matrix { get; protected set; }
            public abstract void SetMatrix();
            public abstract void Move();
            virtual public Point Position { get; set; }

            virtual public void Add(Shape shape)
            {
                throw new NotImplementedException();
            }
            virtual public void Remove(Shape shape)
            {
                throw new NotImplementedException();
            }
            virtual public void SetPoints(List<Point> points)
            {
                throw new NotImplementedException();
            }
            virtual public List<int[,]> GetMatrices()
            {
                throw new NotImplementedException();
            }
            virtual public void Displace(int x, int y)
            {
                throw new NotImplementedException();
            }
            
        }
        class Rectangle : Shape
        {
            public override int[,] Matrix { get; protected set; }
            public override Point Position
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

            protected Point _position;

            public Rectangle()
            {
                _position = new Point();
                _position.X = 0;
                _position.Y = 0;
                SetMatrix();
            }
            public override void Move()
            {
                var newArr = new int[Position.Y + Matrix.GetLength(0), Position.X + Matrix.GetLength(1)];
                for (var i = 0; i < Matrix.GetLength(0); i++)
                    for (var j = 0; j < Matrix.GetLength(1); j++)
                    {
                        newArr[Position.Y + i, Position.X + j] = Matrix[i, j];
                    }
                Matrix = newArr;
            }

            public override void SetMatrix()
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
        class Triangle : Shape
        {
            private const int width = 13;
            public override int[,] Matrix { get; protected set; }
            private Point _position;
            public Triangle()
            {
                _position = new Point();
                _position.X = 0;
                _position.Y = 0;
                SetMatrix();
            }
            public override Point Position
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

            public override void SetMatrix()
            {
                var center = width / 2;
                Matrix = new int[center, width];
                for (var i = 0; i < center; i++)
                {
                    Matrix[i, center + i] = 1;
                    Matrix[i, center - i] = 1;
                }

                for (int j = 0; j < width - 1; j++)
                {
                    Matrix[Matrix.GetLength(0) - 1, j] = j % 2;
                }
            }
            public override void Move()
            {
                var newArr = new int[Position.Y + Matrix.GetLength(0), Position.X + Matrix.GetLength(1)];
                for (var i = 0; i < Matrix.GetLength(0); i++)
                    for (var j = 0; j < Matrix.GetLength(1); j++)
                    {
                        newArr[Position.Y + i, Position.X + j] = Matrix[i, j];
                    }
                Matrix = newArr;
            }
            public override void Displace(int x, int y)
            {
                this.Position = new Point(this.Position.X + x, this.Position.Y + y);
            }
        }
    }
}
