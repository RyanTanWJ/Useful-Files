using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class EnumTest : MonoBehaviour
{
    List<EnumList> enumLists;
    List<EnumBitMask> enumBitMasks;

    private void Start()
    {
        enumLists = new List<EnumList>(64);
        enumBitMasks = new List<EnumBitMask>(64);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            new EnumList(FlagEnum.A);
            new EnumBitMask(FlagEnum.B);
            Profiler.BeginSample("ListTest1");
            var el = ListTest1();
            Profiler.EndSample();
            enumLists.Add(el);

            Profiler.BeginSample("BitMaskTest1");
            var eBM = BitMaskTest1();
            Profiler.EndSample();
            enumBitMasks.Add(eBM);

            Profiler.BeginSample("ListTest2");
            el = ListTest2();
            Profiler.EndSample();
            enumLists.Add(el);

            Profiler.BeginSample("BitMaskTest2");
            eBM = BitMaskTest2();
            Profiler.EndSample();
            enumBitMasks.Add(eBM);

            Profiler.BeginSample("ListTest3");
            el = ListTest3();
            Profiler.EndSample();
            enumLists.Add(el);

            Profiler.BeginSample("BitMaskTest3");
            eBM = BitMaskTest3();
            Profiler.EndSample();
            enumBitMasks.Add(eBM);

            Profiler.BeginSample("BitMaskTest3Alt");
            BitMaskTest3Alt();
            Profiler.EndSample();

            Profiler.BeginSample("ListTest4");
            ListTest4();
            Profiler.EndSample();

            Profiler.BeginSample("BitMaskTest4");
            BitMaskTest4();
            Profiler.EndSample();

            Profiler.BeginSample("ListTest5");
            el = ListTest5();
            Profiler.EndSample();
            enumLists.Add(el);

            Profiler.BeginSample("BitMaskTest5");
            eBM = BitMaskTest5();
            Profiler.EndSample();
            enumBitMasks.Add(eBM);

            // Add additional List Objects
            enumLists.Add(new EnumList(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.N, FlagEnum.O, FlagEnum.P, FlagEnum.Q, FlagEnum.R,
                FlagEnum.S, FlagEnum.T, FlagEnum.U, FlagEnum.V, FlagEnum.W,
                FlagEnum.X, FlagEnum.Y, FlagEnum.Z, FlagEnum.A2, FlagEnum.B2,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            enumLists.Add(new EnumList(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.D, FlagEnum.E, FlagEnum.F, FlagEnum.G, FlagEnum.H,
                FlagEnum.I, FlagEnum.J, FlagEnum.K, FlagEnum.L, FlagEnum.M,
                FlagEnum.N, FlagEnum.O, FlagEnum.P, FlagEnum.Q, FlagEnum.R,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            enumLists.Add(new EnumList(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.D, FlagEnum.E, FlagEnum.F, FlagEnum.G, FlagEnum.H,
                FlagEnum.X, FlagEnum.Y, FlagEnum.Z, FlagEnum.A2, FlagEnum.B2,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            enumLists.Add(new EnumList(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.I, FlagEnum.J, FlagEnum.K, FlagEnum.L, FlagEnum.M,
                FlagEnum.S, FlagEnum.T, FlagEnum.U, FlagEnum.V, FlagEnum.W,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            // Add additional Bitmask Objects
            enumBitMasks.Add(new EnumBitMask(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.N, FlagEnum.O, FlagEnum.P, FlagEnum.Q, FlagEnum.R,
                FlagEnum.S, FlagEnum.T, FlagEnum.U, FlagEnum.V, FlagEnum.W,
                FlagEnum.X, FlagEnum.Y, FlagEnum.Z, FlagEnum.A2, FlagEnum.B2,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            enumBitMasks.Add(new EnumBitMask(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.D, FlagEnum.E, FlagEnum.F, FlagEnum.G, FlagEnum.H,
                FlagEnum.I, FlagEnum.J, FlagEnum.K, FlagEnum.L, FlagEnum.M,
                FlagEnum.N, FlagEnum.O, FlagEnum.P, FlagEnum.Q, FlagEnum.R,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            enumBitMasks.Add(new EnumBitMask(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.D, FlagEnum.E, FlagEnum.F, FlagEnum.G, FlagEnum.H,
                FlagEnum.X, FlagEnum.Y, FlagEnum.Z, FlagEnum.A2, FlagEnum.B2,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            enumBitMasks.Add(new EnumBitMask(FlagEnum.A, FlagEnum.B, FlagEnum.C,
                FlagEnum.I, FlagEnum.J, FlagEnum.K, FlagEnum.L, FlagEnum.M,
                FlagEnum.S, FlagEnum.T, FlagEnum.U, FlagEnum.V, FlagEnum.W,
                FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
            ));

            Profiler.BeginSample("ListTest6");
            ListTest6();
            Profiler.EndSample();

            Profiler.BeginSample("BitMaskTest6");
            BitMaskTest6();
            Profiler.EndSample();

            Profiler.BeginSample("ListTest7");
            ListTest7();
            Profiler.EndSample();

            Profiler.BeginSample("BitMaskTest7");
            BitMaskTest7();
            Profiler.EndSample();

            Profiler.BeginSample("ListTest8");
            ListTest8();
            Profiler.EndSample();

            Profiler.BeginSample("BitMaskTest8");
            BitMaskTest8();
            Profiler.EndSample();

            Profiler.BeginSample("BitMaskTest8Alt");
            BitMaskTest8Alt();
            Profiler.EndSample();
        }
    }

    #region Check an object for an enum which exists within it
    private EnumList ListTest1()
    {
        EnumList eL = new EnumList(FlagEnum.A);
        eL.Contains(FlagEnum.A);
        return eL;
    }

    private EnumBitMask BitMaskTest1()
    {
        EnumBitMask eBM = new EnumBitMask(FlagEnum.A);
        eBM.Contains(FlagEnum.A);
        return eBM;
    }
    #endregion

    #region Check an object for 2 enums of which one exists within it and the other does not
    private EnumList ListTest2()
    {
        EnumList el2 = new EnumList(FlagEnum.B);
        el2.Contains(FlagEnum.A);
        el2.Contains(FlagEnum.B);
        return el2;
    }

    private EnumBitMask BitMaskTest2()
    {
        EnumBitMask eBM2 = new EnumBitMask(FlagEnum.B);
        eBM2.Contains(FlagEnum.A);
        eBM2.Contains(FlagEnum.B);
        return eBM2;
    }
    #endregion

    #region Check an objects for 2 enums that exist within it
    private EnumList ListTest3()
    {
        EnumList el3 = new EnumList(FlagEnum.A, FlagEnum.B);
        el3.Contains(FlagEnum.A);
        el3.Contains(FlagEnum.B);
        return el3;
    }

    private EnumBitMask BitMaskTest3()
    {
        EnumBitMask eBM3 = new EnumBitMask(FlagEnum.A, FlagEnum.B);
        eBM3.Contains(FlagEnum.A);
        eBM3.Contains(FlagEnum.B);
        return eBM3;
    }

    /// <summary>
    /// This tests a different implementation of the constructor to see if performance time improves
    /// </summary>
    private void BitMaskTest3Alt()
    {
        EnumBitMask eBM3Alt = new EnumBitMask(FlagEnum.A | FlagEnum.B);
        eBM3Alt.Contains(FlagEnum.A);
        eBM3Alt.Contains(FlagEnum.B);
    }
    #endregion

    #region Check 3 objects for an enum that does not exist in the list
    private void ListTest4()
    {
        foreach (var el in enumLists)
        {
            el.Contains(FlagEnum.C);
        }
    }

    private void BitMaskTest4()
    {
        foreach (var eBM in enumBitMasks)
        {
            eBM.Contains(FlagEnum.C);
        }
    }
    #endregion

    #region Check an object with all enums for the first, middle and last enum
    private EnumList ListTest5()
    {

        var el = new EnumList(FlagEnum.A, FlagEnum.B, FlagEnum.C,
            FlagEnum.D, FlagEnum.E, FlagEnum.F, FlagEnum.G, FlagEnum.H,
            FlagEnum.I, FlagEnum.J, FlagEnum.K, FlagEnum.L, FlagEnum.M,
            FlagEnum.N, FlagEnum.O, FlagEnum.P, FlagEnum.Q, FlagEnum.R,
            FlagEnum.S, FlagEnum.T, FlagEnum.U, FlagEnum.V, FlagEnum.W,
            FlagEnum.X, FlagEnum.Y, FlagEnum.Z, FlagEnum.A2, FlagEnum.B2,
            FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
        );
        el.Contains(FlagEnum.A);
        el.Contains(FlagEnum.P);
        el.Contains(FlagEnum.F2);

        return el;
    }

    private EnumBitMask BitMaskTest5()
    {
        var eBM = new EnumBitMask(FlagEnum.A, FlagEnum.B, FlagEnum.C,
            FlagEnum.D, FlagEnum.E, FlagEnum.F, FlagEnum.G, FlagEnum.H,
            FlagEnum.I, FlagEnum.J, FlagEnum.K, FlagEnum.L, FlagEnum.M,
            FlagEnum.N, FlagEnum.O, FlagEnum.P, FlagEnum.Q, FlagEnum.R,
            FlagEnum.S, FlagEnum.T, FlagEnum.U, FlagEnum.V, FlagEnum.W,
            FlagEnum.X, FlagEnum.Y, FlagEnum.Z, FlagEnum.A2, FlagEnum.B2,
            FlagEnum.C2, FlagEnum.D2, FlagEnum.E2, FlagEnum.F2
        );
        eBM.Contains(FlagEnum.A);
        eBM.Contains(FlagEnum.P);
        eBM.Contains(FlagEnum.F2);
        return eBM;
    }
    #endregion

    #region Check 8 objects for the middle enum
    private void ListTest6()
    {
        foreach (var el in enumLists)
        {
            el.Contains(FlagEnum.P);
        }
    }

    private void BitMaskTest6()
    {
        foreach (var eBM in enumBitMasks)
        {
            eBM.Contains(FlagEnum.P);
        }
    }
    #endregion

    #region Get All Objects with certain enum
    private void ListTest7()
    {
        List<EnumList> list = new List<EnumList>();
        foreach (var el in enumLists)
        {
            if (el.Contains(FlagEnum.P))
            {
                list.Add(el);
            }
        }
    }

    private void BitMaskTest7()
    {
        List<EnumBitMask> list = new List<EnumBitMask>();
        foreach (var eBM in enumBitMasks)
        {
            if (eBM.Contains(FlagEnum.P))
            {
                list.Add(eBM);
            }
        }
    }
    #endregion

    #region Get All Objects with certain 2 enums
    private List<EnumList> ListTest8()
    {
        List<EnumList> list = new List<EnumList>();
        foreach (var el in enumLists)
        {
            if (el.Contains(FlagEnum.P) && el.Contains(FlagEnum.Q))
            {
                list.Add(el);
            }
        }
        return list;
    }

    private List<EnumBitMask> BitMaskTest8()
    {
        List<EnumBitMask> list = new List<EnumBitMask>();
        foreach (var eBM in enumBitMasks)
        {
            if (eBM.Contains(FlagEnum.P) && eBM.Contains(FlagEnum.Q))
            {
                list.Add(eBM);
            }
        }
        return list;
    }

    private List<EnumBitMask> BitMaskTest8Alt()
    {
        List<EnumBitMask> list = new List<EnumBitMask>();
        foreach (var eBM in enumBitMasks)
        {
            if (eBM.Contains(FlagEnum.P | FlagEnum.Q))
            {
                list.Add(eBM);
            }
        }
        return list;
    }
    #endregion
}

[Flags]
public enum FlagEnum
{
    None = 0,
    A = 1 << 0,
    B = 1 << 1,
    C = 1 << 2,
    D = 1 << 3,
    E = 1 << 4,
    F = 1 << 5,
    G = 1 << 6,
    H = 1 << 7,
    I = 1 << 8,
    J = 1 << 9,
    K = 1 << 10,
    L = 1 << 11,
    M = 1 << 12,
    N = 1 << 13,
    O = 1 << 14,
    P = 1 << 15,
    Q = 1 << 16,
    R = 1 << 17,
    S = 1 << 18,
    T = 1 << 19,
    U = 1 << 20,
    V = 1 << 21,
    W = 1 << 22,
    X = 1 << 23,
    Y = 1 << 24,
    Z = 1 << 25,
    A2 = 1 << 26,
    B2 = 1 << 27,
    C2 = 1 << 28,
    D2 = 1 << 29,
    E2 = 1 << 30,
    F2 = 1 << 31
}

public class BitMask
{
    private FlagEnum mask;

    public BitMask(FlagEnum e)
    {
        mask = e;
    }

    public void RaiseFlag(FlagEnum flag)
    {
        mask = mask | flag;
    }

    public void LowerFlag(FlagEnum flag)
    {
        mask = mask & ~flag;
    }

    public bool CheckFlag(FlagEnum flag)
    {
        return (mask & flag) > 0;
    }
}

public class EnumList
{
    private List<FlagEnum> testList;

    public EnumList(params FlagEnum[] flags)
    {
        testList = new List<FlagEnum>();
        foreach (var flag in flags)
        {
            testList.Add(flag);
        }
    }

    public bool Contains(FlagEnum flag)
    {
        return testList.Contains(flag);
    }
}

public class EnumBitMask
{
    private BitMask bitMask;

    public EnumBitMask(FlagEnum flags)
    {
        bitMask = new BitMask(flags);
    }

    public EnumBitMask(params FlagEnum[] flags)
    {
        bitMask = new BitMask(FlagEnum.None);
        foreach (var flag in flags)
        {
            bitMask.RaiseFlag(flag);
        }
    }

    public bool Contains(FlagEnum flag)
    {
        return bitMask.CheckFlag(flag);
    }
}