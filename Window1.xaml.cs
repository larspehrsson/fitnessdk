using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FitnessDK
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            String imgPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()) + "\\giphy.gif";

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<img src = \"" + imgPath + "\">");
            sb.Append("</body></html>");
            //webBrowser.Navigate(imgPath);
            webBrowser.Navigate(@"C:\Dropbox\Visual Studio\FitnessDK\running.gif");
            //WebBrowser.NavigateToString(sb.ToString());
        }
    }
}
