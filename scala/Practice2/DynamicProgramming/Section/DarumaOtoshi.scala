// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1611

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    Stream.continually(io.StdIn.readLine())
      .takeWhile(_ != "0")
      .foldLeft((0, List[Int](), List[Array[Int]]())) {
        case ((i, ns, wss), s) => (
          if (i % 2 == 0) (
            s.toInt
            |> (n => (i + 1, n :: ns, wss))
          )
          else (
            s.split(" ").map(_.toInt)
            |> (ws => (i + 1, ns, ws :: wss))
          )
        )
      }
      |> { case (_x, ns, wss) => (ns zip wss) }
      |> (_.reverse)
      |> (_.map { case (n, ws) => (
        (
          { case (a, b) => ws(a) - ws(b) |> Math.abs |> (_ <= 1) }: (Int, Int) => Boolean,
          (1 to n).map(_x => Array.fill[Boolean](n)(true)).toArray,
          (1 to n).map(_x => Array.fill[Int](n)(0)).toArray
        )
        |> { case (isR, memR, memC) => (
          (
            { case (s, e) => (
              (
                (e - s) match {
                  case x if x % 2 == 0 => false
                  case 1 => isR(s, e)
                  case _ =>
                    (isR(s, e) && memR(s + 1)(e - 1)) ||
                      (memR(s)(s + 1) && memR(s + 2)(e))

                }
              )
              |> (x => ((memR(s)(e) = x) |> (_x => x)))
            )} : (Int, Int) => Boolean
          )
          |> (fnR =>
            (1 until n)
              .map((i) =>
                (for { j <- (0 until (n - i)) } yield (j, j + i))
                  .map { case (s, e) => (
                    (
                      if (fnR(s, e))
                        e - s + 1
                      else
                        (for { k <- (s until e) } yield ((s, k), (k + 1, e)))
                          .map { case ((s1, e1), (s2, e2)) =>
                            memC(s1)(e1) + memC(s2)(e2)
                          }
                          .max
                    )
                    |> (x => memC(s)(e) = x)
                  )}
              )
          )
          |> (_x => memC(0)(n - 1))
        )}
      )})
      |> (_ foreach println)
  )
}

// http://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5225206
