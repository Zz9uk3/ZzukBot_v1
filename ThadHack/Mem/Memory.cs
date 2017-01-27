using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Binarysharp.Assemblers.Fasm;
using GreyMagic;
using ZzukBot.AntiWarden;
using ZzukBot.Constants;
using ZzukBot.Hooks;
using static ZzukBot.Constants.Offsets;

namespace ZzukBot.Mem
{
    internal static class Memory
    {
        private static InProcessMemoryReader _Reader;
        private static FasmNet Asm;
        private static bool Applied;

        /// <summary>
        ///     Memory Reader Instance
        /// </summary>
        internal static InProcessMemoryReader Reader => _Reader ?? (_Reader = new InProcessMemoryReader(Process.GetCurrentProcess()));

        /// <summary>
        ///     Initialise InternalMemoryReader
        /// </summary>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void Init()
        {
            if (Applied) return;
            // Init Warden Module32First/Next hook
            //MessageBox.Show("TEST");

            //var snapshot = WinImports.CreateToolhelp32Snapshot(0x00000010 | 0x00000008, (uint) Reader.Process.Id);
            //var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //var entry = new WinImports.MODULEENTRY32() { dwSize = 548 };
            //if (WinImports.Module32First(snapshot, ref entry))
            //{
            //    entry = new WinImports.MODULEENTRY32() { dwSize = 548 };
            //    while (WinImports.Module32Next(snapshot, ref entry))
            //    {
            //        if (entry.szExePath.Contains(basePath))
            //            Reader.WriteBytes(entry.modBaseAddr, new byte[] {0, 0, 0, 0});
            //    }
            //}

            HookModule32.Init();

            Libs.Clear();
            Libs.InjectFastcall();
            Libs.ReloadNav();

            ErrorEnumHook.Init();
            ChatHook.Init();
            GlobalHooks.Init();
            //EnterWorldHook.Init();
            //EnterWorldCompleteHook.Init();
            WindowProcHook.Init();
            HookWardenMemScan.SetupDetour();

            // Init DirectX
            DirectX.Init();
            // Init the object manager
            ObjectManager.Init();

            // Apply no collision hack with trees
            var DisableCollision = new Hack(Hacks.DisableCollision, new byte[] {0x0F, 0x85, 0x1B, 0x01, 0x00, 0x00},
                "Collision");
            HookWardenMemScan.AddHack(DisableCollision);
            //DisableCollision.Apply();
            // Ctm Patch
            var CtmPatch = new Hack(Hacks.CtmPatch,
                new byte[] {0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90}, "Ctm");
            HookWardenMemScan.AddHack(CtmPatch);
            //CtmPatch.Apply();
            // wallclimb hack yay :)
            //float wc = 0.5f;
            //Hack Wallclimb = new Hack(Hacks.Wallclimb, BitConverter.GetBytes(wc), "Wallclimb");
            //HookWardenMemScan.AddHack(Wallclimb);
            //Wallclimb.Apply();

            var Collision3 = new Hack(Hacks.Collision3, new byte[] {0xEB, 0x69}, "Collision3");
            HookWardenMemScan.AddHack(Collision3);

            // Loot patch
            var LootPatch = new Hack(Hacks.LootPatch, new byte[] {0xEB}, "LootPatch");
            HookWardenMemScan.AddHack(LootPatch);
            LootPatch.Apply();

            // Ctm Hide
            var CtmHide = new Hack(Player.CtmState, new byte[] {0x0, 0x0, 0x0, 0x0}, new byte[] {0x0C, 0x00, 0x00, 0x00},
                "CtmHideHack") {DynamicHide = true};
            HookWardenMemScan.AddHack(CtmHide);

            var CtmHideX = new Hack(Player.CtmX, new byte[] {0x0, 0x0, 0x0, 0x0}, new byte[] {0x00, 0x00, 0x00, 0x00},
                "CtmHideHackX") {DynamicHide = true};
            HookWardenMemScan.AddHack(CtmHideX);

            var CtmHideY = new Hack(Player.CtmY, new byte[] {0x0, 0x0, 0x0, 0x0}, new byte[] {0x00, 0x00, 0x00, 0x00},
                "CtmHideHackY") {DynamicHide = true};
            HookWardenMemScan.AddHack(CtmHideY);

            var CtmHideZ = new Hack(Player.CtmZ, new byte[] {0x0, 0x0, 0x0, 0x0}, new byte[] {0x00, 0x00, 0x00, 0x00},
                "CtmHideHackZ") {DynamicHide = true};
            HookWardenMemScan.AddHack(CtmHideZ);


            // Lua Unlock
            var LuaUnlock = new Hack(Hacks.LuaUnlock, new byte[] {0xB8, 0x01, 0x00, 0x00, 0x00, 0xc3}, "LuaUnlock");
            HookWardenMemScan.AddHack(LuaUnlock);
            LuaUnlock.Apply();

#if !DEBUG
            Hack DisableErrorSpam = new Hack((IntPtr)0x00496810, new byte[] { 0xB9, 0x04, 0x00, 0x00, 0x00, 0x90 }, "ErrorSpamDisable");
            HookWardenMemScan.AddHack(DisableErrorSpam);
            DisableErrorSpam.Apply();
#endif
            Applied = true;
        }

