using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
#if !__IOS__
using Emgu.CV.Cuda;
#endif
using Emgu.CV.XFeatures2D;
using Emgu.CV.UI;
using System.IO;
using System.Runtime.InteropServices;

namespace MyCroft1._0V.Win
{
    public partial class goog : Form
    {
        [DllImport("user32")]
        public static extern int SetCursorPos(int x = 300, int y = 300);
        [DllImport("user32.dll",
     CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons,
            int dwExtraInfo);
        public const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        public const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public static int nokta_x=20, nokta_y=20;
        Mat a1, a2;
        Image<Bgr, Byte> img1;//taranıcak
        Image<Bgr, Byte> img2;
        long a4;

        public goog()
        {
            InitializeComponent();
        }
        string deneme;
        SpeechSynthesizer synt = new SpeechSynthesizer();
        PromptBuilder pbuilder = new PromptBuilder();
        SpeechRecognitionEngine rengine = new SpeechRecognitionEngine();
        
        private void button1_Click(object sender, EventArgs e)
        {
            Choices list = new Choices();
            list.Add(new string[] { "mycroft", "exit", "open","homepage","openchrome","openyoutube" });
            Grammar gramer = new Grammar(new GrammarBuilder(list));
            try
            {
                rengine.RequestRecognizerUpdate();
                rengine.LoadGrammar(gramer);
                rengine.SpeechRecognized += rengine_SpeechRecognized;
                rengine.SetInputToDefaultAudioDevice();
                rengine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception)
            {

                return;
            }
        }

        void rengine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            
            if (deneme != e.Result.Text)
            {
                switch (e.Result.Text)
                {
                    case "scan":
                         pbuilder.ClearContent();
                        pbuilder.AppendText("yes Sir Mertcan");
                        synt.Speak(pbuilder);
                        SendKeys.Send("{PRTSC}");
                        Image img31 = Clipboard.GetImage();
                        img31.Save("an.jpg");
                        Thread.Sleep(500);
                        Image img30= MyCroft1._0V.Win.Properties.Resources.an ;
                        img30.Save("bulunan.jpg");
                        Thread.Sleep(500);
                        img1 = new Image<Bgr, Byte>("an.jpg");
                        img2 = new Image<Bgr, Byte>("bulunan.jpg");
                        a1 = img1.Mat;//screenshot orta taranacak orta
                        a2 = img2.Mat;
                       
                        DrawMatches.Draw(a2, a1, out a4);
                        Cursor.Position = new Point(nokta_x, nokta_y);
                        for (int i = 1; i < 4; i++)
                        {
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, nokta_x, nokta_y, 0, 0);
                            mouse_event(MOUSEEVENTF_RIGHTUP, nokta_x, nokta_y, 0, 0);   
                        }
                        Thread.Sleep(1000);  
                     File.Delete("an.jpg");
                     File.Delete("bulunan.jpg");//dosya silme
                     deneme = e.Result.Text;
                        break;
                    case "openad":
                        System.Diagnostics.Process.Start("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Advanced SystemCare\\Advanced SystemCare");
                        Thread.Sleep(1000);
                        deneme = e.Result.Text;
                        break;
                    case "homepage":
                        listView1.Visible = true;
                        listView2.Visible = true;
                        break;
                    case "move":
                        pbuilder.ClearContent();
                        pbuilder.AppendText("tabikide Sir Mertcan");
                        synt.Speak(pbuilder);
                        SendKeys.Send("{PRTSC}");
                        Image img41 = Clipboard.GetImage();
                        img41.Save("screenci.jpg");
                        Thread.Sleep(500);
                        Image img42= MyCroft1._0V.Win.Properties.Resources.an ;
                        img42.Save("bulunan.jpg");
                        Thread.Sleep(500);
                        img1 = new Image<Bgr, Byte>("screenci.jpg");
                        img2 = new Image<Bgr, Byte>("bulunan.jpg");
                        a1 = img1.Mat;//screenshot orta taranacak orta
                        a2 = img2.Mat;
                       
                        DrawMatches.Draw(a2, a1, out a4);
                        Cursor.Position = new Point(nokta_x, nokta_y);
                        for (int i = 1; i < 4; i++)
                        {
                            mouse_event(MOUSEEVENTF_LEFTDOWN, nokta_x, nokta_y, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTUP, nokta_x, nokta_y, 0, 0);   
                        }
                        Thread.Sleep(1000);  
                     File.Delete("screenci.jpg");
                     File.Delete("bulunan.jpg");//dosya silme
                     deneme = e.Result.Text;
                        break;
                    case "browser":
                        System.Diagnostics.Process.Start("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Microsoft Edge");
                        Thread.Sleep(1000);
                        deneme = e.Result.Text;
                        break;
                    case "mycroft":
                        timer1.Enabled = true;
                        deneme = e.Result.Text;
                        // idef.Document.GetElementById("text").SetAttribute("value", "Mertcan");
                        break;
                    case "search":
                        timer1.Enabled = false;
                        //System.Diagnostics.Process.Start();dosya açma
                        pbuilder.ClearContent();
                        pbuilder.AppendText("yes sir");
                        synt.Speak(pbuilder);
                        listView1.Visible = false;
                        listView2.Visible = false;
                        webBrowser1.Document.GetElementById("lst-ib").SetAttribute("value", "Mertcan");
                        foreach (HtmlElement btn in webBrowser1.Document.GetElementsByTagName("submit"))
                        {
                            if (btn.GetAttribute("className") == "btnK")
                            {
                                btn.InvokeMember("Click");
                                break;
                            }
                        }

                        yand.Document.GetElementById("text").SetAttribute("value", "Mertcan");
                        foreach (HtmlElement btn in yand.Document.GetElementsByTagName("button"))
                        {
                            if (btn.GetAttribute("className") == "button suggest2-form__button button_theme_websearch button_size_ws-head i-bem button_js_inited")
                            {
                                btn.InvokeMember("Click");
                                break;
                            }
                        }

                        bing.Document.GetElementById("sb_form_q").SetAttribute("value", "Mertcan");
                        foreach (HtmlElement btn in bing.Document.GetElementsByTagName("input"))
                        {
                            if (btn.GetAttribute("name") == "go")
                            {
                                btn.InvokeMember("Click");
                                break;
                            }
                        }

                        tek.Document.GetElementById("search").SetAttribute("value", "Mertcan");
                        foreach (HtmlElement btn in tek.Document.GetElementsByTagName("button"))
                        {
                            if (btn.GetAttribute("className") == "searchSubmit")
                            {
                                btn.InvokeMember("Click");
                                break;
                            }
                        }
                        deneme = e.Result.Text;
                        break;
                    case "exit":

                        //Application.Exit();
                        break;
                }    
            }
            
        }

