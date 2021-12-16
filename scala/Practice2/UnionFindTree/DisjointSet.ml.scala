// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DSL_1_A&lang=ja

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  val findTree = (table: Array[Int], x: Int) =>
    Array(true) |> (next =>
      Stream.continually(0)
        .takeWhile(_x => next(0))
        .scanLeft(x)((c, _x) => table(c) |> (p => if (p == c) (next(0) = false) |> (_ => c) else p))
        .reverse
        .toList
    )

  val findRoot = (table: Array[Int], x: Int) =>
    findTree(table, x) |> (tree =>
      tree.tail.foreach(i => table(i) = tree.head) |> (_x => tree.head)
    )

  val updateRoot = (table: Array[Int], x: Int, newRoot: Int) =>
    findTree(table, x) |> (tree =>
      tree.foreach(i => table(i) = newRoot)
    )

  val unite = (table: Array[Int], a: Int, b: Int) =>
    findRoot(table, a) |> (ra => updateRoot(table, b, ra))

  val same = (table: Array[Int], a: Int, b: Int) =>
    findRoot(table, a) |> (ra => findRoot(table, b) |> (rb => ra == rb))

  def main(args: Array[String]) = (
    io.StdIn.readLine.split(' ').map(_.toInt) |> (x => (x(0), x(1))) |> { case (n, q) =>
      Array.range(0, n) |> (table =>
        (1 to q).foreach(_x => io.StdIn.readLine.split(' ').map(_.toInt) |> (x => (x(0), x(1), x(2))) |> { case (com, x, y) =>
          com match {
            case 0 => unite(table, x, y)
            case _ => same(table, x, y) |> (b => if (b) 1 else 0) |> println
          }
        })
      )
    }
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=6128998
// https://onlinejudge.u-aizu.ac.jp/recent_judges/DSL_1_A/judge/6128998/dsudo/Scala
