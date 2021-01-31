// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_B

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.toInt
    |> ((n) =>
      (
        (1 to n).map((_x) => (io.StdIn.readLine.split(" ").map(_.toInt) |> ((x) => (x(0), x(1))))).toArray.unzip,
        (1 to n).map((_x) => Array.fill[Int](n)(0))
      )
      |> { case ((mss, mes), memo) => (
        (1 until n)
          .map((i) =>
            (for { j <- (0 until (n - i)) } yield (j, j + i))
              .map { case (s, e) => (
                (for { k <- (s until e) } yield ((s, k), (k + 1, e)))
                  .map { case ((s1, e1), (s2, e2)) =>
                    mss(s1) * mes(e1) * mes(e2) + memo(s1)(e1) + memo(s2)(e2)
                  }
                  .min
                  |> ((v) => memo(s)(e) = v)
              )}
          )
          |> ((_x) => memo(0).last)
      )}
    )
    |> println
  )
}

// http://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5183199
