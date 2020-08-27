// https://atcoder.jp/contests/joi2007ho/tasks/joi2007ho_c

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  T MaxOrDefault<T>(IEnumerable<T> list, T state) =>
    list.Count() > 0 ? list.Max() : state;

  bool[,] toArray2(List<Tuple<int, int>> list) =>
    list.Aggregate(
      new bool[
        list.Select(t => t.Item1).Max() + 1,
        list.Select(t => t.Item2).Max() + 1
      ],
      (arr2, t) => {
        arr2[t.Item1, t.Item2] = true;
        return arr2;
      }
    );

  bool hasValueArray2(bool[,] arr2, int i, int j) =>
    i < 0 || i >= arr2.GetLength(0) ||
    j < 0 || j >= arr2.GetLength(1)
    ? false : arr2[i, j];

  bool canFormSquare(bool[,] arr2, int x1, int y1, int x2, int y2) =>
    (
      hasValueArray2(arr2, (x1 + y1 - y2), (y1 + x2 - x1)) &&
      hasValueArray2(arr2, (x2 + y1 - y2), (y2 + x2 - x1))
    ) || (
      hasValueArray2(arr2, (x1 - y1 + y2), (y1 - x2 + x1)) &&
      hasValueArray2(arr2, (x2 - y1 + y2), (y2 - x2 + x1))
    );

  bool canFormSquare(bool[,] arr2, Tuple<int, int> p1, Tuple<int, int> p2) =>
    canFormSquare(arr2, p1.Item1,　p1.Item2, p2.Item1, p2.Item2);

  int squareArea(int a, int b) =>
     a * a + b * b;

  int squareArea(int x1, int y1, int x2, int y2) =>
    squareArea(Math.Abs(x1 - x2), Math.Abs(y1 - y2));

  int squareArea(Tuple<int, int> p1, Tuple<int, int> p2) =>
    squareArea(p1.Item1,　p1.Item2, p2.Item1, p2.Item2);

  double squareLength(double area) => 
    Math.Ceiling(Math.Sqrt(area) / 1.4);

  int squareLength(int area) => 
    (int)squareLength((double)area);

  Tuple<int, int> SearchSub(Tuple<int, int> acc, int newArea) =>
    acc.Item1 < newArea
      ? new Tuple<int, int>(newArea, squareLength(newArea))
      : acc;

  int Search(bool[,] arr2, List<Tuple<int, int>> list) =>
    list.Aggregate(
        new Tuple<int, int>(0, 1),
        (acc, p1) => SearchSub(acc,
          MaxOrDefault(
            list
              .Where(p2 => p2.Item1 - p1.Item1 >= acc.Item2 && canFormSquare(arr2, p1, p2))
              .Select(p2 => squareArea(p1, p2))
              .ToList()
            ,
            0)
        )
      ).Item1;

  int Search(List<Tuple<int, int>> list) =>
    Search(toArray2(list), list);

  Tuple<int, int> convertToIntTuple2(List<int> arr) =>
    new Tuple<int, int>(arr[0], arr[1]);

  void Run() =>
    Console.WriteLine(
      Search(
        Enumerable.Range(1, int.Parse(Console.ReadLine()))
          .Select(_ =>
            convertToIntTuple2(
              Console.ReadLine()
                .Split(' ')
                .Select(x => int.Parse(x))
                .ToList()
            )
          )
          .ToList()
      )
    );

  static void Main(string[] args) => new Program().Run();
}

// https://atcoder.jp/contests/joi2007ho/submissions/12079117
