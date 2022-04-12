using Biden.Func.Clone;
using Biden.Model;
using Biden.View;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices; //required for dll import
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Biden.Func
{

    [DefaultEvent("ClipboardChanged")]
    class Macro
    {

        private bool flag1 = false;
        private bool flag2 = false;
        private bool flag3 = false;
        private bool flag4 = false;
        private bool flag5 = false;

        private static bool isRunning = false;
        private bool isInit = false;

        private bool modeOn1 = false;
        private bool modeOn2 = false;
        private bool modeOn3 = false;
        private bool modeOn4 = false;

        public bool removeBlankFlag = false;
        public static bool doublePasteFlag = true;
        public static bool clipBoardMonitorFlag = false; // Ctrl+V 중 클립보드모니터 중지

        private static string key1 = "";
        private static string key2 = "";
        private static string clipboardChangedResult = "";


        private static List<String> list;
        private static List<String> parameterList;

        private CorrectString correctString;
        private FindAndAlert findAndAlert;
        private MultiClipboard multiClipboard;
        private PasteAlert pasteAlert;
        private ClipboardMonitor clipboardMonitor;

        public static object pasteSelectedObject = "";

        public Image ggg = null;


        //public static Bitmap screenPixel = new Bitmap(500, 200, PixelFormat.Format32bppArgb);
        public static Bitmap screenPixel = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        public static Color color; 

        private static Macro instance = null;

        private Macro()
        {
            list = new List<String>();
            parameterList = new List<String>();
            correctString = new CorrectString();
            findAndAlert = new FindAndAlert();
            multiClipboard = new MultiClipboard();
            pasteAlert = new PasteAlert();
            clipboardMonitor = new ClipboardMonitor(this);
        }

        public static Macro getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Macro();
                }
                return instance;
            }
        }

        public bool IsInit { get => isInit; set => isInit = value; }
        public bool ModeOn1 { get => modeOn1; set => modeOn1 = value; }
        public bool ModeOn2 { get => modeOn2; set => modeOn2 = value; }
        public bool ModeOn3 { get => modeOn3; set => modeOn3 = value; }
        public bool ModeOn4 { get => modeOn4; set => modeOn4 = value; }
        public static bool IsRunning { get => isRunning; set => isRunning = value; }
        public bool Flag1 { get => flag1; set => flag1 = value; }
        public bool Flag2 { get => flag2; set => flag2 = value; }
        public bool Flag3 { get => flag3; set => flag3 = value; }
        public bool Flag4 { get => flag4; set => flag4 = value; }
        public bool Flag5 { get => flag5; set => flag5 = value; }

        public enum VK
        {
            //Keycodes may be found on many internet sites.
            //Some keys are commented feel free to uncomment them, explanations are provided for uncommon ones ;)

            VK_LBUTTON = 0X01, //Left mouse
            VK_RBUTTON = 0X02, //Right mouse
            //VK_CANCEL       = 0X03,
            VK_MBUTTON = 0X04,
            VK_BACK = 0X08, //Backspace
            VK_TAB = 0X09,
            //VK_CLEAR        = 0X0C,
            VK_RETURN = 0X0D, //Enter
            VK_SHIFT = 0X10,
            VK_CONTROL = 0X11, //CTRL
            //VK_MENU         = 0X12,
            VK_PAUSE = 0X13,
            VK_CAPITAL = 0X14, //Caps-Lock
            VK_ESCAPE = 0X1B,
            VK_SPACE = 0X20,
            VK_PRIOR = 0X21, //Page-Up
            VK_NEXT = 0X22, //Page-Down
            VK_END = 0X23,
            VK_HOME = 0X24,
            VK_LEFT = 0X25,
            VK_UP = 0X26,
            VK_RIGHT = 0X27,
            VK_DOWN = 0X28,
            //VK_SELECT       = 0X29,
            //VK_PRINT        = 0X2A,
            //VK_EXECUTE      = 0X2B,
            VK_SNAPSHOT = 0X2C, //Print Screen
            VK_INSERT = 0X2D,
            VK_DELETE = 0X2E,
            //VK_HELP         = 0X2F,

            VK_0 = 0X30,
            VK_1 = 0X31,
            VK_2 = 0X32,
            VK_3 = 0X33,
            VK_4 = 0X34,
            VK_5 = 0X35,
            VK_6 = 0X36,
            VK_7 = 0X37,
            VK_8 = 0X38,
            VK_9 = 0X39,

            VK_A = 0X41,
            VK_B = 0X42,
            VK_C = 0X43,
            VK_D = 0X44,
            VK_E = 0X45,
            VK_F = 0X46,
            VK_G = 0X47,
            VK_H = 0X48,
            VK_I = 0X49,
            VK_J = 0X4A,
            VK_K = 0X4B,
            VK_L = 0X4C,
            VK_M = 0X4D,
            VK_N = 0X4E,
            VK_O = 0X4F,
            VK_P = 0X50,
            VK_Q = 0X51,
            VK_R = 0X52,
            VK_S = 0X53,
            VK_T = 0X54,
            VK_U = 0X55,
            VK_V = 0X56,
            VK_W = 0X57,
            VK_X = 0X58,
            VK_Y = 0X59,
            VK_Z = 0X5A,

            VK_NUMPAD0 = 0X60,
            VK_NUMPAD1 = 0X61,
            VK_NUMPAD2 = 0X62,
            VK_NUMPAD3 = 0X63,
            VK_NUMPAD4 = 0X64,
            VK_NUMPAD5 = 0X65,
            VK_NUMPAD6 = 0X66,
            VK_NUMPAD7 = 0X67,
            VK_NUMPAD8 = 0X68,
            VK_NUMPAD9 = 0X69,

            VK_SEPERATOR = 0X6C, // | (shift + backslash)
            VK_SUBTRACT = 0X6D, // -
            VK_DECIMAL = 0X6E, // .
            VK_DIVIDE = 0X6F, // /

            VK_F1 = 0X70,
            VK_F2 = 0X71,
            VK_F3 = 0X72,
            VK_F4 = 0X73,
            VK_F5 = 0X74,
            VK_F6 = 0X75,
            VK_F7 = 0X76,
            VK_F8 = 0X77,
            VK_F9 = 0X78,
            VK_F10 = 0X79,
            VK_F11 = 0X7A,
            VK_F12 = 0X7B,
            //and for the 8 people in the world who do, I think they can live without using them

            VK_NUMLOCK = 0X90,
            VK_SCROLL = 0X91, //Scroll-Lock
            VK_LSHIFT = 0XA0,
            VK_RSHIFT = 0XA1,
            VK_LCONTROL = 0XA2,
            VK_RCONTROL = 0XA3,
            //VK_LMENU        = 0XA4,
            //VK_RMENU        = 0XA5,
            //VK_PLAY         = 0XFA,
            //VK_ZOOM         = 0XFB

            WM_PASTE = 0x302
        } //keycodes

        //There are detailed explanations for these functions on MSDNAA and implementations.
        public delegate IntPtr HookDel(
            int nCode,
            IntPtr wParam,
            IntPtr lParam);

        public delegate void KeyHandler(
            IntPtr wParam,
            IntPtr lParam);

        private static IntPtr hhk = IntPtr.Zero;
        private static HookDel hd;
        private static KeyHandler kh;


        //Creation of the hook
        public static void CreateHook(KeyHandler _kh)
        {
            Process _this = Process.GetCurrentProcess();
            ProcessModule mod = _this.MainModule;

            hd = HookFunc;
            kh = _kh;

            //13 is the parameter specifying that we're gonna do a low-level keyboard hook
            hhk = User32.API.SetWindowsHookEx(13, hd, User32.API.GetModuleHandle(mod.ModuleName), 0);

            //MessageBox.Show(Marshal.GetLastWin32Error().ToString()); //for debugging
            //Note that this could be a Console.WriteLine(), as well. I just happened
            //to be debugging this in a Windows Application
        }

        public static bool DestroyHook()
        {
            //to be called when we're done with the hook

            return User32.API.UnhookWindowsHookEx(hhk);
        }

        //called when key is active
        private static IntPtr HookFunc(
            int nCode,
            IntPtr wParam,
            IntPtr lParam)
        {
            int iwParam = wParam.ToInt32();
            //depending on what you want to detect you can either detect keypressed or keyrealased also with  a bit tweaking keyclicked.
            if (nCode >= 0 &&
                (iwParam == 0x100 ||
                iwParam == 0x104)) //0x100 = WM_KEYDOWN, 0x104 = WM_SYSKEYDOWN
                kh(wParam, lParam);
            return User32.API.CallNextHookEx(hhk, nCode, wParam, lParam);
            }

        private static void KeyReaderr(IntPtr wParam, IntPtr lParam)
        {
            int key = Marshal.ReadInt32(lParam);

            Macro.VK vk = (Macro.VK)key;


            String temp = "";

            #region

            switch (vk)
            {
                case Macro.VK.VK_F1:
                    //temp = "&lt;-F1-&gt;";
                    temp = "{F1}";
                    break;
                case Macro.VK.VK_F2:
                    //temp = "&lt;-F2-&gt;";
                    temp = "{F2}";
                    break;
                case Macro.VK.VK_F3:
                    //temp = "&lt;-F3-&gt;";
                    temp = "{F3}";
                    break;
                case Macro.VK.VK_F4:
                    //temp = "&lt;-F4-&gt;";
                    temp = "{F4}";
                    break;
                case Macro.VK.VK_F5:
                    //temp = "&lt;-F5-&gt;";
                    temp = "{F5}";
                    break;
                case Macro.VK.VK_F6:
                    //temp = "&lt;-F6-&gt;";
                    temp = "{F6}";
                    break;
                case Macro.VK.VK_F7:
                    //temp = "&lt;-F7-&gt;";
                    temp = "{F7}";
                    break;
                case Macro.VK.VK_F8:
                    //temp = "&lt;-F8-&gt;";
                    temp = "{F8}";
                    break;
                case Macro.VK.VK_F9:
                    //temp = "&lt;-F9-&gt;";
                    temp = "{F9}";
                    break;
                case Macro.VK.VK_F10:
                    //temp = "&lt;-F10-&gt;";
                    temp = "{F10}";
                    break;
                case Macro.VK.VK_F11:
                    //temp = "&lt;-F11-&gt;";
                    temp = "{F11}";
                    break;
                case Macro.VK.VK_F12:
                    //temp = "&lt;-F12-&gt;";
                    temp = "{F12}";
                    break;
                case Macro.VK.VK_NUMLOCK:
                    //temp = "&lt;-numlock-&gt;";
                    temp = "{NUMLOCK}";
                    break;
                case Macro.VK.VK_SCROLL:
                    //temp = "&lt;-scroll&gt;";
                    temp = "{SCROLLLOCK}";
                    break;
                case Macro.VK.VK_LSHIFT:
                    //temp = "&lt;-left shift-&gt;";
                    temp = "{+}";
                    break;
                case Macro.VK.VK_RSHIFT:
                    //temp = "&lt;-right shift-&gt;";
                    temp = "{+}";
                    break;
                case Macro.VK.VK_LCONTROL:
                    //temp = "&lt;-left control-&gt;";
                    temp = "{CTRL}";
                    break;
                case Macro.VK.VK_RCONTROL:
                    //temp = "&lt;-right control-&gt;";
                    temp = "{CTRL}";
                    break;
                case Macro.VK.VK_SEPERATOR:
                    temp = "|";
                    break;
                case Macro.VK.VK_SUBTRACT:
                    temp = "-";
                    break;
                case Macro.VK.VK_DECIMAL:
                    temp = ".";
                    break;
                case Macro.VK.VK_DIVIDE:
                    temp = "/";
                    break;
                case Macro.VK.VK_NUMPAD0:
                    temp = "0";
                    break;
                case Macro.VK.VK_NUMPAD1:
                    temp = "1";
                    break;
                case Macro.VK.VK_NUMPAD2:
                    temp = "2";
                    break;
                case Macro.VK.VK_NUMPAD3:
                    temp = "3";
                    break;
                case Macro.VK.VK_NUMPAD4:
                    temp = "4";
                    break;
                case Macro.VK.VK_NUMPAD5:
                    temp = "5";
                    break;
                case Macro.VK.VK_NUMPAD6:
                    temp = "6";
                    break;
                case Macro.VK.VK_NUMPAD7:
                    temp = "7";
                    break;
                case Macro.VK.VK_NUMPAD8:
                    temp = "8";
                    break;
                case Macro.VK.VK_NUMPAD9:
                    temp = "9";
                    break;
                case Macro.VK.VK_Q:
                    temp = "q";
                    break;
                case Macro.VK.VK_W:
                    temp = "w";
                    break;
                case Macro.VK.VK_E:
                    temp = "e";
                    break;
                case Macro.VK.VK_R:
                    temp = "r";
                    break;
                case Macro.VK.VK_T:
                    temp = "t";
                    break;
                case Macro.VK.VK_Y:
                    temp = "y";
                    break;
                case Macro.VK.VK_U:
                    temp = "u";
                    break;
                case Macro.VK.VK_I:
                    temp = "i";
                    break;
                case Macro.VK.VK_O:
                    temp = "o";
                    break;
                case Macro.VK.VK_P:
                    temp = "p";
                    break;
                case Macro.VK.VK_A:
                    temp = "a";
                    break;
                case Macro.VK.VK_S:
                    temp = "s";
                    break;
                case Macro.VK.VK_D:
                    temp = "d";
                    break;
                case Macro.VK.VK_F:
                    temp = "f";
                    break;
                case Macro.VK.VK_G:
                    temp = "g";
                    break;
                case Macro.VK.VK_H:
                    temp = "h";
                    break;
                case Macro.VK.VK_J:
                    temp = "j";
                    break;
                case Macro.VK.VK_K:
                    temp = "k";
                    break;
                case Macro.VK.VK_L:
                    temp = "l";
                    break;
                case Macro.VK.VK_Z:
                    temp = "z";
                    break;
                case Macro.VK.VK_X:
                    temp = "x";
                    break;
                case Macro.VK.VK_C:
                    temp = "c";
                    break;
                case Macro.VK.VK_V:
                    temp = "v";
                    break;
                case Macro.VK.VK_B:
                    temp = "b";
                    break;
                case Macro.VK.VK_N:
                    temp = "n";
                    break;
                case Macro.VK.VK_M:
                    temp = "m";
                    break;
                case Macro.VK.VK_0:
                    temp = "0";
                    break;
                case Macro.VK.VK_1:
                    temp = "1";
                    break;
                case Macro.VK.VK_2:
                    temp = "2";
                    break;
                case Macro.VK.VK_3:
                    temp = "3";
                    break;
                case Macro.VK.VK_4:
                    temp = "4";
                    break;
                case Macro.VK.VK_5:
                    temp = "5";
                    break;
                case Macro.VK.VK_6:
                    temp = "6";
                    break;
                case Macro.VK.VK_7:
                    temp = "7";
                    break;
                case Macro.VK.VK_8:
                    temp = "8";
                    break;
                case Macro.VK.VK_9:
                    temp = "9";
                    break;
                case Macro.VK.VK_SNAPSHOT:
                    //temp = "&lt;-print screen-&gt;";
                    temp = "{PRTSC}";
                    break;
                case Macro.VK.VK_INSERT:
                    //temp = "&lt;-insert-&gt;";
                    temp = "{INSERT}";
                    break;
                case Macro.VK.VK_DELETE:
                    //temp = "&lt;-delete-&gt;";
                    temp = "{DELETE}";
                    break;
                case Macro.VK.VK_BACK:
                    //temp = "&lt;-backspace-&gt;";
                    temp = "{BACKSPACE}";
                    break;
                case Macro.VK.VK_TAB:
                    //temp = "&lt;-tab-&gt;";
                    temp = "{TAB}";
                    break;
                case Macro.VK.VK_RETURN:
                    //temp = "&lt;-enter-&gt;" + Environment.NewLine;
                    temp = "{ENTER}";
                    break;
                case Macro.VK.VK_PAUSE:
                    //temp = "&lt;-pause-&gt;";
                    temp = "{PAUSE}";
                    break;
                case Macro.VK.VK_CAPITAL:
                    //temp = "&lt;-caps lock-&gt;";
                    temp = "{CAPSLOCK}";
                    break;
                case Macro.VK.VK_ESCAPE:
                    //temp = "&lt;-esc-&gt;";
                    temp = "{ESC}";
                    break;
                case Macro.VK.VK_SPACE:
                    //temp = "&lt;-space-&gt;";
                    temp = "{SPACE}";
                    break;
                case Macro.VK.VK_PRIOR:
                    //temp = "&lt;-page up-&gt;";
                    temp = "{PGUP}";
                    break;
                case Macro.VK.VK_NEXT:
                    //temp = "&lt;-page down-&gt;";
                    temp = "{PGDN}";
                    break;
                case Macro.VK.VK_END:
                    //temp = "&lt;-end-&gt;";
                    temp = "{END}";
                    break;
                case Macro.VK.VK_HOME:
                    //temp = "&lt;-home-&gt;";
                    temp = "{HOME}";
                    break;
                case Macro.VK.VK_LEFT:
                    //temp = "&lt;-arrow left-&gt;";
                    temp = "{LEFT}";
                    break;
                case Macro.VK.VK_UP:
                    //temp = "&lt;-arrow up-&gt;";
                    temp = "{UP}";
                    break;
                case Macro.VK.VK_RIGHT:
                    //temp = "&lt;-arrow right-&gt;";
                    temp = "{RIGHT}";
                    break;
                case Macro.VK.VK_DOWN:
                    //temp = "&lt;-arrow down-&gt;";
                    temp = "{DOWN}";
                    break;
                default: break;
            }

            #endregion

            key1 = vk + "";
            key2 = temp + "";


            counter();
            /*
            if (form.recordFlag == true)
            {
                list.Add(key2);
            }*/
            send((Keys)key);

        }

        private static void counter()
        {
        }














        private static void send(Keys tempKey)//Keys tempKey, IntPtr wParam, IntPtr lParam
        {
            //MessageBox.Show(Control.ModifierKeys + "");
            //MessageBox.Show(tempKey.ToString().ToUpper() + "");

            if ((Control.ModifierKeys + "").Contains("Control"))
            {
                //Paste Select Option이 활성화 된 경우, 클립보드를 공백으로 만듦.
                //form.textBox17.Text = tempKey.ToString().ToUpper();
                if (tempKey.ToString().ToUpper() == "C" || tempKey.ToString().ToUpper() == "X") 
                { 
                    Macro.getInstance.Flag1 = true; 
                }
                else if (tempKey.ToString().ToUpper() == "V") {
                    Macro.getInstance.Flag2 = true; GetItemList();
                    clipBoardMonitorFlag = false;
                    if (ModelFunc3.getInstance.IsChecked03 == true && ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(2) && IsRunning == false)
                    {
                        setClipBoardText("");
                    }
                }
                else if (tempKey.ToString().ToUpper() == "D3") { Macro.getInstance.Flag3 = true; }
                else if (tempKey.ToString().ToUpper() == "D4") { Macro.getInstance.Flag4 = true; }
                else if (tempKey.ToString().ToUpper() == "D5") { Macro.getInstance.Flag5 = true; }
                else
                {

                }
            }

            if ((tempKey.ToString().ToUpper() + "").Contains("LWIN"))
            {
                if ((tempKey.ToString().ToUpper() + "").Contains("LSHIFTKEY"))
                {
                    if (tempKey.ToString().ToUpper() == "S") 
                    {
                        //Macro.getInstance.Flag1 = true; 
                    }
                }
            }

            key1 = "";
            key2 = "";

        }

        

        private static void getWindow()
        {
            IntPtr zero = IntPtr.Zero;
            IntPtr curWindow = User32.API.GetForegroundWindow();


            /*
            for (int i = 0; (i < 60) && (zero == IntPtr.Zero); i++)
            {
                Thread.Sleep(500);
                zero = API.FindWindow(null, "YourWindowName");
            }
            */


            if (curWindow != null)
            {
                User32.API.SetForegroundWindow(curWindow);
                SendKeys.SendWait("{A 10}");
                //SendKeys.Flush();
            }
        }


        public String getClipBoardText()
        {

            String res = "";
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        IDataObject idat = Clipboard.GetDataObject();
                        //MessageBox.Show(idat.GetFormats(). + "");
                        if (Clipboard.ContainsText()) //Clipboard.ContainsText(TextDataFormat.Text)
                        {
                            res = Clipboard.GetText();
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }




        //fff
        public DataObjectClass getClipBoardIData()
        {

            Exception threadEx = null;
            DataObject dataObject = null;
            IDataObject idataObject;
            string curFormat = "";
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if (getClipBoardDataType() == "Text") { curFormat = DataFormats.Text; }
                        else if (getClipBoardDataType() == "Image") { curFormat = DataFormats.Bitmap; }
                        else { }

                        dataObject = new DataObject();
                        idataObject = Clipboard.GetDataObject();
                        object data = idataObject.GetData(curFormat);
                        if (data != null) { dataObject.SetData(curFormat, data); }

                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            try
            {
                //MessageBox.Show(((IDataObject)dataObject).GetData(DataFormats.Text) + "!!\n" + dataObject);
            }
            catch(Exception e)
            {
                MessageBox.Show("Error \n\n\n" + e);
            }
            DataObjectClass tempDataObjectClass = new DataObjectClass();
            tempDataObjectClass.dataObject = dataObject;
            tempDataObjectClass.type = curFormat;
            return tempDataObjectClass;
        }



        public Bitmap getClipBoardImage()
        {

            Bitmap res = null;
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        IDataObject idat = Clipboard.GetDataObject();
                        string tempStr = idat.GetFormats() + "";
                        //MessageBox.Show(tempStr, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        if (Clipboard.ContainsImage())
                        {
                            res = (Bitmap)Clipboard.GetImage();
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }

        public Bitmap getClipBoardObject()
        {

            Bitmap res = null;
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        IDataObject idat = Clipboard.GetDataObject();
                        string tempStr = idat.GetFormats() + "";
                        string tempStr2 = idat.GetFormats().GetType() + "";
                        if (idat.GetDataPresent(DataFormats.Bitmap))
                        {

                        }
                        //MessageBox.Show(tempStr1, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        //MessageBox.Show(tempStr2, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        if (Clipboard.ContainsImage())
                        {
                            Bitmap x;
                            x = (Bitmap)idat.GetData(DataFormats.Bitmap, true);
                            x.SetResolution(x.HorizontalResolution, x.VerticalResolution);
                            x = cropAtRect(x, new Rectangle(0, 0, x.Width, x.Height));
                            //img.MakeTransparent();
                            //img = GetResizeImage(img, 1000, 1000);
                            res = x.Clone(new Rectangle(0, 0, x.Width, x.Height), x.PixelFormat);
                            //res = img;
                            string path = System.IO.Path.GetFullPath(@"clipimage\ccc.png");
                            x.Save(path,System.Drawing.Imaging.ImageFormat.Png);
                            setClipBoardImage(res);
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }


        public Bitmap cropAtRect(Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                return nb;
            }
        }


        public void saveFile(Image img)
        {
            //string path = 
        }


        public String getClipBoardDataType()
        {

            String res = "";
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if (Clipboard.ContainsText()) //Clipboard.ContainsText(TextDataFormat.Text)
                        {
                            res = "Text";
                        }
                        else if (Clipboard.ContainsImage() == true)
                        {
                            res = "Image";
                        }
                        else
                        {
                            res = "None";
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }

        public static void setClipBoardText(object obj)
        {
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if ((string)obj == "") {
                            Clipboard.Clear();
                        }
                        else 
                        {
                            Clipboard.SetText(obj + "");
                        }
                    }
                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        public static void setClipBoardImage(Bitmap obj)
        {
            Exception threadEx = null;
            //MessageBox.Show(obj.GetFormats()+"@");
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        Clipboard.Clear();
                        if (obj == null)
                        {
                        }
                        else
                        {
                            //Clipboard.SetImage((Bitmap)obj.GetData(DataFormats., true));
                            Clipboard.SetImage((Bitmap)obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }


        public String[] SplitStrByDoubleEnter(String str)
        {
            return str.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
        }

        public static int NthIndexOf(string input, String charToFind, int n)
        {
            int position;

            switch (Math.Sign(n))
            {
                case 1:
                    position = 0;
                    while (((position = input.IndexOf(charToFind, position)) != -1) && ((--n) > 0)) { position++; }
                    break;
                case -1:
                    position = input.Length - 1;
                    while (((position = input.LastIndexOf(charToFind, position)) != -1) && ((++n) < 0)) { position--; }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(message: "param cannot be equal to 0", paramName: nameof(n));
            }

            return position;
        }

        public static Point getMousePosAndColor()
        {
            Point p = GetCursorPosition();
            //form.textBox8.Text = p.X+"";
            //form.textBox9.Text = p.Y+"";
            GetColorAt(p.X, p.Y);
            return p;

        }
        public static Point GetCursorPosition()
        {
            User32.API.POINT lpPoint;
            User32.API.GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        public static void create()
        {
            Macro.CreateHook(KeyReaderr);
        }

        public static void destroy()
        {
            Macro.DestroyHook();
        }

        //sss
        public async void start()
        {
            //await System.Threading.Tasks.Task.Run(() => run());
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            await Task.Run(() =>
            {
                // Were we already canceled?
                ct.ThrowIfCancellationRequested();
                
                bool moreToDo = true;
                while (moreToDo)
                {
                    //ClipboardDetect();
                    sendKeyInput(tokenSource2);
                    //getMousePosAndColor();
                    Task.Delay(1);
                    
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }
                }
            }, tokenSource2.Token); // Pass same token to Task.Run.

            tokenSource2.Cancel();

            tokenSource2.Dispose();
        }



        public void reset()
        {
            //form.recordFlag = false;
            list = new List<string>();
        }

        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = User32.API.GetDesktopWindow();
            IntPtr dc = User32.API.GetWindowDC(desk);
            int a = (int)User32.API.GetPixel(dc, x, y);
            User32.API.ReleaseDC(desk, dc);

            String r = ((a >> 0) & 0xff) + "";
            String g = ((a >> 8) & 0xff) + "";
            String b = ((a >> 16) & 0xff) + "";

            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public static Color GetColorAt2(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = User32.API.BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return screenPixel.GetPixel(0, 0);
        }
        public static void isRGB()
        {
            //Point cursor = new Point();
            //API.GetCursorPos(ref cursor);

            User32.API.POINT cursor;
            User32.API.GetCursorPos(out cursor);

            var c = GetColorAt2(cursor);
            //form.BackColor = c;
        }


        //ddd
        public void sendKeyInput(CancellationTokenSource ct)
        {
            //form.textBox1.Text = key1;
            //form.textBox2.Text = key2;

            //form.textBox6.Text = (Int32.Parse(form.textBox6.Text) + 1) + "";

            if ( (key1 == "" && key2 == "") && ((Control.ModifierKeys + "").Contains("None") || (Control.ModifierKeys + "").Contains("Control")))
            {
                
                if (Macro.getInstance.Flag1 && (modeOn1 == true || modeOn2 == true || modeOn3 == true || modeOn4 == true) && IsRunning == false && doublePasteFlag)
                {
                    if (getClipBoardDataType() == "Text" || getClipBoardDataType() == "Image")
                    {
                        try
                        {
                            IsRunning = true;
                            removeBlankFlag = false; // 차트 형식 내 변환 확인. flag가 false상태로 유지 될 경우 빈칸을 "_"으로 변환함.
                            string modifiedText = "";
                            string clipboardText = "";
                            clipboardText = "" + getClipBoardText();
                            DataObjectClass tempIData = getClipBoardIData();
                            //setClipBoardImage((Bitmap)(tempIData.dataObject).GetData(DataFormats.Bitmap));
                            //string str = tempIData.GetData(DataFormats.Text)+"";
                            //Correct
                            if (ModelFunc1.getInstance.IsChecked01 == true)
                            {
                                modifiedText = correctString.getModifiedText(clipboardText);
                                clipboardText = correctString.getModifiedText(clipboardText);
                                if (clipboardText != "")
                                {
                                    setClipBoardText(modifiedText);
                                }
                            }
                            //Find and Alert
                            if (ModelFunc2.getInstance.IsChecked02 == true)
                            {
                                findAndAlert.FindStringAndAlert(clipboardText);
                            }
                            //Multi Clipboard
                            if (ModelFunc3.getInstance.IsChecked03 == true)
                            {
                                multiClipboard.SetItem(tempIData);/////////
                            }
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show(e+"");
                        }
                        finally
                        {
                            //SendKeys.SendWait(form.textBox1.Text);
                            IsRunning = false;
                            Macro.getInstance.Flag1 = false;
                        }
                    }else if(getClipBoardDataType() == "Image")
                    {
                        try
                        {
                            IsRunning = true;
                            removeBlankFlag = false; // 차트 형식 내 변환 확인. flag가 false상태로 유지 될 경우 빈칸을 "_"으로 변환함.
                            Bitmap clipboardImage = null;
                            //clipboardImage = getClipBoardImage();
                            clipboardImage = getClipBoardObject(); // Bitmap, Image로 저장 시 화질 저하됨. 때문에 object로 저장.

                            //setClipBoardImage(clipboardImage);

                            if (ModelFunc3.getInstance.IsChecked03 == true)
                            {
                                multiClipboard.SetItem(clipboardImage);/////////
                            }
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show(e+"");
                        }
                        finally
                        {
                            IsRunning = false;
                            Macro.getInstance.Flag1 = false;
                        }
                    }
                }
                else if (Macro.getInstance.Flag2 && modeOn3 == true && IsRunning == false && ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(2))
                {
                    IsRunning = true;
                    try
                    {
                        
                        if (ModelFunc3.getInstance.IsChecked03 == true)
                        {
                            DataObjectClass obj = multiClipboard.GetMapItem();  /////////////////// 창이 안뜸
                            if (obj != null)
                            {
                                
                                //MessageBox.Show(obj.type+"+++");
                                if (obj.type == "Text")
                                {
                                    setClipBoardText(obj.dataObject.GetData(DataFormats.Text));
                                }
                                else if (obj.type == "Bitmap")
                                {
                                    setClipBoardImage((Bitmap)(obj.dataObject).GetData(DataFormats.Bitmap));
                                    //setClipBoardImage((Bitmap)obj);
                                }
                                else if (obj.type == "Something")
                                {
                                    MessageBox.Show("Non Type1");
                                }
                                else
                                {
                                    MessageBox.Show("Non Type2");
                                }
                            }
                            if (ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(2))
                            {
                                pasteSelectedObject = getClipBoardText(); // 출력용, 의미 없음.
                                PasteSelectedString();

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e+"");
                    }
                    finally
                    {
                        //form.textBox1.Text = clipboardText;
                        //form.textBox2.Text = modifiedText;
                        //SendKeys.SendWait(form.textBox1.Text);
                        IsRunning = false;
                        Macro.getInstance.Flag2 = false;
                    }
                }
                else if (Macro.getInstance.Flag3)
                {
                    Macro.getInstance.Flag3 = false;
                }
                else if (Macro.getInstance.Flag4)
                {
                    Macro.getInstance.Flag4 = false;
                }
                else if (Macro.getInstance.Flag5)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Thread.Sleep(8);
                        SendKeys.SendWait(list[i]);
                    }
                    Macro.getInstance.Flag5 = false;
                }

                Macro.getInstance.Flag1 = false;
                Macro.getInstance.Flag2 = false;
                Macro.getInstance.Flag3 = false;
                Macro.getInstance.Flag4 = false;
                Macro.getInstance.Flag5 = false;
            }
        }

        private static void GetItemList()
        {
            if (ModelFunc3.getInstance.IsChecked03 == true && ModelFunc3.getInstance.TheSelectedRule != ModelFunc3.getInstance.Source.ElementAt(2) && doublePasteFlag == true)
            {
                object obj = "";
                try
                {
                    obj = MultiClipboard.GetItem();
                    if (obj != null)
                    {
                        setClipBoardText(obj);
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e+"");
                }
                finally
                {
                    DispatcherService.Invoke((System.Action)(() =>
                    {
                    FuncWindow3.getInstance.SetListView();
                    }));
                    Thread.Sleep(1);
                }
            }
            if(doublePasteFlag == false)
            {
                doublePasteFlag = true;
            }
        }

        private static void PasteSelectedString()
        {
            doublePasteFlag = false;
            //SendKeys.Send("^{v}");
            //SendKeys.Send("{a}");
            //SendKeys.SendWait("{CTRL}");
            //SendKeys.SendWait("^");
            string gg = pasteSelectedObject +"";
            string gg2 = pasteSelectedObject +"";
            //SendKeys.SendWait("^a");
            //SendKeys.SendWait(tempStr);
            SendKeys.SendWait("^v");
        }






    }

    public class DataObjectClass
    {
        public DataObject dataObject;
        public string type;
    }
}
