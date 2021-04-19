// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_A

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    ((_x: Int) => io.StdIn.readLine.split(" ").map(_.toInt).let(x => (x(0), x(1), x(2))))
      .let(readAsNum3 =>
        (readAsNum3(0)).let { case (v, e, r) =>
          (
            Array.range(0, e).map(readAsNum3)
          )
          .let(stds =>
            (
              (Array.fill(v)(List[(Int, Int)]()))
                .let(x => stds.foreach { case (s, t, d) => x(s) = (t, d) :: x(s) }.let(_x => x)),
              Array.fill(v)(Int.MaxValue).let(x => (x(r) = 0).let(_x => x)),
              Array.fill(v)(false).let(x => (x(r) = true).let(_x => x))
            )
          )
          .let { case (stds, memo, fixed) =>
            (
              new scala.collection.mutable.PriorityQueue[Int]()(Ordering.by(memo(_)).reverse)
            )
            .let(q =>
              List.range(1, v).foldLeft(Option(r))((os, _x) =>
                os match {
                  case Some(s) =>
                    stds(s)
                      .filter { case (t, d) => memo(t) > memo(s) + d }
                      .foreach { case (t, d) => (memo(t) = memo(s) + d).let(_x => q += t) }
                      .let(_x => fixed(s) = true)
                      .let(_x => LazyList.continually(q.dequeue))
                      .takeWhile(_x => !q.isEmpty)
                      .filter(!fixed(_))
                      .headOption
                  case _ => None
                }
              )
            )
            .let(_x => memo)
          }
        }
      )
      .map(x => if (x == Int.MaxValue) "INF" else x.toString)
      .mkString("\n")
      .let(println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5390699
