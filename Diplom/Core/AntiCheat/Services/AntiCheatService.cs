using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Diplom.Core.AntiCheat.Services;

public class AntiCheatService
{
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    private static List<long> _keyTimestamps = new List<long>();
    private static Stopwatch _sw = Stopwatch.StartNew();

    private static int kol = 0;
    private static int maxViolations = 24;

    private static Thread _analysisThread;

    private static HiddenAntiCheatService _hiddenService;

    public static void Start(HiddenAntiCheatService hiddenService)
    {
        _hiddenService = hiddenService;
        _hookID = SetHook(_proc);

        _analysisThread = new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(2000);
                AnalyzeInput();
            }
        })
        {
            IsBackground = true
        };
        _analysisThread.Start();
    }

    public static void Stop()
    {
        UnhookWindowsHookEx(_hookID);
        _analysisThread?.Abort();
    }

    private static void AnalyzeInput()
    {
        if (_keyTimestamps.Count < 5) return;

        var intervals = new List<long>();
        for (int i = 1; i < _keyTimestamps.Count; i++)
            intervals.Add(_keyTimestamps[i] - _keyTimestamps[i - 1]);

        double avg = intervals.Average();
        double stddev = Math.Sqrt(intervals.Average(i => Math.Pow(i - avg, 2)));

        Console.WriteLine($"⏱ Интервалы: ср={avg:F1}мс, σ={stddev:F1}");

        if (stddev < 80 && avg < 250)
        {
            kol++;
            Console.WriteLine($"🚨 Обнаружен скриптовый ввод! Подозрений: {kol}/{maxViolations}");
        }
        else if (kol > 0)
        {
            kol--;
            Console.WriteLine($"✅ Подозрения снижены: {kol}/{maxViolations}");
        }

        if (kol == maxViolations / 2)
        {
            Console.WriteLine("⚠️ Античит заподозрил использование скрипта.");
            MessageBox.Show("⚠️ Античит заподозрил использование скрипта. Прекратите использование стороннего ПО, или нажмите на любую клавишу.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        if (kol >= maxViolations)
        {
            Console.WriteLine("⛔ Вы заблокированы за подозрительное поведение.");
            MessageBox.Show("Вы были заблокированы античитом", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Error);

           
            _hiddenService?.LogSuspiciousActivity();

            Application.Exit();
        }
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        var curProcess = Process.GetCurrentProcess();
        var curModule = curProcess.MainModule;
        IntPtr hook = SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        curProcess.Dispose();
        return hook;
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            long timestamp = _sw.ElapsedMilliseconds;
            _keyTimestamps.Add(timestamp);

            if (_keyTimestamps.Count > 100)
                _keyTimestamps.RemoveAt(0);
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}
