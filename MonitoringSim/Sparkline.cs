using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MonitoringSim
{
    public sealed class Sparkline : FrameworkElement
    {
        private readonly Queue<double> _values = new();

        public static readonly DependencyProperty MinYProperty =
            DependencyProperty.Register(nameof(MinY), typeof(double), typeof(Sparkline),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MaxYProperty =
            DependencyProperty.Register(nameof(MaxY), typeof(double), typeof(Sparkline),
                new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty CapacityProperty =
            DependencyProperty.Register(nameof(Capacity), typeof(int), typeof(Sparkline),
                new FrameworkPropertyMetadata(240, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register(nameof(LineBrush), typeof(Brush), typeof(Sparkline),
                new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(96, 165, 250)),
                                              FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register(nameof(BackgroundBrush), typeof(Brush), typeof(Sparkline),
                new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(11, 18, 32)),
                                              FrameworkPropertyMetadataOptions.AffectsRender));

        public double MinY { get => (double)GetValue(MinYProperty); set => SetValue(MinYProperty, value); }
        public double MaxY { get => (double)GetValue(MaxYProperty); set => SetValue(MaxYProperty, value); }
        public int Capacity { get => (int)GetValue(CapacityProperty); set => SetValue(CapacityProperty, value); }
        public Brush LineBrush { get => (Brush)GetValue(LineBrushProperty); set => SetValue(LineBrushProperty, value); }
        public Brush BackgroundBrush { get => (Brush)GetValue(BackgroundBrushProperty); set => SetValue(BackgroundBrushProperty, value); }

        public void Push(double v)
        {
            if (_values.Count >= Math.Max(2, Capacity)) _values.Dequeue();
            _values.Enqueue(v);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var w = ActualWidth; var h = ActualHeight;
            if (w < 2 || h < 2)
            {
                dc.DrawRectangle(BackgroundBrush, null, new Rect(0, 0, w, h));
                return;
            }

            dc.DrawRectangle(BackgroundBrush, null, new Rect(0, 0, w, h));

            if (_values.Count < 2 || MaxY <= MinY) return;

            var pen = new Pen(LineBrush, 2);
            var arr = _values.ToArray();
            double dx = w / (arr.Length - 1);

            Point? prev = null;
            for (int i = 0; i < arr.Length; i++)
            {
                double norm = (arr[i] - MinY) / (MaxY - MinY);
                norm = Math.Clamp(norm, 0, 1);
                var p = new Point(i * dx, h - norm * h);
                if (prev != null) dc.DrawLine(pen, prev.Value, p);
                prev = p;
            }
        }
    }

}
