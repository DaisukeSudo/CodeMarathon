// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=NTL_1_A

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.toInt
    |> (n =>
      (Array(List.empty[Int]), Array(n)) |> { case (acc, r) => (
        Stream.empty
          .append(Stream(2))
          .append(for (i <- 3 to scala.math.sqrt(n).toInt by 2) yield i)
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
      |> (ans => s"${n}: ${ans.mkString(" ")}")
    )
    |> println
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5705093
// https://onlinejudge.u-aizu.ac.jp/status/users/dsudo/submissions/1/NTL_1_A/judge/5705093/Scala
