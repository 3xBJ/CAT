using CAT.Analysers.Methods;
using CAT.GUI.Functionalities.Analyzers.Drawables.Util;

namespace CAT.GUI.Functionalities.Analyzers.Drawables
{
    public class GraphDrawable : IDrawable
    {
        public List<MethodInformation> methodsInformation;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if(methodsInformation == null) return;

            int lineNumber = 0;
            List<DrawableNode> nodesToDraw = new(methodsInformation.Count);

            foreach (MethodInformation methodInfo in methodsInformation)
            {
                DrawableNode drawed = nodesToDraw.FirstOrDefault(methodInfo.FullName.Equals);
                if (drawed == null)
                {
                    int midPoint = methodInfo.CalledMethods.Count % 2;
                    drawed = GetDrawableNode(methodInfo, lineNumber++, midPoint);
                }

                int rowNumber = 0;
                foreach (var calledMethod in methodInfo.CalledMethods)
                {
                    DrawableNode drawedCalled = nodesToDraw.FirstOrDefault(calledMethod.FullName.Equals);
                    if (drawedCalled == null)
                    {
                        drawedCalled = GetDrawableNode(calledMethod, lineNumber, rowNumber++);
                        nodesToDraw.Add(drawedCalled);
                    }

                    drawedCalled.Pointed.Add(drawed);
                    drawed.PointTo.Add(drawedCalled);
                }

                nodesToDraw.Add(drawed);
            }

            int emptyColumn = 0;
            foreach(DrawableNode node in nodesToDraw)
            {
                if (node.Pointed.Count == 0)
                {
                    node.Coordinates.Y = 0;
                    node.Coordinates.X = 10 + emptyColumn++ * 100;
                }

                DrawNode(canvas, node);
            }
        }

        private DrawableNode GetDrawableNode(MethodInformation methodInfo, int lineNumber, int rowNumber)
        {
            int radius = 100;
            int y = 10 + (radius * lineNumber);
            int x = 10 + (radius * rowNumber);


            return new DrawableNode(methodInfo.FullName, methodInfo.PrettyName, x, y);
        }

        private void DrawNode(ICanvas canvas, DrawableNode drawableNode)
        {
            int radius = 100;
            int x = drawableNode.Coordinates.X;
            int y = drawableNode.Coordinates.Y;
            canvas.StrokeSize = 4;
            canvas.StrokeColor = Color.FromRgb(100, 0, 0);
            canvas.DrawEllipse(x, y, radius, radius);
            canvas.FontColor = Color.FromRgb(100, 0, 0);

            canvas.DrawString(drawableNode.PrettyName, x + 50, y + 50, HorizontalAlignment.Center);
        }
    }
}