        private void goog_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com.tr/");
            yand.Navigate("https://www.google.com.tr/");
            bing.Navigate("https://www.bing.com/");
            tek.Navigate("http://www.yandex.com/");
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            
            Choices list = new Choices();
            list.Add(new string[] { "mycroft", "exit", "open", "search", "homepage","openad","openyoutube","scan","browser","move" });
            Grammar gramer = new Grammar(new GrammarBuilder(list));
            try
            {
                rengine.RequestRecognizerUpdate();
                rengine.LoadGrammar(gramer);
                rengine.SpeechRecognized += rengine_SpeechRecognized;
                rengine.SetInputToDefaultAudioDevice();
                rengine.RecognizeAsync(RecognizeMode.Multiple);
                
            }
            catch (Exception)
            {

                return;
            }
            
        }

        public void noktahesabi(Point noktaci1, Point noktaci2)//emgu gelen noktayı fare gönderme
        {
            string sinir = noktaci1.X.ToString();
            nokta_x = (int.Parse(noktaci1.X.ToString()) + int.Parse(noktaci2.X.ToString())) / 2;
            nokta_y = (int.Parse(noktaci1.Y.ToString()) + int.Parse(noktaci2.Y.ToString())) / 2;
         }
    }
    public static class DrawMatches//emgucv kütüphanesi
    {
        public static void FindMatch(Mat modelImage, Mat observedImage, out long matchTime, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            Stopwatch watch;
            homography = null;

            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();

#if !__IOS__
            if (CudaInvoke.HasCuda)
            {
                MessageBox.Show("Test");
                CudaSURF surfCuda = new CudaSURF((float)hessianThresh);
                using (GpuMat gpuModelImage = new GpuMat(modelImage))
                //extract features from the object image
                using (GpuMat gpuModelKeyPoints = surfCuda.DetectKeyPointsRaw(gpuModelImage, null))
                using (GpuMat gpuModelDescriptors = surfCuda.ComputeDescriptorsRaw(gpuModelImage, null, gpuModelKeyPoints))
                using (CudaBFMatcher matcher = new CudaBFMatcher(DistanceType.L2))
                {
                    surfCuda.DownloadKeypoints(gpuModelKeyPoints, modelKeyPoints);
                    watch = Stopwatch.StartNew();

                    // extract features from the observed image
                    using (GpuMat gpuObservedImage = new GpuMat(observedImage))
                    using (GpuMat gpuObservedKeyPoints = surfCuda.DetectKeyPointsRaw(gpuObservedImage, null))
                    using (GpuMat gpuObservedDescriptors = surfCuda.ComputeDescriptorsRaw(gpuObservedImage, null, gpuObservedKeyPoints))
                    //using (GpuMat tmp = new GpuMat())
                    //using (Stream stream = new Stream())
                    {
                        matcher.KnnMatch(gpuObservedDescriptors, gpuModelDescriptors, matches, k);

                        surfCuda.DownloadKeypoints(gpuObservedKeyPoints, observedKeyPoints);

                        mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                        mask.SetTo(new MCvScalar(255));
                        Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                        int nonZeroCount = CvInvoke.CountNonZero(mask);
                        if (nonZeroCount >= 4)
                        {
                            nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                               matches, mask, 1.5, 20);
                            if (nonZeroCount >= 4)
                                homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                                   observedKeyPoints, matches, mask, 2);
                        }
                    }
                    watch.Stop();
                }
            }
            else
