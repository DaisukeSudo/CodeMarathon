// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=NTL_1_A

import scala.math.sqrt

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  val readInput = () => io.StdIn.readLine.toInt

  val solve = (n: Int) =>
    (Array(List.empty[Int]), Array(n)) |> { case (acc, r) => (
      Stream.empty
        .append(Stream(2))
        .append(for (i <- 3 to sqrt(n).toInt by 2) yield i)
        .foreach(i =>
          Stream.continually(i)
            .takeWhile(r(0) % _ == 0)
            .foreach(p =>
              ((acc(0) = p::acc(0)), (r(0) = r(0) / p))
            )
        )
        |> (_x => if (r(0) > 1) acc(0) = r(0)::acc(0))
        |> (_x => acc(0).reverse)
    )}

  val format = (n: Int, answers: List[Int]) =>
    s"${n}: ${answers.mkString(" ")}"

  def main(args: Array[String]) = (
    readInput()
      |> (n => solve(n) |> (format(n, _)))
      |> println
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5705064
// https://onlinejudge.u-aizu.ac.jp/status/users/dsudo/submissions/1/NTL_1_A/judge/5705064/Scala
