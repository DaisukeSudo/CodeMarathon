// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=NTL_1_B

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  val readInput = () =>
    io.StdIn.readLine.split(" ").map(_.toLong) |> (a => (a(0), a(1)))

  val power = (p: Long) => (
    (Array((_m: Long, _n: Long) => 0L))
    |> (fnc => (
      (m: Long, n: Long) =>
        n match {
          case _x if n == 0L => 1L
          case _x if n % 2L == 0L => fnc(0)(m, n / 2L) |> (x => x * x % p)
          case _ => m * fnc(0)(m, n - 1L) % p
        }
      )
      |> (fn => (fnc(0) = fn) |> (_x => fn))
    )
  )

  def main(args: Array[String]) = (
    readInput()
    |> power(1000000007L).tupled
    |> println
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5740215
// https://onlinejudge.u-aizu.ac.jp/status/users/dsudo/submissions/1/NTL_1_B/judge/5740215/Scala
