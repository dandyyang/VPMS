using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

namespace Common.Util
{
    public class KeyboardUtil
    {
        IntPtr hookID = IntPtr.Zero;
        HookHandlerDelegate proc;
        private const int WH_KEYBOARD_LL = 13;
        private const int llkhf_altdown = 0x20; //test the context code
        private const short vk_tab = 0x9;
        private const short vk_control = 17;
        private const short vk_escape = 0x1B;
        private const short vk_win_left = 91;
        private const short vk_win_right = 92;
        private const int vk_F4 = 115;
        private const int vk_del = 46;

        private const int WM_KEYDOWN = 0x100;
        private const int WM_SYSKEYDOWN = 0x104;

        private static KeyboardUtil _instance;

        /// <summary>
        /// 屏蔽键盘事件
        /// ctrl+esc 
        /// alt+tab
        /// alt+esc 
        /// alt+F4    
        /// win
        /// </summary>
        public static void KeyMaskStart()
        {
            if (_instance == null)
            {
                _instance = new KeyboardUtil();
            }
            _instance.KeyboardHook();
        }




        /// <summary>
        /// 挂上键盘钩子
        /// </summary>
        private void KeyboardHook()
        {
            proc = new HookHandlerDelegate(HookCallback);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                hookID = SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                if (hookID == IntPtr.Zero) throw new System.Exception("没有足够权限安装钩子！");
            }
        }

        #region WINAPI
        /// <summary>
        /// 钩子函数代理
        /// </summary>
        private delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        //负责建立键盘钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

        //负责移去键盘钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hHook);

        //负责把击键信息传递到下一个监听键盘事件的应用程序
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hHook, int ncode, IntPtr wParam, IntPtr lParam);

        // 获取一个应用程序或动态链接库的模块句柄
        [DllImport("kernel32", EntryPoint = "GetModuleHandle", SetLastError = false, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        //函数功能:该函数判断在此函数被调用时,某个键是出于UP状态还是出于DOWN状态,及前次调用GetAsyncKeyState函数后,是否按过此键.如果返回值的最高位被置位,那么该键处于DOWN状态;如果最低位被置位,那么在前一次调用此函数后,此键被按过,否则表示该键没被按过. 
        [DllImport("user32.dll", EntryPoint = "GetAsyncKeyState", SetLastError = false, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetAsyncKeyState(int vKey);

        //在按过某键之后再调用，它返回最近的键盘消息从线程的队列中移出时的键盘状态，判断刚按过了某键。 
        [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = false, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int nVirtKey);
        #endregion


        private struct KeyboardMSG
        {
            public int vkcode; //a virtual-key code in the range 1 to 254
            int scancode;//hardware scan code for the key
            public int flags; //specifies the extended-key flag, event-injected flag, context code, and transition-state flag
            int time; //time stamp for this message
            int dwextrainfo;//extra info associated with the message
        }



        /// <summary>
        /// 钩子处理过程
        /// </summary>
        /// <param name="nCode">正常的键盘事件将返回一个大于或等于零的nCode值。</param>
        /// <param name="wParam">这个值指示发生了什么类型的事件：键被按下还是松开，以及是否按下的键是一个系统键（左边或右边的Alt键）。</param>
        /// <param name="lParam">这是一个存储精确击键信息的结构，例如被按键的代码。</param>
        /// <returns></returns>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            KeyboardMSG m = (KeyboardMSG)Marshal.PtrToStructure(lParam, typeof(KeyboardMSG));

            //仅为KeyDown事件过滤wParam，否则该代码将再次执行-对于每一次击键（也就是，相应于KeyDown和KeyUp）
            //WM_SYSKEYDOWN是捕获Alt相关组合键所必需的
            //if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)) return (IntPtr)1;
            IntPtr result = (IntPtr)1;
            if (nCode >= 0)
            {
                bool ctrldown = GetAsyncKeyState(vk_control) >> 15 == 1;
                bool altdown = (m.flags & llkhf_altdown) != 0;


                //ctrl+esc --------------
                if ((m.vkcode == vk_escape) && ctrldown) return result;

                //alt+tab --------------
                if ((m.vkcode == vk_tab) && altdown) return result;

                //alt+esc --------------
                if ((m.vkcode == vk_escape) && altdown) return result;

                //alt+F4 --------------
                if ((m.vkcode == vk_F4) && altdown) return result;

                //win
                if (m.vkcode == vk_win_left || m.vkcode == vk_win_right) return result;


            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// 项目启动时
        /// 禁用CTRL+ALT+DELETE
        /// </summary>   
        public static void StopSystemKeys()
        {
            try
            {
                RegistryKey r = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies", true);
                r.CreateSubKey("System");
                r.Close();
                RegistryKey s = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                s.SetValue("DisableTaskMgr", 1);
                s.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 项目关闭时
        /// 启用CTRL+ALT+DELETE
        /// </summary>
        public static void UseSystemKeys()
        {
            try
            {
                RegistryKey r = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                r.DeleteValue("DisableTaskMgr", true);
                r.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

