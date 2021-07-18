// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=NTL_1_A

import scala.math.{floor, sqrt}

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  val readInput = () => io.StdIn.readLine.toInt

  val upperLimit = (x: Int) => sqrt(x).toInt

  val isPrimeNumber = (x: Int, ps: List[Int]) =>
    upperLimit(x) |> (ul => ps.filter(_ <= ul).forall(x % _ != 0))

  val primeNumbers = () =>
    Array(List(2)) |> ((ps) =>
      Stream.empty
        .append(Stream(2))
        .append(
          for (x <- Stream.from(3) if isPrimeNumber(x, ps(0)))
          yield (ps(0) = x::ps(0)) |> (_x => x)
        )
    )

  val solve = (n: Int) =>
    Array(n) |> (r =>
      primeNumbers()
        .takeWhile(_x => r(0) > 1)
        .flatMap(p =>
          Stream.continually(p)
            .takeWhile(r(0) % _ == 0)
            .map(p => (r(0) = r(0) / p) |> (_x => p))
        )
    )

  val format = (n: Int, answers: Stream[Int]) =>
    s"${n}: ${answers.toArray.mkString(" ")}"

  def main(args: Array[String]) = (
    readInput()
      |> (n => solve(n) |> (format(n, _)))
      |> println
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5704579
