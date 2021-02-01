using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DropShadowSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            // 墨迹的渲染宽度
            double width = inkCanvas1.ActualWidth;
            // 墨迹的渲染高度
            double height = inkCanvas1.ActualHeight;
            // 将将 Visual(视觉呈现) 对象转换为位图。
            RenderTargetBitmap renderTargetBitmap =
               new RenderTargetBitmap((int)Math.Round(width), (int)Math.Round(height),
                96, 96, PixelFormats.Default);
            //可用于在屏幕上呈现矢量图形的视觉对象。 内容由系统保留。
            DrawingVisual drawingVisual = new DrawingVisual();
            // using 代码端相当于离开就抛弃
            // using 结束后会隐式的调用Disposable方法
            // 打开用于呈现的 DrawingVisual 对象。 返回的 DrawingContext 值可用于呈现为 DrawingVisual。
            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(inkCanvas1);
                // 绘制一个矩形
                // Rect 描述矩形的高度宽度和位置
                dc.DrawRectangle(visualBrush, new Pen(Brushes.White, 2),
                    new Rect(new System.Windows.Point(),
                    new System.Windows.Size(width, height)));
            }
            // 呈现一个Visual 图像
            renderTargetBitmap.Render(drawingVisual);
            //路径 创建 读/写权限
            using (FileStream fileStream = new FileStream(@"E:\a.bmp", FileMode.Create, FileAccess.Write))
            {
                // 定义用于编码位图BMP 格式图像编码器
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                // 设置图像的帧
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                // 图片背景色就是InkCanvas背景色
                encoder.Save(fileStream);
            }
            // 用来指定图片背景色
            //renderTargetBitmap.Render(inkCanvas1);
            //BitmapSource bitmapSource = BitmapSourceExtensions.ReplaceTransparency(renderTargetBitmap, Colors.White);
            //using (FileStream fileStream = new FileStream(@"E:\a.bmp", FileMode.Create, FileAccess.Write))
            //{
            //    // 定义用于编码位图BMP 格式图像编码器
            //    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            //    // 设置图像的帧
            //    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            //    encoder.Save(fileStream);
            //}
        }

        // 绘画
        private void DrawingInk_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }
        // 按点清除
        private void PointErase_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        // 按线清除
        private void LineErase_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }
        // 选中模式
        private void SelectInk_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
        }
        // 停止操作 鼠标变为箭头
        private void StopOperation_Click(object sender, RoutedEventArgs e)
        {
            // 停止操作 
            //比如绘画模式下 停止绘画 选中模式下 停止选中
            inkCanvas1.EditingMode = InkCanvasEditingMode.None;
        }
       
    }
}
