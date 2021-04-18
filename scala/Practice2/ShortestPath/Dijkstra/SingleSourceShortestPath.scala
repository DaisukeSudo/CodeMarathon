// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_A

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) =
    ((_x: Int) => io.StdIn.readLine.split(" ").map(_.toInt).let(x => (x(0), x(1), x(2))))
      .let(readAsNum3 =>
        (readAsNum3(0)).let { case (v, e, r) =>
          (
            Array.range(0, e).map(readAsNum3),
            Array.fill(v)(List[(Int, Int)]()),
            Array.fill(v)(Int.MaxValue),
            Array(Set[Int]())
          )
          .let { case (stds, dict, memo, touched) =>
            (
              (stds.foreach { case (s, t, d) => dict(s) = (t, d) :: dict(s) }),
              (memo(r) = 0)
            )
            .let(_x => List.range(0, v))
            .foldLeft(Option(r))((os, _x) =>
              os match {
                case Some(s) =>
                  dict(s)
                    .filter { case (t, d) => memo(t) > memo(s) + d }
                    .map { case (t, d) => (memo(t) = memo(s) + d).let(_x => t) }
                    .toSet
                    .let(_ ++ touched(0))
                    .let(ts =>
                      if (ts.isEmpty)
                        None
                      else
                        ts.minBy(x => memo(x)).let(next =>
                          (touched(0) = (ts - next)).let(_x => Some(next))
                        )
                    )
                case _ => None
              }
            )
            .let(_x =>
              memo
                .map(x => if (x == Int.MaxValue) "INF" else x.toString)
                .mkString("\n")
            )
          }
        }
      )
      .let(println)
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5390699
