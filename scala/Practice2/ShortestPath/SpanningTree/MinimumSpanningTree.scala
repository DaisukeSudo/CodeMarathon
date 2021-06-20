// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_2_A

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.split(" ").map(_.toInt).let(x => (x(0), x(1)))
      .let { case (v, e) =>
        (
          Array.range(0, v).toArray,
          Stream.range(0, e).map(_x => io.StdIn.readLine.split(" ").map(_.toInt).let(x => (x(0), x(1), x(2))))
        )
      }
      .let { case (table, edges) =>
        (
          (x: Int) => Array(true).let(isContinuing =>
            Stream.continually(0)
              .takeWhile(_x => isContinuing(0))
              .foldLeft(x)((current, _x) =>
                table(current).let(parent =>
                  if (parent != current) parent else (isContinuing(0) = false).let(_x => current)
                )
              )
              .let(r => (table(x) = r).let(_x => r))
          )
        ).let(findRoot =>
          edges
            .sortBy(x => x._3)
            .foldLeft(0) { case (cost, (s, t, w)) =>
              (findRoot(s), findRoot(t)).let { case (rS, rT) =>
                if (rS != rT) (table(rT) = rS).let(_x => cost + w) else cost
              }
            }
        )
      }
      .let(println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5593471
// https://onlinejudge.u-aizu.ac.jp/recent_judges/GRL_2_A/judge/5593471/dsudo/Scala
