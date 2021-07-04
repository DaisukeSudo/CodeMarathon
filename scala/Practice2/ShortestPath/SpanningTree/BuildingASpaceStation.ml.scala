// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1127

import scala.math.sqrt

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  val readInput = () =>
    Stream.continually(io.StdIn.readLine())
      .takeWhile(_ != "0")
      .foldLeft((0, 0, List.empty[Array[Array[Double]]])) {
        case ((n, i, cs3), s) =>
          if (n == i)
            s.toInt.let(n => (n, 0, Array.fill[Array[Double]](n)(Array[Double]()) :: cs3))
          else
            (cs3(0)(i) = s.split(" ").map(_.toDouble)).let(_x => (n, i + 1, cs3))
      }
      ._3
      .reverse

  val distance3 = (v1: Array[Double], v2: Array[Double]) =>
    List.range(0, 3).map(i => v1(i) - v2(i)).map(x => x * x).sum.let(sqrt)

  val makeEdges = (cells: Array[Array[Double]]) =>
    (
      for (
        i <- Stream.range(0, cells.length);
        j <- Stream.range(i + 1, cells.length)
      ) yield (i, j)
    ).foldLeft(List[Tuple3[Int, Int, Double]]()) { case (edges, (i, j)) =>
      (distance3(cells(i), cells(j)))
        .let(_ - cells(i)(3) - cells(j)(3))
        .max(0.0)
        .let((i, j, _) :: edges)
    }

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

  val kruskal = (v: Int, edges: List[Tuple3[Int, Int, Double]]) =>
    Array.range(0, v).let(table =>
      edges
        .sortBy(_._3)
        .foldLeft(0.0) { case (cost, (a, b, c)) =>
          findRoot(table, a).let(rA =>
            updateRoot(table, b, rA).let(rB =>
              if (rA != rB)
                cost + c
              else
                cost
            )
          )
        }
    )

  def main(args: Array[String]) = (
    readInput()
      .map(cells =>
        makeEdges(cells)
          .let(edges =>
            kruskal(cells.length, edges)
          )
      )
      .map("%.3f".format(_))
      .foreach(println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5646653
// https://onlinejudge.u-aizu.ac.jp/recent_judges/1127/judge/5646653/dsudo/Scala
