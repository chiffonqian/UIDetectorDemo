using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Automation = System.Windows.Automation;
using System.Windows.Automation;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Win32Native;

namespace UIDetector
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MouseHook mouseHook = new MouseHook();
        AutomationElement rootElement = AutomationElement.RootElement;
        string processName = String.Empty;
        string processPath = String.Empty;
        string mainwndText = String.Empty;
        public MainWindow()
        {
            InitializeComponent();
            mouseHook.LeftButtonUp += MouseHook_LeftButtonUp;
        }

        private void MouseHook_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseHook.Uninstall();
            int hwnd = User.WindowFromPoint(mouseStruct.pt.x, mouseStruct.pt.y);
            if(0 == hwnd)
            {
                return;
            }
            int root = User.GetAncestor((IntPtr)hwnd, User.GA_ROOT);

            int pid=0;
            User.GetWindowThreadProcessId((IntPtr)root,ref pid);

            
            
            Process[] processlist = Process.GetProcesses();
            foreach (Process p in processlist)
            {
                if(p.Id == pid)
                {
                    mainwndText = p.MainWindowTitle;
                    processName = p.ProcessName;
                    processPath = p.MainModule.FileName;
                }
            }
            if (String.IsNullOrEmpty(processPath))
            {
                return;
            }
            Automation.Condition condition = new PropertyCondition(AutomationElement.NameProperty, mainwndText);

            throw new NotImplementedException();
        }

        private void bt_start_Click(object sender, RoutedEventArgs e)
        {
            mouseHook.Install();
        }
    }
}
