using System;
using TestLibrary;

class StringJoin
{
    static int Main()
    {
        StringJoin test = new StringJoin();

        TestFramework.BeginTestCase("String.Join");

        if (test.RunTests())
        {
            TestFramework.EndTestCase();
            TestFramework.LogInformation("PASS");
            return 100;
        }
        else
        {
            TestFramework.EndTestCase();
            TestFramework.LogInformation("FAIL");
            return 0;
        }

    }

    public bool RunTests()
    {
        bool ret = true;

        // Positive

        ret &= Test1();
        ret &= Test2();
        ret &= Test3();
        ret &= Test4();
        ret &= Test5();
        ret &= Test6();
        ret &= Test7();
        ret &= Test8();
        ret &= Test9();

        // Negative

        ret &= Test10();
        ret &= Test11();
        ret &= Test12();
        ret &= Test13();
        //ret &= Test14();
        ret &= Test15();

        return ret;
    }

    // Positive Tests

    public bool Test1() { return PositiveTest("-.", new string[] { "a", "b", "c" }, 0, 3, "a-.b-.c", "00B"); }
    public bool Test2() { return PositiveTest("", new string[] { "a", "b", "c" }, 0, 3, "abc", "00C"); }
    public bool Test3() { return PositiveTest("-.", new string[] { "a", "b", "c" }, 0, 0, "", "00D"); }
    public bool Test4() { return PositiveTest("-.", new string[] { "", "", "" }, 0, 3, "-.-.", "00E"); }
    public bool Test5() { return PositiveTest(null, new string[] { "a", "b", "c" }, 0, 3, "abc", "00F"); }
    public bool Test6() { return PositiveTest("y", new string[] { "a", "b", "c" }, 1, 1, "b", "00G"); }
    public bool Test7() { return PositiveTest("y", new string[] { "a", "b", "c", "d" }, 1, 2, "byc", "00H"); }
    public bool Test8() { return PositiveTest(null, new string[] { "", "", "" }, 1, 2, "", "00I"); }

    public bool Test9() { return PositiveTest2(" ", new string[] { "a", "b", "c" }, "a b c", "00J"); }

    // Negative Tests

    public bool Test10() { return NegativeTest("a", null, 0, 0, typeof(ArgumentNullException), "00K"); }
    public bool Test11() { return NegativeTest("a", new string[] { "a", "b", "c" }, -1, 3, typeof(ArgumentOutOfRangeException), "00L"); }
    public bool Test12() { return NegativeTest("a", new string[] { "a", "b", "c" }, 0, -1, typeof(ArgumentOutOfRangeException), "00M"); }
    public bool Test13() { return NegativeTest("a", new string[] { "a", "b", "c" }, 2, 2, typeof(ArgumentOutOfRangeException), "00N"); }

    // This test is disabled because the expected OOM Exception is not guaranteed to occur on ARM
    /*
    public bool Test14() {
        string longStr = string.Empty;
        try
        {
            longStr = new string('a', 507374182);
        }
        catch (OutOfMemoryException) { return true; }   // If the system is too low on resources to create this string, there's nothing we can do
        return (IntPtr.Size == 4)?NegativeTest(longStr, new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m" }, 0, 3, typeof(OutOfMemoryException), "00O"):true; 
    }
    */

    public bool Test15() { return NegativeTest2("a", null, typeof(ArgumentNullException), "00P"); }

    public bool PositiveTest(string seperator, string[] input, int index, int count, string expected, string id)
    {
        bool result = true;

        TestFramework.BeginScenario(id + ": Testing String.Join");
        try
        {
            string i = string.Join(seperator, input, index, count);
            if (i != expected)
            {
                result = false;
                TestFramework.LogError("001", "Error in " + id + ", unexpected join result. Actual: " + i + ", Expected: " + expected);
            }
        }
        catch (Exception exc)
        {
            result = false;
            TestFramework.LogError("003", "Unexpected exception in " + id + ", excpetion: " + exc.ToString());
        }
        return result;
    }

    public bool PositiveTest2(string seperator, string[] input, string expected, string id)
    {
        bool result = true;

        TestFramework.BeginScenario(id + ": Testing String.Join");
        try
        {
            string i = string.Join(seperator, input);
            if (i != expected)
            {
                result = false;
                TestFramework.LogError("004", "Error in " + id + ", unexpected join result. Actual: " + i + ", Expected: " + expected);
            }
        }
        catch (Exception exc)
        {
            result = false;
            TestFramework.LogError("005", "Unexpected exception in " + id + ", excpetion: " + exc.ToString());
        }
        return result;
    }

    public bool NegativeTest(string seperator, string[] input, int index, int count, Type expected, string id)
    {
        bool result = true;
        TestFramework.BeginScenario(id + ": Testing String.Join");
        try
        {
            string i = string.Join(seperator, input, index, count);
            result = false;
            TestFramework.LogError("006", "Error in " + id + ", expected exception did not occur.");
        }
        catch (Exception exc)
        {
            if (!exc.GetType().Equals(expected))
            {
                result = false;
                TestFramework.LogError("007", "Unexpected exception in " + id + ", excpetion: " + exc.ToString());
            }
        }
        return result;
    }

    public bool NegativeTest2(string seperator, string[] input, Type expected, string id)
    {
        bool result = true;
        TestFramework.BeginScenario(id + ": Testing String.Join");
        try
        {
            string i = string.Join(seperator, input);
            result = false;
            TestFramework.LogError("008", "Error in " + id + ", expected exception did not occur.");
        }
        catch (Exception exc)
        {
            if (!exc.GetType().Equals(expected))
            {
                result = false;
                TestFramework.LogError("009", "Unexpected exception in " + id + ", excpetion: " + exc.ToString());
            }
        }
        return result;
    }
}