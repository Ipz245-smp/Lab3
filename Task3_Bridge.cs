using System;

interface IRenderer
{
    void RenderCircle();
    void RenderSquare();
    void RenderTriangle();
}

class VectorRenderer : IRenderer
{
    public void RenderCircle()   => Console.WriteLine("Drawing Circle as vector lines");
    public void RenderSquare()   => Console.WriteLine("Drawing Square as vector lines");
    public void RenderTriangle() => Console.WriteLine("Drawing Triangle as vector lines");
}

class RasterRenderer : IRenderer
{
    public void RenderCircle()   => Console.WriteLine("Drawing Circle as pixels");
    public void RenderSquare()   => Console.WriteLine("Drawing Square as pixels");
    public void RenderTriangle() => Console.WriteLine("Drawing Triangle as pixels");
}

abstract class Shape
{
    protected IRenderer _renderer;

    public Shape(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public abstract void Draw();
}

class Circle : Shape
{
    public Circle(IRenderer renderer) : base(renderer) { }
    public override void Draw() => _renderer.RenderCircle();
}

class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer) { }
    public override void Draw() => _renderer.RenderSquare();
}

class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer) { }
    public override void Draw() => _renderer.RenderTriangle();
}
