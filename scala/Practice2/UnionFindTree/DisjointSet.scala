// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DSL_1_A&lang=ja

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.split(' ').map(_.toInt) |> (x => (x(0), x(1))) |> { case (n, q) =>
      (
        Array.range(0, n),
        (table: Array[Int], x: Int) =>
          Array(true) |> (next =>
            Stream.continually(0)
              .takeWhile(_x => next(0))
              .scanLeft(x)((c, _x) => table(c) |> (p => if (p == c) (next(0) = false) |> (_ => c) else p))
              .reverse
              .toList
          )
      ) |> { case (table, findTree) =>
        (
          (x: Int) => findTree(table, x) |> (tree => tree.tail.foreach(i => table(i) = tree.head) |> (_x => tree.head)),
          (x: Int, newRoot: Int) => findTree(table, x) |> (tree => tree.foreach(i => table(i) = newRoot))
        )
      } |> { case (findRoot, updateRoot) =>
        (
          (a: Int, b: Int) => findRoot(a) |> (ra => updateRoot(b, ra)),
          (a: Int, b: Int) => findRoot(a) |> (ra => findRoot(b) |> (rb => ra == rb))
        )
      } |> { case (unite, same) =>
        (1 to q).foreach(_x => io.StdIn.readLine.split(' ').map(_.toInt) |> (x => (x(0), x(1), x(2))) |> { case (com, x, y) =>
          com match {
            case 0 => unite(x, y)
            case _ => same(x, y) |> (b => if (b) 1 else 0) |> println
          }
        })
      }
    }
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=6129054
// https://onlinejudge.u-aizu.ac.jp/recent_judges/DSL_1_A/judge/6129054/dsudo/Scala
