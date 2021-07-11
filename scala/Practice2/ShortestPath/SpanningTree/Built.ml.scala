// https://atcoder.jp/contests/abc065/tasks/arc076_b

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  val readInput = () =>
    io.StdIn.readLine.toInt.let(n =>
      Array.range(0, n).map(i =>
        io.StdIn.readLine
          .split(" ")
          .map(_.toInt)
          .let(x => (i, x(0), x(1)))
      )
    )

  val makeEdges = (vs: Array[Tuple3[Int, Int, Int]]) =>
    (
      vs.sortWith((a, b) => if (a._2 != b._2) a._2 < b._2 else a._3 < b._3).let(vsX =>
        List.range(1, vs.length).map(i => (vsX(i - 1)._1, vsX(i)._1, vsX(i)._2 - vsX(i - 1)._2))
      ),
      vs.sortWith((a, b) => if (a._3 != b._3) a._3 < b._3 else a._2 < b._2).let(vsY =>
        List.range(1, vs.length).map(i => (vsY(i - 1)._1, vsY(i)._1, vsY(i)._3 - vsY(i - 1)._3))
      )
    )
    .let { case (a, b) => a ++ b }

  val findTree = (table: Array[Int], x: Int) =>
    Array(true).let(next =>
      Stream.continually(0)
        .takeWhile(_x => next(0))
        .scanLeft(x)((current, _x) =>
          table(current).let(parent =>
            (if (parent == current) next(0) = false).let(_x => parent)
          )
        )
        .reverse
        .toList
    )

  val findRoot = (table: Array[Int], x: Int) =>
    findTree(table, x).let(tree =>
      tree.tail
        .foreach(item => table(item) = tree.head)
        .let(_x => tree.head)
    )

  val updateRoot = (table: Array[Int], x: Int, newRoot: Int) =>
    findTree(table, x).let(tree =>
      tree.head.let(oldRoot =>
        tree
          .foreach(item => table(item) = newRoot)
          .let(_x => oldRoot)
      )
    )

  val kruskal = (v: Int, edges: List[Tuple3[Int, Int, Int]]) =>
    Array.range(0, v).let(table =>
      edges
        .sortBy(_._3)
        .foldLeft(0L) { case (cost, (a, b, c)) =>
          findRoot(table, a).let(rA =>
            updateRoot(table, b, rA).let(rB =>
              if (rA != rB)
                cost + c.toLong
              else
                cost
            )
          )
        }
    )

  def main(args: Array[String]) = (
    readInput()
      .let(vs => (vs.length, makeEdges(vs)))
      .let(kruskal.tupled)
      .let(println)
  )
}

// https://atcoder.jp/contests/abc065/submissions/24155699
