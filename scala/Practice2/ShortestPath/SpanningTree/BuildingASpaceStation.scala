// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1127

import scala.math.sqrt

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (
      (v: Int) =>
        Array.range(0, v).let(table =>
          (
            (x: Int) => (
              Array(true).let(next =>
                Stream.continually(0)
                  .takeWhile(_x => next(0))
                  .scanLeft(x)((c, _x) => table(c).let(p => (if (p == c) next(0) = false).let(_x => p)))
                  .reverse
                  .toList
              )
            )
          )
          .let(findTree =>
            (
              /* findRoot */
              (x: Int) =>
                findTree(x).let(tree =>
                  tree.tail
                    .foreach(item => table(item) = tree.head)
                    .let(_x => tree.head)
                ),
              /* updateRoot */
              (x: Int, newRoot: Int) =>
                findTree(x).let(tree =>
                  tree.head.let(oldRoot =>
                    tree
                      .foreach(item => table(item) = newRoot)
                      .let(_x => oldRoot)
                  )
                )
            )
          )
        )
    )
    .let(unionTree =>

      /* main */
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
        .map(cells =>
          unionTree(cells.length).let { case (findRoot, updateRoot) =>
            (
              for (
                i <- Stream.range(0, cells.length);
                j <- Stream.range(i + 1, cells.length)
              ) yield (i, j)
            )
            .foldLeft(List[Tuple3[Int, Int, Double]]()) { case (edges, (i, j)) =>
              List.range(0, 3)
                .map(k => cells(i)(k) - cells(j)(k))
                .map(x => x * x)
                .sum
                .let(sqrt)
                .let(_ - cells(i)(3) - cells(j)(3))
                .max(0.0)
                .let((i, j, _) :: edges)
            }
            .sortBy(_._3)
            .foldLeft(0.0) { case (cost, (a, b, c)) =>
              findRoot(a).let(rA =>
                updateRoot(b, rA).let(rB =>
                  if (rA != rB)
                    cost + c
                  else
                    cost
                )
              )
            }
          }
        )
        .map("%.3f".format(_))
        .foreach(println)
    )
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5646726
// https://onlinejudge.u-aizu.ac.jp/recent_judges/1127/judge/5646726/dsudo/Scala
