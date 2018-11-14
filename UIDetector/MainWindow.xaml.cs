﻿using System;
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
            tb_detail.Text += "mouse hook stop:";
            Point mousePoint = new Point(mouseStruct.pt.x, mouseStruct.pt.y);
            //int hwnd = User.WindowFromPoint(mouseStruct.pt.x, mouseStruct.pt.y);
            //if(0 == hwnd)
            //{
            //    return;
            //}
            //int root = User.GetAncestor((IntPtr)hwnd, User.GA_ROOT);

            //int pid=0;
            //User.GetWindowThreadProcessId((IntPtr)root,ref pid);

            
            
            //Process[] processlist = Process.GetProcesses();
            //foreach (Process p in processlist)
            //{
            //    if(p.Id == pid)
            //    {
            //        mainwndText = p.MainWindowTitle;
            //        processName = p.ProcessName;
            //        //processPath = p.MainModule.FileName;
            //    }
            //}
            //if (String.IsNullOrEmpty(processPath))
            //{
            //    return;
            //}
            //Automation.Condition condition = new PropertyCondition(AutomationElement.NameProperty, mainwndText);
            //AutomationElement appElement = rootElement.FindFirst(TreeScope.Children, condition);
            //if(null == appElement)
            //{
            //    return;
            //}
            //AutomationElementCollection theCollection = appElement.FindAll(TreeScope.Descendants,Automation.Condition.TrueCondition);
            //if(null == appElement)
            //{
            //    return;
            //}
            AutomationElement focusElement = AutomationElement.FromPoint(mousePoint);
            switch (focusElement.Current.FrameworkId)
            {
                case "WPF":
                    switch (focusElement.Current.ClassName)
                    {
                        case "TextBox":
                            {
                                ValuePattern pattern = focusElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                                tb_detail.Text += pattern.Current.Value + "\r\n";
                            }
                            break;
                        case "TextBlock":
                        case "Text":
                            {
                                tb_detail.Text += focusElement.Current.Name + "\r\n";
                            }
                            break;
                        case "RichTextBox":
                            {
                                TextPattern pattern = focusElement.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
                                string controlText = pattern.DocumentRange.GetText(-1);
                                tb_detail.Text += controlText+"\r\n";
                            }
                            break;
                    }
                    break;
                case "Win32":
                    switch (focusElement.Current.ClassName)
                    {
                        case "Edit":
                            {
                                tb_detail.Text += focusElement.Current.Name + "\r\n";
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case "WinForms":
                    break;
                case "Silverlight":
                    break;
                case "SWT":
                    break;
                default:
                    break;

            }
            TreeWalker tWalker = TreeWalker.ControlViewWalker;
            AutomationElement parentElement = tWalker.GetParent(focusElement);
            if(parentElement == AutomationElement.RootElement)
            {
                return;
            }
            while (true)
            {
                AutomationElement element = tWalker.GetParent(parentElement);
                if (element == AutomationElement.RootElement)
                {
                    break;
                }
                else
                {

                    parentElement = element;
                }
            }

            AutomationElementCollection theCollection = appElement.FindAll(TreeScope.Descendants, Automation.Condition.TrueCondition);
            if (null == appElement)
            {
                return;
            }


        }

        private void bt_start_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void bt_start_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void bt_start_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseHook.Install();
            tb_detail.Text += "mouse hook start:\r\n";

        }
    }
}
