// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_2_A

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  implicit class ArrayString[T](x: Array[T]) {
    def toArrayString: String = x.mkString("[", ",", "]")
  }

  val readInput = () =>
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      .let(x => (x(0), x(1)))
      .let { case (v, e) =>
        (
          v,
          Stream.range(0, e).map(_x =>
            io.StdIn.readLine
              .split(" ")
              .map(_.toInt)
              .let(x => (x(0), x(1), x(2)))
          )
        )
      }

  var findRoot = (table: Array[Int], x: Int) =>
    Array(true).let(isContinuing =>
      Stream.continually(0)
        .takeWhile(_x => isContinuing(0))
        .foldLeft(x)((current, _x) =>
          table(current).let(parent =>
            if (parent != current)
              parent
            else
              (isContinuing(0) = false).let(_x => current)
          )
        )
        .let(r => (table(x) = r).let(_x => r))      // optimisation
    )

  var kruskal = (v: Int, edges: Stream[Tuple3[Int, Int, Int]]) =>
    Array.range(0, v).toArray.let(table =>          // vertex -> parent
      edges
        .sortBy(x => x._3)
        .foldLeft(0) { case (cost, (s, t, w)) =>
          (findRoot(table, s), findRoot(table, t)).let { case (rS, rT) =>
            if (rS != rT)                           // is bridge
              (table(rT) = rS).let(_x => cost + w)  // concatenate trees
            else
              cost
          }
        }
    )

  def main(args: Array[String]) = (
    readInput()
      .let(kruskal.tupled)
      .let(println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5593356
// https://onlinejudge.u-aizu.ac.jp/recent_judges/GRL_2_A/judge/5593356/dsudo/Scala
