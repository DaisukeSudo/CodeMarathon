// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1611

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    Stream.continually(io.StdIn.readLine())
      .takeWhile(_ != "0")
      .foldLeft((0, List[Int](), List[Array[Int]]())) {
        case ((i, ns, wss), s) =>
          if (i % 2 == 0)
            (s.toInt                   |> (n =>  (i + 1, n :: ns, wss)))
          else
            (s.split(" ").map(_.toInt) |> (ws => (i + 1, ns, ws :: wss)))
      }
      |> { case (_x, ns, wss) => (ns zip wss) }
      |> (_.reverse)
      |> (_.map { case (n, ws) => (
        (Array.range(0, n).map(_x => Array.fill[Int](n)(0)))
        |> (memo => (
          (for (i <- 1 to n; j <- 0 until n - i) yield (i, j, j + i))
            .foreach { case (i, s, e) =>
              if (
                (ws(s) - ws(e) |> Math.abs |> (_ <= 1))
                  && (memo(s + 1)(e - 1) == i - 1)
              )
                memo(s)(e) = i + 1
              else
                (for (k <- s until e) yield ((s, k), (k + 1, e)))
                  .foreach { case ((s1, e1), (s2, e2)) => (
                    (memo(s1)(e1) + memo(s2)(e2)) 
                    |> (memo(s)(e).max _)
                    |> (memo(s)(e) = _)
                  )}
            }
          |> (_x => memo(0)(n - 1))
          |> println
        ))
      )})
  )
}

// http://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5229992