        internal static Hack GetHack(string parName)
        {
            return HookWardenMemScan.GetHack(parName);
        }

        //internal static void InjectAsm(uint parPtr, string[] parInstructions, string parPatchName = "")
        //{
        //    if (Asm == null) Asm = new FasmNet();

        //    Asm.Clear();
        //    IntPtr start = new IntPtr(parPtr);
        //    Asm.AddLine("use32");
        //    foreach (string x in parInstructions)
        //    {
        //        Asm.AddLine(x);
        //    }

        //    byte[] byteCode = new byte[0];
        //    try
        //    {
        //        byteCode = Asm.Assemble(start);
        //    }
        //    catch (FasmAssemblerException ex)
        //    {
        //        MessageBox.Show(String.Format("Error definition: {0}; Error code: {1}; Error line: {2}; Error offset: {3}; Mnemonics: {4}",
        //            ex.ErrorCode, (int)ex.ErrorCode, ex.ErrorLine, ex.ErrorOffset, ex.Mnemonics));
        //    }
        //    byte[] originalBytes = Memory.Reader.ReadBytes((IntPtr)start, byteCode.Length);
        //    Memory.Reader.WriteBytes(start, byteCode);

        //    if (parPatchName != "")
        //    {
        //        Hack parHack = new Hack(start,
        //            byteCode,
        //            originalBytes, parPatchName);
        //        HookWardenMemScan.AddHack(parHack);
        //    }
        //}

        //[Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static IntPtr InjectAsm(string[] parInstructions, string parPatchName)
        {
            if (Asm == null) Asm = new FasmNet();

            Asm.Clear();
            Asm.AddLine("use32");
            foreach (var x in parInstructions)
            {
                Asm.AddLine(x);
            }

            var byteCode = new byte[0];
            try
            {
                byteCode = Asm.Assemble();
            }
            catch (FasmAssemblerException ex)
            {
                MessageBox.Show(
                    $"Error definition: {ex.ErrorCode}; Error code: {(int) ex.ErrorCode}; Error line: {ex.ErrorLine}; Error offset: {ex.ErrorOffset}; Mnemonics: {ex.Mnemonics}");
            }

            var start = Reader.Alloc(byteCode.Length);
            Asm.Clear();
            Asm.AddLine("use32");
            foreach (var x in parInstructions)
            {
                Asm.AddLine(x);
            }
            byteCode = Asm.Assemble(start);

            HookWardenMemScan.RemoveHack(start);
            HookWardenMemScan.RemoveHack(parPatchName);
            var originalBytes = Reader.ReadBytes(start, byteCode.Length);
            if (parPatchName != "")
            {
                var parHack = new Hack(start,
                    byteCode,
                    originalBytes, parPatchName);
                HookWardenMemScan.AddHack(parHack);
                parHack.Apply();
            }
            else
            {
                Reader.WriteBytes(start, byteCode);
            }
            return start;
        }

        //[Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void InjectAsm(uint parPtr, string parInstructions, string parPatchName)
        {
            Asm.Clear();
            Asm.AddLine("use32");
            Asm.AddLine(parInstructions);
            var start = new IntPtr(parPtr);

            byte[] byteCode;
            try
            {
                byteCode = Asm.Assemble(start);
            }
            catch (FasmAssemblerException ex)
            {
                MessageBox.Show(
                    $"Error definition: {ex.ErrorCode}; Error code: {(int)ex.ErrorCode}; Error line: {ex.ErrorLine}; Error offset: {ex.ErrorOffset}; Mnemonics: {ex.Mnemonics}");
                return;
            }

            HookWardenMemScan.RemoveHack(start);
            HookWardenMemScan.RemoveHack(parPatchName);
            var originalBytes = Reader.ReadBytes(start, byteCode.Length);
            if (parPatchName != "")
            {
                var parHack = new Hack(start,
                    byteCode,
                    originalBytes, parPatchName);
                HookWardenMemScan.AddHack(parHack);
                parHack.Apply();
            }
            else
            {
                Reader.WriteBytes(start, byteCode);
            }
        }

        internal static void InjectAsm(uint parPtr, string[] parInstructions, string parPatchName)
        {
            Asm.Clear();
            Asm.AddLine("use32");
            foreach (var item in parInstructions)
            {
                Asm.AddLine(item);
            }
            var start = new IntPtr(parPtr);

            byte[] byteCode;
            try
            {
                byteCode = Asm.Assemble(start);
            }
            catch (FasmAssemblerException ex)
            {
                MessageBox.Show(
                    $"Error definition: {ex.ErrorCode}; Error code: {(int)ex.ErrorCode}; Error line: {ex.ErrorLine}; Error offset: {ex.ErrorOffset}; Mnemonics: {ex.Mnemonics}");
                return;
            }

            HookWardenMemScan.RemoveHack(start);
            HookWardenMemScan.RemoveHack(parPatchName);
            var originalBytes = Reader.ReadBytes(start, byteCode.Length);
            if (parPatchName != "")
            {
                var parHack = new Hack(start,
                    byteCode,
                    originalBytes, parPatchName);
                HookWardenMemScan.AddHack(parHack);
                parHack.Apply();
            }
            else
            {
                Reader.WriteBytes(start, byteCode);
            }
        }
    }
}