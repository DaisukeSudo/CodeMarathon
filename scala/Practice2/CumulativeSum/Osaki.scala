// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=2013

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    Stream.continually(io.StdIn.readLine())
      .takeWhile(_ != "0")
      .foldLeft((0, 0, List.empty[Array[String]])) {
        case ((n, i, lines), s) =>
          if (n == i) (
            s.toInt |> (n => (n, 0, Array.fill[String](n)(null) :: lines))
          )
          else (
            (lines(0)(i) = s) |> (_x => (n, i + 1, lines))
          )
      }
      ._3
      .reverse
      .map(times =>
        Array.fill[Long](86401)(0) |> (acc => (
          times.foreach(t => (
            t.split(" ")
              .map(s => s.split(":").map(_.toInt) |> (x =>
                x(0) * 3600 + x(1) * 60 + x(2)
              ))
              |> (x => (x(0), x(1)))
              |> { case (a, b) =>
                (
                  (acc(a) = acc(a) + 1),
                  (acc(b) = acc(b) - 1)
                )
              }
          ))
          |> (_x => (1 until 86401).foreach(i => acc(i) = acc(i) + acc(i - 1)))
          |> (_x => acc.max)
        ))
      )
      |> (_ foreach println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=6066731
// https://onlinejudge.u-aizu.ac.jp/recent_judges/2013/judge/6066731/dsudo/Scala