#endif
            {
                using (UMat uModelImage = modelImage.ToUMat(AccessType.Read))
                using (UMat uObservedImage = observedImage.ToUMat(AccessType.Read))
                {
                    SURF surfCPU = new SURF(hessianThresh);
                    //extract features from the object image
                    UMat modelDescriptors = new UMat();
                    surfCPU.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                    watch = Stopwatch.StartNew();

                    // extract features from the observed image
                    UMat observedDescriptors = new UMat();
                    surfCPU.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);
                    BFMatcher matcher = new BFMatcher(DistanceType.L2);
                    matcher.Add(modelDescriptors);

                    matcher.KnnMatch(observedDescriptors, matches, k, null);
                    mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    mask.SetTo(new MCvScalar(255));
                    Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                    int nonZeroCount = CvInvoke.CountNonZero(mask);
                    if (nonZeroCount >= 4)
                    {
                        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                           matches, mask, 1.5, 20);
                        if (nonZeroCount >= 4)
                            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                               observedKeyPoints, matches, mask, 2);
                    }

                    watch.Stop();
                }
            }
            matchTime = watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Draw the model image and observed image, the matched features and homography projection.
        /// </summary>
        /// <param name="modelImage">The model image</param>
        /// <param name="observedImage">The observed image</param>
        /// <param name="matchTime">The output total time for computing the homography matrix.</param>
        /// <returns>The model image and observed image, the matched features and homography projection.</returns>
        public static Mat Draw(Mat modelImage, Mat observedImage, out long matchTime)
        {
            Point nokta1, nokta2;
            Mat homography;
          //  Point pozz;
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask;
                FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                   out mask, out homography);

                //Draw the matched keypoints
                Mat result = new Mat();
                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

                #region draw the projected region on the image

                if (homography != null)
                {
                    //draw a rectangle along the projected model
                    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                    PointF[] pts = new PointF[]
               {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                  
               };


                    pts = CvInvoke.PerspectiveTransform(pts, homography);

                    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);

                  /*  if (points[0] != null)//mouse için nokta belirleme 1 ile 3 toplanıp orta noktası alınacak
                    {
                        pozz = points[0];
                        string asdc = pozz.ToString();
                        Form1 frm = new Form1();
                        frm.Mert(asdc);
                    }*/
                    if (points[0]!=null)
                    {
                        MessageBox.Show(points[1].ToString());
                        MessageBox.Show(points[3].ToString());
                        
                        nokta1 = points[1];
                        nokta2 = points[3];
                        goog gm = new goog();
                        gm.noktahesabi(nokta1, nokta2);

                    }
                    using (VectorOfPoint vp = new VectorOfPoint(points))
                    {
                        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                    }
                    
                }

                #endregion
                return result;

            }
        }
    }
}
