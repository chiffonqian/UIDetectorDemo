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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDetector
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MouseHook mouseHook = new MouseHook();
        public MainWindow()
        {
            InitializeComponent();
            mouseHook.LeftButtonUp += MouseHook_LeftButtonUp;
        }

        private void MouseHook_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            throw new NotImplementedException();
        }

        private void bt_start_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
