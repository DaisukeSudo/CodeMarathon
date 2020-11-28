// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_C

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  int Solve(String xs, String ys) {
    var xsl = xs.ToList();
    var ysl = ys.ToList();
    var memo = new int[ys.Length + 1];
    xsl.ForEach(
      x => {
        var i = 0;
        var used = false;
        ysl.ForEach(
          y => {
            if (memo[i] < memo[i + 1]) {
              used = false;
            } else if (!used && x == y) {
              used = true;
              memo[i + 1] = memo[i] + 1;
            } else if (memo[i] == memo[i + 1]) {
              used = false;
            } else {
              memo[i + 1] = memo[i];
            }
            ++i;
          }
        );
      }
    );
    return memo[ys.Length];
  }

  void Run() =>
    Console.WriteLine(String.Join("\n",
      Enumerable.Range(0, int.Parse(Console.ReadLine()))
        .Select(_ => Solve(Console.ReadLine(), Console.ReadLine()))
    ));

  static void Main(string[] args) => new Program().Run();
}

// http://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5020300#1
