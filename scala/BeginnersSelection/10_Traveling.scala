// https://atcoder.jp/contests/abs/tasks/arc089_a

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.toInt
    |> ((n) =>
      (1 to n).map(_ => (
        io.StdIn.readLine
          .split(" ")
          .map(_.toInt)
          |> ((x) => (x(0), x(1), x(2)))
      ))
      .foldLeft(Option(0, 0, 0)) { case (acc, (t2, x2, y2)) =>
        acc match {
          case Some((t1, x1, y1))
            if (((t2 & 1) == (x2 + y2 & 1))
              && ((t2 - t1) >= (math.abs(x2 - x1) + math.abs(y2 - y1)))
            ) => Some((t2, x2, y2))
          case _ => None
        }
      }
    )
    |> ((x) =>
      x match {
        case Some(x) => "Yes"
        case _       => "No"
      }
    )
    |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/21020571
