namespace MauiGraphics.Sample.Blazor.WebAssembly.Client.Pages;

public partial class Graphics
{
    private SKCanvasView _skiaCanvas = null!;
    private readonly SkiaCanvas _mauiGraphicsCanvas = new();
    private bool _mouseDown = false;
    private readonly List<List<PointF>> _points = new();
    private readonly Color _color = Colors.Green;

    private void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        // Get the Skia canvas and clear it
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);
        // Let the Maui.Graphics canvas know which underlying SkCanvas to draw to.
        _mauiGraphicsCanvas.Canvas = canvas;
        // Actually do the drawing with the Maui.Graphics API.
        _mauiGraphicsCanvas.StrokeColor = _color;
        if (_points.Count <= 0)
        {
            return;
        }
        foreach (var pointList in _points)
        {
            if (pointList.Count <= 1)
            {
                continue;
            }
            var start = pointList[0];
            for (var i = 1; i < pointList.Count; i++)
            {
                var end = pointList[i];
                _mauiGraphicsCanvas.DrawLine(start, end);
                start = end;
            }
        }
    }

    private void HandleMouseDown(MouseEventArgs evt)
    {
        _mouseDown = true;
        var points = new List<PointF>
        {
            GetPoint(evt)
        };
        _points.Add(points);
        _skiaCanvas.Invalidate();
    }

    /// <summary>
    /// Create a Maui.Graphics point from the mouse event.
    /// </summary>
    /// <param name = "evt"></param>
    /// <returns></returns>
    private static PointF GetPoint(MouseEventArgs evt) => new((float)evt.OffsetX, (float)evt.OffsetY);

    private void HandleMouseUp(MouseEventArgs evt)
    {
        if (!_mouseDown)
        {
            return;
        }

        var point = GetPoint(evt);
        _points[^1].Add(point);
        _skiaCanvas.Invalidate();
        _mouseDown = false;
    }

    private void HandleMouseMove(MouseEventArgs evt)
    {
        if (!_mouseDown)
        {
            return;
        }

        var point = GetPoint(evt);
        _points[^1].Add(point);
        _skiaCanvas.Invalidate();
    }

    private void HandleMouseOut(MouseEventArgs evt)
    {
        if (!_mouseDown)
        {
            return;
        }

        var point = GetPoint(evt);
        _points[^1].Add(point);
        _skiaCanvas.Invalidate();
        _mouseDown = false;
    }

    private void HandlePointerDown(PointerEventArgs evt)
    {
        if (!_mouseDown)
        {
            return;
        }

        var point = GetPoint(evt);
        _points[^1].Add(point);
        _skiaCanvas.Invalidate();
    }

    /// <summary>
    /// Clear the drawing canvas
    /// </summary>
    private void HandleClear()
    {
        _points.Clear();
        _skiaCanvas.Invalidate();
    }
}
