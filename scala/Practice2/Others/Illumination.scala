// https://atcoder.jp/contests/joi2013ho/tasks/joi2013ho1

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    Array(List.empty[Tuple2[String, Int]]) |> (line => (
      io.StdIn.readLine |> (_ => io.StdIn.readLine.split(" ").foreach(x =>
        line(0) match {
          case (y, size)::ts if x != y => line(0) = (x, size + 1)::ts
          case _ => line(0) = (x, 1)::line(0)
        }
      ))
      |> (_ => line(0)
        .foldLeft((0, 0, 0)) { case ((x1, x2, acc), (_, x0)) =>
          (x0, x1, (x0 + x1 + x2).max(acc))
        }
      )
      |> { case (_, _, acc) => acc }
    ))
    |> println
  )
}

// https://atcoder.jp/contests/joi2013ho/submissions/28716573
