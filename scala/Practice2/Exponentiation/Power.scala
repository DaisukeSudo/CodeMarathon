// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=NTL_1_B

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.split(" ").map(_.toLong)
    |> (a => (a(0), a(1)))
    |> (
      (Array((_m: Long, _n: Long) => 0L))
      |> (fnc => (
        (m: Long, n: Long) =>
          n match {
            case _x if n == 0L => 1L
            case _x if n % 2L == 0L => fnc(0)(m, n / 2L) |> (x => x * x % 1000000007L)
            case _ => m * fnc(0)(m, n - 1L) % 1000000007L
          }
        )
        |> (fn => (fnc(0) = fn) |> (_x => fn))
      )
    ).tupled
    |> println
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5740281
// https://onlinejudge.u-aizu.ac.jp/status/users/dsudo/submissions/1/NTL_1_B/judge/5740281/Scala
