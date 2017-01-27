using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZzukBot.Mem;
using ZzukBot.Server;
using ZzukBot.Settings;
using Ptr = ZzukBot.Constants.Offsets;

namespace ZzukBot.AntiWarden
{
    //[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    internal static class HookWardenMemScan
    {
        /// <summary>
        ///     Delegate to our c# function we will jmp to
        /// </summary>
        private static WardenMemCpyDelegate _wardenMemCpyDelegate;

        /// <summary>
        ///     Delegate to our c# function we will jmp to
        /// </summary>
        private static WardenPageScanDelegate _wardenPageScanDelegate;

        /// <summary>
        ///     First 5 bytes of Wardens Memscan function
        /// </summary>
        private static readonly byte[] MemScanOriginalBytes = {0x56, 0x57, 0xFC, 0x8B, 0x54};
        private static readonly byte[] PageScanOriginalBytes = { 0x8B, 0x45, 0x08, 0x8A, 0x04 };

        /// <summary>
        ///     Is Wardens Memscan modified?
        /// </summary>
        private static IntPtr WardensMemScanFuncPtr = IntPtr.Zero;
        private static IntPtr WardensPageScanFuncPtr = IntPtr.Zero;

        private static IntPtr WardenMemCpyDetourPtr = IntPtr.Zero;
        private static IntPtr WardenPageScanDetourPtr = IntPtr.Zero;

        private static IntPtr AddrToWardenMemCpy = IntPtr.Zero;
        private static IntPtr AddrToWardenPageScan = IntPtr.Zero;

        /// <summary>
        ///     Delegate to our C# function
        /// </summary>
        private static ModifyWardenDetour _modifyWarden;

        /// <summary>
        ///     A private list to keep track of all hacks registered
        /// </summary>
        private static readonly List<Hack> Hacks = new List<Hack>();

        //internal static string HacksDebugOutput()
        //{
        //    string strFinal = "";

        //    foreach (Hack x in Hacks)
        //    {
        //        if (!x.RelativeToPlayerBase || (x.RelativeToPlayerBase && ObjectManager.EnumObjects()))
        //        {
        //            string str = "";
        //            str += x.address.ToString("X") + " - " + x.Name + "\nOriginal: ";
        //            foreach (byte b in x.MemScanOriginalBytes)
        //            {
        //                str += b.ToString("X2") + " ";
        //            }
        //            str += "\nCustom: ";
        //            foreach (byte b in x.CustomBytes)
        //            {
        //                str += b.ToString("X2") + " ";
        //            }
        //            str += "\n\n";
        //            strFinal += str;
        //        }
        //    }
        //    return strFinal;
        //}


#if DEBUG
        private static readonly List<IntPtr> UniqueScans = new List<IntPtr>();
#endif

        /// <summary>
        ///     Init the hack
        /// </summary>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void DisableWarden(IntPtr parWardenPtr1)
        {
            //var second = Memory.Reader.Read<IntPtr>(parWardenPtr1);
            var wardenModuleStart = (parWardenPtr1).ReadAs<IntPtr>();
            var memScanPtr = IntPtr.Add(wardenModuleStart, (int) Ptr.Warden.WardenMemScanStart);
            var pageScanPtr = IntPtr.Add(wardenModuleStart, (int) Ptr.Warden.WardenPageScan);

            Console.WriteLine(pageScanPtr.ToString("X"));

            if (pageScanPtr != WardensPageScanFuncPtr)
            {
                var CurrentBytes = Memory.Reader.ReadBytes(pageScanPtr, 5);
                //var CurrrentBytes = (tmpPtr).ReadAs<Byte>(); //How do I read 5 bytes?
                var isEqual = CurrentBytes.SequenceEqual(PageScanOriginalBytes);
                if (!isEqual) return;

                if (AddrToWardenPageScan == IntPtr.Zero)
                {
                    _wardenPageScanDelegate = WardenPageScanHook;
                    AddrToWardenPageScan = Marshal.GetFunctionPointerForDelegate(_wardenPageScanDelegate);
                    if (WardenPageScanDetourPtr == IntPtr.Zero)
                    {
                        // IntPtr readBase, int readOffset, IntPtr writeTo
                        string[] asmCode =
                        {
                            "mov eax, [ebp+8]", // read base
                            "pushfd",
                            "pushad",

                            "mov ecx, esi",
                            "add ecx, edi",
                            "add ecx, 0x1C",

                            "push ecx",
                            "push edi",
                            "push eax",
                            "call " + AddrToWardenPageScan,
                            "popad",
                            "popfd",
                            "inc edi",
                            "jmp " + (uint) (wardenModuleStart + 0x2B2C),
                        };
                        WardenPageScanDetourPtr = Memory.InjectAsm(asmCode, "WardenPageScanDetour");
                    }
                }

               Memory.InjectAsm((uint) pageScanPtr,
                    "jmp 0x" + WardenPageScanDetourPtr.ToString("X"),
                    "WardenPageScanJmp");
                WardensPageScanFuncPtr = pageScanPtr;
            }

            if (memScanPtr != WardensMemScanFuncPtr)
            {
                var CurrentBytes = Memory.Reader.ReadBytes(memScanPtr, 5);
                //var CurrrentBytes = (tmpPtr).ReadAs<Byte>(); //How do I read 5 bytes?
                var isEqual = CurrentBytes.SequenceEqual(MemScanOriginalBytes);
                if (!isEqual) return;

                if (AddrToWardenMemCpy == IntPtr.Zero)
                {
                    _wardenMemCpyDelegate = WardenMemCpyHook;
                    AddrToWardenMemCpy = Marshal.GetFunctionPointerForDelegate(_wardenMemCpyDelegate);

                    if (WardenMemCpyDetourPtr == IntPtr.Zero)
                    {
                        string[] asmCodeOnline =
                        {
                            DownloadedOffsets.WardenDetour[0],
                            DownloadedOffsets.WardenDetour[1],
                            DownloadedOffsets.WardenDetour[2],
                            DownloadedOffsets.WardenDetour[3],
                            DownloadedOffsets.WardenDetour[4],
                            DownloadedOffsets.WardenDetour[5],
                            DownloadedOffsets.WardenDetour[6],
                            DownloadedOffsets.WardenDetour[7],
                            DownloadedOffsets.WardenDetour[8],
                            DownloadedOffsets.WardenDetour[9],
                            DownloadedOffsets.WardenDetour[10],
                            DownloadedOffsets.WardenDetour[11],
                            DownloadedOffsets.WardenDetour[12],
                            DownloadedOffsets.WardenDetour[13] + " " + (uint) AddrToWardenMemCpy,
                            DownloadedOffsets.WardenDetour[14],
                            DownloadedOffsets.WardenDetour[15],
                            DownloadedOffsets.WardenDetour[16],
                            DownloadedOffsets.WardenDetour[17],
                            DownloadedOffsets.WardenDetour[18] + " " + (uint) (memScanPtr + 0x24)
                        };
                        WardenMemCpyDetourPtr = Memory.InjectAsm(asmCodeOnline, "WardenMemCpyDetour");
                    }
                }

                Memory.InjectAsm((uint) memScanPtr, "jmp 0x" + WardenMemCpyDetourPtr.ToString("X"), "WardenMemCpyJmp");
                WardensMemScanFuncPtr = memScanPtr;
            }
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void SetupDetour()
        {
            _modifyWarden = DisableWarden;
            // get PTR for our c# function
            var addrToDetour = Marshal.GetFunctionPointerForDelegate(_modifyWarden);
            string[] asmCode =
            {
                "MOV [0xCE8978], EAX",
                "pushfd",
                "pushad",
                "push EAX",
                "call " + (uint) addrToDetour,
                "popad",
                "popfd",
                "jmp 0x006CA233"
            };
            var WardenDetour = Memory.InjectAsm(asmCode, "WardenLoadDetour");
            Memory.InjectAsm(0x006CA22E, "jmp " + WardenDetour, "WardenLoadDetourJmp");
        }

        /// <summary>
        ///     add a hack to the list from the outside
        ///     hack contains: original bytes, bytes we inject, the address we inject to
        /// </summary>
        internal static void AddHack(Hack parHack)
        {
            if (Hacks.All(i => i.Address != parHack.Address))
            {
                RemoveHack(parHack.Name);
                Hacks.Add(parHack);
            }
        }

        internal static void RemoveHack(string parName)
        {
            var hack = Hacks.Where(i => i.Name == parName).ToList();
            foreach (var x in hack)
            {
                x.Remove();
            }
            Hacks.RemoveAll(i => i.Name == parName);
        }

        internal static void RemoveHack(IntPtr parAddress)
        {
            var hack = Hacks.Where(i => i.Address == parAddress).ToList();
            foreach (var x in hack)
            {
                x.Remove();
            }
            Hacks.RemoveAll(i => i.Address == parAddress);
        }

        internal static Hack GetHack(string parName)
        {
            return Hacks.FirstOrDefault(i => i.Name == parName);
        }

        internal static Hack GetHack(IntPtr parAddress)
        {
            return Hacks.FirstOrDefault(i => i.Address == parAddress);
        }


        private static void WardenPageScanHook(IntPtr readBase, int readOffset, IntPtr writeTo)
        {
            var readByteFrom = readBase + readOffset;

            var activeHacks = Hacks.Where(x => x.IsActivated && x.IsWithinScan(readByteFrom, 1)).ToList();
            activeHacks.ForEach(x =>
            {
                x.Remove();
                Console.WriteLine($@"[PageScan] Disabling {x.Name} at {x.Address.ToString("X")}");
            });
            var myByte = Memory.Reader.Read<Byte>(readByteFrom);
            Memory.Reader.Write(writeTo, myByte);

            activeHacks.ForEach(x => x.Apply());
        }

        /// <summary>
        ///     Will be called from our ASM stub
        ///     will check if the scanned addr range contains any registered hack
        ///     if yes: restore original byte for the hack
        ///     do the scan
        ///     restore back to the "hacked" state
        /// </summary>
        private static void WardenMemCpyHook(IntPtr addr, int size, IntPtr bufferStart)
        {
            if (size != 0)
            {
                // LINQ to get all affected hacks
                //                var match = Hacks
                //                    .Where(i => i.address.ToInt32() <= IntPtr.Add(addr, size).ToInt32()
                //                                && i.address.ToInt32() >= addr.ToInt32())
                //                    .ToList();
                //                // Remove the hacks
                //#if DEBUG
                //                var str = "Scan: 0x" + addr.ToString("X") + " Size: " + size;

                //#endif
                //                var ActiveHacks = new List<Hack>();
                //                foreach (var x in match)
                //                {
                //                    if (!x.IsActivated) continue;
                //                    ActiveHacks.Add(x);
                //                    x.Remove();
                //#if DEBUG
                //                    str += " Names: " + x.Name;
                //#endif
                //                }
                //#if DEBUG
                //                if (!UniqueScans.Contains(addr))
                //                {
                //                    File.AppendAllText(Paths.Root + "\\WardenScans.txt", str + Environment.NewLine);
                //                    UniqueScans.Add(addr);
                //                }
                //#endif
                var activeHacks = Hacks.Where(x => x.IsActivated && x.IsWithinScan(addr, size)).ToList();



                activeHacks.ForEach(x =>
                {
                    x.Remove();
                    Console.WriteLine($@"[MemScan] Disabling {x.Name} at {x.Address.ToString("X")}");
                });
                Memory.Reader.WriteBytes(bufferStart, Memory.Reader.ReadBytes(addr, size));
                activeHacks.ForEach(x => x.Apply());
                // reapply
                //ActiveHacks.ForEach(i => i.Apply());
            }
        }


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ModifyWardenDetour(IntPtr parWardenPtr);

        /// <summary>
        ///     Delegate for our c# function
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WardenMemCpyDelegate(IntPtr addr, int size, IntPtr bufferStart);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WardenPageScanDelegate(IntPtr readBase, int readOffset, IntPtr writeTo);
    }
}