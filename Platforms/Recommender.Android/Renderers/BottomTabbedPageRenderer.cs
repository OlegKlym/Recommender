using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Internal;
using Java.Util;
using Recommender.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(BottomTabbedPageRenderer))]
namespace Recommender.Droid.Renderers
{
    public class BottomTabbedPageRenderer : TabbedPageRenderer
    {
        // the radius of the FloatingActionButton
        private const float FAB_RADIUS = Resource.Dimension.design_fab_size_normal / 2;
        // offset of the first control point (top part)
        private float TOP_CONTROL_X = FAB_RADIUS + FAB_RADIUS / 2;
        private float TOP_CONTROL_Y = FAB_RADIUS / 6;
        // offset of the second control point (bottom part)
        private float BOTTOM_CONTROL_X = FAB_RADIUS + (FAB_RADIUS / 2);
        private float BOTTOM_CONTROL_Y = FAB_RADIUS / 4;
        // width of the curve
        private float CURVE_OFFSET = FAB_RADIUS * 2 + (FAB_RADIUS / 6);
        private string TAG = "CurvedBottomNavigation";

        // first bezier curve
        private PointF firstCurveStart = new PointF();
        private PointF firstCurveEnd = new PointF();
        private PointF firstCurveControlPoint1 = new PointF();
        private PointF firstCurveControlPoint2 = new PointF();

        // second bezier curve
        private PointF secondCurveStart = new PointF();
        private PointF secondCurveEnd = new PointF();
        private PointF secondCurveControlPoint1 = new PointF();
        private PointF secondCurveControlPoint2 = new PointF();

        // path to represent the background including the curve
        private Path path = new Path();
        // just a basic paint
        private Paint paint = new Paint
        {
            Color = Android.Graphics.Color.White
        };
        
        public BottomTabbedPageRenderer(Context context) : base(context)
        {
           paint.SetStyle(Paint.Style.FillAndStroke);

           SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (ViewGroup != null && ViewGroup.ChildCount > 0)
            {
                
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

             firstCurveStart.X = w / 2 - CURVE_OFFSET;
             firstCurveStart.Y = 0f;
             
             // set the end point for the first curve (P3)
             firstCurveEnd.X = (w / 2f);
             firstCurveEnd.Y = FAB_RADIUS + (FAB_RADIUS / 4);
             
             // set the first control point (C1)
             firstCurveControlPoint1.X = firstCurveStart.X + TOP_CONTROL_X;
             firstCurveControlPoint1.Y = TOP_CONTROL_Y;
            
             // set the second control point (C2)
             firstCurveControlPoint2.X = firstCurveEnd.X - BOTTOM_CONTROL_X;
             firstCurveControlPoint2.Y = firstCurveEnd.Y - BOTTOM_CONTROL_Y;
             
             // second curve
             // end of first curve and start of second curve is the same (P3)
             secondCurveStart.Set(firstCurveEnd.X, firstCurveEnd.Y);
             // end of the second curve (P4)
             
             // second curve
             // end of first curve and start of second curve is the same (P3)
             secondCurveStart.Set(firstCurveEnd.X, firstCurveEnd.Y);
             // end of the second curve (P4)

             secondCurveEnd.X = (w / 2) + CURVE_OFFSET;
             secondCurveEnd.Y = 0f;
             
             // set the first control point of second curve (C4)
             secondCurveControlPoint1.X = secondCurveStart.X + BOTTOM_CONTROL_X;
             secondCurveControlPoint1.Y = secondCurveStart.Y - BOTTOM_CONTROL_Y;
             
             // set the second control point (C3)
             secondCurveControlPoint2.X = secondCurveEnd.X - TOP_CONTROL_X;
             secondCurveControlPoint2.Y = TOP_CONTROL_Y;
             
             // clear any previous path
             path.Reset();
             // start from P1 of the BottomNavigationView
             path.MoveTo(0f, 0f);
             // horizontal line from P1 to P2
             path.LineTo(firstCurveStart.X, firstCurveStart.Y);
             // bezier curve with (P2, C1, C2, P3)
             path.CubicTo(
                 firstCurveControlPoint1.X,
                 firstCurveControlPoint1.Y,
                 firstCurveControlPoint2.X,
                 firstCurveControlPoint2.Y,
                 firstCurveEnd.X,
                 firstCurveEnd.Y
             );
             
             // bezier curve with (P3, C4, C3, P4)
             path.CubicTo(
                 secondCurveControlPoint1.X,
                 secondCurveControlPoint1.Y,
                 secondCurveControlPoint2.X,
                 secondCurveControlPoint2.Y,
                 secondCurveEnd.X,
                 secondCurveEnd.Y
             );
             
             // line from P4 to P5
             path.LineTo(w, 0f);
             // line from P5 to P6
             path.LineTo(w, h);
             // line from P6 to P7
             path.LineTo(0f, h);
             // complete the path
             path.Close();
        }

        protected override void OnDraw(Canvas? canvas)
        {
            base.OnDraw(canvas);
            
            var paint = new Paint();
            canvas.DrawPath(path, paint);
        }
    }
}