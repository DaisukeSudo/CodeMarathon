using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  string FnInt2(int a, int b) =>
    a * b % 2 == 0 ? "Even" : "Odd";

  string FnIntArr2(List<int> arr) =>
    FnInt2(arr[0], arr[1]);

  void NumberSplitSpace2() =>
    Console.WriteLine(
      FnIntArr2(
        Console.ReadLine()
          .Split(' ')
          .Select(x => int.Parse(x))
          .ToList()
      )
    );

  static void Main(string[] args) =>
    new Program().NumberSplitSpace2();
}

// ABC086A - Product

// C# (.NET Core 3.1.201)
// 73 ms / 28156 KB
// https://atcoder.jp/contests/abs/submissions/12026415

// C# (Mono-mcs 6.8.0.105)
// 29 ms / 18936 KB
// https://atcoder.jp/contests/abs/submissions/12026537

// C# (Mono-csc 3.5.0)
// 31 ms / 19128 KB
// https://atcoder.jp/contests/abs/submissions/12026561
