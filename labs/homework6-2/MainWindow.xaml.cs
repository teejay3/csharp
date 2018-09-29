using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Management;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;

namespace homework6_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MyDll info = new MyDll();
        public MainWindow()
        {
            InitializeComponent();
            string[] info_arr = info.GetInfo();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] info_arr = info.GetInfo();
            textbox1.Clear();
            textbox1.Text = "HW info:" + "\n";
            textbox1.Text = textbox1.Text + info.GetInfo()[0];
            textbox1.Text = textbox1.Text + info.GetInfo()[1];
            textbox1.Text = textbox1.Text + info.GetInfo()[2];
            textbox1.Text = textbox1.Text + info.GetInfo()[3];
            textbox1.Text = textbox1.Text + info.GetInfo()[4];
            textbox1.Text = textbox1.Text + info.GetInfo()[5];
            textbox1.Text = textbox1.Text + info.GetInfo()[6];
            textbox1.Text = textbox1.Text + info.GetInfo()[7];
            textbox1.Text = textbox1.Text + info.GetInfo()[8];
            textbox1.Text = textbox1.Text + info.GetInfo()[9];
            textbox1.Text = textbox1.Text + info.GetInfo()[10];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox1.Clear();
            textbox1.Text = "System folders:" + "\n";
            textbox1.Text = textbox1.Text + "System: " + 
                Environment.GetFolderPath(Environment.SpecialFolder.System) + "\n";
            textbox1.Text = textbox1.Text + "Windows: " +
                Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\n";
            textbox1.Text = textbox1.Text + "My Computer: " +
                Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) + "\n";
            textbox1.Text = textbox1.Text + "Program Files: " +
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\n";
            textbox1.Text = textbox1.Text + "Desktop: " +
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\n";


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var env_dic = Environment.GetEnvironmentVariables();
            textbox1.Clear();
            textbox1.Text = "Envirioment:" + "\n";
            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                textbox1.Text = textbox1.Text + "\n" + item.Key + " " + item.Value + "\n";
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            textbox1.Clear();
            string name = null;
            ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject obj in search.Get())
            {
                name = obj["Caption"].ToString();
                textbox1.Text = "System:" + "\n";
                textbox1.Text = textbox1.Text + name + "\n";
            }
            foreach (ManagementObject obj in search.Get())
            {
                name = obj["OSARchitecture"].ToString();
                textbox1.Text = textbox1.Text + "OSARchitecture:" + "\n";
                textbox1.Text = textbox1.Text + name + "\n";
            }
            foreach (ManagementObject obj in search.Get())
            {
                name = obj["Manufacturer"].ToString();
                textbox1.Text = textbox1.Text + "Manufacturer:" + "\n";
                textbox1.Text = textbox1.Text + name + "\n";
            }
        }
    }
    public class MyDll
    {
        [StructLayout(LayoutKind.Sequential)]
        struct SYSTEM_INFO
        {
            public Int16 wProcessorArchitecture;
            public Int16 wReserved;
            public Int32 dwPageSize;
            public Int32 lpMinimumApplicationAddress;
            public Int32 lpMaximumApplicationAddress;
            public Int32 dwActiveProcessorMask;
            public Int32 dwNumberOfProcessors;
            public Int32 dwProcessorYtpe;
            public Int32 dwAllocationGranularity;
            public Int16 wProcessorLevel;
            public Byte wProcessorRevision;
        }

        unsafe public string[] GetInfo()
        {
            SYSTEM_INFO pInfo = new SYSTEM_INFO();
            GetSystemInfo(&pInfo);
            string[] info = new string[11];
            info[0] = "OEM ID :" + pInfo.wProcessorArchitecture + '\n';
            info[1] = "Number of processors: " + pInfo.dwNumberOfProcessors + '\n';
            info[2] = "Page size: " + pInfo.dwPageSize + '\n';
            info[3] = "Processor type: " + pInfo.dwProcessorYtpe + '\n';
            info[4] = "Minumum application address: " + pInfo.lpMinimumApplicationAddress + '\n';
            info[5] = "Maximum application address: " + pInfo.lpMaximumApplicationAddress + '\n';
            info[6] = "Active processor mask: " + pInfo.dwActiveProcessorMask + '\n';
            info[7] = "Alloc granularity: " + pInfo.dwAllocationGranularity + '\n';
            info[8] = "Processor revision: " + pInfo.wProcessorRevision + '\n';
            info[9] = "ARch: " + pInfo.wProcessorArchitecture + '\n';
            info[10] = "" + '\n';
            return info;
        }

        [DllImport("kernel32.dll")]
        unsafe static extern void GetSystemInfo(SYSTEM_INFO* pInfo);
        
    }
}
